namespace Multitasking;

internal class _04_TaskExceptions
{
	static void Main(string[] args)
	{
		try
		{
			//AggregateException: Sammelt mehrere Exceptions
			Task t1 = Task.Run(Methode1);
			Task t2 = Task.Run(Methode2);
			Task t3 = Task.Run(Methode3);

			//3 Möglichkeiten um diese Exception zu erhalten: t.Result, t.Wait(), Task.WaitAll(...)
			Task.WaitAll(t1, t2, t3);
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
                Console.WriteLine(e);
        }
	}

	static void Methode1()
	{
		try //Einzelnes Exception Handling ist auch möglich
		{
			throw new Exception("Eine Exception");
		}
		catch (Exception) { }
	}

	static void Methode2()
	{
		throw new InvalidDataException("Zwei Exception");
	}

	static void Methode3()
	{
		throw new ArgumentException("Drei Exception");
	}
}
