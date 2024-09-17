using CommunityToolkit.Maui.Views;

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

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //Set root page
            await SaveVideo();
        }

        private void ContentPage_Unloaded(object sender, EventArgs e)
        {
            // Stop and cleanup MediaElement when we navigate away
            uxMediaElement.Handler?.DisconnectHandler();
        }

        private async Task SaveVideo()
        {
            try
            {
                var url = "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
                string saveDirectory = FileSystem.Current.AppDataDirectory;
                var fileName = Path.GetFileName(url);

                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                if (response != null && response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    using Stream stream = await response.Content.ReadAsStreamAsync();

                    string targetFilePath = Path.Combine(saveDirectory, fileName);
                    using FileStream outputStream = File.Create(targetFilePath);
                    await stream.CopyToAsync(outputStream);

                    Preferences.Set("DownloadedVideoPath", targetFilePath);

                    await DisplayAlert("MAUI", "Video downloaded success", "OK");

                    PlayVideoButton.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("MAUI", "Video downloaded failed", "OK");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("MAUI", "Video downloaded failed", "OK");
            }
        }

        private void PlayVideoButton_Clicked(object sender, EventArgs e)
        {
            var videoPath = Preferences.Get("DownloadedVideoPath", string.Empty);

            uxMediaElement.IsVisible = true;
            uxMediaElement.Source = MediaSource.FromFile(videoPath);

            Button3.IsVisible = true;
        }

        private void Button3_Clicked(object sender, EventArgs e)
        {
            var page = new NavigationPage(new MainTabbedPage());

            Application.Current.MainPage = page;
        }
    }

}
