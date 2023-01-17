using System;
using System.IO.Ports;
using System.Threading;
using Sensors;
using Boards;
using SocketClient;
using MQTTClient;
using GuidanceSystem;

SerialPort serialPort;

serialPort = new SerialPort();
serialPort.BaudRate = 115200;
//serialPort.PortName = "COM4";

//serialPort.Open();


//while (true)
//{

//try
//{
//    string message = serialPort.ReadLine();
//    Console.WriteLine(message);
//}
//catch (TimeoutException) { }

//}
Console.WriteLine("Done");
Console.ReadLine();


