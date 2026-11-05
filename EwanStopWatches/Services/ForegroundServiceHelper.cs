using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if ANDROID
using Android.Content;
using EwanStopWatches.Platforms.Android;
#endif

namespace EwanStopWatches.Services
{
    public static class ForegroundServiceHelper
    {
        public static void StartMyForegroundService()
        {
#if ANDROID
        var context = Android.App.Application.Context;
        var intent = new Android.Content.Intent(context, typeof(MyForegroundService));
        context.StartForegroundService(intent);
#endif
        }

        public static void StopMyForegroundService()
        {
#if ANDROID
        var context = Android.App.Application.Context;
        var intent = new Android.Content.Intent(context, typeof(MyForegroundService));
        context.StopService(intent);
#endif
        }
    }
}
