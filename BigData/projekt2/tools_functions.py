import subprocess
import os
import shutil
from urllib.parse import urlparse, parse_qs

def delete_dir(spark=None, path=None):
    try:
        if spark and spark.conf.get("spark.master") == "yarn":
            # Usunięcie pliku w HDFS
            try:
                subprocess.run(["hadoop", "version"], check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
                hadoop_installed = True
            except FileNotFoundError:
                hadoop_installed = False
            except subprocess.CalledProcessError:
                hadoop_installed = False
            
            # Jeśli Hadoop jest zainstalowany, wykonaj polecenie
            if hadoop_installed:
                # Usunięcie katalogu na HDFS
                subprocess.run(["hadoop", "fs", "-rm", "-r", path], check=True)
                print(f"Successfully deleted file in HDFS: {path}")
            else:
                print("Hadoop is not installed on this system.")
                
        else:
            # Usunięcie pliku w lokalnym systemie plików
            parsed_path = urlparse(path)
            local_path = parsed_path.path if parsed_path.scheme == "file" else path

            if os.path.exists(local_path):
                if os.path.isfile(local_path):
                    os.remove(local_path)
                    print(f"Successfully deleted file: {path}")
                elif os.path.isdir(local_path):
                    shutil.rmtree(local_path)
                    print(f"Successfully deleted directory: {path}")
                else:
                    print(f"Path {path} is neither a file nor a directory.")
            else:
                print(f"Path {path} does not exist.")
    except Exception as e:
        print(f"Error deleting file {path}: {e}")
        
def drop_table(spark, table_name):
    if spark.catalog.tableExists(table_name):
        # Pobranie informacji o tabeli z metastore
        table_desc = spark.sql(f"DESCRIBE EXTENDED {table_name}")

        # Wyszukanie lokalizacji fizycznej w wynikach
        physical_location_line = [line for line in table_desc.collect() if "Location" in line[0]][0]

        # Pobranie samej lokalizacji
        physical_location = physical_location_line[1]

        # Wyświetlenie lokalizacji
        print(f"The physical location of the table {table_name} is: {physical_location}")

        # Usunięcie tabeli
        spark.sql(f"DROP TABLE IF EXISTS {table_name}")

        # Usunięcie katalogu z fizycznymi danymi
        delete_dir(spark, physical_location)
    else:
        print(f"The table {table_name} does not exist.")
        
        warehouse_dir = spark.conf.get("spark.sql.warehouse.dir")
        
        delete_dir(spark, warehouse_dir + f"/{table_name}")

        
def extract_host_and_port(spark, default):
    try:
        # Parsowanie adresu URL
        parsed_url = urlparse(spark.conf.get("spark.driver.appUIAddress"))

        # Ekstrakcja hosta i portu
        host = parsed_url.hostname
        port = parsed_url.port

        # Jeśli port nie jest dostępny w adresie URL, użyj domyślnego portu HTTP (80)
        if port is None:
            port = 80
    
        return f"http://{host}:{port}"

    except Exception as e:
        # print(f"Error extracting host and port: {e}")
        return default