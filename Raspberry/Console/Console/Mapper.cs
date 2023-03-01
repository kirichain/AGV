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
        PackageId,
        Rfid,
        RfidData
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
            mapLength = 4;
            mapWidth = 4;
            baseLayer = new string[mapLength, mapWidth];
            beaconIdLayer = new string[mapLength, mapWidth];
            for (int i = 0; i < mapLength; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    baseLayer[i, j] = "0";
                    beaconIdLayer[i, j] = "0";
                }
            }
        }
        public static void LoadMap(string name, LayerName layer)
        {
            baseLayerData = @"{""name"":""Warehouse 1"", ""layer"":""base"", ""length"":4, ""width"":4, ""coordinates"":[{""x"":""0"", ""y"":""0"", ""type"":""*""},{""x"":""1"", ""y"":""0"", ""type"":""#""},{""x"":""2"", ""y"":""0"", ""type"":""*""},{""x"":""3"", ""y"":""0"", ""type"":""1""},{""x"":""0"", ""y"":""1"", ""type"":""0""},{""x"":""1"", ""y"":""1"", ""type"":""#""},{""x"":""2"", ""y"":""1"", ""type"":""0""},{""x"":""3"", ""y"":""1"", ""type"":""0""},{""x"":""0"", ""y"":""2"", ""type"":""*""},{""x"":""1"", ""y"":""2"", ""type"":""1""},{""x"":""2"", ""y"":""2"", ""type"":""*""},{""x"":""3"", ""y"":""2"", ""type"":""0""},{""x"":""0"", ""y"":""3"", ""type"":""0""},{""x"":""1"", ""y"":""3"", ""type"":""*""},{""x"":""2"", ""y"":""3"", ""type"":""#""},{""x"":""3"", ""y"":""3"", ""type"":""#""}]}";

            beaconIdLayerData = @"{""name"":""Warehouse 1"", ""layer"":""beaconId"", ""length"":4, ""width"":4, ""coordinates"":[{""x"":""0"", ""y"":""0"", ""name"":""Beacon-6""},{""x"":""1"",""y"":""0"", ""name"":""""},{""x"":""2"", ""y"":""0"", ""name"":""Beacon-2""},{""x"":""3"", ""y"":""0"", ""name"":""""},{""x"":""0"", ""y"":""1"", ""name"":""""},{""x"":""1"", ""y"":""1"", ""name"":""""},{""x"":""2"", ""y"":""1"", ""name"":""""},{""x"":""3"", ""y"":""1"", ""name"":""""},{""x"":""0"", ""y"":""2"", ""name"":""Beacon-3""},{""x"":""1"", ""y"":""2"", ""name"":""""},{""x"":""2"", ""y"":""2"", ""name"":""Beacon-4""},{""x"":""3"", ""y"":""2"", ""name"":""""},{""x"":""0"", ""y"":""3"", ""name"":""""},{""x"":""1"", ""y"":""3"", ""name"":""""},{""x"":""2"", ""y"":""3"", ""name"":""""},{""x"":""3"", ""y"":""3"", ""name"":""""}]}";

            switch (layer)
            {
                case LayerName.Base:
                    InitMap(baseLayerData, LayerName.Base);
                    break;
                case LayerName.BeaconId:
                    InitMap(beaconIdLayerData, LayerName.BeaconId);
                    break;
                default:
                    break;
            }         
        }
        public static void InitMap(string layerData, LayerName layerName)
        {
            JsonNode document = JsonNode.Parse(layerData)!;

            JsonNode root = document.Root;

            JsonArray coords = root["coordinates"]!.AsArray();

            foreach (JsonNode? coord in coords)
            {
                int x = Convert.ToInt32(coord["x"].ToString());
                int y = Convert.ToInt32(coord["y"].ToString());

                if (layerName == LayerName.Base)
                {
                    baseLayer[x, y] = coord["type"].ToString();

                }
                else if (layerName == LayerName.BeaconId)
                {
                    beaconIdLayer[x, y] = coord["name"].ToString();
                }
                else if (layerName == LayerName.BeaconData)
                {
                    //layer[x, y] = coord["data"].ToString();
                }
                //Console.WriteLine("Cell type = " + coord["type"] + " coord = " + x + " " + y);
            }
        }
    }
}