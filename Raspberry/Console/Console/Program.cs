using Boards;
using SocketClients;
using MQTTClients;
using GuidanceSystems;
using APIs;

SocketClient socketClient;
MQTTClient mqttClient;
Board beacon_scanner;
GuidanceSystem guider;
API api;

socketClient = new SocketClient();
//beacon_scanner = new Board("/dev/ttyUSB0");
beacon_scanner = new Board("COM4");
mqttClient = new MQTTClient();
guider= new GuidanceSystem();
api= new API();
//socketClient.Init();

beacon_scanner.Init();
if (beacon_scanner.isPortReady)
{
    Console.WriteLine("Start reading from beacon scanner");
    beacon_scanner.ReadSerial();
} else
{
    Console.WriteLine("Beacon Scanner fails to connect");
}

Console.ReadKey();

//mqttClient.Init();





