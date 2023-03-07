using GuidanceSystems;
using Boards;
using MQTTClients;
using Localizers;
using Mappers;

namespace Navigators
{
    public class Navigator
    {
        public string nav_command, nav_route;
        public int deliveryX, deliveryY, pickingX, pickingY, baseX, baseY;
        public bool isPathPlanned, isRoutePlanned, isDeliveryDone;
        public Navigator()
        {
            baseX = 0;
            baseY = 0;
            Console.WriteLine("Navigator Init Done");
        }
        public void PlanPath()
        {
            Mapper.LoadMap("Warehouse 1", LayerName.Base);
            Mapper.LoadMap("Warehouse 1", LayerName.BeaconId);
            if ((baseX == 1 ) && (baseY == 1))
            {
                //Console.WriteLine("Delivery request receiving position is root");
            } else
            {
                //Console.WriteLine("Delivery request receiving position is not root");
            }
            Localizer.ScanBeacon();
            Localizer.FindNearbyBeacon();
            //Check if the AGV is inside and center the cell
            Localizer.CheckLocation();

            //If checking step is done, then create a new path or continue the planned one
            if (Localizer.isCheckLocationDone)
            {
                Console.WriteLine("AGV is center the cell. Start navigating now");
            } else
            {
                if (Localizer.isLocationInside)
                {
                    Console.WriteLine("AGV is not in center of the cell. Start calibrating now");
                } else
                {
                    Console.WriteLine("Location check done but AGV position is not correct. End navigating");
                }
            }
        }
        public void DetectObstacle()
        {

        }
        public void DetectHuman()
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