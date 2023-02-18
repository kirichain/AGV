using Boards;
//using SocketClients;
using MQTTClients;
using GuidanceSystems;
using APIs;

//SocketClient socketClient;
Board beacon_scanner, motor_controller;
GuidanceSystem guider;
API api;

//socketClient = new SocketClient();
//beacon_scanner = new Board("/dev/ttyUSB0");
//motor_controller = new Board("/dev/ttyUSB1");
//beacon_scanner = new Board("COM6", BoardName.Beacon_Scanner);
//motor_controller = new Board("COM3", BoardName.Motor_Controller);
//motor_controller = new Board("/dev/ttyUSB0", BoardName.Motor_Controller);
motor_controller = new Board();

Console.WriteLine("Press key to select port");

if (Console.ReadKey().Key == ConsoleKey.Enter)
{
    Console.WriteLine("Windows port");
    motor_controller.Init("COM3", BoardName.Motor_Controller);

}
else if (Console.ReadKey().Key == ConsoleKey.Spacebar)
{
    Console.WriteLine("Linux port");
    motor_controller.Init("/dev/ttyUSB0", BoardName.Motor_Controller);

}
guider = new GuidanceSystem();
api = new API();
bool systemCheck = true;
string agvId = "001";
//socketClient.Init();
MQTTClients.MQTTClient.agvId = agvId;
MQTTClients.MQTTClient.Init();
//beacon_scanner.Init();
//if (beacon_scanner.isPortReady)
//{
//    Console.WriteLine("Start reading from beacon scanner");
//    beacon_scanner.ReadSerial();
//} else
//{
//    Console.WriteLine("Beacon Scanner fails to connect");
//}


//await MQTTClient.Publish_Message();
//await MQTTClient.Subscribe_Handle();

if (MQTTClient.isConnected)
{
    Console.WriteLine("MQTT Client Connection init done");
}
else
{
    systemCheck = false;
}
systemCheck = true;
if (systemCheck)
{
    Console.WriteLine("System check done. Switch to idle mode");
    guider.mode = Mode.Idle;
    Console.WriteLine("Mode: " + guider.mode);
    guider.mode = Mode.Direct;
    MQTTClient.Subscribe_Handle();
    while (true)
    {
        //Boards.Board.SendSerial(SerialReceiver.Motor_Controller, "forward");
        //guider.mode = Mode.Direct;
        //Console.WriteLine("Message come. Start to guide now");
        guider.guide();
        //return;
    }
}
else
{
    Console.WriteLine("System check failed");
}

//Console.ReadLine();
//Console.ReadKey();






