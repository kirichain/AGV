using Boards;
using SocketClients;
using MQTTClients;
using GuidanceSystems;
using APIs;

SocketClient socketClient;
Board beacon_scanner, motor_controller;
GuidanceSystem guider;
API api;

socketClient = new SocketClient();
//beacon_scanner = new Board("/dev/ttyUSB0");
//motor_controller = new Board("/dev/ttyUSB1");
//beacon_scanner = new Board("COM4");
motor_controller = new Board("COM3");
guider = new GuidanceSystem();
api= new API();
bool systemCheck = true;
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

//await motor_controller.Init();
//motor_controller.checkBoardName();
if (motor_controller.isPortReady)
{
    Console.WriteLine("Motor Controller connection is established");
    //motor_controller.ReadSerial();
}
else
{
    Console.WriteLine("Motor Controller fails to connect");
    systemCheck = false;
}

//await MQTTClient.Publish_Message();
//await MQTTClient.Handle_Received_Message();

if (MQTTClient.isConnected)
{
    Console.WriteLine("MQTT Client Connection init done");
} else
{
    systemCheck = false;
}
systemCheck = true;
if (systemCheck)
{
    Console.WriteLine("System check done. Switch to idle mode");
    guider.mode = Mode.Idle;
    Console.WriteLine("Mode: " + guider.mode);
    while (true)
    {
        Boards.Board.SendSerial(SerialReceiver.Motor_Controller, "forward");
        //guider.mode = Mode.Direct;
        //await MQTTClient.Handle_Received_Message();
        //guider.guide();
        //return;
    }
}
else
{
    Console.WriteLine("System check failed");
}

//while (systemCheck)
//{
//    await MQTTClient.Handle_Received_Message();
//}
Console.ReadLine();
//Console.ReadKey();






