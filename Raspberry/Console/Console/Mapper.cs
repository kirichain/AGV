using GuidanceSystems;

namespace Mappers
{
    public class Mapper
    {
        public static string[,] baseLayer, beaconLayer;
        public byte mapLength, mapWidth;
        public Mapper()
        {
            mapLength = 10;
            mapWidth= 10;
            baseLayer = new string[mapLength,mapWidth];
            for (int i = 0;i < mapLength; i++)
            {
                for (int j = 0;j < mapWidth; j++)
                {
                    baseLayer[i, j] = "0";
                    Console.WriteLine("Cell data = " + baseLayer[i, j]);
                }
            }
            beaconLayer = baseLayer;
            beaconLayer[0, 0] = "Beacon-1";
            Console.WriteLine("Mapper Init Done");
        }
    }
}