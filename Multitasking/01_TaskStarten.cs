namespace Multitasking;

public class Program
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Tasks sind am ThreadPool -> Hintergrundthreads
		t.Start();

		Task t2 = Task.Factory.StartNew(Run); //ab .NET Framework 4.0

		Task t3 = Task.Run(Run); //ab .NET Framework 4.5

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 200; i++)
			Console.WriteLine($"Side Task: {i}");
	}
}