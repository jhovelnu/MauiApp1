namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var videoPath = Preferences.Get("DownloadedVideoPath", string.Empty);

            if (!string.IsNullOrEmpty(videoPath))
            {
                DownloadVideoButton.IsVisible = false;
                PlayVideoButton.IsVisible = true;
            }
        }

        private void ContentPage_Unloaded(object sender, EventArgs e)
        {
            // Stop and cleanup MediaElement when we navigate away
            uxMediaElement.Handler?.DisconnectHandler();
        }

    }

}
