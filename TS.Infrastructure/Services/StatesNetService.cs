using System.Linq;
using AutoMapper;
using TS.Core.Models;

namespace TS.Infrastructure.Services
{
    public class StatesNetService
    {
        public StatesNet StatesNet { get; set; }

        private readonly JsonConfigService _jsonConfigService;

        public StatesNetService(JsonConfigService jsonConfigService)
        {
            _jsonConfigService = jsonConfigService;
            var statesNetJson = _jsonConfigService.ReadConfig("net_config.txt");

            StatesNet = Mapper.Map<StatesNet>(statesNetJson);
            StatesNet.CurrentState = GetById(statesNetJson.StartStateId);
        }

        public State GetById(string id)
        {
            return StatesNet.AllStates.SingleOrDefault(s => s.Id == id);
        }
    }
}
