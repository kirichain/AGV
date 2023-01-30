using System;
using System.IO.Ports;
using System.Threading;

namespace Boards
{
    public class Board
    {
        public SerialPort serialPort;
        public bool isPortReady, isReading, isDisconnect;
        public string portName, serialReading;
        public Board(string _portName)
        {
            portName = _portName;
            isPortReady = false;
            isReading = false;
            isDisconnect = false;
        }
        public void Init()
        {
            Console.WriteLine("Checking boards");

            using (serialPort = new SerialPort(portName, 115200))
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                else
                {
                    serialPort.Dispose();
                    while (serialPort.IsOpen)
                    {

                    }
                    serialPort.Open();
                }

                while (!serialPort.IsOpen)
                {
                    Console.WriteLine("Connecting");
                }

                Console.WriteLine("Port " + portName + " opened successfully");

                isPortReady = true;

                Console.WriteLine("Board Init Done");
            }
        }
        public void Read()
        {
            Console.WriteLine("Reading");

            using (serialPort = new SerialPort(portName, 115200))
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                else
                {
                    serialPort.Dispose();
                    while (serialPort.IsOpen)
                    {

                    }
                    serialPort.Open();
                }

                while (!serialPort.IsOpen)
                {
                    //Console.WriteLine("Connecting");
                }

                try
                {
                    serialReading = serialPort.ReadLine();
                    if (serialReading.Length > 0)
                    {
                        Console.WriteLine(serialReading);
                    }
                }
                catch (TimeoutException) { }
            }
        }
    }


}