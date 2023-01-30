using Boards;
using SocketClients;
using MQTTClients;
using GuidanceSystems;

SocketClient socketClient;
MQTTClient mqttClient;
Board board_1;
GuidanceSystem guider;

socketClient = new SocketClient();
board_1 = new Board("/dev/ttyUSB0");
//board_1 = new Board("COM4");
mqttClient = new MQTTClient();

//socketClient.Init();
Console.WriteLine("Starting now 14:20 PM");

board_1.Init();
//mqttClient.Init();

while (!board_1.isPortReady)
{
    Console.WriteLine("Boards is not ready");
}

Console.WriteLine("Boards is ready");

while (board_1.isPortReady)
{
    board_1.Read();
}

Console.ReadLine();
