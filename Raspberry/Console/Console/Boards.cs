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
            serialPort = new SerialPort(portName, 115200);
        }
        public void Init()
        {
            Console.WriteLine("Checking boards");

            using (serialPort = new SerialPort(portName, 115200))
            {
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                //else
                //{
                //    serialPort.Close();
                //    while (serialPort.IsOpen)
                //    {

                //    }
                //    serialPort.Open();
                //}

                //while (!serialPort.IsOpen)
                //{
                //    Console.WriteLine("Connecting");
                //}

                Console.WriteLine("Port " + portName + " opened successfully");

                isPortReady = true;

                Console.WriteLine("Board Init Done");
                Console.ReadKey();
            }
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);
        }
        public void Read()
        {
            Console.WriteLine("Reading");

            //using (serialPort = new SerialPort(portName, 115200))
            //{
            //if (!serialPort.IsOpen)
            //{
            //    serialPort.Open();
            //}
            //else
            //{
            //    serialPort.Dispose();
            //    while (serialPort.IsOpen)
            //    {

            //    }
            //    serialPort.Open();
            //}

            //while (!serialPort.IsOpen)
            //{
            //    //Console.WriteLine("Connecting");
            //}
            if (isPortReady && isReading)
            {
                try
                {
                    serialReading = serialPort.ReadLine();
                    //if (serialReading.Length > 0)
                    //{
                    Console.WriteLine(serialReading);
                    //}
                }
                catch (TimeoutException) { }
            }
            //}
        }
    }


}