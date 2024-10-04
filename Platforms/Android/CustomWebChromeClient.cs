using Android.Webkit;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MauiApp1.Platforms.Android
{
    internal class MyWebChromeClient : WebChromeClient
    {
        public override void OnPermissionRequest(PermissionRequest request)
        {
            foreach (var resource in request.GetResources())
            { if (resource.Equals(PermissionRequest.ResourceVideoCapture, StringComparison.OrdinalIgnoreCase)) { request.Grant(request.GetResources()); return; } }
            base.OnPermissionRequest(request);
        }
    }

    public class CustomWebChromeClient : MauiWebChromeClient
    {
        private readonly MainActivity _activity;

        //public CustomWebChromeClient(MainActivity activity)
        //{
        //    _activity = activity;
        //}

        public CustomWebChromeClient(IWebViewHandler handler) : base(handler)
        {

        }

        public override void OnPermissionRequest(PermissionRequest request)
        {
            // Process each request
            foreach (var resource in request.GetResources())
            {
                // Check if the web page is requesting permission to the camera
                if (resource.Equals(PermissionRequest.ResourceVideoCapture, StringComparison.OrdinalIgnoreCase))
                {
                    // Get the status of the .NET MAUI app's access to the camera
                    PermissionStatus status = Permissions.CheckStatusAsync<Permissions.Camera>().Result;

                    // Deny the web page's request if the app's access to the camera is not "Granted"
                    if (status != PermissionStatus.Granted)
                        request.Deny();
                    else
                        request.Grant(request.GetResources());

                    return;
                }
            }

            base.OnPermissionRequest(request);
        }
    }
}
