using Boards;
//using SocketClients;
using MQTTClients;
using GuidanceSystems;
using APIs;
using Localizers;

//SocketClient socketClient;
Board beacon_scanner, motor_controller;
GuidanceSystem guider;
API api;

bool systemCheck = true;
string agvId = "001";
string systemStatus;

//socketClient = new SocketClient();
//motor_controller = new Board("/dev/ttyUSB1");
//beacon_scanner = new Board("COM6", BoardName.Beacon_Scanner);
//motor_controller = new Board("COM3", BoardName.Motor_Controller);
//motor_controller = new Board("/dev/ttyUSB0", BoardName.Motor_Controller);
motor_controller = new Board();
beacon_scanner = new Board();
guider = new GuidanceSystem();
api = new API();

//Give user to choose the port 
Console.WriteLine("Press key to select port");

if (Console.ReadKey().Key == ConsoleKey.Enter)
{
    Console.WriteLine("Windows port");
    //motor_controller.Init("COM3", BoardName.Motor_Controller);
    //beacon_scanner.Init("COM3", BoardName.Beacon_Scanner);
}
else if (Console.ReadKey().Key == ConsoleKey.Spacebar)
{
    Console.WriteLine("Linux port");
    motor_controller.Init("/dev/ttyUSB0", BoardName.Motor_Controller);
    //beacon_scanner.Init("/dev/ttyUSB1", BoardName.Beacon_Scanner);
}


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
    Console.WriteLine("MQTT Client Connection init failed");
    systemCheck = false;
}
systemCheck = true;

systemStatus =
    @"{""id"":""001"",""workingMap"":"""+ Localizer.workingMap + @""",""currentX"":"""+ Localizer.currentX.ToString() + @""",""currentY"":"""+ Localizer.currentY.ToString() + @""",""motor-controller"":""connected"",""beacon-scanner"":""connected""}";

if (systemCheck)
{
    Console.WriteLine("System check done. Switch to idle mode");
    guider.mode = Mode.Idle;
    guider.mode = Mode.Direct;
    //guider.mode = Mode.Delivery;
    Console.WriteLine("Mode: " + guider.mode);
    MQTTClient.Subscribe_Handle();

    while (true)
    {
        //Boards.Board.SendSerial(SerialReceiver.Motor_Controller, "forward");
        MQTTClients.MQTTClient.Publish_Message("agv/status", systemStatus);
        guider.mode = Mode.Direct;
        //guider.Guide();
        //return;
    }
}
else
{
    Console.WriteLine("System check failed");
}

//Console.ReadLine();
//Console.ReadKey();






