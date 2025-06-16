using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using System;
using System.Text;
using UnityEngine;

public class IoTHubDataReceiver : MonoBehaviour
{
    private EventHubConsumerClient consumer;
    private string eventHubConnectionString = "your_eventhub_connection_string";

    [System.Serializable]
    public class SensorData
    {
        public int messageId;
        public string deviceId;
        public float temperature;
        public float humidity;
    }

    async void Start()
    {
        consumer = new EventHubConsumerClient(
            EventHubConsumerClient.DefaultConsumerGroupName,
            eventHubConnectionString
        );

        await StartListeningAsync();
    }

    private async System.Threading.Tasks.Task StartListeningAsync()
    {
        await foreach (PartitionEvent receivedEvent in consumer.ReadEventsAsync())
        {
            string jsonData = Encoding.UTF8.GetString(receivedEvent.Data.Body.ToArray());
            SensorData data = JsonUtility.FromJson<SensorData>(jsonData);

            // Unity 메인 스레드에서 UI 업데이트
            UnityMainThreadDispatcher.Instance.Enqueue(() => {
                UpdateTemperatureDisplay(data.temperature);
                UpdateHumidityDisplay(data.humidity);
            });
        }
    }
}
