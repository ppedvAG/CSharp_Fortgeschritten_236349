namespace Multithreading;

internal class _08_Interlocked
{
	static int Counter;

	static void Main(string[] args)
	{
		//Interlocked: Ermöglicht das automatische Locking von einer Ganzen Zahl
		Interlocked.Increment(ref Counter);
	}
}
