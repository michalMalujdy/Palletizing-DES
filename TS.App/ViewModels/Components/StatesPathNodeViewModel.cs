using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using TS.Core.Models;

namespace TS.App.ViewModels.Components
{
    public class StatesPathNodeViewModel
    {
        public State State { get; set; }
        public string LeavingEventId { get; set; }

        public Brush Brush { get; set; }
        public char TransitionSign => '↓';
        public Visibility TransitionVisibility { get; set; } = Visibility.Visible;
    }
}
