namespace MauiApp1
{
    public partial class MainPage2 : ContentPage
    {
        public MainPage2()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("..");
            await Navigation.PopAsync();
        }
    }
}
