using AutoMapper;
using TS.Core.Json;
using TS.Core.Models;

namespace TS.Infrastructure.AppStart
{
    public static class AutomapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<StateJson, State>();
                config.CreateMap<StatesNetJson, StatesNet>();
                config.CreateMap<StatesPath, StatesPath>();
                config.CreateMap<StatesNet, StatesNet>();
            });
        }
    }
}
