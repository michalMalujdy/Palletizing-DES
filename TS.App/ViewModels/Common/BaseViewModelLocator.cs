using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace TS.App.ViewModels.Common
{
    public class BaseViewModelLocator
    {
        public StatesNetViewModel StatesNetViewModel =>
            ServiceLocator.Current.GetInstance<StatesNetViewModel>();

        public BaseViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<StatesNetViewModel>();
        }
    }
}
