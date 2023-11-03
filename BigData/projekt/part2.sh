beeline -n ${USER} -u jdbc:hive2://localhost:10000/default

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

select month, borough, passengers from mrout_ext m join taxizones_ext t on m.zone = t.locationID order by passengers desc limit 10;