namespace Multithreading;

internal class _04_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Sender, kann beliebig viele CancellationTokens erzeugen
		CancellationToken token = cts.Token; //Dieser Token wird an die Threads weitergegeben, und dient als Empfänger

		Thread t = new Thread(Run);
		t.Start((200, token));

		Thread.Sleep(500);

		cts.Cancel(); //Hier wird allen Tokens das Cancel Signal gesendet, die an der Source registriert sind
	}

	static void Run(object o)
	{
		try
		{
			if (o is ValueTuple<int, CancellationToken> tuple)
			{
				for (int i = 0; i < tuple.Item1; i++)
				{
					tuple.Item2.ThrowIfCancellationRequested(); //Exception werfen
					Console.WriteLine($"Side Thread: {i}");
					Thread.Sleep(25);
				}
			}
		}
		catch (Exception e)
		{
            Console.WriteLine("Thread abgebrochen");
        }
	}
}
