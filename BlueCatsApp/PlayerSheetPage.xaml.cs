namespace BlueCatsApp;

public partial class PlayerSheetPage : ContentPage
{
    public PlayerSheetPage(MyPlayerPage.Player player)
    {
        InitializeComponent();
        BindingContext = player;
    }
}