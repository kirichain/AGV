using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System.Text;

namespace MQTTClients
{
    public static class MQTTClient
    {
        public static string mqttBrokerUrl = "pirover.xyz";
        public static bool isConnected = false;
        public static string agvId, controlMessage, statusMessage, packageDeliveryMessage, packagePositionMessage;
        public static void Init()
        {
            Console.WriteLine("MQTT Starting");
            controlMessage = "";
        }
        public static async Task Publish_Message(string topic, string message)
        {
            /*
             * This sample pushes a simple application message including a topic and a payload.
             *
             * Always use builders where they exist. Builders (in this project) are designed to be
             * backward compatible. Creating an _MqttApplicationMessage_ via its constructor is also
             * supported but the class might change often in future releases where the builder does not
             * or at least provides backward compatibility where possible.
             */

            var mqttFactory = new MqttFactory();

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
                var mqttFactory = new MqttFactory();
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