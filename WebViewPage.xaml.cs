#if ANDROID
using MauiApp1.Platforms.Android;
using Microsoft.Maui.Handlers;
#endif

namespace MauiApp1;

public partial class WebViewPage : ContentPage
{
    public WebViewPage()
    {
        InitializeComponent();        

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await RequestCameraPermission();

        //        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        //        if (status != PermissionStatus.Granted) { if (Permissions.ShouldShowRationale<Permissions.Camera>()) { } await Permissions.RequestAsync<Permissions.Camera>(); }
        //#if ANDROID
        //        if (uxMyWebView.Handler == null)
        //        {
        //            return;
        //        }
        //    ((IWebViewHandler)uxMyWebView.Handler).PlatformView.SetWebChromeClient(new MyWebChromeClient());
        //#endif
    }

    private async Task RequestCameraPermission()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();

        if (status != PermissionStatus.Granted)
            await Permissions.RequestAsync<Permissions.Camera>();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(uxEntry.Text))
        {
            await DisplayAlert("App", "Please write a url", "OK");
            return;
        }

        // Intenta crear un objeto Uri con el esquema de HTTP o HTTPS
        var isValidUrl = Uri.TryCreate(uxEntry.Text, UriKind.Absolute, out Uri uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!isValidUrl)
        {
            await DisplayAlert("App", "Please write a valid url", "OK");
            return;
        }

        uxWebView.Source = uriResult;
        uxWebView.IsVisible = true;

        await DisplayAlert("App", "Url loaded succesful", "OK");
    }

    private async void NavigateToBrowserBtn_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(uxEntry.Text))
        {
            await DisplayAlert("App", "Please write a url", "OK");
            return;
        }

        // Intenta crear un objeto Uri con el esquema de HTTP o HTTPS
        var isValidUrl = Uri.TryCreate(uxEntry.Text, UriKind.Absolute, out Uri uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!isValidUrl)
        {
            await DisplayAlert("App", "Please write a valid url", "OK");
            return;
        }

        await Browser.OpenAsync(uxEntry.Text, new BrowserLaunchOptions
        {
            LaunchMode = BrowserLaunchMode.SystemPreferred,
            TitleMode = BrowserTitleMode.Hide,
            PreferredToolbarColor = Colors.Blue,
            PreferredControlColor = Colors.White
        });

    }
}