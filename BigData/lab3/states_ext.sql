CREATE TABLE IF NOT EXISTS states_ext (
id INT, name STRING, abbrev STRING, area FLOAT,
population FLOAT, capital STRING, statehood STRING)
ROW FORMAT DELIMITED
FIELDS TERMINATED BY ','
STORED AS TEXTFILE
location '/tmp/states';