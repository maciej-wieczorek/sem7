export AIRFLOW_HOME=~/airflow
pip install apache-airflow
export PATH=$PATH:~/.local/bin
airflow db init
airflow standalone