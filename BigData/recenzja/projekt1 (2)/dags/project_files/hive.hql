CREATE EXTERNAL TABLE IF NOT EXISTS datasource1
(month STRING, zone INT, passengers INT)
COMMENT 'passengers' ROW FORMAT DELIMITED FIELDS TERMINATED BY ','
STORED AS TEXTFILE LOCATION '${input_dir3}';

CREATE EXTERNAL TABLE IF NOT EXISTS datasource4
(zone INT, borough STRING, name STRING, service STRING)
COMMENT 'zones' ROW FORMAT DELIMITED FIELDS TERMINATED BY ','
STORED AS TEXTFILE LOCATION '${input_dir4}';

CREATE EXTERNAL TABLE IF NOT EXISTS result
(month STRING, borough STRING, passengers INT)
ROW FORMAT SERDE 'org.apache.hadoop.hive.serde2.JsonSerDe'
STORED AS TEXTFILE LOCATION '${output_dir6}';

INSERT INTO result
SELECT t.month, t.borough, t.total_passengers
FROM (SELECT t1.month, t2.borough, SUM(t1.passengers) AS total_passengers,
             ROW_NUMBER() OVER(PARTITION BY t1.month ORDER BY SUM(t1.passengers) DESC)
AS rn FROM datasource1 t1 JOIN datasource4 t2 ON t1.zone = t2.zone
      GROUP BY t1.month, t2.borough) t
WHERE t.rn <= 3
ORDER BY t.month, t.total_passengers DESC;