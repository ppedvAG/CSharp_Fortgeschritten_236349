namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run);
		t.Start();

		Thread.Sleep(500);

		t.Interrupt();
	}

	static void Run()
	{
		try
		{
			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine($"Side Thread: {i}");
				Thread.Sleep(25);
			}
		}
		catch (Exception e) //ThreadInterruptedException muss hier unten gefangen werden
		{
            Console.WriteLine("Thread abgebrochen");
            //Console.WriteLine(e);
        }
	}
}
