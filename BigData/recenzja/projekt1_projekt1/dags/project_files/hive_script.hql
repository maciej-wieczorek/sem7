DROP TABLE first_output;
DROP TABLE taxi_zones;
DROP TABLE result;
CREATE EXTERNAL TABLE IF NOT EXISTS first_output(month STRING, starting_location INT, passengers_count INT) ROW FORMAT DELIMITED FIELDS TERMINATED BY ' ' STORED AS TEXTFILE location '${input_dir3}';
CREATE EXTERNAL TABLE IF NOT EXISTS taxi_zones(locationId INT, borough STRING, zone STRING, service_zone STRING) ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' STORED AS TEXTFILE location '${input_dir4}';
CREATE EXTERNAL TABLE IF NOT EXISTS result
(
 `month` string,
 `borough` string,
 `passengers` int
)
ROW FORMAT SERDE 'org.apache.hadoop.hive.serde2.JsonSerDe'
LOCATION '${output_dir6}';
INSERT INTO result SELECT month, borough, passengers FROM (
SELECT first_output.month, taxi_zones.borough, SUM(first_output.passengers_count) as passengers,
ROW_NUMBER() OVER (PARTITION BY first_output.month ORDER BY SUM(first_output.passengers_count) DESC) as n
FROM first_output
JOIN taxi_zones
ON (first_output.starting_location=taxi_zones.locationId)
GROUP BY first_output.month, taxi_zones.borough
) t where n <=3;
