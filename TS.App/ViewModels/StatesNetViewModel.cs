using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using AutoMapper;
using GalaSoft.MvvmLight.Command;
using TS.App.ViewModels.Common;
using TS.App.ViewModels.Components;
using TS.Common.Extensions;
using TS.Core.Enums;
using TS.Core.Models;
using TS.Infrastructure.Services;

namespace TS.App.ViewModels
{
    public class StatesNetViewModel : BaseViewModel
    {
        public string GpText => _statesNetService.StatesNet?.CurrentState?.Gp ?? "none";
        public string Sl1Text => _statesNetService.StatesNet?.CurrentState?.Sl1 ?? "none";
        public string Sl2Text => _statesNetService.StatesNet?.CurrentState?.Sl2 ?? "none";
        public string UpText => _statesNetService.StatesNet?.CurrentState?.Up ?? "none";
        public string SvText => _statesNetService.StatesNet?.CurrentState?.Sv ?? "none";

        public string PreviousState => _statesNetService.StatesNet?.PreviousStateId ?? "none";
        public string RecentEvent => _statesNetService.StatesNet?.PreviousEventId ?? "none";

        public int LargeFontSize => 50;
        public int MediumFontSize => 32;
        public int SmallFontSize => 28;
        public int TinyFontSize => 18;
        public int MicroFontSize => 11;

        public string ChosenEventId { get; set; }
        private string _chosenStartStateId;

        public string ChosenStartStateId
        {
            get => _chosenStartStateId;
            set
            {
                _chosenStartStateId = value;
                RaisePropertyChanged(nameof(AllFinishStates));
            }
        }
        public string ChosenFinishStateId { get; set; }
        public ICollection<StatesPathNodeViewModel> FoundPath { get; set; }

        public ICollection<string> AvailableEvents =>
            _statesNetService.StatesNet?.CurrentState?.AvaliableStatesIds?.Keys;

        public ICollection<string> AllStates =>
            _statesNetService.StatesNet?.AllStates?.Select(s => s.Id).ToList();

        public ICollection<string> AllFinishStates
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ChosenStartStateId))
                {
                    return new List<string>();
                }

                return AllStates.Where(id => !id.Equals(ChosenStartStateId)).ToList();
            }
        }

        public StatesPathStatus BlockingStatus { get; set; }
        public Brush BlockingTextForeground
        {
            get
            {
                switch (BlockingStatus)
                {
                    case StatesPathStatus.Undefined:
                        return new SolidColorBrush(Colors.Black);

                    case StatesPathStatus.Successful:
                        return new SolidColorBrush(Colors.Green);

                    case StatesPathStatus.Unsuccessful:
                        return new SolidColorBrush(Colors.DimGray);

                    case StatesPathStatus.Blocked:
                        return new SolidColorBrush(Colors.Red);

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public ICommand SubmitEventCommand { get; set; }
        public ICommand LoadConfigCommand { get; set; }
        public ICommand PathBetweenStatesCommand { get; set; }
        public ICommand CheckBlockingCommand { get; set; }

        private readonly StatesNetService _statesNetService;
        private readonly JsonConfigService _jsonConfigService;

        public StatesNetViewModel(StatesNetService statesNetService, JsonConfigService jsonConfigService)
        {
            _statesNetService = statesNetService;
            _jsonConfigService = jsonConfigService;

            SubmitEventCommand = new RelayCommand(SubmitEventButtonClicked);
            LoadConfigCommand = new RelayCommand(() => OnLoadConfigClicked());
            PathBetweenStatesCommand = new RelayCommand(FindPathBetweenStatesClicked);
            CheckBlockingCommand = new RelayCommand(CheckBlockingClicked);
        }

        public void RefreshAll()
        {
            RaisePropertyChanged(nameof(GpText));
            RaisePropertyChanged(nameof(Sl1Text));
            RaisePropertyChanged(nameof(Sl2Text));
            RaisePropertyChanged(nameof(UpText));
            RaisePropertyChanged(nameof(SvText));
            RaisePropertyChanged(nameof(AvailableEvents));
            RaisePropertyChanged(nameof(AllStates));
            RaisePropertyChanged(nameof(AllFinishStates));
            RaisePropertyChanged(nameof(PreviousState));
            RaisePropertyChanged(nameof(RecentEvent));
            RaisePropertyChanged(nameof(BlockingStatus));
            RaisePropertyChanged(nameof(BlockingTextForeground));
        }

        private void SubmitEventButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(ChosenEventId))
            {
                //TODO: might show an error that no event is selected
                return;
            }

            _statesNetService.AriseEvent(ChosenEventId);
            RefreshAll();
        }

        private async Task OnLoadConfigClicked()
        {
            var statesNetJson = await _jsonConfigService.ReadConfigFile();
            _statesNetService.Initialize(statesNetJson);

            RefreshAll();
        }

        private void FindPathBetweenStatesClicked()
        {
            if (string.IsNullOrWhiteSpace(ChosenStartStateId) || string.IsNullOrWhiteSpace(ChosenFinishStateId))
            {
                return;
            }

            var foundPath = _statesNetService.FindPath(ChosenStartStateId, ChosenFinishStateId)?.StatesPathCollection;
            var foundPathViewModel = Mapper.Map<List<StatesPathNode>, List<StatesPathNodeViewModel>>(foundPath);
            var statesPathViewModel = new StatesPathViewModel(foundPathViewModel);
            FoundPath = statesPathViewModel.StatesPathCollection;

            RaisePropertyChanged(nameof(FoundPath));
        }

        private void CheckBlockingClicked()
        {
            if (_statesNetService.StatesNet == null)
            {
                return;
            }

            var blockedPath = _statesNetService.CheckBlocking();

            BlockingStatus = blockedPath == null
                ? BlockingStatus = StatesPathStatus.Successful: StatesPathStatus.Blocked;

            RaisePropertyChanged(nameof(BlockingStatus));
            RaisePropertyChanged(nameof(BlockingTextForeground));
        }
    }
}
