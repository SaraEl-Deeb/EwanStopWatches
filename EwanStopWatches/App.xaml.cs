namespace EwanStopWatches
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnStart()
        {
            base.OnStart();
            // Start the foreground service when the app starts
            Services.ForegroundServiceHelper.StartMyForegroundService();
        }
    }
}