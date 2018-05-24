using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using TS.Common.Extensions;
using TS.Core.Enums;

namespace TS.App.ViewModels.Components
{
    public class StatesPathViewModel
    {
        public StatesPathStatus Status { get; set; }
        public List<StatesPathNodeViewModel> StatesPathCollection { get; set; }

        public StatesPathViewModel()
        {
            StatesPathCollection = new List<StatesPathNodeViewModel>();
        }

        public StatesPathViewModel(List<StatesPathNodeViewModel> nodes)
        {
            if (nodes.IsNullOrEmpty())
            {
                throw new ArgumentException("Nodes cannot be null or empty", nameof(nodes));
            }

            nodes.First().Brush = new SolidColorBrush(Colors.Green);
            nodes.Last().Brush = new SolidColorBrush(Colors.Red);
            nodes.Last().TransitionVisibility = Visibility.Collapsed;

            StatesPathCollection = nodes;
        }

        public StatesPathViewModel(List<StatesPathNodeViewModel> nodes, StatesPathStatus status) : this(nodes)
        {
            Status = status;
        }
    }
}
