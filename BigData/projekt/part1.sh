export BUCKET_NAME=bd-23-mw
hadoop fs -copyToLocal gs://$BUCKET_NAME/projekt1/input/datasource1
hadoop fs -mkdir -p input
hadoop fs -put datasource1/* input/
hadoop jar CountStartingZonePassengers.jar CountStartingZonePassengers input output