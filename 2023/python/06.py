input = list()

with open('../input/06.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())
        
def part1():
    times = list()
    distances = list()
    total = 1

    for num in input[0].split(':')[1].split():
        if num:
            times.append(int(num))

    for num in input[1].split(':')[1].split():
        if num:
            distances.append(int(num))

    for (time, distance) in zip(times, distances):
        count = 0
        for i in range(time):
            speed = i
            remaining = time - i
            if speed * remaining > distance:
                count += 1
        total *= count
    return total

def part2():
    time = None
    distance = None

    time_str = ""
    for num in input[0].split(':')[1].split():        
        if num:
           time_str += num
    time = int(time_str)

    dist_str = ""
    for num in input[1].split(':')[1].split():
        if num:
            dist_str += num
    distance = int(dist_str)

    count = 0
    for i in range(time):
        speed = i
        remaining = time - i
        if speed * remaining > distance:
            count += 1
            
    return count


print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")