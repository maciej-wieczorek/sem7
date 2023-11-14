#!/usr/bin/env python3
import sys

for line in sys.stdin:
    values = line.strip().split(',')
    if len(values) >= 10:
        month = values[1].split(' ')[0].split('-')[1]
        payment_type = int(values[9])
        pickup_location = values[7]
        passenger_count = int(values[3])

        if payment_type == 2:
            print(f"{month},{pickup_location},{passenger_count}")
