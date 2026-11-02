using EwanStopWatches.PageModels;
namespace EwanStopWatches.Controls;

public partial class StopWatchControl : ContentView
{
	readonly StopWatchPageModel _PM;
	public event EventHandler? DeleteRequested;
	public StopWatchControl()
	{
		_PM = new StopWatchPageModel { Name = "Stopwatch" };
		BindingContext = _PM;
		InitializeComponent();
	}

	public StopWatchControl(StopWatchPageModel pm)
	{
		_PM = pm;
		BindingContext = _PM;
		InitializeComponent();
	}

	private void OnDeleteClicked(object sender, EventArgs e)
	{
		DeleteRequested?.Invoke(this, EventArgs.Empty);
    }
}