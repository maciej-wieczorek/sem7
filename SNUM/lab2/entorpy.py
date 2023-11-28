import math
def H(l):
    return -sum([x*math.log2(x) for x in l])

print(H([2/10, 2/10, 3/10, 3/10]))