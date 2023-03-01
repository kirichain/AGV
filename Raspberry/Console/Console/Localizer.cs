using GuidanceSystems;
using Mappers;

namespace Localizers
{
    public enum Direction
    {
        North,
        East,
        South,
        West,
        Northeast,
        Southeast,
        Northwest,
        Southwest
    }
    public class Localizer
    {
        public static string beaconScannerData;
        public static string[] beacon, beaconData;
        public static string[] beaconName;
        public static double[] beaconRssi;
        //NE/NW/SE/SW stand for northeast, southeast, northwest, southwest
        public static int neBeaconRssi, seBeaconRssi, nwBeaconRssi, swBeaconRssi, recentX, recentY;
        public static string enBeaconName, esBeaconName, wnBeaconName, wsBeaconName;
        public static double neDistance, seDistance, nwDistance, swDistance;
        public Localizer()
        {
            beaconName = new string[6];
            beaconRssi = new double[6];
            recentX = 1;
            recentY = 1;
            Console.WriteLine("Localizer Init Done");
        }
        public static void GetClosestDirection()
        {

        }
        public static bool IsLocationInside()
        {
            return false;
        }
        public static bool IsLocationCenter()
        {
            return false;
        }
        public static void CalculateDistance(double rssi)
        {
            double A = -60; // reference signal strength at 1 meter
            double n = 2;   // path loss exponent
            double RSSI = rssi; // received signal strength

            double distance = Math.Pow(10, (RSSI - A) / (10 * n));

            Console.WriteLine($"Distance: {distance} meters");          
        }
        public static void ScanBeacon()
        {
            int i = 0;
            if ((GuidanceSystem.beaconScannerReading != "") && (GuidanceSystem.beaconScannerReading != null))
            {
                beaconScannerData = GuidanceSystem.beaconScannerReading.Trim('&', '*');
                beacon = beaconScannerData.Split('#');
                foreach (var b in beacon)
                {
                    beaconData = b.Split('=');
                    beaconName[i] = beaconData[0].Trim();
                    beaconRssi[i] = Int32.Parse(beaconData[1]);
                    Console.WriteLine("Name = " + beaconName[i] + " - RSSI = " + beaconRssi[i]);
                    //Console.WriteLine($"{b}");
                    i++;
                }
                //Console.WriteLine("Beacon Scanner Result = " + beaconScannerData);
                //Console.WriteLine("Filtered beacon scanner result. Start to build realtime map now");
            }

        }
        public static bool IsBeaconExisting(string _beaconName)
        {
            Console.WriteLine("Checking existing " + _beaconName);
            if (beaconName.Contains(_beaconName))
            {
                //Console.WriteLine("Contained");
                int index = Array.IndexOf(beaconName, _beaconName);
                if (index != -1)
                {
                    if (beaconRssi[index] != 0)
                    {
                        //CalculateDistance(beaconRssi[index]);
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Not contained");
            }
            return false;
        }
        public static void FindNearbyBeacon()
        {
            //Check western south cell
            if ((Mapper.baseLayer[recentX - 1, recentY + 1] != null) && (Mapper.baseLayer[recentX - 1, recentY + 1] != ""))
            {
                if (Mapper.baseLayer[recentX - 1, recentY + 1] == "*")
                {
                    Console.WriteLine("Checking " + Mapper.beaconIdLayer[recentX - 1, recentY + 1]);
                    if (IsBeaconExisting(Mapper.beaconIdLayer[recentX - 1, recentY + 1]))
                    {
                        Console.WriteLine("Western South Beacon Found " + Mapper.beaconIdLayer[recentX - 1, recentY + 1]);
                    }
                }
            }
            //Check western north cell
            if ((Mapper.baseLayer[recentX - 1, recentY - 1] != null) && (Mapper.baseLayer[recentX - 1, recentY - 1] != ""))
            {
                if (Mapper.baseLayer[recentX - 1, recentY - 1] == "*")
                {
                    Console.WriteLine("Checking " + Mapper.beaconIdLayer[recentX - 1, recentY - 1]);
                    if (IsBeaconExisting(Mapper.beaconIdLayer[recentX - 1, recentY - 1]))
                    {
                        Console.WriteLine("Western North Beacon Found " + Mapper.beaconIdLayer[recentX - 1, recentY - 1]);
                    }
                }
            }
            //Check estern north cell
            if ((Mapper.baseLayer[recentX + 1, recentY - 1] != null) && (Mapper.baseLayer[recentX + 1, recentY - 1] != ""))
            {
                if (Mapper.baseLayer[recentX + 1, recentY - 1] == "*")
                {
                    Console.WriteLine("Checking " + Mapper.beaconIdLayer[recentX + 1, recentY - 1]);
                    if (IsBeaconExisting(Mapper.beaconIdLayer[recentX + 1, recentY - 1]))
                    {
                        Console.WriteLine("Eastern North Beacon Found " + Mapper.beaconIdLayer[recentX + 1, recentY - 1]);
                    }
                }
            }
            //Check eastern south cell
            if ((Mapper.baseLayer[recentX + 1, recentY + 1] != null) && (Mapper.baseLayer[recentX + 1, recentY + 1] != ""))
            {
                if (Mapper.baseLayer[recentX + 1, recentY + 1] == "*")
                {
                    Console.WriteLine("Checking " + Mapper.beaconIdLayer[recentX + 1, recentY + 1]);
                    if (IsBeaconExisting(Mapper.beaconIdLayer[recentX + 1, recentY + 1]))
                    {
                        Console.WriteLine("Eastern South Beacon Found " + Mapper.beaconIdLayer[recentX + 1, recentY + 1]);
                    }
                }
            }
        }
    }
}