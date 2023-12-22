grid = list()

with open('../input/21.txt', 'r') as f:
    for line in f:  
        grid.append([c for c in line.rstrip()])
        
UP = (-1, 0)
DOWN = (1, 0)
LEFT = (0, -1)
RIGHT = (0, 1)

DIRECTIONS = [UP, DOWN, LEFT, RIGHT]

for ri, row in enumerate(grid):
    if 'S' in row:
        starting = (ri, row.index('S'))
grid[starting[0]][starting[1]] = '.'

def part1():
    current_positions = {starting}

    for _ in range(64):
        current_positions = {(pos[0]+dir[0],pos[1]+dir[1]) for pos in current_positions for dir in DIRECTIONS if grid[pos[0]+dir[0]][pos[1]+dir[1]] == '.'}
    
    return len(current_positions)


def part2():    

    with open('../input/21.txt', 'r') as f:
        lines = f.read().splitlines()
    size = len(lines)
    grid = {complex(y,x):c for y,line in enumerate(lines) for x,c in enumerate(line.rstrip())}
        
    UP = -1j
    DOWN = 1
    LEFT = -1
    RIGHT = 1j

    DIRECTIONS = [UP, DOWN, LEFT, RIGHT]

    target = 26501365
    #target % 131 = 65
    

    total_size = (2*target+1)//size

    opens = {k for k,v in grid.items() if v =='.'}
    starting, =  {k for k,v in grid.items() if v =='S'}
    opens.add(starting)

    all_reachable = {starting}
    
    for _ in range(size):
        all_reachable = {pos+dir for pos in all_reachable for dir in DIRECTIONS + [0] if pos+dir in opens}

    odd_reachable = {starting}
    for _ in range(65):
        odd_reachable = {pos+dir for pos in odd_reachable for dir in DIRECTIONS if pos+dir in opens}

    even_reachable = {starting}
    for _ in range(64):
        even_reachable = {pos+dir for pos in even_reachable for dir in DIRECTIONS if pos+dir in opens}
    
    all_odd_reachable = {complex(y,x) for x in range(size) for y in range(not x%2, size, 2)} & all_reachable
    all_even_reachable = {complex(y,x) for x in range(size) for y in range(x%2, size, 2)} & all_reachable

    outer_odd = all_odd_reachable - odd_reachable
    outer_even = all_even_reachable - even_reachable

    outer_pve_pos = {c for c in outer_odd if (c.real-65)*(c.imag-65)>0} | {c for c in outer_even if (c.real-65)*(c.imag-65)<0}
    outer_nve_pos = {c for c in outer_odd if (c.real-65)*(c.imag-65)<0} | {c for c in outer_even if (c.real-65)*(c.imag-65)>0}

    AO = len(odd_reachable)
    AE = len(even_reachable)
    BP = len(outer_pve_pos)
    BN = len(outer_nve_pos)

    even_half, odd_half = sorted([total_size//2, -((-total_size)//2)], key=lambda x: x%2)


    return odd_half**2 * AO + even_half * odd_half * (BN+BP) + even_half**2 * AE

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")