using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace EwanStopWatches.Platforms.Android
{
    [Service]
    public class MyForegroundService : Service
    {
        public const int ServiceNotificationId = 1001;

        public override IBinder? OnBind(Intent? intent) => null;

        public override void OnCreate()
        {
            base.OnCreate();

            var notification = new NotificationCompat.Builder(this, "my_channel")
                .SetContentTitle("Foreground Service")
                .SetContentText("Service is running in the foreground")
                //.SetSmallIcon(Resource.Drawable.ic_launcher)
                .Build();

            StartForeground(ServiceNotificationId, notification);
        }

        public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
        {
            // Do your background work here

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            // Cleanup if needed
        }
    }
}