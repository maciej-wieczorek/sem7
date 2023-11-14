create database projekt1;
use projekt1;

!sh mkdir /tmp/source

create external table if not exists passengers_ext(
month int, 
start_zone int, 
passengers_number float) 
COMMENT 'datasource1_output'
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE
location "${hivevar:input_dir3}";

create table if not exists passengers_orc(
month int, 
start_zone int, 
passengers_number float)
COMMENT 'datasource1_output'
STORED AS ORC;

insert overwrite table passengers_orc 
select * from passengers_ext;

create external table if not exists taxi_zones_ext(
LocationID int, 
Borough string, 
Zone string, 
service_zone string) 
COMMENT 'datasource4'
ROW FORMAT DELIMITED FIELDS TERMINATED BY ','
    STORED AS TEXTFILE
location "${hivevar:input_dir4}"
TBLPROPERTIES ("skip.header.line.count"="1");

create table if not exists taxi_zones_orc(LocationID int, Borough string, Zone string, service_zone string)
COMMENT 'datasource4'
STORED AS ORC;

insert overwrite table taxi_zones_orc select * from taxi_zones_ext where LocationID is not null;

CREATE EXTERNAL TABLE final_result (
    month int,
    Borough string,
    total_passengers float
)
ROW FORMAT SERDE 'org.apache.hadoop.hive.serde2.JsonSerDe' STORED AS TEXTFILE
location "${hivevar:output_dir6}";

WITH monthly_passenger_counts AS (
    SELECT
        p.month,
        t.Borough,
        SUM(p.passengers_number) AS total_passengers
    FROM
        passengers_orc p
    JOIN
        taxi_zones_orc t
    ON
        p.start_zone = t.LocationID
    GROUP BY
        p.month, t.Borough
)
, ranked_boroughs AS (
    SELECT
        month,
        Borough,
        total_passengers,
        ROW_NUMBER() OVER (PARTITION BY month ORDER BY total_passengers DESC) AS rank
    FROM
        monthly_passenger_counts
)
INSERT INTO final_result 
SELECT
    month,
    Borough,
    total_passengers
FROM
    ranked_boroughs
WHERE
    rank <= 3;

drop table final_result;
drop table passengers_ext;
drop table passengers_orc;
drop table taxi_zones_orc;
drop table taxi_zones_ext;
drop database projekt1;
