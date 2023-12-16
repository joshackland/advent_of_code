input = list()

with open('../input/10.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())

def part1():
    pipes = {
        "|": ["N","S"],
        "-": ["W","E"],
        "L": ["N","E"],
        "J": ["W","N"],
        "7": ["W","S"],
        "F": ["S","E"]
    }

    directions = {
        "N": [-1,0],
        "S": [1,0],
        "W": [0,-1],
        "E": [0,1],
    }

    opposite_direction = {
        "N": "S",
        "S": "N",
        "W": "E",
        "E": "W",
    }

    grid = [[cell for cell in line] for line in input]
    grid_steps = [[-1 for _ in line] for line in input]

    start_x = [x for x,line in enumerate(grid) if 'S' in line][0]
    start_y = [y for y,cell in enumerate(grid[start_x]) if 'S' == cell][0]

    valid_directions = list()
    for direction, value in directions.items():
        current_x = start_x + value[0]
        current_y = start_y + value[1]
        if current_x >= 0 and current_x < len(grid) and current_y >= 0 and current_y < len(grid[0]):
            if grid[current_x][current_y] == '.':
                continue

            if opposite_direction[direction] in pipes[grid[current_x][current_y]]:
                valid_directions.append(direction)
    
    for pipe, direction in pipes.items():
        if valid_directions[0] in direction and valid_directions[1] in direction:
            grid[start_x][start_y] = pipe
            break

    def follow_path(current_location, current_path):
        valid_directions = pipes[grid[current_location[0]][current_location[1]]]
        
        for direction in valid_directions:
            new_path = list(current_path)
            x, y = directions[direction]
            x += current_location[0]
            y += current_location[1]
            
            if [x,y] in new_path:
                continue
            
            if grid_steps[x][y] == -1 or grid_steps[x][y] > len(new_path):
                loc = [x,y]
                new_path.append(loc)
                grid_steps[x][y] = len(new_path)
                follow_path(loc, new_path)
                continue

    import sys
    sys.setrecursionlimit(len(grid)*len(grid[0])*2)
    follow_path([start_x, start_y],[])
    
    return max([cell for line in grid_steps for cell in line])

def part2():
    pipes = {
        "|": ["N","S"],
        "-": ["W","E"],
        "L": ["N","E"],
        "J": ["W","N"],
        "7": ["W","S"],
        "F": ["S","E"]
    }

    directions = {
        "N": [-1,0],
        "S": [1,0],
        "W": [0,-1],
        "E": [0,1],
    }

    opposite_direction = {
        "N": "S",
        "S": "N",
        "W": "E",
        "E": "W",
    }

    grid = [[cell for cell in line] for line in input]
    valid_pipe = set()

    start_x = [x for x,line in enumerate(grid) if 'S' in line][0]
    start_y = [y for y,cell in enumerate(grid[start_x]) if 'S' == cell][0]

    valid_directions = list()
    for direction, value in directions.items():
        current_x = start_x + value[0]
        current_y = start_y + value[1]
        if current_x >= 0 and current_x < len(grid) and current_y >= 0 and current_y < len(grid[0]):
            if grid[current_x][current_y] == '.':
                continue

            if opposite_direction[direction] in pipes[grid[current_x][current_y]]:
                valid_directions.append(direction)
    
    for pipe, direction in pipes.items():
        if valid_directions[0] in direction and valid_directions[1] in direction:
            grid[start_x][start_y] = pipe
            break

    def follow_path(current_location, current_path):
        valid_directions = pipes[grid[current_location[0]][current_location[1]]]
        
        for direction in valid_directions:
            new_path = list(current_path)
            x, y = directions[direction]
            x += current_location[0]
            y += current_location[1]
            
            if [x,y] in new_path:
                continue
            
            if (x,y) not in valid_pipe:
                valid_pipe.add((x,y))
                loc = [x,y]
                new_path.append(loc)
                follow_path(loc, new_path)
                break

    import sys
    sys.setrecursionlimit(len(grid)*len(grid[0])*2)
    follow_path([start_x, start_y],[])
    
    total = 0

    for x,line in enumerate(grid):
        for y,cell in enumerate(line):
            if (x,y) in valid_pipe:
                continue
            x2 = x
            y2 = y
            crosses = list()
            while x2 < len(grid) and y2 < len(line):
                c = grid[x2][y2]
                if (x2,y2) in valid_pipe and c not in ['L','7']:
                    crosses.append((x2,y2))
                x2 += 1
                y2 += 1
            if len(crosses) % 2 == 1:
                total += 1
               
    return total


#Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")