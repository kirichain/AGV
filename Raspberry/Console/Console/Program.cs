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
Console.WriteLine("Starting now 9:30 AM");
//board_1.Init();

Console.WriteLine("Starting reading");
board_1.Init();
//while (true)
//{
//    board_1.Init();
//}
//mqttClient.Init();





