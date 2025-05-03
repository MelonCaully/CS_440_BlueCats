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
                            Damage = reader.GetString(5)
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
            public string SpellName { get; set; }
            public string SpellLevel { get; set; }
            public string CastingTime { get; set; }
            public string Duration { get; set; }
            public string Range { get; set; }
            public string Damage { get; set; }
            public string HitDice { get; set; }
            public string Effect { get; set; }
        }
    }
}
