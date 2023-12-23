def initialise_tower():
    tower = list()

    with open('../input/22.txt', 'r') as f:
        for line in f:
            tower.append([int(xyz) for brick in line.rstrip().split('~') for xyz in brick.split(',')])
    
    
    tower = sorted(tower, key= lambda brick: brick[5])

    return tower

def drop(tower):
    tallest = dict()
    new_tower = list()
    fallen = 0
    for brick in tower:
        dropped_brick = drop_brick(brick, tallest)
        if brick != dropped_brick:
            fallen += 1
            
        for x in range(dropped_brick[0], dropped_brick[3] + 1):
            for y in range(dropped_brick[1], dropped_brick[4] + 1):
                tallest[(x,y)] = dropped_brick[5]
        
        new_tower.append(dropped_brick)
        
    return new_tower, fallen

def drop_brick(brick, tallest):
    highest = max([tallest.get((x,y), 0) for x in range(brick[0], brick[3] + 1) for y in range(brick[1], brick[4] + 1)])
    distance = brick[2] - highest - 1
    return [brick[0],brick[1],brick[2]-distance,brick[3],brick[4],brick[5]-distance]

def find_disintegratable():
    pass

def part1():
    tower = initialise_tower()
    
    new_tower, _ = drop(tower)
    total = 0
    
    for i in range(len(new_tower)):
        removed_tower = new_tower[:i] + new_tower[i+1:]
        _, fallen = drop(removed_tower)
        if not fallen:
            total += 1

    return total

def part2():
    tower = initialise_tower()
    
    new_tower, _ = drop(tower)
    total = 0
    
    for i in range(len(new_tower)):
        removed_tower = new_tower[:i] + new_tower[i+1:]
        _, fallen = drop(removed_tower)
        if fallen:
            total += fallen

    return total

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")