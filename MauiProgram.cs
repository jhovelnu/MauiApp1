#if ANDROID
using Android.Webkit;
using MauiApp1.Platforms.Android;
#endif
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    //handlers.AddHandler<WebView, CustomWebViewHandler>();
                    CustomizeWebViewHandler();
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

#if ANDROID
        //public class CustomWebViewHandler : WebViewHandler
        //{
        //    protected override void ConnectHandler(Android.Webkit.WebView platformView)
        //    {
        //        base.ConnectHandler(platformView);
        //        platformView.Settings.JavaScriptEnabled = true;
        //        platformView.Settings.AllowFileAccess = true;
        //        platformView.Settings.MediaPlaybackRequiresUserGesture = false;
        //        platformView.SetWebChromeClient(new CustomWebChromeClient(MainActivity.Instance));
        //    }
        //}

        private static void CustomizeWebViewHandler()
        {
#if ANDROID26_0_OR_GREATER
            Microsoft.Maui.Handlers.WebViewHandler.Mapper.ModifyMapping(
                nameof(Android.Webkit.WebView.WebChromeClient),
                (handler, view, args) => handler.PlatformView.SetWebChromeClient(new CustomWebChromeClient(handler)));
#endif
        }
#endif
    }
}
