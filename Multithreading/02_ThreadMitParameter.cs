namespace Multithreading;

public class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run);
		t.Start(200); //Hier bei Start muss der Parameter übergeben werden
					  //t.Start(new ThreadData(50, "Test", false)); //Komplexe Datenstruktur über eigene Klasse

		List<int> threadOutputs = new();
		Thread t2 = new Thread(RunWithReturn); //Callback: Action, die von einer Methode aufgerufen wird, um etwas zu tun
		t2.Start((int e) => threadOutputs.Add(e)); //Hier eine Action als Callback definieren um Rückgabewerte zu empfangen

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	public static void RunWithReturn(object o)
	{
		if (o is Delegate dg)
		{
			dg.DynamicInvoke(Random.Shared.Next()); //Über das Delegate den Rückgabewert zurückgeben
		}
	}

	public static void Run(object o)
	{
		if (o is int x) //Hier muss ein Typvergleich gemacht werden
			for (int i = 0; i < x; i++)
				Console.WriteLine($"Side Thread: {i}");
	}
}

public record ThreadData(int x, string y, bool z);