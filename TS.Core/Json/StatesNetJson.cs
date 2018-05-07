using System.Collections.Generic;

namespace TS.Core.Json
{
    public class StatesNetJson
    {
        public string StartStateId { get; set; }
        public ICollection<StateJson> AllStates { get; set; }
    }
}
