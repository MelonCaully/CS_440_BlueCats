using Microsoft.Data.Sqlite;
using System.Xml.Schema;
using System.Collections;

namespace BlueCatsApp;

public partial class AddPlayerPage : ContentPage
{
	public ArrayList races = new ArrayList();
	public AddPlayerPage()
	{
		InitializeComponent();
		using (var connection = new SqliteConnection("Data source = dungeonbase.db"))
		{
			try
			{
				connection.Open();

                var RaceCommand = connection.CreateCommand();
				RaceCommand.CommandText = "SELECT name FROM Race";
				var RaceReader = RaceCommand.ExecuteReader();
				while (RaceReader.Read())
				{
					races.Add(RaceReader.GetString(0));
				}

				RaceReader.Close();
				RacePicker.ItemsSource = races;

            }
            catch (Exception ex)
			{
				title_label.Text = "Failed to connect to database: " + ex.Message;
			}

			
		}
	}
}