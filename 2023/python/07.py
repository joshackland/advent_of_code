input = list()

with open('../input/07.txt', 'r') as f:
    for line in f:
        input.append(line.rstrip())

def part1():
    card_types = {
        "A":14, 
        "K":13, 
        "Q":12, 
        "J":11, 
        "T":10, 
        "9":9, 
        "8":8, 
        "7":7, 
        "6":6, 
        "5":5, 
        "4":4, 
        "3":3,
        "2":2
    }
    games = {}
    for line in input:
        hand, bid = line.split()
        cards = dict()
        for char in hand:
            if char not in cards:
                cards[char] = 0
            cards[char] += 1
        cards = dict(sorted(cards.items(), key=lambda x: -x[1]))
        values = ""
        for value in cards.values():
            values += str(value)
        hand_types = ""
        for hand_type in hand:
            hand_types += str(card_types[hand_type]).zfill(2)
        games[(values,hand_types)] = int(bid)
    
    ordered_games = dict(sorted(games.items(), key=lambda x: (x[0][0], x[0][1])))
    
    total = 0
    for index, value in enumerate(ordered_games.values()):
        total += (index+1) * value
    
    return total


def part2():
    card_types = {
        "A":14, 
        "K":13, 
        "Q":12, 
        "J":1, 
        "T":10, 
        "9":9, 
        "8":8, 
        "7":7, 
        "6":6, 
        "5":5, 
        "4":4, 
        "3":3,
        "2":2
    }
    games = {}
    for line in input:
        hand, bid = line.split()
        cards = dict()
        for char in hand:
            if char not in cards:
                cards[char] = 0
            cards[char] += 1
        cards = dict(sorted(cards.items(), key=lambda x: -x[1]))
        values = ""
        if 'J' in cards:
            normal_values = [v for k, v in cards.items() if k != 'J']
            if len(normal_values) > 0:
                max_count = max(normal_values)
                max_values = [k for k,v in cards.items() if k != 'J' and v == max_count]
                max_value = max(max_values, key=lambda x: card_types[x])
            else:
                max_value = 'A'
            cards[max_value] = cards['J'] + cards.get(max_value, 0)
            cards = dict(sorted(cards.items(), key=lambda x: -x[1]))
            del cards['J']
            
        for value in cards.values():
            values += str(value)
        hand_types = ""
        for hand_type in hand:
            hand_types += str(card_types[hand_type]).zfill(2)
        games[(values,hand_types)] = int(bid)
    
    ordered_games = dict(sorted(games.items(), key=lambda x: (x[0][0], x[0][1])))
    #print(ordered_games)
    total = 0
    for index, value in enumerate(ordered_games.values()):
        total += (index+1) * value
    
    return total
    pass

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")