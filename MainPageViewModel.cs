using MauiApp1.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp1
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand RefreshCommand { get; }

        public MainPageViewModel()
        {
            RefreshCommand = new Command(async () => await RefreshBanners());
        }

        private async Task RefreshBanners()
        {
            BannerVisible = false;
            CarouselProducts = [];
            
            await LoadBanners();
        }

        public async Task Init()
        {
            await LoadCards();
            await LoadBanners();
        }

        private async Task LoadCards()
        {
            IsBusy = true;

            await Task.Delay(1000);

            var card1 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15529_240705111218177321.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            var card2 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15522_2407051112127483690.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            var card3 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15515_2407051112099499026.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            var card4 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15529_240705111218177321.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            var card5 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15522_2407051112127483690.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            var card6 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15515_2407051112099499026.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            var card7 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15529_240705111218177321.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            var card8 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/products/mobile/15522_2407051112127483690.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "12.500",
            };

            CardsCollection = [card1, card2, card3, card4, card5, card6, card7, card8];
            //CardsCollection.Clear();
            //CardsCollection.Add(card1);
            //CardsCollection.Add(card2);
            //CardsCollection.Add(card3);
            //CardsCollection.Add(card4);
            //CardsCollection.Add(card5);
            //CardsCollection.Add(card6);
            //CardsCollection.Add(card7);
            //CardsCollection.Add(card8);

            IsBusy = false;
        }

        private async Task LoadBanners()
        {
            IsBusy = true;

            await Task.Delay(1000);

            var carousel1 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/slide/mobile/00906224-c395-4b2c-ba5b-71af5e68c0a5_20240724095141.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "100.500",
            };

            var carousel2 = new PlatformOption
            {
                ImageFullPath = "https://devinmotionstoragedevqa.blob.core.windows.net/emotion/surtiapp/images/slide/mobile/5996da67-495e-ee11-9937-000d3ae4bdfb_20230928165306.png",
                Name = Guid.NewGuid().ToString(),
                PriceFormatted = "200.500",
            };

            BannerVisible = true;
            CarouselProducts = [carousel1, carousel2];
            //CarouselProducts.Clear();
            //CarouselProducts.Add(carousel1);
            //CarouselProducts.Add(carousel2);

            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        private ObservableCollection<PlatformOption> _cardsCollection = [];

        public ObservableCollection<PlatformOption> CardsCollection
        {
            get => _cardsCollection;
            set
            {
                if (_cardsCollection != value)
                {
                    _cardsCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<PlatformOption> _carouselProducts = [];

        public ObservableCollection<PlatformOption> CarouselProducts
        {
            get => _carouselProducts;
            set
            {
                if (_carouselProducts != value)
                {
                    _carouselProducts = value;
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

        private bool _bannerVisible;

        public bool BannerVisible
        {
            get => _bannerVisible;
            set
            {
                if (_bannerVisible != value)
                {
                    _bannerVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}
