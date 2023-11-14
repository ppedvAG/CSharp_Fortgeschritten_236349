using System.Data;

namespace Multithreading;

public class _06_Lock
{
	static int Count;

	static object LockObject = new();

	static void Main(string[] args)
	{
		for (int i = 0; i < 100; i++)
		{
			new Thread(CountPlusPlus).Start();
		}
	}

	static void CountPlusPlus()
	{
		//Problem: Wenn Zwei Threads gleichzeitig auf Count zugreifen, wird ein Thread blockiert
		//Lösung: Locking/Monitor
		lock (LockObject) //Öffne das Lock, und speichere den State in dem Object
		{
			//Warte hier, bis der Thread, der diesen Code gerade ausführt, fertig ist
			for (int i = 0; i < 1000; i++)
				Count++;
			Console.WriteLine(Count);
		} //Hier gibt der Thread den Code wieder frei

		Monitor.Enter(LockObject); //1:1 der selbe Code wie Lock
		for (int i = 0; i < 1000; i++)
			Count++;
		Console.WriteLine(Count);
		Monitor.Exit(LockObject);
	}
}
