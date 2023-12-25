import re
import math

class Hailstone:
    def __init__(self, px, py, pz, vx, vy, vz):
        self.px = px
        self.py = py
        self.pz = pz
        self.vx = vx
        self.vy = vy
        self.vz = vz

hailstones = list()

with open('../input/24.txt', 'r') as f:
    for line in f:
        line = line.rstrip().replace('@', ',')
        line = re.sub(re.compile(' '), '', line)
        vals = line.split(',')
        
        hailstones.append(
            Hailstone(
                int(vals[0]),
                int(vals[1]),
                int(vals[2]),
                int(vals[3]),
                int(vals[4]),
                int(vals[5])
            )
        )

def intersect(h1, h2):
	a1 = h1.vy/h1.vx
	b1 = h1.py - a1*h1.px
	a2 = h2.vy/h2.vx
	b2 = h2.py - a2*h2.px
	if math.isclose(a1, a2):
		if math.isclose(b1, b2):
			return False
		return False
	cx = (b2-b1)/(a1-a2)
	cy = cx*a1 + b1
	in_future = (cx > h1.px) == (h1.vx > 0) and (cx > h2.px) == (h2.vx > 0)
	return (in_future, cx, cy)

def part1():
    MIN = 7
    MAX = 27
    
    MIN = 200000000000000
    MAX = 400000000000000
    total = 0

    for i1, h1 in enumerate(hailstones[:-1]):
        for i2, h2 in enumerate(hailstones[i1:]):
            value = intersect(h1, h2)

            if value:
                future, x, y = value
                if future:
                    if MIN <= x <= MAX and MIN <= y <= MAX:
                        total += 1
    
    return total

def part2():    
    s = list()
    sv = list()

    value = 0

    for h in hailstones:
        s.append(h.px+h.py+h.pz)
        sv.append(h.vx+h.vy+h.vz)

    for sv_r in range(-1000, 1000):
        if sv_r in sv:
            continue
        m_and_s = [[(sv_i - sv_r), s_i % (sv_i - sv_r)] for s_i, sv_i in zip(s, sv)]
        for i in range(len(m_and_s)):
            if m_and_s[i][0] < 0:
                m_and_s[i][0] = -m_and_s[i][0]
                m_and_s[i][1] = m_and_s[i][1] + m_and_s[i][0]
        m_and_s.sort(key=lambda p: p[0], reverse=True)
        m = []
        s_ = []
            
        while m_and_s:
            m_i, s_i = m_and_s.pop(0)
            m.append(m_i)
            s_.append(s_i)
            m_and_s = [(m_j, s_j) for (m_j, s_j) in m_and_s if math.gcd(m_j, m_i) == 1]
        s_r = chinese_remainder(m, s_)
        if all(is_positive_integer((s_r-s_i)/(sv_i-sv_r)) for s_i, sv_i in zip(s, sv)):
            value = s_r
    return value
    

def is_positive_integer(x):
    return x > 0 and math.floor(x) == x

class ChineseRemainderConstructor:
    def __init__(self, bases):
        self._bases = bases
        p = 1
        for x in bases:
            p *= x
        self._prod = p
        self._inverses = [p//x for x in bases]
        self._muls = [inv * self.mul_inv(inv, base) for base, inv
                      in zip(self._bases, self._inverses)]
          
    def rem(self, mods):
        ret = 0
        for mul, mod in zip(self._muls, mods):
            ret += mul * mod
        return ret % self._prod
    
    def mul_inv(self, a, b):
        initial_b = b
        x0, x1 = 0, 1
        if b == 1:
            return 1
        while a > 1:
            div, mod = divmod(a, b)
            a, b = b, mod
            x0, x1 = x1 - div * x0, x0
        return (x1 if x1 >= 0 else x1 + initial_b)
    
def chinese_remainder(n, mods):
    return ChineseRemainderConstructor(n).rem(mods)


print(f"Part 1: {str(part1())}")
print(f"Part 2: {str(part2())}")