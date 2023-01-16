using System;
using System.IO.Ports;
using System.Threading;

SerialPort serialPort;

serialPort = new SerialPort();
serialPort.BaudRate = 115200;
serialPort.PortName = "COM4";

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



