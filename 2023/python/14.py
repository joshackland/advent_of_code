from functools import cache

input = list()

with open('../input/14.txt', 'r') as f:
    for line in f:
        input.append([c for c in line.rstrip()])

@cache
def tilt(grid, direction):
    grid = [list(row) for row in grid]

    grid_range = range(len(grid)) if -1 not in direction else range(len(grid)-1,-1,-1)

    COLUMN = direction[1] == 0
    ROW = direction[0] == 0

    free_space = dict()

    for index1 in grid_range:
        column = index1 if COLUMN else 0
        row = index1 if ROW else 0

        for index2 in grid_range:
            column = index2 if COLUMN else index1
            row = index2 if ROW else index1
            cell = grid[column][row]
            
            key = row if COLUMN else column
            current_free = free_space.get(key, None)
            
            if cell == '.':
                free_space[key] = (column,row) if current_free is None else current_free
            elif cell == '#':
                free_space[key] = None
            else:
                if current_free is not None:
                    free_space_loc = free_space[key]

                    grid[free_space_loc[0]][free_space_loc[1]] = cell
                    grid[column][row] = '.'

                    new_col = free_space_loc[0] if ROW else free_space_loc[0] + direction[0]
                    new_row = free_space_loc[1] if COLUMN else free_space_loc[1] + direction[1]

                    free_space[key] = (new_col,new_row)

    return grid


def calculate_score(grid):
    total_score = 0
    
    for index1 in range(len(grid)):
        for index2 in range(len(grid)):

            if grid[index1][index2] == 'O':
                total_score += len(grid) - index1

    return total_score


def part1():
    grid = tilt(tuple(tuple(line.copy()) for line in input), (1,0))
    return calculate_score(grid)

def part2():
    grid = [line.copy() for line in input]
    prev_grids = dict()

    for index in range(1000000000):
        prev_grids[str(grid)] = index
        for direction in [(1,0),(0,1),(-1,0),(0,-1)]:
            grid = tilt(tuple(tuple(row) for row in grid),direction) 

        if str(grid) in prev_grids:
            index += 1
            prev_index = prev_grids[str(grid)]
            difference = index - prev_index

            grid_indexes = dict()

            for k, v in prev_grids.items():
                grid_indexes[v] = k

            grid = eval(grid_indexes[((1000000000 - prev_index) % difference) + prev_index])
            break
    
    return calculate_score(grid)

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")