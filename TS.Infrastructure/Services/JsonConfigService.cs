using System.IO;
using Newtonsoft.Json;
using TS.Core.Json;

namespace TS.Infrastructure.Services
{
    public class JsonConfigService
    {
        public StatesNetJson ReadConfig(string filename)
        {
            var json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<StatesNetJson>(json);
        }
    }
}
