﻿using System.Collections.Generic;
using System.Linq;

namespace TS.Core.Models
{
    public class State
    {
        public string Gp { get; set; }
        public string Sl1 { get; set; }
        public string Sl2 { get; set; }
        public string Up { get; set; }
        public string Sv { get; set; }

        public string Id => $"{Gp},{Sl1},{Sl2},{Up},{Sv}";

        public Dictionary<string, string> AvaliableStatesIds { get; set; }
        public List<string> AvailableEvents => AvaliableStatesIds.Keys.ToList();

        public State()
        {
            AvaliableStatesIds = new Dictionary<string, string>();
        }
    }
}
