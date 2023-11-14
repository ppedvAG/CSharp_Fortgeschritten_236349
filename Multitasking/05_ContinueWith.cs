namespace Multitasking;

public class _05_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith: Wird verwendet, um Task Bäume zu erzeugen
		//Es können bei jedem Folgetask Optionen angegeben werden, zu welchen dieser Task läuft
		Task<int> t = new Task<int>(() =>
		{
			Thread.Sleep(1000);
			return Random.Shared.Next();
		});
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Dieser Folgetask blockiert nicht, weil er ein Task ist
		t.ContinueWith(x => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion); //Mithilfe von TaskContinuationOptions festlegen, welche Folgetasks gestartet werden
		t.ContinueWith(x => Console.WriteLine("Fehler"), TaskContinuationOptions.OnlyOnFaulted);
		t.Start();

        //Das Ergebnis von t soll während der Main Thread die Schleife ausführt geprinted werden
        for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(25);
		}
	}
}
