using System.IO.Ports;

namespace Boards
{
    public class Board
    {
        public SerialPort serialPort1, serialPort2, serialPort3, serialPort4;
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

            serialPort1.PortName = "/dev/ttyUSB0";
            serialPort2.PortName = "/dev/ttyUSB1";
            serialPort3.PortName = "/dev/ttyUSB2";
            serialPort4.PortName = "/dev/ttyUSB3";

            serialPort1.Open();
            //serialPort2.Open();
            //serialPort3.Open();
            //serialPort4.Open();

            Console.WriteLine("Boards Init Done");
        }

        public void Reading()
        {
            Console.WriteLine("Reading");

            try
            {
                string serialReading1 = serialPort1.ReadLine();
                //string serialReading2 = serialPort2.ReadLine();
                //string serialReading3 = serialPort3.ReadLine();
                //string serialReading4 = serialPort4.ReadLine();

                Console.WriteLine(serialReading1);
                //Console.WriteLine(serialReading2);
                //Console.WriteLine(serialReading3);
                //Console.WriteLine(serialReading4);

            }
            catch (TimeoutException) { }
        }
    }


}