input = list()

with open('../input/05.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())
        
def part1():
    seeds = list()

    locations = list()

    for line in input:
        if line.startswith('seeds:'):
            for num in line.split(':')[1].strip().split(' '):
                seeds.append(int(num))
        break
    
    for seed in seeds:
        location = seed
        found = False
        for line in input:
            if ':' in line or line == '':
                found = False
                continue
            if found:
                continue

            nums = line.split(' ')
            destination = int(nums[0])
            source = int(nums[1])
            length = int(nums[2])

            if location >= source and location <= source + length:
                location = destination + location - source
                found = True
                
        locations.append(location)
    
    return min(locations)


def part2():
    seed_ranges = list()
    maps = list()
    map_section = list()
    for line in input:
        if line.startswith('seeds:'):
            s_range = list()
            for num in line.split(':')[1].strip().split(' '):
                if len(s_range) == 0:                    
                    s_range.append(int(num))
                else:
                    s_range.append(s_range[0] + int(num))
                    seed_ranges.append(s_range)
                    s_range = list()
            continue

        if ':' in line or line == '':
            if len(map_section) > 0:
                maps.append(map_section)
                map_section = list()
            continue

        nums = line.split(' ')
        map_section.append([         
            int(nums[0]),
            int(nums[1]),
            int(nums[1]) + int(nums[2]) - 1,
        ])
    if map_section not in maps:
        maps.append(map_section)
    
    for current_map in maps:
        new_seed_ranges = list()
        for index, seed_range in enumerate(seed_ranges):
            for map in current_map:
                convert = map[0] - map[1]
                #not within range at all
                if (seed_range[0] < map[1] and seed_range[1] < map[1]) or (seed_range[0] > map[2] and seed_range[1] > map[2]):
                    continue
                
                #seed range completely within map range
                if seed_range[0] >= map[1] and seed_range[1] <= map[2]:
                    seed_ranges[index] = [
                        seed_range[0] + convert,
                        seed_range[1] + convert,
                        ]
                    break
                #first seed number outside of map range
                elif seed_range[0] < map[1]:
                    #second seed number within map range
                    if seed_range[1] <= map[2]:
                        #continue with the part of seed range which is outside
                        seed_ranges[index] = [
                            seed_range[0],
                            map[1] - 1
                        ]
                        #add part within map range as new seed for next map section
                        new_seed_ranges.append([
                            map[1] + convert,
                            seed_range[1] + convert,
                        ])
                    #second seed number outside of map range (seed range over extends both ways)
                    else:
                        #add whole range
                        new_seed_ranges.append([
                        map[1] + convert,
                        map[2] + convert,
                        ])
                        #redo range part too low
                        seed_ranges[index] = [
                            seed_range[0],
                            map[1] - 1
                        ]
                        #redo range part too high
                        seed_ranges.append([
                            map[2] + 1,
                            seed_range[1],
                        ])
                #first seed part is within range
                else:
                    seed_ranges[index] = [
                        map[2],
                        seed_range[1],
                    ]
                    new_seed_ranges.append([
                        seed_range[0] + convert,
                        map[0] + map[2] - map[1],
                    ])
                seed_range = seed_ranges[index]
        seed_ranges += new_seed_ranges

    min = 999999999999999999999
    for seed in seed_ranges:
        if seed[0]  < min:
            min = seed[0]  
    return min


print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")