using Localizers;
using Mappers;
using Navigators;

namespace GuidanceSystems
{
    public enum Mode
    {
        Direct,
        Idle,
        Delivery
    }
    public class GuidanceSystem
    {
        public Localizer localizer;
        public Mapper mapper;
        public Navigator navigator;
        
        public Mode mode;
        public GuidanceSystem()
        {
            mode = Mode.Idle;
            localizer= new Localizer();
            mapper= new Mapper();
            navigator= new Navigator();
        }
        public void guide()
        {
            if (mode == Mode.Direct)
            {
                Console.WriteLine("Operating in direct mode");
                navigator.nav(mode);
            }
        }
    }
}