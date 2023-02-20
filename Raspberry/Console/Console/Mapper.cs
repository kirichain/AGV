using GuidanceSystems;

namespace Mappers
{
    public class Mapper
    {
        public static string[,] baseLayer;
        public Mapper()
        {
            baseLayer = new string[10,10];
            for (int i = 0;i < 5; i++)
            {
                for (int j = 0;j < 5; j++)
                {
                    baseLayer[i, j] = "0";
                    Console.WriteLine("Cell data = " + baseLayer[i, j]);
                }
            }
            Console.WriteLine("Mapper Init Done");
        }
    }
}