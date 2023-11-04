create external table if not exists taxizones_ext(locationID int, borough string, zone string, service_zone string)
ROW FORMAT DELIMITED
FIELDS TERMINATED BY ','
STORED AS TEXTFILE
location 'gs://$BUCKET_NAME/projekt1/input/datasource4';

create external table if not exists mrout_ext(month string, zone string, passengers int)
ROW FORMAT DELIMITED
FIELDS TERMINATED BY '\t'
STORED AS TEXTFILE
location 'output';

CREATE TABLE IF NOT EXISTS temp_table(month, string, zone borough, passengers int);
insert into temp_table
select month, borough, passengers from mrout_ext m join taxizones_ext t on m.zone = t.locationID order by passengers desc limit 10;

INSERT OVERWRITE LOCAL DIRECTORY 'hive_output'
ROW FORMAT SERDE 'org.apache.hive.hcatalog.data.JsonSerDe'
SELECT * FROM temp_table;