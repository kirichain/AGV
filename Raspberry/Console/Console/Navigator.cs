using GuidanceSystems;
using Boards;
using MQTTClients;
using Localizers;
using Mappers;

namespace Navigators
{
    public class Navigator
    {
        public string nav_command;
        public Navigator()
        {
            Localizer.recentCoord = Mapper.baseLayer[1, 1];

            Console.WriteLine("Navigator Init Done");
        }
        public void PathFinder()
        {
            Localizer.ScanBeacon();
            Localizer.FindNearbyBeacon();
            Console.WriteLine("Recent position = " + Localizer.recentCoord);
        }
        public void CollisionDetector()
        {

        }
        public void Move(string direction)
        {
            nav_command = direction;
            Board.SendSerial(BoardName.Motor_Controller, nav_command);
        }
        public void Nav(Mode mode)
        {
            if (mode == Mode.Direct)
            {
                //Console.WriteLine("Navigating in direct mode");
                Move(MQTTClient.controlMessage);
            }
            if (mode == Mode.Delivery)
            {
                Localizer.ScanBeacon();

                Console.WriteLine("Navigating in delivery mode");

            }
        }
    }
}