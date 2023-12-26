wires = dict()

with open('../input/25.txt', 'r') as f:
    for line in f:
        initial, others = line.split(': ')

        for wire in others.split():
            wires[initial] = wires.get(initial, set())
            wires[initial].add(wire)
            wires[wire] = wires.get(wire, set())
            wires[wire].add(initial)

def part1():
    wire_count = lambda w: len(wires[w] - wire_group)
    wire_group = set(wires.keys())
    
    while sum(map(wire_count, wire_group)) != 3:
        wire_group.remove(max(wire_group, key=wire_count))

    return len(wire_group) * len(set(wires.keys()) - wire_group)



def part2():
    pass

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")