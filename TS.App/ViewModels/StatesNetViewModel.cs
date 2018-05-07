using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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
        public int SmallFontSize => 30;

        public ICommand SubmitEventCommand { get; set; }

        private string _chosenEventId;
        public string ChosenEventId
        {
            get => _chosenEventId;

            set
            {
                _chosenEventId = value;
                RaisePropertyChanged(nameof(ChosenEventId));
            }
        }

        public ICollection<string> AvailableEvents => _statesNetService.StatesNet.CurrentState.AvaliableStatesIds.Keys;

        private readonly StatesNetService _statesNetService;

        public StatesNetViewModel(StatesNetService statesNetService)
        {
            _statesNetService = statesNetService;

            SubmitEventCommand = new RelayCommand(SubmitEventButtonClicked);

            ChosenEventId = AvailableEvents.FirstOrDefault();
        }

        public void Refresh()
        {
            RaisePropertyChanged(nameof(GpText));
            RaisePropertyChanged(nameof(Sl1Text));
            RaisePropertyChanged(nameof(Sl2Text));
            RaisePropertyChanged(nameof(UpText));
            RaisePropertyChanged(nameof(SvText));

            ChosenEventId = AvailableEvents.FirstOrDefault();
        }

        private void SubmitEventButtonClicked()
        {
            Refresh();
        }
    }
}
