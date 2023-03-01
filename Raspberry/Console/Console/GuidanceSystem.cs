using Localizers;
using Mappers;
using Navigators;
using MQTTClients;
using Boards;

namespace GuidanceSystems
{
    public enum Mode
    {
        Direct,
        Idle,
        Delivery
    }
    public class GuidanceSystem
    {
        public Localizer localizer;
        public Mapper mapper;
        public Navigator navigator;

        public Mode mode;
        public static string beaconScannerReading;
        public GuidanceSystem()
        {
            mode = Mode.Idle;
            localizer = new Localizer();
            mapper = new Mapper();
            navigator = new Navigator();
            beaconScannerReading = "";          
        }
        public void Guide()
        {
            beaconScannerReading = Board.beaconScannerReading;
            //Direct Mode (Manual Control)
            if (mode == Mode.Direct)
            {
                if (MQTTClients.MQTTClient.controlMessage != "")
                {
                    Console.WriteLine("Operating in direct mode");
                    navigator.nav_command = MQTTClient.controlMessage;
                    navigator.Nav(mode);
                    MQTTClients.MQTTClient.controlMessage = "";
                }
            }
            //Delivery Mode (Auto Control)
            else if (mode == Mode.Delivery)
            {
                //if (MQTTClient.controlMessage != "")
                //{
                    //Console.WriteLine("Operating in delivery mode");
                    navigator.PlanPath();
                    MQTTClient.controlMessage = "";
                //}
            }
            //Idle Mode (Waiting for signal)
            else if (mode == Mode.Idle)
            {
                Console.WriteLine("Operating in idle mode");
            }
            else Console.WriteLine("No mode provided");
        }
    }
}