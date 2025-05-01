using Microsoft.Data.Sqlite;
using System.Collections;
using Windows.UI.Popups;

namespace BlueCatsApp;

public partial class MyPlayerPage : ContentPage
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int Level { get; set; }
        public string RaceImage => $"{Race}.png";
        public string InfoLine => $"Level {Level} | {Race} | {Class}";
    }
    
    public MyPlayerPage()
    {
        InitializeComponent();
        LoadPlayers();
    }

    async private void LoadPlayers()
    {
        using (var connection = new SqliteConnection("Data source = dungeonBase.db"))
		{
			try
			{
				connection.Open();
                List<Player> output = new List<Player>();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name, race, class, level FROM Player";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    output.Add(new Player
                    {
                        Name = reader.GetString(0),
                        Race = reader.GetString(1),
                        Class = reader.GetString(2),
                        Level = reader.GetInt32(3)
                    });
                }
                PlayerCollectionView.ItemsSource = output;
            }
            catch (Exception ex)
			{
				await DisplayAlert("Error", "Failed to connect to database: " + ex.Message, "OK");
			}
		}
    }

    async void OnTrashClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        if (button?.BindingContext is not Player player)
            return;

        bool confirm = await DisplayAlert("Delete Player", $"Are you sure you want to delete {player.Name}?", "Yes", "No");
        if (!confirm)
            return;

        using (var connection = new SqliteConnection("Data source = dungeonBase.db"))
        {
            try{
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Player WHERE name = @name";
                command.Parameters.AddWithValue("@name", player.Name);
                command.ExecuteNonQuery();

                LoadPlayers();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to delete player: " + ex.Message, "OK");
            }
        }
    }

    async public void OnPlayerTapped(object sender, EventArgs e)
    {
        var view = sender as VisualElement;
        var tappedPlayer = view?.BindingContext as Player;

        await Navigation.PushAsync(new PlayerSheetPage(tappedPlayer));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPlayers(); // Refresh the CollectionView
    }
}