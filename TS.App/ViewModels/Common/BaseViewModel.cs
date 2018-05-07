using GalaSoft.MvvmLight;
using TS.App.Tools;

namespace TS.App.ViewModels.Common
{
    public class BaseViewModel : ViewModelBase
    {
        public override async void RaisePropertyChanged(string propertyName = null)
        {
            if (propertyName != null)
            {
                await UiSynchronizer.Run(() =>
                    base.RaisePropertyChanged(propertyName));
            }
        }
    }
}
