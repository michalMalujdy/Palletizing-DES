using System.Collections.Generic;
using TS.Core.Enums;

namespace TS.Core.Models
{
    public class StatesPath
    {
        public StatesPathStatus Status { get; set; }
        public List<StatesPathNode> StatesPathCollection { get; set; }

        public StatesPath()
        {
            StatesPathCollection = new List<StatesPathNode>();
        }
    }
}
