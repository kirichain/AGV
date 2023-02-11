using System;
using System.Threading.Tasks;
using System.Security.Authentication;
using MQTTnet.Client;
using MQTTnet.Extensions.WebSocket4Net;
using MQTTnet.Formatter;

namespace MQTTClients
{
    public class MQTTClient
    {
        public static string mqttBrokerUrl;
        public MQTTClient()
        {
            mqttBrokerUrl = "pirover.xyz";
        }
        public static async Init()
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(mqttBrokerUrl).Build();
                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                Console.WriteLine("The MQTT client is connected.");

                response.DumpToConsole();

                var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

                await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
            }

            Console.WriteLine("MQTT Client init done");
        }
    }
}