using GuidanceSystems;
using Mappers;

namespace Localizers
{
    public class Localizer
    {
        public static string beaconScannerData;
        public static string[] beacon, beaconData;
        public static string[,] beaconName, beaconRssi;
        //EN/ES/WN/WS stand for eeastern north, eastern south, western north, western south
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
                    //beaconName.Append(beaconData[0]).ToArray();
                    //beaconRssi.Append(beaconData[1]).ToArray();
                    //Console.Write("Name = " + beaconData[0] + " - RSSI = " + beaconData[1]);

                    //Console.WriteLine($"{b}");
                }
                //Console.WriteLine("Beacon Scanner Result = " + beaconScannerData);
                Console.WriteLine("Filtered beacon scanner result. Start to build realtime map now");
            }
        }
        public bool isBeaconExisting(string beaconName)
        {
            if (beaconName == "beaconNameList")
            {
                return true;
            }
            else
                return false;
        }
        public static void FindNearbyBeacon()
        {
            //Check western south cell
            if ((Mapper.baseLayer[recentX - 1, recentY + 1] != null) && (Mapper.baseLayer[recentX - 1, recentY + 1] != ""))
            {
                if (Mapper.baseLayer[recentX - 1, recentY + 1] == "*")
                {
                    if (Mapper.beaconIdLayer[recentX - 1, recentY + 1] == beaconName[recentX - 1, recentY + 1])
                    {
                        Console.WriteLine("Western South Beacon Found");
                    }
                }
            }
            //Check western north cell
            if ((Mapper.baseLayer[recentX - 1, recentY - 1] != null) && (Mapper.baseLayer[recentX - 1, recentY - 1] != ""))
            {
                if (Mapper.baseLayer[recentX - 1, recentY - 1] == "*")
                {
                    if (Mapper.beaconIdLayer[recentX - 1, recentY - 1] == beaconName[recentX - 1, recentY - 1])
                    {
                        Console.WriteLine("Western North Beacon Found");
                    }
                }
            }
            //Check estern north cell
            if ((Mapper.baseLayer[recentX + 1, recentY - 1] != null) && (Mapper.baseLayer[recentX + 1, recentY - 1] != ""))
            {
                if (Mapper.baseLayer[recentX + 1, recentY - 1] == "*")
                {
                    if (Mapper.beaconIdLayer[recentX + 1, recentY - 1] == beaconName[recentX + 1, recentY - 1])
                    {
                        Console.WriteLine("Eastern North Beacon Found");
                    }
                }
            }
            //Check eastern south cell
            if ((Mapper.baseLayer[recentX + 1, recentY + 1] != null) && (Mapper.baseLayer[recentX + 1, recentY + 1] != ""))
            {
                if (Mapper.baseLayer[recentX + 1, recentY + 1] == "*")
                {
                    if (Mapper.beaconIdLayer[recentX + 1, recentY + 1] == beaconName[recentX + 1, recentY + 1])
                    {
                        Console.WriteLine("Eastern South Beacon Found");
                    }
                }
            }
        }
    }
}