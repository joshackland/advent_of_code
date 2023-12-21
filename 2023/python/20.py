import math


modules = dict()

with open('../input/20.txt', 'r') as f:
    for line in f:  
        line = line.rstrip()

        module, destination = line.split(' -> ')
        m_type = None
        if module != 'broadcaster':
            m_type = module[0]
            module = module[1:]
            
        modules[module] = (m_type, destination.split(', '))
        
def part1():
    low = 0
    high = 0

    input_map = dict()
    memory = dict()

    for module, (m_type, destination) in modules.items():
        for d in destination:
            input_map[d] = input_map.get(d, []) + [module]
    
    
    for module, (m_type, destination) in modules.items():
        if m_type == '%':
            memory[module] = False
        elif m_type == '&':
            memory[module] = {i:False for i in input_map[module]}
    
    for _ in range(1000):
        queue = [(None, 'broadcaster', False)]

        while queue:
            new_queue = []

            for source, module, is_high in queue:
                if is_high:
                    high += 1
                else:
                    low += 1

                if module not in modules:
                    continue

                m_type, destinations = modules[module]

                if m_type is None:
                    for d in destinations:
                        new_queue.append((module, d, is_high))
                elif m_type == '%':
                    if is_high:
                        continue

                    current_state = memory[module]
                    
                    memory[module] = not current_state
                    
                    for d in destinations:
                        new_queue.append((module, d, not current_state))
                elif m_type == '&':
                    memory[module][source] = is_high
                                        
                    send_signal = any([not signal for signal in memory[module].values() if not signal])
                    
                    for d in destinations:
                        new_queue.append((module, d, send_signal))

            queue = new_queue
            
    return low * high

    

def part2():
    input_map = dict()
    memory = dict()

    for module, (m_type, destination) in modules.items():
        for d in destination:
            input_map[d] = input_map.get(d, []) + [module]
    
    
    for module, (m_type, destination) in modules.items():
        if m_type == '%':
            memory[module] = False
        elif m_type == '&':
            memory[module] = {i:False for i in input_map[module]}


    module = input_map["rx"][0]
    sources = input_map[module]

    low_counts = dict()
    cycle = 0

    while len(low_counts) < len(sources):
        cycle += 1

        queue = [(None, 'broadcaster', False)]

        while queue:
            new_queue = list()

            for source, module, is_high in queue:
                if module in sources:
                    if not is_high:
                        if module not in low_counts:
                            low_counts[module] = cycle
                
                info = modules.get(module)

                if info is None:
                    continue

                m_type, destinations = info

                if m_type is None:
                    for d in destinations:
                        new_queue.append((module, d, is_high))
                elif m_type == '%':
                    if is_high:
                        continue

                    current_state = memory[module]
                    
                    memory[module] = not current_state
                    
                    for d in destinations:
                        new_queue.append((module, d, not current_state))
                elif m_type == '&':
                    memory[module][source] = is_high
                                        
                    send_signal = any([not signal for signal in memory[module].values() if not signal])
                    
                    for d in destinations:
                        new_queue.append((module, d, send_signal))
            
            queue = new_queue
        
    return math.lcm(*low_counts.values())

    

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")