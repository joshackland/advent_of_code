import sys
sys.setrecursionlimit(1000000)

from functools import cache

grid = list()

with open('../input/16.txt', 'r') as f:
    for line in f:
        grid.append([cell for cell in line.rstrip()])

previous_inputs = dict()

def move(loc, direction):
    global previous_inputs
    if loc[0] < 0 or loc[0] >= len(grid[0]) or loc[1] < 0 or loc[1] >= len(grid):
        return

    if loc in previous_inputs and direction in previous_inputs[loc]:
        return
    
    previous_inputs[loc] = previous_inputs.get(loc,[]) + [direction]
    
    cell = grid[loc[1]][loc[0]]

    if cell == '.' or (direction in ['E','W'] and cell == '-') or (direction in ['N','S'] and cell == '|'):
        if direction == 'N':
            loc = (loc[0],loc[1]-1)
        if direction == 'S':
            loc = (loc[0],loc[1]+1)
        if direction == 'E':
            loc = (loc[0]+1,loc[1])
        if direction == 'W':
            loc = (loc[0]-1,loc[1])
        move(loc, direction)
    elif (direction in ['E','W'] and cell == '|') or (direction in ['N','S'] and cell == '-'):
        if direction in ['E','W']:
            locN = (loc[0],loc[1]-1)
            locS = (loc[0],loc[1]+1)
            move(locN, "N")
            move(locS, "S")
        elif direction in ['N','S']:
            locE = (loc[0]+1,loc[1])
            locW = (loc[0]-1,loc[1])
            move(locE, "E")
            move(locW, "W")
    elif (cell in ["\\","/"]):
        if direction == 'E':
            if cell == '\\':
                move((loc[0],loc[1]+1), "S")
            else:
                move((loc[0],loc[1]-1), "N")
        elif direction == 'W':
            if cell == '\\':
                move((loc[0],loc[1]-1), "N")
            else:
                move((loc[0],loc[1]+1), "S")
        elif direction == 'N':
            if cell == '\\':
                move((loc[0]-1,loc[1]), "W")
            else:
                move((loc[0]+1,loc[1]), "E")
        elif direction == 'S':
            if cell == '\\':
                move((loc[0]+1,loc[1]), "E")
            else:
                move((loc[0]-1,loc[1]), "W")
    else:
        print('SOMETHING ISNT WORKING')


def part1():
    move((0,0),'E')
    return len(previous_inputs.keys())

def part2():
    global previous_inputs
    total = list()
    for i in range(len(grid)):
        previous_inputs = dict()
        move((0,0+i),'E')
        total.append(len(previous_inputs.keys()))

        previous_inputs = dict()
        move((len(grid[0])-1,0+i),'W')
        total.append(len(previous_inputs.keys()))

    for i in range(len(grid[0])):
        previous_inputs = dict()
        move((0+i,0),'S')
        total.append(len(previous_inputs.keys()))

        previous_inputs = dict()
        move((0+i,len(grid)-1),'N')
        total.append(len(previous_inputs.keys()))
    
    return max(total)
        

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")