namespace Multithreading;

public class _05_ThreadPool
{
	static List<Guid> Threads = new();

	static void Main(string[] args)
	{
		//Normalerweise sind alle Threads Vordergrundthreads -> Programm wird nicht beendet
		//Am ThreadPool sind alle Threads Hintergrundthreads -> Wenn alle Vordergrundthreads fertig sind, werden die Hintergrundthreads abgebrochen

		ThreadPool.QueueUserWorkItem(Run);

		Thread t = new Thread(Wait); //Dieser Thread hält auch das Programm auf
		t.Start();

		Thread.Sleep(500);

		//Alle Threads am ThreadPool werden abgebrochen

		Console.ReadKey(); //Mit ReadKey den Main Thread (= Vordergrundthread) blockieren

		while (Threads.Count > 0) continue;
	}

	static void Run(object o)
	{
		Guid g = Guid.NewGuid();
		Threads.Add(g);
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"ThreadPool: {i}");
			Thread.Sleep(25);
		}
		Threads.Remove(g);
	}

	static void Wait()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Wait Thread: {i}");
			Thread.Sleep(10);
		}
	}
}
