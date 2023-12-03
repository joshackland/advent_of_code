input = list()

with open('../input/01.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())

def part1():
    values = list()

    for line in input:
        first = ""
        last = ""

        for char in line:
            if char in "0123456789":
                first = char
                break
        
        
        for char in reversed(line):
            if char in "0123456789":
                last = char
                break

        values.append(int(first + last))

    return sum(values)

def part2():
    num_words = {
        "zero": "0",
        "one": "1",
        "two": "2",
        "three": "3",
        "four": "4",
        "five": "5",
        "six": "6",
        "seven": "7",
        "eight": "8",
        "nine": "9",
    }

    values = list()

    for line in input:
        first = ""
        last = ""

        for index, char in enumerate(line):
            if char in "0123456789":
                if not first:
                    first = char
                last = char
            else:
                for key, value in num_words.items():
                    key_length = len(key)
                    if index + key_length <= len(line):
                        if line[index:index+key_length] == key:
                            if not first:
                                first = value
                            last = value
                            break

        values.append(int(first + last))
        
    return sum(values)


print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")