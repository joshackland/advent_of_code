input = list()

with open('../input/13.txt', 'r') as f:
    new_grid = list()
    for line in f:
        line = line.rstrip()
        if line != '':
            new_grid.append(line)
        else:
            input.append(new_grid)
            new_grid = list()
    if new_grid:
        input.append(new_grid)

def reflection(pattern, smudge=0):
    for offset in range(len(pattern)-1):
        total_smudge = 0
        
        for index in range(len(pattern)):
            index1 = index 
            index2 = offset+1-index+offset

            if index1 > index2:
                break
            
            if index2 in range(len(pattern)) and pattern[index1] != pattern[index2]:
                total_smudge += len([cell for cell in range(len(pattern[index1])) if pattern[index1][cell] != pattern[index2][cell]])
                
        if total_smudge == smudge:
            return offset
    return None


def part1():
    score = 0

    for rows in input:
        row_index = reflection(rows)
        
        if row_index is not None:
            score += 100 * (row_index+1)

        columns = list()

        for i in range(len(rows[0])):
            column = list()
            for row in rows:
                column.append(row[i])
            columns.append("".join(column))
        
        column_index = reflection(columns)
        
        if column_index is not None:
            score += column_index+1
    return score

def part2():
    score = 0

    for rows in input:
        row_index = reflection(rows,1)
        
        if row_index is not None:
            score += 100 * (row_index+1)

        columns = list()

        for i in range(len(rows[0])):
            column = list()
            for row in rows:
                column.append(row[i])
            columns.append("".join(column))
        
        column_index = reflection(columns, 1)
        
        if column_index is not None:
            score += column_index+1
    return score

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")