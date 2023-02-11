using Boards;
using SocketClients;
using MQTTClients;
using GuidanceSystems;
using APIs;

SocketClient socketClient;
MQTTClient mqttClient;
Board beacon_scanner, motor_controller;
GuidanceSystem guider;
API api;

socketClient = new SocketClient();
//beacon_scanner = new Board("/dev/ttyUSB0");
//motor_controller = new Board("/dev/ttyUSB1");
//beacon_scanner = new Board("COM4");
motor_controller = new Board("COM4");
mqttClient = new MQTTClient();
guider= new GuidanceSystem();
api= new API();
//socketClient.Init();

//beacon_scanner.Init();
//if (beacon_scanner.isPortReady)
//{
//    Console.WriteLine("Start reading from beacon scanner");
//    beacon_scanner.ReadSerial();
//} else
//{
//    Console.WriteLine("Beacon Scanner fails to connect");
//}

motor_controller.Init();
if (motor_controller.isPortReady)
{
    Console.WriteLine("Motor Controller connection is established");
    motor_controller.checkBoardName();
    //motor_controller.ReadSerial();
}
else
{
    Console.WriteLine("Motor Controller fails to connect");
}

//await mqttClient.Init();

Console.ReadKey();






