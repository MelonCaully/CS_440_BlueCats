using Microsoft.Data.Sqlite;

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
			}
			catch (Exception ex)
			{
				title_label.Text = "Failed to connect to database: " + ex.Message;
			}
		}
	}
}