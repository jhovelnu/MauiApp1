namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainPageViewModel viewModel)
            {
                await viewModel.Init();
            }
        }
    }

}
