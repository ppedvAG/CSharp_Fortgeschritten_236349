namespace DelegatesEvents;

/// <summary>
/// Entwicklerseite -> Events definieren und ausführen
/// </summary>
public class Component
{
	public event Action ProcessStarted;

	public event Action ProcessEnded;

	public event Action<int> Progress;

	public void DoWork()
	{
		ProcessStarted?.Invoke(); //Hier immer ?.Invoke benutzen, falls der User keine Methode hier anhängt
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i);
		}
		ProcessEnded?.Invoke();
	}
}
