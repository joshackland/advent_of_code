import sys
sys.setrecursionlimit(1000000)
import heapq
grid = list()

with open('../input/17.txt', 'r') as f:
    for line in f:
        grid.append([int(cell) for cell in line.rstrip()])

UP = (-1, 0)
DOWN = (1, 0)
LEFT = (0, -1)
RIGHT = (0, 1)

TARGET = (len(grid)-1, len(grid[0])-1)

def pathfind(grid, MIN_TOTAL_DIRECTION, MAX_TOTAL_DIRECTION):
    visited = set()
    directions_to_go = [(0,0,0,RIGHT,1),(0,0,0,DOWN,1)]
    heapq.heapify(directions_to_go)

    while len(directions_to_go) > 0:
        total,y,x,direction,total_direction = heapq.heappop(directions_to_go)

        if (y,x,direction,total_direction) in visited:
            continue
        else:
            visited.add((y,x,direction,total_direction))

        if total_direction > MAX_TOTAL_DIRECTION:
            continue

        coord = (y+direction[0],x+direction[1])

        if coord[0] < 0 or coord[0] >= len(grid) or coord[1] < 0 or coord[1] >= len(grid[0]):
            continue
        
        total += grid[coord[0]][coord[1]]

        if total_direction >= MIN_TOTAL_DIRECTION and coord[0] == TARGET[0] and coord[1] == TARGET[1]:
            return total
        
        for dir in [UP,DOWN,LEFT,RIGHT]:        
            if dir[0] + direction[0] == 0 and dir[1] + direction[1] == 0:
                continue
            if dir != direction and total_direction < MIN_TOTAL_DIRECTION:
                continue
            new_total_dir = 1 if dir != direction else total_direction+1
            heapq.heappush(directions_to_go, (total,coord[0],coord[1],dir,new_total_dir))

def part1():
    return pathfind(grid,0,3)

def part2():
    return pathfind(grid,4,10)
        

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")