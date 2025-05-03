using Microsoft.Data.Sqlite;

namespace BlueCatsApp
{
    public partial class SpellsPage : ContentPage
    {
        public SpellsPage()
        {
            InitializeComponent(); // This initializes the UI components defined in XAML
            LoadSpells();
        }

        async private void LoadSpells()
        {
            using (var connection = new SqliteConnection("Data source = dungeonBase.db"))
            {
                try
                {
                    connection.Open();
                    List<Spell> output = new List<Spell>();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Spell";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(new Spell
                        {
                            SpellName = reader.GetString(0),
                            SpellLevel = reader.GetString(1),
                            CastingTime = reader.GetString(2),
                            Duration = reader.GetString(3),
                            Range = reader.GetString(4),
                            Damage = reader.GetString(5),
                            HitDice = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Effect = reader.GetString(7)
                        });
                    }
                    SpellsCollectionView.ItemsSource = output;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Failed to connect to database: " + ex.Message, "OK");
                }
            }
        }

        public class Spell
        {
            public required string SpellName { get; set; }
            public required string SpellLevel { get; set; }
            public required string CastingTime { get; set; }
            public required string Duration { get; set; }
            public required string Range { get; set; }
            public required string Damage { get; set; }
            public string? HitDice { get; set; }
            public required string Effect { get; set; }
        }
    }
}
