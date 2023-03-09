using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Text;
using System.Text.Json;

namespace MQTTClients
{

    public static class MQTTClient
    {
        public static string mqttBrokerUrl = "pirover.xyz";
        public static bool isConnected = false;
        public static string agvId, controlMessage, statusMessage, packageDeliveryMessage, packagePositionMessage;
        static MqttFactory mqttFactory;

        public static void Init()
        {
            mqttFactory = new MqttFactory();
            Console.WriteLine("MQTT Starting");
            controlMessage = "";
        }
        public static async Task Publish_Message(string topic, string message)
        {
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(mqttBrokerUrl)
                    .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

                await mqttClient.DisconnectAsync();

                Console.WriteLine("Message was published.");
            }

        }
        public static async Task Subscribe_Handle()
        {
            if (!isConnected)
            {
                var mqttClient = mqttFactory.CreateMqttClient();

                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(mqttBrokerUrl).Build();

                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    switch (e.ApplicationMessage.Topic)
                    {
                        case "agv/status":
                            break;
                        case "agv/control/001":
                            controlMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            break;
                        case "agv/package/delivery":
                            break;
                        case "agv/pacakge/location":
                            break;
                    }
                    Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                    Console.WriteLine("Message processing done");
                    return Task.CompletedTask;
                };

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("agv/status");
                        })
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("agv/control/001");
                        })
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("agv/package/delivery");
                        })
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("agv/pacakge/location");
                        })
                    .Build();

                await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                Console.WriteLine("MQTT client subscribed to topics.");
                isConnected = true;
                return;
            }
        }
    }
}