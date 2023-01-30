using System;
using System.IO.Ports;
using System.Threading;

namespace Boards
{
    public class Board
    {
        public SerialPort serialPort1, serialPort2, serialPort3, serialPort4;
        public bool isPort1Ready;
        public void Init()
        {
            serialPort1 = new SerialPort();
            serialPort2 = new SerialPort();
            serialPort3 = new SerialPort();
            serialPort4 = new SerialPort();

            serialPort1.BaudRate = 115200;
            serialPort2.BaudRate = 115200;
            serialPort3.BaudRate = 115200;
            serialPort4.BaudRate = 115200;

            //serialPort1.PortName = "COM4";
            serialPort1.PortName = "/dev/ttyUSB0";
            serialPort2.PortName = "/dev/ttyUSB1";
            serialPort3.PortName = "/dev/ttyUSB2";
            serialPort4.PortName = "/dev/ttyUSB3";
            //using (serialPort1)
            //{
            Console.WriteLine("Checking boards");

            if (!serialPort1.IsOpen | serialPort1 == null)
            {
                serialPort1.Open();
                Console.WriteLine("Port 1 opened successfully");
            }
            //else
            //{
            //    serialPort1.Close();
            //    while (serialPort1.IsOpen)
            //    {
            //        Console.WriteLine("port 1 not close successfully");
            //    }
            //    Console.WriteLine("port 1 closed successfully");
            //    serialPort1.Open();
            //    while (!serialPort1.IsOpen)
            //    {
            //        Console.WriteLine("port 1 not open successfully");
            //    }
            //    Console.WriteLine("port 1 opened successfully");
            //}

            //if (!serialPort2.IsOpen)
            //{
            //    serialPort2.Open();
            //}
            //else
            //{
            //    while (serialPort2.IsOpen)
            //    {
            //        serialPort2.Close();
            //    }
            //    serialPort2.Open();
            //}

            //if (!serialPort3.IsOpen)
            //{
            //    serialPort3.Open();
            //}
            //else
            //{
            //    while (serialPort3.IsOpen)
            //    {
            //        serialPort3.Close();
            //    }
            //    serialPort3.Open();
            //}

            //if (!serialPort4.IsOpen)
            //{
            //    serialPort4.Open();
            //}
            //else
            //{
            //    while (serialPort4.IsOpen)
            //    {
            //        serialPort4.Close();
            //    }
            //    serialPort4.Open();
            //}
            isPort1Ready = true;
            Console.WriteLine("Boards Init Done");
            //}
        }

        public void Disconnect()
        {
            if (serialPort1.IsOpen) { serialPort1.Close(); }
            if (serialPort2.IsOpen) { serialPort2.Close(); }
            if (serialPort3.IsOpen) { serialPort3.Close(); }
            if (serialPort4.IsOpen) { serialPort4.Close(); }
        }

        public void Read()
        {
            //Console.WriteLine("Reading");

            try
            {
                string serialReading1 = serialPort1.ReadLine();
                //string serialReading2 = serialPort2.ReadLine();
                //string serialReading3 = serialPort3.ReadLine();
                //string serialReading4 = serialPort4.ReadLine();

                if (serialReading1.Length > 0)
                {
                    Console.WriteLine(serialReading1);
                }
                //Console.WriteLine(serialReading2);
                //Console.WriteLine(serialReading3);
                //Console.WriteLine(serialReading4);

            }
            catch (TimeoutException) { }
        }
    }


}