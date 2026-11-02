using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;

namespace EwanStopWatches.PageModels;

public partial class StopWatchPageModel : ObservableObject
{
    private readonly Stopwatch _stopwatch = new();
    private CancellationTokenSource? _timerCancellation;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private TimeOnly startTime = TimeOnly.MinValue;

    [ObservableProperty]
    private TimeOnly endTime = TimeOnly.MinValue;

    [ObservableProperty]
    private string elapsedTime = "00:00:00";

    [ObservableProperty]
    private bool isRunning;

    [RelayCommand]
    private void StartStopWatch()
    {
        if (IsRunning)
        {
            return;
        }

        StartTime = TimeOnly.FromDateTime(DateTime.Now);
        EndTime = TimeOnly.MinValue;
        IsRunning = true;

        _stopwatch.Restart();

        _timerCancellation?.Cancel();
        _timerCancellation = new CancellationTokenSource();

        _ = UpdateElapsedTimeAsync(_timerCancellation.Token);
    }

    [RelayCommand]
    private void StopWatch()
    {
        if (!IsRunning)
        {
            return;
        }

        _stopwatch.Stop();
        EndTime = TimeOnly.FromDateTime(DateTime.Now);
        ElapsedTime = FormatElapsedTime(_stopwatch.Elapsed);
        IsRunning = false;

        _timerCancellation?.Cancel();
    }

    [RelayCommand]
    private void ResetStopWatch()
    {
        _timerCancellation?.Cancel();
        _stopwatch.Reset();

        StartTime = TimeOnly.MinValue;
        EndTime = TimeOnly.MinValue;
        ElapsedTime = "00:00:00";
        IsRunning = false;
    }

    private async Task UpdateElapsedTimeAsync(CancellationToken cancellationToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

        try
        {
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                string elapsedTime = FormatElapsedTime(_stopwatch.Elapsed);

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    ElapsedTime = elapsedTime;
                });
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when the stopwatch is stopped or reset.
        }
    }

    private static string FormatElapsedTime(TimeSpan elapsedTime)
    {
        return $"{(int)elapsedTime.TotalHours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}";
    }
}

