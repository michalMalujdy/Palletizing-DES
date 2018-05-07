using TS.Infrastructure.Services;

namespace TS.App.ViewModels
{
    public class StatesNetViewModel
    {
        private readonly StatesNetService _statesNetService;

        public StatesNetViewModel(StatesNetService statesNetService)
        {
            _statesNetService = statesNetService;
        }
    }
}
