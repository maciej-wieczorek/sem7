#!/usr/bin/env python
import sys

last_key = None
sum_value = 0

for input_line in sys.stdin:
	input_line = input_line.strip()
	key, value = input_line.rsplit(" ", 1)
	value = int(value)

	if last_key == key:
		sum_value += value
	else:
		if last_key:
			print(f'{last_key} {sum_value} ')
		sum_value = value
		last_key = key

if last_key == key:
	print(f'{last_key} {sum_value} ')

