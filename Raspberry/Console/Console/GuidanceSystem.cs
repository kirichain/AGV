using Localizers;
using Mappers;
using Navigators;
using MQTTClients;

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
        public GuidanceSystem()
        {
            mode = Mode.Idle;
            localizer= new Localizer();
            mapper= new Mapper();
            navigator= new Navigator();
        }
        public void guide()
        {
            //Direct Mode (Manual Control)
            if (mode == Mode.Direct)
            {
                if (MQTTClients.MQTTClient.controlMessage != "")
                {
                    Console.WriteLine("Operating in direct mode");
                    navigator.nav_command = MQTTClients.MQTTClient.controlMessage;
                    navigator.nav(mode);
                    MQTTClients.MQTTClient.controlMessage = "";
                }
            }
            //Delivery Mode (Auto Control)
            else if (mode == Mode.Delivery) 
            {
                Console.WriteLine("Operating in delivery mode");
                
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