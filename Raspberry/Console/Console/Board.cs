using System;
using System.IO.Ports;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Boards
{
    public enum SerialReceiver
    {
        Motor_Controller,
        Beacon,
        Laser_Sensor
    }
    public class Board
    {
        private SerialPort serialPort;
        public bool isPortReady, isReading, isDisconnected;
        //LF character used for determining if data from serial port reading contains break line character 
        private static char LF = (char)10;
        private bool isNewReading, isCheckingName;
        public string portName, serialReading, buffer;
        public string boardName;
        private int waitingCount;
        private static bool isSerialReady;
        public Board(string _portName)
        {
            portName = _portName;
            isPortReady = false;
            isReading = false;
            isDisconnected = false;
            serialReading = "";
            isNewReading = false;
        }
        public async Task Init()
        {
            Console.WriteLine("Checking boards");

            using (serialPort = new SerialPort(portName, 115200))
            {

                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;

                //serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                if (!serialPort.IsOpen)
                {
                    Console.WriteLine("Opening port");
                    serialPort.Open();
                }

                while (!serialPort.IsOpen)
                {
                    Console.WriteLine("Connecting to serial port " + portName);
                }

                Console.WriteLine("Port " + portName + " opened successfully");
                isPortReady = true;
                Console.WriteLine("Board Init Done");
                return;
            }
        }
        public void checkBoardName()
        {
            Console.WriteLine("Checking board name");
            waitingCount = 0;
            using (serialPort = new SerialPort(portName, 115200))
            {
                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;

                serialPort.DataReceived += new SerialDataReceivedEventHandler(BoardNameDataReceivedHandler);

                if (!serialPort.IsOpen)
                {
                    Console.WriteLine("Opening port");
                    serialPort.Open();
                }
                else
                {
                    Console.WriteLine("Port Still Opens");
                    return;
                }

                while (!serialPort.IsOpen)
                {
                    Console.WriteLine("Connecting to serial port " + portName);
                }

                Console.WriteLine("Port " + portName + " opened successfully");
                Console.WriteLine("Board Init Done");
                Console.WriteLine("Start checking board name");

                isPortReady = true;
                isCheckingName = true;
                serialPort.WriteLine("checkName");

                while (isCheckingName)
                {
                    Console.Write(".");
                    waitingCount++;
                    if (waitingCount >= 100)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Board serial port error");
                        serialPort.Close();
                        Thread.Sleep(2000);
                        return;
                    }
                }
                //serialPort.Close();
                Console.WriteLine("Checking board name done");
                return;
            }
        }
        public static void SendSerial(SerialReceiver receiver, string data)
        {
            if (receiver == SerialReceiver.Motor_Controller)
            {
                using (var serialPort = new SerialPort("COM3", 115200))
                {
                    serialPort.RtsEnable = true;
                    serialPort.DtrEnable = true;
                    serialPort.Parity = Parity.None;
                    serialPort.StopBits = StopBits.One;
                    serialPort.DataBits = 8;
                    serialPort.Handshake = Handshake.None;
                    isSerialReady = false;
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(ConfirmDataReceivedHandler);

                    if (!serialPort.IsOpen)
                    {
                        Console.WriteLine("Opening port");
                        serialPort.Open();
                    }

                    while (!serialPort.IsOpen)
                    {
                        Console.Write(".");
                    }

                    while (!isSerialReady)
                    {
                        //Console.WriteLine("|");
                    }
                    serialPort.WriteLine(data);
                    serialPort.WriteLine(data);
                    serialPort.WriteLine(data);
                    Thread.Sleep(1000);
                    serialPort.Close();
                }
                Console.ReadKey();
            }
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
                //Console.ReadKey();
            }
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
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

        private void BoardNameDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            foreach (char c in indata)
            {
                if (c != LF)
                {
                    buffer += c;
                }
                else if (c == LF)
                {
                    boardName = buffer;
                    buffer = "";
                    boardName = boardName.Trim();
                    if (boardName == "motor-controller")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Board name: " + boardName);
                        isCheckingName = false;
                        return;
                    }
                }
            }
        }
        private static void ConfirmDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            string buff = "";
            Console.WriteLine(indata);
            foreach (char c in indata)
            {
                if (!isSerialReady)
                {
                    if (c == '*')
                    {
                        buff = buff.Trim();
                        if (buff == "Ready")
                        {
                            isSerialReady = true;
                            Console.WriteLine("Serial is ready");
                            return;
                        }
                    }
                    else if (c == '&')
                    {
                        buff = "";
                    }
                    else if (c != LF)
                    {
                        buff += c;
                    }
                }
            }
        }
    }
}