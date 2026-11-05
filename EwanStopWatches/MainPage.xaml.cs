using EwanStopWatches.Controls;
using EwanStopWatches.PageModels;
using System.Diagnostics;
using System.Threading.Tasks;

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

        stopwatchControl.DeleteRequested += OnDeleteStopwatchRequested;
        StopwatchesLayout.Children.Add(stopwatchControl);

        //Add a splitter
        var splitter = new BoxView {
            HeightRequest = 1,
            BackgroundColor = Colors.Gray,
            HorizontalOptions = LayoutOptions.Fill,
            Margin = new Thickness(0,10,0,10)
        };
        StopwatchesLayout.Children.Add(splitter);
    }

    private async void OnDeleteStopwatchRequested(object? sender, EventArgs e)
    {
        if (sender is StopWatchControl stopwatchControl)
        {
            bool confirm = await DisplayAlert(
            "Delete Stopwatch",
        "Are you sure you want to delete " + ((StopWatchPageModel)stopwatchControl.BindingContext).Name,
        "Delete",
        "Cancel");

            if (confirm)
            {
                int index = StopwatchesLayout.Children.IndexOf(stopwatchControl);

                //Remove the splitter (if exists)
                if (index + 1 < StopwatchesLayout.Children.Count && StopwatchesLayout.Children[index + 1] is BoxView)
                {
                    StopwatchesLayout.Children.RemoveAt(index + 1);
                }
                stopwatchControl.DeleteRequested -= OnDeleteStopwatchRequested;
                StopwatchesLayout.Children.Remove(stopwatchControl);


            }
        }
    }

    private void OnShutdownClicked(object sender, EventArgs e)
    {
        Services.ForegroundServiceHelper.StopMyForegroundService();
    }
}
