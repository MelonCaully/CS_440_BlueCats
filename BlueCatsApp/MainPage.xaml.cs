namespace BlueCatsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnCreateCharacterClicked(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("///AddPlayerPage");
        }

        async void OnMyCharacterClicked(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("///MyPlayerPage");
        }
    }
}
