using Microsoft.Data.Sqlite;
using System.Xml.Schema;
using System.Collections;

namespace BlueCatsApp;

public partial class AddPlayerPage : ContentPage
{
	public AddPlayerPage()
	{
		InitializeComponent();
		using (var connection = new SqliteConnection("Data source = dungeonbase.db"))
		{
			try
			{
				connection.Open();
				RacePicker.ItemsSource = populatePicker("Race", connection);
				ClassPicker.ItemsSource = populatePicker("Class", connection);
				SpellPicker.ItemsSource = populatePicker("Spell", connection);
				WeaponPicker.ItemsSource = populatePicker("Weapons", connection);

            }
            catch (Exception ex)
			{
				title_label.Text = "Failed to connect to database: " + ex.Message;
			}

			
		}
	}
	private ArrayList populatePicker(string tableName, SqliteConnection connection)
	{
		ArrayList output = new ArrayList();
		var Command = connection.CreateCommand();
        Command.CommandText = "SELECT name FROM " +tableName;
        var Reader = Command.ExecuteReader();
        while (Reader.Read())
        {
            output.Add(Reader.GetString(0));
        }
		return output;
    }
}