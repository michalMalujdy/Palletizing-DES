using TS.App.ViewModels.Common;
using TS.Infrastructure.Services;

namespace TS.App.ViewModels
{
    public class StatesNetViewModel : BaseViewModel
    {
        public string GpText => _statesNetService.StatesNet.CurrentState.Gp;
        public string Sl1Text => _statesNetService.StatesNet.CurrentState.Sl1;
        public string Sl2Text => _statesNetService.StatesNet.CurrentState.Sl2;
        public string UpText => _statesNetService.StatesNet.CurrentState.Up;
        public string SvText => _statesNetService.StatesNet.CurrentState.Sv;

        public int LargeFontSize => 55;
        public int MediumFontSize => 40;


        private readonly StatesNetService _statesNetService;

        public StatesNetViewModel(StatesNetService statesNetService)
        {
            _statesNetService = statesNetService;
        }
    }
}
