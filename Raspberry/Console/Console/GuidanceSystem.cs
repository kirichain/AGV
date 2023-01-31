using Localizers;
using Mappers;
using Navigators;

namespace GuidanceSystems
{
    public class GuidanceSystem
    {
        public Localizer localizer;
        public Mapper mapper;
        public Navigator navigator;
        public GuidanceSystem()
        {
            localizer= new Localizer();
            mapper= new Mapper();
            navigator= new Navigator();
        }
    }
}