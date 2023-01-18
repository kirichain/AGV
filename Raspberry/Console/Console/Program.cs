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
board.Init();
//mqttClient.Init();

while (true)
{
    board.Reading();
}

Console.ReadLine();
