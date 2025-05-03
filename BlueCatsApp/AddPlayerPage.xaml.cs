using Microsoft.Data.Sqlite;
using System.Xml.Schema;
using System.Collections;
using Windows.UI.Popups;

namespace BlueCatsApp;

public partial class AddPlayerPage : ContentPage
{
	public AddPlayerPage()
	{
		InitializeComponent();
		ClassPicker.SelectedIndexChanged += OnClassChange;
		using (var connection = new SqliteConnection("Data source = dungeonBase.db"))
		{
			try
			{
				connection.Open();
				RacePicker.ItemsSource = populatePicker("Race", connection);
				ClassPicker.ItemsSource = populatePicker("Class", connection); 
				SpellPicker.ItemsSource = populateSpellPicker("Class_Spells", connection);
				WeaponPicker.ItemsSource = populatePicker("Weapons", connection);

            }
            catch (Exception ex)
			{
				title_label.Text = "Failed to connect to database: " + ex.Message;
			}
		}
	}

	private void OnClassChange(object? sender, EventArgs e)
	{
		var selectedClass = ClassPicker.SelectedItem?.ToString();
		if (string.IsNullOrEmpty(selectedClass))
		    return;	

		using (var connection = new SqliteConnection("Data source = dungeonBase.db"))
		{
			try
			{
				connection.Open();
				SpellPicker.ItemsSource = populateSpellPicker(selectedClass, connection);
			}
			catch (Exception ex)
			{
				title_label.Text = "Failed to load spells: " + ex.Message;
			}
		}
	}

    // Method used for inserting variables into pickers form the database
	private ArrayList populatePicker(string tableName, SqliteConnection connection)
	{
		ArrayList output = new ArrayList();
		var command = connection.CreateCommand();
        command.CommandText = "SELECT name FROM " + tableName;
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(reader.GetString(0));
        }
		return output;
    }

	private ArrayList populateSpellPicker(string className, SqliteConnection connection)
	{
		ArrayList output = new ArrayList();
		var command = connection.CreateCommand();
        command.CommandText = "SELECT spell FROM Class_Spells WHERE class = @class";
		command.Parameters.AddWithValue("@class", className);
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(reader.GetString(0));
        }
		return output;
    }

    // Button method for inserting players into the database
	async void OnButtonClicked(object sender, EventArgs args)
    {
		// Assigns variables entries from the AddPlayerPage.xaml
		string playerName = name.Text.Trim();
		string? playerRace = RacePicker.SelectedItem?.ToString();
		string? playerClass = ClassPicker.SelectedItem?.ToString();
		string? playerSpell = SpellPicker.SelectedItem?.ToString() ?? string.Empty;
		string? playerWeapon = WeaponPicker.SelectedItem?.ToString();
		
		// Checks to see if entries were enterd before INSERT
		if (string.IsNullOrEmpty(playerName))
    	{
			await DisplayAlert("Error", "Please enter a name.", "OK");
			return;
    	} else if (string.IsNullOrEmpty(playerRace))
		{
			await DisplayAlert("Error", "Please pick a race.", "OK");
			return;
		} else if (string.IsNullOrEmpty(playerClass))
		{
			await DisplayAlert("Error", "Please pick a class.", "OK");
			return;
		}

		// establishes a connection with database and inserts variables into database
		using (var connection = new SqliteConnection("Data source = dungeonbase.db"))
		{
			try
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "INSERT INTO Player(name, race, class, spell, weapon) "
									+ "VALUES (@name, @race, @class, @spell, @weapon)";
				command.Parameters.AddWithValue("@name", playerName);
				command.Parameters.AddWithValue("@race", playerRace);
				command.Parameters.AddWithValue("@class", playerClass);
				command.Parameters.AddWithValue("@spell", playerSpell);
				command.Parameters.AddWithValue("@weapon", playerWeapon);
				command.ExecuteNonQuery();

				await DisplayAlert("Success", "Player added successfully!", "OK");
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", "Failed to add player: " + ex.Message, "OK");
			}
		}
    }
}