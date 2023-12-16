from functools import cache

input = list()

with open('../input/15.txt', 'r') as f:
    for line in f:
        input = line.rstrip().split(',')
        
def hash(label):
    hash = 0
    for c in label:
        hash += ord(c)
        hash *= 17
        hash %= 256
    
    return hash

def part1():
    total = 0
    
    for value in input:
        total += hash(value)
    return total

def part2():
    total = 0

    boxes = {box:dict() for box in range(256)}
    
    for value in input:
        if '=' in value:
            label, operation = value.split('=')
            box = hash(label)
            boxes[box][label] = int(operation)
        else:
            label, operation = value.split('-')
            box = hash(label)
            if label in boxes[box]:
                del boxes[box][label]

    total = 0
    
    for box in boxes:
        if len(boxes[box]) == 0: continue
        item_count = 1
        for value in boxes[box].values():
            total += (box + 1) * item_count * value
            item_count += 1
    
    return total

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")