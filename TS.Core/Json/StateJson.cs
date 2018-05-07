using System.Collections.Generic;

namespace TS.Core.Json
{
    public class StateJson
    {
        public string Gp { get; set; }
        public string Sl1 { get; set; }
        public string Sl2 { get; set; }
        public string Up { get; set; }
        public string Sv { get; set; }
        
        public Dictionary<string, string> AvaliableStatesIds { get; set; }

        public StateJson()
        {
            AvaliableStatesIds = new Dictionary<string, string>();
        }

        public string GetId()
        {
            return $"{Gp},{Sl1},{Sl2},{Up},{Sv}";
        }
    }
}
