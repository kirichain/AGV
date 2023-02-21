using GuidanceSystems;
using Mappers;

namespace Localizers
{
    public class Localizer
    {
        public static string beaconScannerData, recentCoord;
        public static string[] beacon;
        public static string[] beaconData;
        public static string[] beaconName;
        public static string[] beaconRSSI;
        public static int enBeaconRssi, esBeaconRssi, wnBeaconRssi, wsBeaconRssi, recentX, recentY;
        public static string enBeaconName, esBeaconName, wnBeaconName, wsBeaconName;
        public Localizer()
        {
            recentX = 1;
            recentY = 1;
            Console.WriteLine("Localizer Init Done");
        }
        public static void ScanBeacon()
        {
            if ((GuidanceSystem.beaconScannerReading != "") && (GuidanceSystem.beaconScannerReading != null))
            {
                beaconScannerData = GuidanceSystem.beaconScannerReading.Trim('&', '*');
                beacon = beaconScannerData.Split('#');
                foreach (var b in beacon)
                {
                    beaconData = b.Split('=');
                    beaconName.Append(beaconData[0]).ToArray();
                    beaconRSSI.Append(beaconData[1]).ToArray();
                    Console.Write("Name = " + beaconData[0] + " - RSSI = " + beaconData[1]);

                    //Console.WriteLine($"{b}");
                }
                //Console.WriteLine("Beacon Scanner Result = " + beaconScannerData);
                Console.WriteLine("Filtered beacon scanner result. Start to build realtime map now");
            }
        }
        public static void FindNearbyBeacon()
        {
            //Check western south cell
            if ((Mapper.baseLayer[recentX - 1, recentY + 1] != null) && (Mapper.baseLayer[recentX - 1, recentY + 1] != ""))
            {
                if (Mapper.baseLayer[recentX - 1, recentY + 1] == "*")
                {

                }
            }
            //Check western north cell
            if ((Mapper.baseLayer[recentX - 1, recentY - 1] != null) && (Mapper.baseLayer[recentX - 1, recentY - 1] != ""))
            {
                if (Mapper.baseLayer[recentX - 1, recentY - 1] == "*")
                {

                }
            }
            //Check estern north cell
            if ((Mapper.baseLayer[recentX + 1, recentY - 1] != null) && (Mapper.baseLayer[recentX + 1, recentY - 1] != ""))
            {
                if (Mapper.baseLayer[recentX + 1, recentY - 1] == "*")
                {

                }
            }
            //Check eastern south cell
            if ((Mapper.baseLayer[recentX + 1, recentY + 1] != null) && (Mapper.baseLayer[recentX + 1, recentY + 1] != ""))
            {
                if (Mapper.baseLayer[recentX + 1, recentY + 1] == "*")
                {

                }
            }
        }
    }
}