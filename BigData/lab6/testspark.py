from pyspark import SparkConf, SparkContext
import re
import sys
import os

os.environ['PYSPARK_PYTHON'] = sys.executable
os.environ['PYSPARK_DRIVER_PYTHON'] = sys.executable

if len(sys.argv) < 2:
    print("Wymagane dwa parametry: dane źródłowe i szukany wzorzec")
    exit(0)

sourceData = sys.argv[1]
pattern = sys.argv[2]

conf = SparkConf().setAppName("sparkWordCount")
sc = SparkContext(conf=conf)

cano = sc.textFile(sourceData)
canoTokenized = cano.flatMap(lambda line: re.split(r'\W+', line))
canoTokenized = canoTokenized.filter(lambda word: bool(re.match(pattern, word)))
canoWordGroups = canoTokenized.groupBy(lambda word: word)
canoWordCounts = canoWordGroups.map(lambda pair: (pair[0], len(pair[1])))

topWordList = canoWordCounts.sortBy(lambda pair: pair[1], False).take(10)
for wordPair in topWordList:
    print(wordPair)