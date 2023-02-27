using GuidanceSystems;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mappers
{
    public enum LayerName
    {
        Base,
        BeaconId,
        BeaconData,
        PackageId
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
        public static string[,] baseLayer, beaconIdLayer, packageIdLayer;
        public static byte mapLength, mapWidth;
        public static string baseLayerData, beaconIdLayerData;
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
            beaconIdLayer = baseLayer;
            //beaconIdLayer[0, 0] = "Beacon-1";
            Console.WriteLine("Mapper Init Done");
        }
        public static void LoadMap(string name, LayerName layer)
        {
            if (layer == LayerName.Base)
            {
                Console.WriteLine("Loading base layer");
            }

            baseLayerData = @"{""name"":""Warehouse 1"", ""layer"":""base"", ""length"":12, ""width"":10, ""coordinates"":[{""x"":""0"", ""y"":""0"", ""type"":""0""},{""x"":""1"", ""y"":""0"", ""type"":""#""},{""x"":""2"", ""y"":""0"", ""type"":""1""},{""x"":""3"", ""y"":""0"", ""type"":""1""},{""x"":""0"", ""y"":""1"", ""type"":""0""},{""x"":""1"", ""y"":""1"", ""type"":""*""},{""x"":""2"", ""y"":""1"", ""type"":""0""},{""x"":""3"", ""y"":""1"", ""type"":""0""},{""x"":""0"", ""y"":""2"", ""type"":""1""},{""x"":""1"", ""y"":""2"", ""type"":""1""},{""x"":""2"", ""y"":""2"", ""type"":""0""},{""x"":""3"", ""y"":""2"", ""type"":""0""},{""x"":""0"", ""y"":""3"", ""type"":""0""},{""x"":""1"", ""y"":""3"", ""type"":""*""},{""x"":""2"", ""y"":""3"", ""type"":""#""},{""x"":""3"", ""y"":""3"", ""type"":""#""}]}";

            beaconIdLayerData = @"{""name"":""Warehouse 1"", ""layer"":""beaconId"", ""length"":12, ""width"":10, ""coordinates"":[{""x"":""0"", ""y"":""0"", ""name"":""Beacon-1""},{""x"":""1"", ""name"":""""},{""x"":""2"", ""y"":""0"", ""name"":""Beacon-2""},{""x"":""3"", ""y"":""0"", ""name"":""""},{""x"":""0"", ""y"":""1"", ""name"":""""},{""x"":""1"", ""y"":""1"", ""name"":""""},{""x"":""2"", ""y"":""1"", ""name"":""""},{""x"":""3"", ""y"":""1"", ""name"":""""},{""x"":""0"", ""y"":""2"", ""name"":""Beacon-3""},{""x"":""1"", ""y"":""2"", ""name"":""""},{""x"":""2"", ""y"":""2"", ""name"":""Beacon-4""},{""x"":""3"", ""y"":""2"", ""name"":""""},{""x"":""0"", ""y"":""3"", ""name"":""""},{""x"":""1"", ""y"":""3"", ""name"":""""},{""x"":""2"", ""y"":""3"", ""name"":""""},{""x"":""3"", ""y"":""3"", ""name"":""""}]}";

            InitMap(baseLayer, baseLayerData, LayerName.Base);
            InitMap(beaconIdLayer, beaconIdLayerData, LayerName.BeaconId);
        }
        public static void InitMap(string[,] layer, string layerData, LayerName layerName)
        {
            JsonNode document = JsonNode.Parse(layerData)!;

            JsonNode root = document.Root;

            JsonArray coords = root["coordinates"]!.AsArray();

            foreach (JsonNode? coord in coords)
            {
                var x = Convert.ToByte(coord["x"].ToString());
                var y = Convert.ToByte(coord["y"].ToString());

                if (layerName == LayerName.Base)
                {
                    layer[x, y] = coord["type"].ToString();
                }
                else if (layerName == LayerName.BeaconId)
                {
                    layer[x, y] = coord["name"].ToString();
                }
                else if (layerName == LayerName.BeaconData)
                {
                    layer[x, y] = coord["data"].ToString();
                }
                //Console.WriteLine("Cell type = " + coord["type"] + " coord = " + x + " " + y);
            }
        }
    }
}