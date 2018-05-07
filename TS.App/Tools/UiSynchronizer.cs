using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace TS.App.Tools
{
    public static class UiSynchronizer
    {
        /// <summary>
        /// Runs passed action on the UI thread.
        /// </summary>
        public static async Task Run(Action action)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    action?.Invoke();
                });
        }
    }
}
