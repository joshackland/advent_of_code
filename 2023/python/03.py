input = list()

with open('../input/03.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())
        
def part1():
    part_nums = list()
    NUM = '0123456789'

    for y, line in enumerate(input):
        for x, cell in enumerate(line):            
            if cell not in '0123456789.':
                if y > 0:
                    current_num = ""
                    if input[y-1][x] in NUM:
                        current_num += input[y-1][x]
                    left = 1
                    while x - left >= 0 and input[y-1][x - left] in NUM:
                        current_num = input[y-1][x - left] + current_num
                        left += 1
                    if input[y-1][x] not in NUM and current_num:
                        part_nums.append(int(current_num))
                        current_num = ""
                    right = 1
                    while x + right < len(line) and input[y-1][x + right] in NUM:
                        current_num += input[y-1][x + right]
                        right += 1
                    if current_num:
                        part_nums.append(int(current_num))
                        current_num = ""
                if y < len(input):
                    current_num = ""
                    if input[y+1][x] in NUM:
                        current_num += input[y+1][x]
                    left = 1
                    while x - left >= 0 and input[y+1][x - left] in NUM:
                        current_num = input[y+1][x - left] + current_num
                        left += 1
                    if input[y+1][x] not in NUM and current_num:
                        part_nums.append(int(current_num))
                        current_num = ""
                    right = 1
                    while x + right < len(line) and input[y+1][x + right] in NUM:
                        current_num += input[y+1][x + right]
                        right += 1
                    if current_num:
                        part_nums.append(int(current_num))
                        current_num = ""

                if x > 0:
                    current_num = ""
                    left = 1
                    while x - left >= 0 and input[y][x - left] in NUM:
                        current_num = input[y][x - left] + current_num
                        left += 1
                    
                    if current_num:
                        part_nums.append(int(current_num))
                        current_num = ""

                if x < len(line):
                    current_num = ""
                    right = 1
                    while x + right < len(line) and input[y][x + right] in NUM:
                        current_num += input[y][x + right]
                        right += 1
                    
                    if current_num:
                        part_nums.append(int(current_num))
                        current_num = ""
    
    return sum(part_nums)


def part2():
    gear_ratio = list()
    NUM = '0123456789'

    for y, line in enumerate(input):
        for x, cell in enumerate(line):
            nums = list()          
            if cell == '*':
                if y > 0:
                    current_num = ""
                    if input[y-1][x] in NUM:
                        current_num += input[y-1][x]
                    left = 1
                    while x - left >= 0 and input[y-1][x - left] in NUM:
                        current_num = input[y-1][x - left] + current_num
                        left += 1
                    if input[y-1][x] not in NUM and current_num:
                        nums.append(int(current_num))
                        current_num = ""
                    right = 1
                    while x + right < len(line) and input[y-1][x + right] in NUM:
                        current_num += input[y-1][x + right]
                        right += 1
                    if current_num:
                        nums.append(int(current_num))
                        current_num = ""
                if y < len(input):
                    current_num = ""
                    if input[y+1][x] in NUM:
                        current_num += input[y+1][x]
                    left = 1
                    while x - left >= 0 and input[y+1][x - left] in NUM:
                        current_num = input[y+1][x - left] + current_num
                        left += 1
                    if input[y+1][x] not in NUM and current_num:
                        nums.append(int(current_num))
                        current_num = ""
                    right = 1
                    while x + right < len(line) and input[y+1][x + right] in NUM:
                        current_num += input[y+1][x + right]
                        right += 1
                    if current_num:
                        nums.append(int(current_num))
                        current_num = ""

                if x > 0:
                    current_num = ""
                    left = 1
                    while x - left >= 0 and input[y][x - left] in NUM:
                        current_num = input[y][x - left] + current_num
                        left += 1
                    
                    if current_num:
                        nums.append(int(current_num))
                        current_num = ""

                if x < len(line):
                    current_num = ""
                    right = 1
                    while x + right < len(line) and input[y][x + right] in NUM:
                        current_num += input[y][x + right]
                        right += 1
                    
                    if current_num:
                        nums.append(int(current_num))
                        current_num = ""
                if len(nums) == 2:
                    gear_ratio.append(nums[0] * nums[1])

    
    return sum(gear_ratio)


print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")