using System;
using System.IO.Ports;
using System.Threading;

namespace Boards
{
    public class Board
    {
        static SerialPort serialPort;
        public bool isPortReady, isReading, isDisconnected;
        //LF character used for determining if data from serial port reading contains break line character 
        static char LF = (char)10;
        static bool isNewReading;
        public static string portName, serialReading, buffer, boardName;

        public Board(string _portName)
        {
            portName = _portName;
            isPortReady = false;
            isReading = false;
            isDisconnected = false;
            serialReading = "";
            isNewReading = false;
        }
        public void Init()
        {
            Console.WriteLine("Checking boards");

            using (serialPort = new SerialPort(portName, 115200))
            {
                //serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                while (!serialPort.IsOpen)
                {
                    Console.WriteLine("Connecting to serial port " + portName);
                }

                Console.WriteLine("Port " + portName + " opened successfully");
                isPortReady = true;
                Console.WriteLine("Board Init Done");
                //Console.ReadKey();
            }
        }
        public void checkBoardName()
        {

        }
        public void ReadSerial()
        {
            using (serialPort = new SerialPort(portName, 115200))
            {
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                Console.WriteLine("Reading config done");
                Console.ReadKey();
            }
        }
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            foreach (char c in indata)
            {
                if (isNewReading)
                {
                    if (c == '*')
                    {
                        serialReading = buffer;
                        if ((serialReading != "") && (serialReading != " "))
                        {
                            Console.WriteLine(serialReading);
                        }
                        buffer = "";
                        isNewReading = false;
                        break;
                    }
                    else if ((c != '&') && (c != LF))
                    {
                        buffer += c;
                    }
                }
                else if (c == '&')
                {
                    isNewReading = true;
                }
            }
            //Console.WriteLine("Data Received:");
        }
    }
}