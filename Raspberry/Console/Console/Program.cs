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
Console.WriteLine("Starting now 13:00 PM");
board_1.Init();

Console.WriteLine("Starting reading");
board_1.ReadSerial();
//while (true)
//{
//    board_1.Init();
//}
//mqttClient.Init();





