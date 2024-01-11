import requests
import json

def get_spark_application_ids(spark_ui_address):
    """
    Pobiera listę identyfikatorów aplikacji z interfejsu API RESTowego Spark.

    Args:
    - spark_ui_address (str): Adres Spark Application UI (np. http://<master-node>:4040).

    Returns:
    - List: Lista identyfikatorów aplikacji.
    """
    try:
        # Pobieranie informacji o aplikacjach
        applications_info = requests.get(f"{spark_ui_address}/api/v1/applications").json()
        # Wybieranie tylko identyfikatorów aplikacji
        application_ids = [app['id'] for app in applications_info]
        return application_ids
    except requests.exceptions.RequestException as e:
        print(f"Błąd podczas pobierania informacji o aplikacjach: {e}")
        return None
    

def get_stages_for_applications(spark_ui_address, application_ids):
    stages_dict = {}

    for app_id in application_ids:
        stages_url = f"{spark_ui_address}/api/v1/applications/{app_id}/stages"
        stages_response = requests.get(stages_url)
        
        if stages_response.status_code == 200:
            stages_data = stages_response.json()
            stage_ids = [stage['stageId'] for stage in stages_data]
            stages_dict[app_id] = stage_ids
        else:
            print(f"Failed to retrieve stages for application {app_id}. Status code: {stages_response.status_code}")

    return stages_dict


def sum_metrics_for_stages(spark_ui_address, applications_stages):
    metrics_sum = {
        "numTasks": 0,
        "numActiveTasks": 0,
        "numCompleteTasks": 0,
        "numFailedTasks": 0,
        "numKilledTasks": 0,
        "numCompletedIndices": 0,
        "executorDeserializeTime": 0,
        "executorDeserializeCpuTime": 0,
        "executorRunTime": 0,
        "executorCpuTime": 0,
        "resultSize": 0,
        "jvmGcTime": 0,
        "resultSerializationTime": 0,
        "memoryBytesSpilled": 0,
        "diskBytesSpilled": 0,
        "peakExecutionMemory": 0,
        "inputBytes": 0,
        "inputRecords": 0,
        "outputBytes": 0,
        "outputRecords": 0,
        "shuffleRemoteBlocksFetched": 0,
        "shuffleLocalBlocksFetched": 0,
        "shuffleFetchWaitTime": 0,
        "shuffleRemoteBytesRead": 0,
        "shuffleRemoteBytesReadToDisk": 0,
        "shuffleLocalBytesRead": 0,
        "shuffleReadBytes": 0,
        "shuffleReadRecords": 0,
        "shuffleWriteBytes": 0,
        "shuffleWriteTime": 0,
        "shuffleWriteRecords": 0
    }

    for app_id, stage_ids in applications_stages.items():
        for stage_id in stage_ids:
            stage_url = f"{spark_ui_address}/api/v1/applications/{app_id}/stages/{stage_id}"
            stage_response = requests.get(stage_url)

            if stage_response.status_code == 200:
                stage_data = stage_response.json()[0]
                for metric, value in stage_data.items():
                    if metric in metrics_sum:
                        metrics_sum[metric] += value
            else:
                print(f"Failed to retrieve metrics for stage {stage_id} in application {app_id}. Status code: {stage_response.status_code}")

    return metrics_sum


def get_current_metrics(spark_ui_address):
    # Pobierz identyfikatory aplikacji
    application_ids = get_spark_application_ids(spark_ui_address)
    
    # Pobierz etapy dla aplikacji
    applications_stages = get_stages_for_applications(spark_ui_address, application_ids)
    
    # Zsumuj miary dla etapów
    metrics_sum = sum_metrics_for_stages(spark_ui_address, applications_stages)
    
    return metrics_sum


def subtract_metrics(first_metrics, second_metrics):
    result_metrics = {}

    for key in first_metrics:
        if key in second_metrics:
            result_metrics[key] = first_metrics[key] - second_metrics[key]
        else:
            result_metrics[key] = first_metrics[key]

    # return json.dumps(result_metrics, ensure_ascii=False)
    json_result = json.dumps(result_metrics, ensure_ascii=False, indent=2)
    
    print(json_result)
