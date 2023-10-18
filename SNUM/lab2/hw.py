from math import log2
import pandas
def H(P):
    s = 0
    for p in P:
        s += p * log2(p)
    
    return -s

def IDD3():
    pass

if __name__ == '__main__':
    data = pandas.read_csv('titanic-homework.csv')
    data.drop(columns=['PassengerId', 'Name'], inplace=True)
    print(data.head())