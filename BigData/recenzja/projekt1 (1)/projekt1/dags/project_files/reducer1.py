#!/usr/bin/env python3
import sys

current_month = None
current_location = None
current_count = 0
passenger_counts = {}

for line in sys.stdin:
    month, location, passenger_count = line.strip().split(",")
    #month, location = month_location.split(',')
    passenger_count = int(passenger_count)
    
    key = (month, location)
    
    if key not in passenger_counts:
        passenger_counts[key] = passenger_count
    else:
        passenger_counts[key] += passenger_count

for (month, location), total_passengers in passenger_counts.items():
    print(f"{month},{location},{total_passengers}")