using Microsoft.Data.Sqlite;
using System.Xml.Schema;
using System.Collections;

namespace BlueCatsApp;

public partial class AddPlayerPage : ContentPage
{
	private ArrayList spells = new ArrayList();
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
				SpellPicker.ItemsSource = spells;//populatePicker("Spell", connection);
				spells = populatePicker("Spell", connection);
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

	//empty or fill the spell picker depending on the chosen class
	void OnClassChange(object sender, EventArgs e)
    {
		var picker = (Picker)sender;
		//if(picker == ClassPicker)
		//{
			if(picker.SelectedItem.ToString() == "Cleric" || picker.SelectedItem.ToString() == "Paladin" || picker.SelectedItem.ToString() == "Druid" || picker.SelectedItem.ToString() == "Sorcerer" || picker.SelectedItem.ToString() == "Warlock" || picker.SelectedItem.ToString() == "Wizard" || picker.SelectedItem.ToString() == "Bard")
			{
				SpellPicker.ItemsSource = spells;
			}
			else
			{
				SpellPicker.ItemsSource = new ArrayList();
				SpellPicker.SelectedIndex = -1;
			}
		//}
	}
}