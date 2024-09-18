using CommunityToolkit.Maui.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp1
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand DownloadVideoCommand { get; }
        public ICommand PlayVideoCommand { get; }
        public ICommand NavigateCommand { get; }

        public MainPageViewModel()
        {
            DownloadVideoCommand = new Command(async () => await DownloadVideo());
            PlayVideoCommand = new Command(() => PlayVideo());
            NavigateCommand = new Command(() => Navigate());
        }

        private void Navigate()
        {
            var page = new NavigationPage(new MainTabbedPage());

            Application.Current.MainPage = page;
        }

        private void PlayVideo()
        {
            var videoPath = Preferences.Get("DownloadedVideoPath", string.Empty);

            ShowMediaElement = true;
            Source = MediaSource.FromFile(videoPath);

            ShowNavigateButton = true;
        }

        private async Task DownloadVideo()
        {
            try
            {
                IsBusy = true;

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

                    await Application.Current.MainPage.DisplayAlert("MAUI", "Video downloaded success", "OK");

                    ShowPlayVideoButton = true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("MAUI", "Video downloaded failed", "OK");
                }

                IsBusy = false;
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("MAUI", "Video downloaded failed", "OK");

                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        private bool _showMediaElement;

        public bool ShowMediaElement
        {
            get => _showMediaElement;
            set
            {
                if (_showMediaElement != value)
                {
                    _showMediaElement = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _showPlayVideoButton;

        public bool ShowPlayVideoButton
        {
            get => _showPlayVideoButton;
            set
            {
                if (_showPlayVideoButton != value)
                {
                    _showPlayVideoButton = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _showNavigateButton;

        public bool ShowNavigateButton
        {
            get => _showNavigateButton;
            set
            {
                if (_showNavigateButton != value)
                {
                    _showNavigateButton = value;
                    OnPropertyChanged();
                }
            }
        }

        private MediaSource _mediaSource;

        public MediaSource Source
        {
            get => _mediaSource;
            set
            {
                if (_mediaSource != value)
                {
                    _mediaSource = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}