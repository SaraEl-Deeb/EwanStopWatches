using EwanStopWatches.Controls;
using EwanStopWatches.PageModels;

namespace EwanStopWatches;

public partial class MainPage : ContentPage
{
    private int _stopwatchCount = 1;

    public MainPage()
    {
        InitializeComponent();

        foreach (StopWatchControl stopwatch in StopwatchesLayout.Children.OfType<StopWatchControl>())
        {
            stopwatch.DeleteRequested += OnDeleteStopwatchRequested;
        }
    }

    private void OnAddStopwatchClicked(object sender, EventArgs e)
    {
        _stopwatchCount++;

        StopWatchPageModel pageModel = new()
        {
            Name = $"Stopwatch {_stopwatchCount}"
        };

        StopWatchControl stopwatchControl = new(pageModel);

        StopwatchesLayout.Children.Add(stopwatchControl);
    }
    
    private void OnDeleteStopwatchRequested(object? sender, EventArgs e)
    {
        if(sender is StopWatchControl stopwatchControl)
        {
            stopwatchControl.DeleteRequested -= OnDeleteStopwatchRequested;
            StopwatchesLayout.Children.Remove(stopwatchControl);
        }
    }

}
