namespace Multithreading;

public class _01_ThreadStarten
{
	static void Main(string[] args)
	{
		Thread t1 = new Thread(Run); //Thread anlegen mit Methodenzeiger
		t1.Start();

		Thread t2 = new Thread(Run); //Thread anlegen mit Methodenzeiger
		t2.Start();

		Thread t3 = new Thread(Run); //Thread anlegen mit Methodenzeiger
		t3.Start();

		//Ab hier parallel

		//Join(): Warte auf diesen Thread

		t1.Join(); //Warte hier auf t1, t2 und t3 laufen parallel weiter
		t2.Join(); //Warte hier auf t2, t3 läuft parallel weiter
		t3.Join(); //Warte hier auf t3, danach beginnt der restliche Code im Main Thread

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	public static void Run()
	{
		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Side Thread: {i}");
    }
}