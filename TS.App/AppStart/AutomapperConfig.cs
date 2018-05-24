using AutoMapper;
using TS.App.ViewModels.Components;
using TS.Core.Json;
using TS.Core.Models;

namespace TS.App.AppStart
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
                config.CreateMap<StatesPathNode, StatesPathNodeViewModel>();
            });
        }
    }
}
