using Boards;
using SocketClients;
using MQTTClients;
using GuidanceSystems;

SocketClient socketClient;
MQTTClient mqttClient;
Board board;
GuidanceSystem guider;

socketClient = new SocketClient();
board = new Board();
mqttClient = new MQTTClient();

//socketClient.Init();
Console.WriteLine("Starting now 9:55");

board.Init();
//mqttClient.Init();

while (!board.isPort1Ready)
{
    Console.WriteLine("Board is not ready");
}

Console.WriteLine("Board is ready");

while (board.isPort1Ready)
{
    board.Read();
}

Console.ReadLine();
