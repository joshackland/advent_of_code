import math


input = list()

with open('../input/08.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())

def part1():
    instructions = input[0]
    step = 0
    instruction_len = len(instructions)

    map = dict()
    
    for i in range(2,len(input)):
        split1 = input[i].split('=')
        current = split1[0].strip()
        next_element = split1[1].replace('(','').replace(')','').split(',')
        map[current] = (next_element[0].strip(), next_element[1].strip())

    current = 'AAA'

    while current != 'ZZZ':
        step += 1

        direction = 0 if instructions[(step-1)%instruction_len] == 'L' else 1
        current = map[current][direction]
    return step

def part2():
    instructions = input[0]
    instruction_len = len(instructions)

    map = dict()
    
    for i in range(2,len(input)):
        split1 = input[i].split('=')
        current = split1[0].strip()
        next_element = split1[1].replace('(','').replace(')','').split(',')
        map[current] = (next_element[0].strip(), next_element[1].strip())

    starting_elements = [element for element in map.keys() if element.endswith('A')]
    steps = list()

    for element in starting_elements:
        current = element
        step = 0

        while not current.endswith('Z'):
            direction = 0 if instructions[(step)%instruction_len] == 'L' else 1
            current = map[current][direction]
            step += 1
        steps.append(step)
    
    return math.lcm(*steps)

#print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")