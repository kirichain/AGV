using GuidanceSystems;

namespace Localizers
{
    public class Localizer
    {
        public static string beaconScannerData;
        public static string[] beacon;
        public static string[] beaconData;
        public static string[] beaconName;
        public static string[] beaconRSSI;
        public static string recentPosition;
        public Localizer()
        {
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
    }
}