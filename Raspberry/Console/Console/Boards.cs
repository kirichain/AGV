using System;
using System.IO.Ports;
using System.Threading;

namespace Boards
{
    public class Board
    {
        public SerialPort serialPort;
        public bool isPortReady;
        public string portName;
        public Board(string _portName)
        {
            portName = _portName;
            isPortReady = false;
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

                while (!serialPort.IsOpen)
                {
                    Console.WriteLine("Connecting");
                }

                Console.WriteLine("Port" + portName + " opened successfully");

                isPortReady = true;

                Console.WriteLine("Boards Init Done");
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

                while (!serialPort.IsOpen)
                {
                    //Console.WriteLine("Connecting");
                }
                //Console.WriteLine("Port" + portName + " opened successfully");
                try
                {
                    string serialReading = serialPort.ReadLine();
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