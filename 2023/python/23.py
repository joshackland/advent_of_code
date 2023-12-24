grid = list()

with open('../input/23.txt', 'r') as f:
    for line in f:
        grid.append([cell for cell in line.rstrip()])

start = (0, [ci for ci, cell in enumerate(grid[0]) if cell == '.'][0])
end = (len(grid)-1, [ci for ci, cell in enumerate(grid[-1]) if cell == '.'][0])

UP = (-1, 0)
DOWN = (1, 0)
LEFT = (0, -1)
RIGHT = (0, 1)

DIRECTIONS = [UP, DOWN, LEFT, RIGHT]

ARROWS = {
    "^": UP,
    ">": RIGHT,
    "v": DOWN,
    "<": LEFT,
    ".": None
}

def combine_coords(coord1, coord2):
    return (coord1[0]+coord2[0], coord1[1]+coord2[1])

def part1_follow_map(start, end):
    max_steps = set()
    queue = [(start, list())]

    while queue:
        current_location, previous_locations = queue.pop()
        new_locations = previous_locations.copy()
        new_locations.append(current_location)
        
        if current_location == end:
            max_steps.add(len(new_locations)-1)
            continue
        
        current_cell = grid[current_location[0]][current_location[1]]
        direction = ARROWS[current_cell]     

        for dir in DIRECTIONS:
            if direction and direction != dir:
                continue
            new_coord = combine_coords(current_location, dir)
            
            if new_coord in new_locations or new_coord[0] < 0 or new_coord[0] >= len(grid) or new_coord[1] < 0 or new_coord[1] >= len(grid[0]):
                continue
            
            cell = grid[new_coord[0]][new_coord[1]]
            if cell == '#':
                continue
            
            queue.append((new_coord, list(new_locations.copy())))
            continue

    return max(max_steps)



def part2_follow_map(start, end):
    connecting_cells = dict()

    for ri, row in enumerate(grid):
        for ci, cell in enumerate(row):
            if cell in '.^><v':
                for dir in DIRECTIONS:
                    new_coord = combine_coords((ri,ci), dir)
                    if new_coord[0] < 0 or new_coord[0] >= len(grid) or new_coord[1] < 0 or new_coord[1] >= len(grid[0]):
                        continue
                    if grid[new_coord[0]][new_coord[1]] in '.^><v':
                        if (ri,ci) not in connecting_cells:
                            connecting_cells[(ri,ci)] = {new_coord:1}
                        else:
                            connecting_cells[(ri,ci)][new_coord] = 1

                        if new_coord not in connecting_cells:
                            connecting_cells[new_coord] = {(ri,ci):1}
                        else:
                            connecting_cells[new_coord][(ri,ci)] = 1
    
    while True:
        for cell, values in connecting_cells.items():
            if len(values) == 2:
                cell1, cell2 = values.keys()
                distance1 = connecting_cells[cell][cell1]
                distance2 = connecting_cells[cell][cell2]
                
                del connecting_cells[cell1][cell]
                del connecting_cells[cell2][cell]

                connecting_cells[cell1][cell2] = distance1+distance2
                connecting_cells[cell2][cell1] = distance1+distance2

                del connecting_cells[cell]
                break
        else:
            break
    
    max_steps = set()
    queue = [(start, list(), 0)]

    while queue:
        current_location, previous_locations, distance = queue.pop()
        new_locations = previous_locations.copy()
        new_locations.append(current_location)
        
        if current_location == end:
            max_steps.add(distance)
            continue 
        
        for new_coord, new_distance in connecting_cells[current_location].items():
            if new_coord in new_locations:
                continue
            new_distance += distance
            queue.append((new_coord, new_locations.copy(), new_distance))

    return max(max_steps)


def part1():
    return part1_follow_map(start, end)
    

def part2():
    return part2_follow_map(start, end)

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")