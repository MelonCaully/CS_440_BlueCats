using Microsoft.Data.Sqlite;

namespace BlueCatsApp;

public partial class RacesPage : ContentPage
{
    public RacesPage()
    {
        InitializeComponent();
        LoadRaces();
    }

    async private void LoadRaces()
    {
        using (var connectioin = new SqliteConnection("Data source = dungeonBase.db"))
        {
            try
            {
                connectioin.Open();
                List<Race> output = new List<Race>();
                var command = connectioin.CreateCommand();
                command.CommandText = "SELECT * FROM Race";
                var reader = command.ExecuteReader();
                while (reader.Read())
                    {
                        output.Add(new Race
                        {
                            RaceName = reader.GetString(0),
                            Traits = reader.GetString(1),
                            Size = reader.GetString(2),
                            Speed = reader.GetInt16(3),
                            Flight = reader.IsDBNull(4) ? null : reader.GetInt16(4),
                            Ability1 = reader.GetString(5),
                            Ability2 = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Ability3 = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Ability4 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Language1 = reader.GetString(9),
                            Language2 = reader.IsDBNull(10) ? null : reader.GetString(10),
                        });
                    }
                    RacesCollectionView.ItemsSource = output;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to connect to database: " + ex.Message, "OK");
            }
        }
    }

    public class Race
    {
        public required string RaceName { get; set; }
        public required string Traits { get; set; }
        public required string Size { get; set; }
        public required int Speed { get; set; }
        public int? Flight { get; set; }
        public required string Ability1 { get; set; }
        public string? Ability2 { get; set; }
        public string? Ability3 { get; set; }
        public string? Ability4 { get; set; }
        public required string Language1 { get; set; }
        public string? Language2 { get; set; }
    }
}