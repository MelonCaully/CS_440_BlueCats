using System.Runtime.InteropServices;
using Microsoft.Data.Sqlite;

namespace BlueCatsApp;

public partial class ClassesPage : ContentPage
{
    public ClassesPage()
    {
        InitializeComponent();
        LoadClasses();
    }

    async private void LoadClasses()
    {
        using (var connection = new SqliteConnection("Data source = dungeonBase.db"))
        {
            try
            {
                connection.Open();
                List<Class> output = new List<Class>();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Class";
                var reader = command.ExecuteReader();
                while (reader.Read())
                    {
                        output.Add(new Class
                        {
                            ClassName = reader.GetString(0),
                            PrimaryAbility = reader.GetString(1),
                            SecondaryAbility = reader.IsDBNull(2) ? null : reader.GetString(2),
                            HitDice = reader.GetInt16(3),
                            PrimarySavingThrow = reader.GetString(4),
                            SecondarySavingThrow = reader.GetString(5)
                        });
                    }
                    ClassesCollectionView.ItemsSource = output;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to connect to database: " + ex.Message, "OK");
            }
        }
    }

    public class Class
    {
        public required string ClassName { get; set; }
        public required string PrimaryAbility { get; set; }
        public string? SecondaryAbility { get; set;}
        public required int HitDice { get; set; }
        public required string PrimarySavingThrow { get; set; }
        public required string SecondarySavingThrow { get; set;}
    }
}