using Microsoft.Data.Sqlite;

namespace BlueCatsApp
{
    public partial class WeaponsPage : ContentPage
    {
        public WeaponsPage()
        {
            InitializeComponent(); // This initializes the UI components defined in XAML
            LoadWeapons();
        }

        async private void LoadWeapons()
        {
            using (var connection = new SqliteConnection("Data source = dungeonBase.db"))
            {
                try
                {
                    connection.Open();
                    List<Weapon> output = new List<Weapon>();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Weapons";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(new Weapon
                        {
                            WeaponName = reader.GetString(0),
                            AttackType = reader.GetString(1),
                            Range = reader.GetString(2),
                            Damage = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Effect = reader.GetString(4),
                            Cost = reader.IsDBNull(5) ? null : reader.GetInt16(5),
                            Properties = reader.GetString(6),
                        });
                    }
                    WeaponsCollectionView.ItemsSource = output;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Failed to connect to database: " + ex.Message, "OK");
                }
            }
        }

        public class Weapon
        {
            public required string WeaponName { get; set; }
            public required string AttackType { get; set; }
            public required string Range { get; set; }
            public string? Damage { get; set; }
            public required string Effect { get; set; }
            public int? Cost { get; set; }
            public required string Properties { get; set; }
        }
    }
}
