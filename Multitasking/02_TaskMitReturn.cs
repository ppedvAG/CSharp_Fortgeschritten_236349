namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = Task.Run<int>(Berechne);
		int x = t.Result;
		//Warte bis der Task fertig ist, und entnimm das Ergebnis
		//Blockiert den Main Thread (vorallem in allen Anwendungen außer Konsolenanwendungen wichtig)
		Console.WriteLine(x);

		Task t2 = Task.Run(Run);

		Task t3 = Task.Run(Run);

		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Main Thread: {i}");

		t.Wait(); //Warte bis der Task fertig ist (kein Result)
		Task.WaitAll(t, t2, t3); //Warte bis alle Tasks fertig sind
		Task.WaitAny(t, t2, t3); //Warte bis einer der gegebenen Tasks fertig ist, gibt den Index des zuerst fertigen Tasks zurück
    }

	public static int Berechne()
	{
		Thread.Sleep(1000);
		return Random.Shared.Next();
	}

	public static void Run()
	{
		Thread.Sleep(1000);
	}
}
