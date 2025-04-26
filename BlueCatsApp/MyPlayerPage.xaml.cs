using Microsoft.Data.Sqlite;
using System.Collections;
using Windows.UI.Popups;

namespace BlueCatsApp;

public partial class MyPlayerPage : ContentPage
{
    public class Player
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
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
}