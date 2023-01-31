using System;
using System.IO.Ports;
using System.Threading;

namespace Boards
{
    public class Board
    {
        public SerialPort serialPort, serialPortx;
        public bool isPortReady, isReading, isDisconnect;
        public string portName, serialReading;
        public Board(string _portName)
        {
            portName = _portName;
            isPortReady = false;
            isReading = false;
            isDisconnect = false;
            //serialPort = new SerialPort(portName, 115200);
            serialPortx = new SerialPort(portName, 115200);
        }
        public void Check()
        {
            Console.WriteLine("Checking boards");

            //using (serialPort = new SerialPort(portName, 115200))
            //{
            //serialPort = new SerialPort(portName, 115200);
            //serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            //while (!serialPort.IsOpen)
            //{
            //    Console.WriteLine("Connecting");
            //}

            Console.WriteLine("Port " + portName + " opened successfully");
            isPortReady = true;
            Console.WriteLine("Board Init Done");
            serialPort.Close();
            //serialPort.Dispose();
            //Console.ReadKey();
            //}
        }
        public void Reading()
        {
            using (serialPortx = new SerialPort(portName, 115200))
            {
                serialPortx.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                if (!serialPortx.IsOpen)
                {
                    serialPortx.Open();
                }

                Console.WriteLine("Reading config done");
                //try
                //{
                //    serialReading = serialPort.ReadLine();
                //    if (serialReading.Length > 0)
                //    {
                //        Console.WriteLine(serialReading);
                //    }
                //}
                //catch (TimeoutException) { }
            }
        }
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.WriteLine(indata);
        }
    }


}