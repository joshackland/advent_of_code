input = list()

with open('../input/11.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())

def part1():
    grid = [[cell for cell in line] for line in input]
    galaxies = [(y,x) for y,row in enumerate(grid) for x,cell in enumerate(row) if cell == '#']
    rows = [y for y,line in enumerate(input) if any(cell == '#' for cell in line)]
    columns = [x for x in range(len(grid[0])) if any(line[x] == '#' for line in grid)]

    distance = 0
    
    for index in range(len(galaxies)-1):
        g1 = galaxies[index]
        for g2 in galaxies[index+1:]:
            count = 0

            if g1[0] != g2[0]:
                y_increment = 1 if g1[0] < g2[0] else -1
                for y2 in range(g1[0]+y_increment,g2[0]+y_increment, y_increment):
                    count += 1 if y2 in rows else 2

            if g1[1] != g2[1]:
                x_increment = 1 if g1[1] < g2[1] else -1
                for x2 in range(g1[1]+x_increment,g2[1]+x_increment, x_increment):
                    count += 1 if x2 in columns else 2
                    
            distance += count
    
    return distance

def part2():
    grid = [[cell for cell in line] for line in input]
    galaxies = [(y,x) for y,row in enumerate(grid) for x,cell in enumerate(row) if cell == '#']
    rows = [y for y,line in enumerate(input) if any(cell == '#' for cell in line)]
    columns = [x for x in range(len(grid[0])) if any(line[x] == '#' for line in grid)]

    distance = 0
    
    for index in range(len(galaxies)-1):
        g1 = galaxies[index]
        for g2 in galaxies[index+1:]:
            count = 0

            if g1[0] != g2[0]:
                y_increment = 1 if g1[0] < g2[0] else -1
                for y2 in range(g1[0]+y_increment,g2[0]+y_increment, y_increment):
                    count += 1 if y2 in rows else 1000000

            if g1[1] != g2[1]:
                x_increment = 1 if g1[1] < g2[1] else -1
                for x2 in range(g1[1]+x_increment,g2[1]+x_increment, x_increment):
                    count += 1 if x2 in columns else 1000000
                    
            distance += count
    
    return distance


print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")