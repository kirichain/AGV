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

while (!board.IsPort1Ready)
{ }
while (board.IsPort1Ready)
{
    board.Read();
}

Console.ReadLine();
