using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Start(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			Progress.Value++;
			Thread.Sleep(25); //Thread.Sleep blockiert den Main Thread
		}
	}

	private void StartTaskRun(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Dispatcher.Invoke(() => Progress.Value++); //UI darf von Side Threads/Tasks nicht verändert werden
				Thread.Sleep(25);
			}
		});
	}

	private async void StartAwait(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			Progress.Value++;
			await Task.Delay(25); //await blockiert nicht
		}
	}

	private async void StartHttpClient(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		Progress.Maximum = 2;

		using HttpClient client = new();
		Task<HttpResponseMessage> request = client.GetAsync("http://www.gutenberg.org/files/54700/54700-0.txt");
		//Hier wird der Request gestartet
		//Wenn dieser Task fertig ist, kommt hier eine ResponseMessage heraus

		TB.Text = "Text wird geladen...";
		//Weitere UI Updates...

		HttpResponseMessage response = await request;
		Progress.Value++;

		if (response.IsSuccessStatusCode)
		{
			Task<string> text = response.Content.ReadAsStringAsync();

			//UI Updates
			TB.Text = "Text wird ausgelesen...";
			await Task.Delay(1000); //Künstliches Delay
			Progress.Value++;

			string x = await text;
			TB.Text = x;
		}
	}

	private async void Button_Click_3(object sender, RoutedEventArgs e)
	{
		//IAsyncEnumerable: Gibt die Möglichkeit, eine unbestimmte Anzahl von Daten zu holen und gleichzeitig zu verarbeiten
		//Während die Datenquelle unsere Daten liefert, können wir unsere bereits vorhandenen Daten schon verarbeiten

		TB.Text = "";
		AsyncDataSource data = new();
		await foreach (int x in data.GetIntsAsync()) //await foreach: Warte bis das nächste Element verfügbar ist, und führe danach die Schleife einmal aus
		{
			TB.Text += x + "\n";
		}
	}
}
