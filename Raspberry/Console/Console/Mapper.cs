using GuidanceSystems;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mappers
{
    public enum LayerName
    {
        BaseLayer,
        BeaconLayer,
        PackageLayer
    }
    public enum CellType
    {
        Blank,
        Beacon,
        AGV,
        RFID,
        Package
    }
    public class Mapper
    {
        public static string[,] baseLayer, beaconLayer, packageLayer;
        public static byte mapLength, mapWidth;
        public static string layerData;
        public Mapper()
        {
            mapLength = 10;
            mapWidth = 10;
            baseLayer = new string[mapLength, mapWidth];
            for (int i = 0; i < mapLength; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    baseLayer[i, j] = "0";
                    Console.WriteLine("Cell data = " + baseLayer[i, j]);
                }
            }
            beaconLayer = baseLayer;
            beaconLayer[0, 0] = "Beacon-1";
            Console.WriteLine("Mapper Init Done");
        }
        public static void LoadMap(string name, LayerName layer)
        {
            if (layer == LayerName.BaseLayer)
            {
                Console.WriteLine("Loading base layer");
            }

            layerData = @"{""name"":""Warehouse 1"", ""layer"":""base"", ""length"":12, ""width"":10, ""coordinates"":[{""x"":""0"", ""y"":""0"", ""type"":""0""},{""x"":""1"", ""y"":""0"", ""type"":""#""},{""x"":""2"", ""y"":""0"", ""type"":""1""},{""x"":""3"", ""y"":""0"", ""type"":""1""},{""x"":""0"", ""y"":""1"", ""type"":""0""},{""x"":""1"", ""y"":""1"", ""type"":""*""},{""x"":""2"", ""y"":""1"", ""type"":""0""},{""x"":""3"", ""y"":""1"", ""type"":""0""},{""x"":""0"", ""y"":""2"", ""type"":""1""},{""x"":""1"", ""y"":""2"", ""type"":""1""},{""x"":""2"", ""y"":""2"", ""type"":""0""},{""x"":""3"", ""y"":""2"", ""type"":""0""},{""x"":""0"", ""y"":""3"", ""type"":""0""},{""x"":""1"", ""y"":""3"", ""type"":""*""},{""x"":""2"", ""y"":""3"", ""type"":""#""},{""x"":""3"", ""y"":""3"", ""type"":""#""}]}";
            InitMap(baseLayer);
        }
        public static void InitMap(string[,] layer)
        {
            JsonNode document = JsonNode.Parse(layerData)!;

            JsonNode root = document.Root;

            JsonArray coords = root["coordinates"]!.AsArray();

            foreach (JsonNode? coord in coords)
            {
                var x = Convert.ToByte(coord["x"].ToString());
                var y = Convert.ToByte(coord["y"].ToString());
                layer[x, y] = coord["type"].ToString();
                //Console.WriteLine("Cell type = " + coord["type"] + " coord = " + x + " " + y);
            }
        }
    }
}