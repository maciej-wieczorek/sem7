export BUCKET_NAME=bd-23-mw
beeline -n ${USER} -u jdbc:hive2://localhost:10000/default -f hive.hql --hivevar input_dir3=dir3 --hivevar input_dir4=gs://$BUCKET_NAME/projekt1/input/datasource4 --hivevar output_dir6=dir6
