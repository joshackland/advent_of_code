from functools import cache

input = list()

with open('../input/12.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())

@cache
def iterate(p_index, n_index, pattern, numbers, total=0):
    if p_index == len(pattern):
        return 1 if n_index == len(numbers) else 0
    
    if pattern[p_index] in '.?':
        total += iterate(p_index+1, n_index, pattern, numbers)
    
    try:
        pn_index = p_index + numbers[n_index]

        if pattern[p_index] in '#?' and '.' not in pattern[p_index:pn_index] and '#' not in pattern[pn_index]:
            total += iterate(pn_index+1, n_index+1, pattern, numbers)
    except IndexError:
        pass

    return total

def part1():
    total = 0
    for line in input:
        pattern, numbers = line.split()
        numbers = [int(num) for num in numbers.split(',')]
        num = iterate(0, 0, pattern+'.', tuple(numbers))
        total += num
    return total

def part2():
    total = 0
    for line in input:
        pattern, numbers = line.split()
        numbers = [int(num) for num in numbers.split(',')]
        num = iterate(0, 0,(pattern+'?')*5, tuple(numbers*5))
        total += num
    return total

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")