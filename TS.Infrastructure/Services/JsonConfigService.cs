using System;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using TS.Core.Json;

namespace TS.Infrastructure.Services
{
    public class JsonConfigService
    {
        public async Task<StatesNetJson> ReadConfigFile()
        {
            var picker = 
                new Windows.Storage.Pickers.FileOpenPicker
                {
                    ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                    SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary,
                };

            picker.FileTypeFilter.Add(".txt");
            var file = await picker.PickSingleFileAsync();

            if (file == null)
            {
                return null;
            }

            var json = await FileIO.ReadTextAsync(file);
            return JsonConvert.DeserializeObject<StatesNetJson>(json);
        }

        private string _sample = @"{
           ""StartStateId"":""GP_I,SL1_S,SL2_S,UP_I,SV_S"",
           ""AllStates"":
           [
              {
                 ""Gp"":""GP_I"",
                 ""Sl1"":""SL1_S"",
                 ""Sl2"":""SL2_S"",
                 ""Up"":""UP_I"",
                 ""Sv"":""SV_S"",
                 ""AvaliableStatesIds"":{
                    ""PalletAvalible"":""GP_I,SL1_S,SL2_S,UP_I,SV_S"",
                    ""ProductionStart"":""GP_I2,SL1_S2,SL2_S2,UP_I2,SV_S2""
                 }
              },
              {
                 ""Gp"":""GP_I2"",
                 ""Sl1"":""SL1_S2"",
                 ""Sl2"":""SL2_S2"",
                 ""Up"":""UP_I2"",
                 ""Sv"":""SV_S2"",
                 ""AvaliableStatesIds"":{
                    ""ProductionStart"":""GP_I,SL1_S,SL2_S,UP_I,SV_S""
                 }
              }
           ]
        }";
    }
}
