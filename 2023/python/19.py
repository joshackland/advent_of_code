import re

workflows = dict()
ratings = list()

original_workflows = dict()

with open('../input/19.txt', 'r') as f:
    add_workflow = True
    for line in f:  
        line = line.rstrip()   

        if line == '':
            add_workflow = False
            continue  

        if add_workflow:   
                     
            key, workflow = line.replace('}','').split('{')
            original_workflows[key] = workflow
            conditions = workflow.split(',')

            lambda_str = "lambda rating: "

            for condition in conditions:
                if ':' in condition:
                    condition, result = condition.split(':')
                    sign = ">" if ">" in condition else "<"
                    rating_key, condition = condition.split(sign)
                    lambda_str += f"'{result}' if rating['{rating_key}'] {sign} {condition} else "
                else:
                    lambda_str += f"'{condition}'"

            workflows[key] = eval(lambda_str)

        else:
            line = re.sub(re.compile(r"="), ":", line)
            line = re.sub(re.compile(r'([xmas]+)'), r'"\1"', line)
            ratings.append(eval(line))

def run_workflow(rating):
    key = "in"

    while key not in ['A','R']:
        key = workflows[key](rating)
    
    score = 0

    if key == 'A':
        for value in rating.values():
            score += value

    return score

def total_combinations(ranges,key=None,):
    score = 1
    for range in ranges.values():
        score *= (range[1]-range[0]+1)
    return score


def a_or_r(key,ranges):
    if key == 'A':
        return total_combinations(ranges)
    return 0

def find_ranges(key="in",valid={"x":(1,4000),"m":(1,4000),"a":(1,4000),"s":(1,4000)}):
    local_total = 0
    total = 0
    
    for condition in original_workflows[key].split(','):    
        if ':' not in condition:
            if condition in ['A','R']:
                local_total += a_or_r(condition, valid.copy())
                continue
            else:
                total += find_ranges(condition, valid.copy())
        else:
            char = condition[0]
            operator = condition[1]
            val = int(condition.split(':')[0][2:])
            next_key = condition.split(':')[1]
            
            new_valid = valid.copy()

            if (operator == '<' and valid[char][0] >= val) or (operator == '>' and valid[char][1] <= val):
                continue
            elif (operator == '<' and valid[char][1] < val) or (operator == '>' and valid[char][0] > val):
                if next_key in ['A','R']:                        
                    local_total += a_or_r(next_key, new_valid)
                else:
                    total += find_ranges(next_key, new_valid)
            else:
                if operator == '<':
                    new_valid[char] = (valid[char][0], val-1)
                    valid[char] = (val, valid[char][1])
                else:
                    new_valid[char] = (val+1, valid[char][1])
                    valid[char] = (valid[char][0], val)
                
                if next_key in ['A','R']:
                    local_total += a_or_r(next_key, new_valid)                     
                    #print(key,valid,local_total)   
                else:
                    total += find_ranges(next_key, new_valid)

    return total + local_total

                
                


def part1():
    score = 0
    for rating in ratings:
        score += run_workflow(rating)
    return score

def part2():
    return find_ranges()
    

print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")