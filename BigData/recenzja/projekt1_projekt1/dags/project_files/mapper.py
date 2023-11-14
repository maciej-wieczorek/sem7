#!/usr/bin/env python

import sys

for line in sys.stdin:
	line = line.strip()
	words = line.split(',')
	if words[9] == '2':
		print(words[1][:7], words[7], words[3])
