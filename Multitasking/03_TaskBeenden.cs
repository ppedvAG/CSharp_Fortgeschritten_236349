namespace Multitasking;

public class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		CancellationToken cancellationToken = cancellationTokenSource.Token;

		Task t = new Task(Run, cancellationToken);

		cancellationTokenSource.Cancel();

		Thread.Sleep(500);
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			Thread.Sleep(200);
			ct.ThrowIfCancellationRequested(); //Tasks geben hier keine Auskunft über Exceptions
		}
	}
}
