import math


input = list()

with open('../input/09.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())

def part1():
    total = 0

    for line in input:
        sequences = [[int(num) for num in line.split()]]
        while any(num != 0 for num in sequences[-1]):
            sequences.append(
                [num - sequences[-1][index]for index, num in enumerate(sequences[-1][1:])]
            )

        sequences[-1].append(0)
        for i in range(len(sequences)-2, -1,-1):
            sequences[i].append(
                sequences[i+1][-1] + sequences[i][-1]
            )
        total += sequences[0][-1]
    return total    

def part2():
    total = 0

    for line in input:
        sequences = [[int(num) for num in line.split()]]
        while any(num != 0 for num in sequences[-1]):
            sequences.append(
                [num - sequences[-1][index]for index, num in enumerate(sequences[-1][1:])]
            )

        sequences[-1].append(0)
        for i in range(len(sequences)-2, -1,-1):
            sequences[i].append(
                sequences[i][0] - sequences[i+1][-1]
            )
        total += sequences[0][-1]
        
    return total   

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")