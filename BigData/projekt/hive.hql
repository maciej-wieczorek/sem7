create external table if not exists taxizones_ext(locationID int, borough string, zone string, service_zone string)
ROW FORMAT DELIMITED
FIELDS TERMINATED BY ','
STORED AS TEXTFILE
location '${input_dir4}'
TBLPROPERTIES ("skip.header.line.count"="1");

create external table if not exists dir3_ext(month string, zone string, passengers int)
ROW FORMAT DELIMITED
FIELDS TERMINATED BY '\t'
STORED AS TEXTFILE
location '${input_dir3}';

drop table if exists temp_table

create table temp_table(month string, borough string, passengers int)
ROW FORMAT SERDE 'org.apache.hadoop.hive.serde2.JsonSerDe'
STORED AS TEXTFILE
location '${output_dir6}';

with grouped as (
    select month, borough, sum(passengers) as passengers
    from dir3_ext d3 join taxizones_ext t on d3.zone = t.locationID
    group by month, borough
),
ranked as (
    select month, borough, passengers,
    row_number() over (partition by month order by passengers DESC) as rank
    from grouped
)
insert into temp_table
select month, borough, passengers
from ranked
where rank <= 3;