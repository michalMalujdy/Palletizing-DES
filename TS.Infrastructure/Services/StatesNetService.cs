using System.Linq;
using AutoMapper;
using TS.Core.Json;
using TS.Core.Models;

namespace TS.Infrastructure.Services
{
    public class StatesNetService
    {
        public StatesNet StatesNet { get; set; }

        public void Initialize(StatesNetJson statesNetJson)
        {
            StatesNet = Mapper.Map<StatesNet>(statesNetJson);
            StatesNet.CurrentState = GetById(statesNetJson.StartStateId);
        }

        public State GetById(string id)
        {
            return StatesNet.AllStates.SingleOrDefault(s => s.Id == id);
        }

        public void AriseEvent(string eventId)
        {
            StatesNet.PreviousStateId = StatesNet.CurrentState.Id;
            StatesNet.PreviousEventId = eventId;

            var nextStateId = StatesNet.CurrentState.AvaliableStatesIds[eventId];
            StatesNet.CurrentState = GetById(nextStateId);
        }
    }
}
