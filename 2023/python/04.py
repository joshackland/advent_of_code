input = list()

with open('../input/04.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())
        
def part1():
    total = 0
    for line in input:
        card_score = 0

        while '  ' in line:
            line = line.replace('  ', ' ')
        cards = line.split(' | ')
        winning_nums = cards[0].split(':')[1].strip().split(' ')
        my_nums = cards[1].strip().split(' ')
        
        for num in my_nums:
            if num in winning_nums:
                if card_score == 0:
                    card_score = 1
                else:
                    card_score *= 2
        
        total += card_score

    return total
    


def part2():
    card_total = dict()
    for index, line in enumerate(input):
        index += 1
        
        if index not in card_total:
            card_total[index] = 0
        card_total[index] += 1
        
        total_win = 0

        while '  ' in line:
            line = line.replace('  ', ' ')
        cards = line.split(' | ')
        winning_nums = cards[0].split(':')[1].strip().split(' ')
        my_nums = cards[1].strip().split(' ')
        
        for num in my_nums:
            if num in winning_nums:
                total_win += 1
                
        for i in range(1,total_win+1):
            current_index = index + i
            if current_index not in card_total:
               card_total[current_index] = 0
            card_total[current_index] += card_total[index]
            
    return sum(card_total.values())


print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")