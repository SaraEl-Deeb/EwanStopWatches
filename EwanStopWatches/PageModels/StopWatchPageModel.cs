using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EwanStopWatches.PageModels
{
    public partial class StopWatchPageModel : ObservableObject
    {
        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private TimeOnly startTime = TimeOnly.MinValue;

        [ObservableProperty]
        private TimeOnly endTime = TimeOnly.MinValue;

        [ObservableProperty]
        private int elapsedTime = 0;

        [ObservableProperty]
        private bool isRunning = false;
    }
}

