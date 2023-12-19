instructions = list()

with open('../input/18.txt', 'r') as f:
    for line in f:
        instructions.append([i for i in line.rstrip().split()])

UP = (-1, 0)
DOWN = (1, 0)
LEFT = (0, -1)
RIGHT = (0, 1)

def part_1_starting_area(instructions):
    starting_area = 0
    for instruction in instructions:
        direction, distance, colour = instruction

        starting_area += int(distance)
    
    return starting_area

def part_2_starting_area(instructions):
    starting_area = 0
    for instruction in instructions:
        hex = instruction[2]
        hex = int((hex.replace('(','').replace('#','').replace(')','')[:-1]),16)
        
        starting_area += hex
    return starting_area

def part_1_create_perimeter(instructions):
    perimeter = [(0,0)]
    
    for instruction in instructions:
        direction, distance, colour = instruction

        distance= int(distance)
        dir = UP

        if direction == 'R':
            dir = RIGHT
        elif direction == 'L':
            dir = LEFT
        elif direction == 'D':
            dir = DOWN

        next = (perimeter[-1][0]+(dir[0]*distance), perimeter[-1][1]+(dir[1]*distance))

        perimeter.append(next)

    return perimeter


def part_2_create_perimeter(instructions):
    perimeter = [(0,0)]
    
    for instruction in instructions:
        hex = instruction[2]

        hex = hex.replace('(','').replace('#','').replace(')','')
        
        direction = hex[-1]
        distance = int(hex[:-1],16)

        distance= int(distance)
        dir = UP

        if direction == '0':
            dir = RIGHT
        elif direction == '2':
            dir = LEFT
        elif direction == '1':
            dir = DOWN

        next = (perimeter[-1][0]+(dir[0]*distance), perimeter[-1][1]+(dir[1]*distance))

        perimeter.append(next)
        
    return perimeter

def find_area(perimeter):
    A = 0

    total = 0
    for index, point in enumerate(perimeter[:-1]):
        x0y1 = (point[1]) * perimeter[index+1][0]
        x1y0 = point[0] * (perimeter[index+1][1])
                
        total += x0y1 
        total -= x1y0
        
    A = total / 2
    return A

def part1():
    area = part_1_starting_area(instructions)
    perimeter = part_1_create_perimeter(instructions)
    inner_area = find_area(perimeter)

    return area/2 + inner_area + 1

def part2():
    area = part_2_starting_area(instructions)
    perimeter = part_2_create_perimeter(instructions)
    inner_area = find_area(perimeter)
    
    return area/2 + inner_area + 1

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")