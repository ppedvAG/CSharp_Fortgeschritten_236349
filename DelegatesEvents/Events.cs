namespace DelegatesEvents;

public class Events
{
	/// <summary>
	/// Event: Statischer Punkt (der nicht statisch sein muss), an den Methoden angehängt werden können -> Delegate
	/// Kann nicht instanziert werden
	/// 
	/// Zweigeteilte Entwicklung
	/// 1. Entwicklerseite, definiert die Events und führt sie aus
	/// 2. Anwenderseite, hängt an den Eventpunkt eine Methode an
	/// 
	/// Beispiel: Button -> Click
	/// Entwicklerseite: Definiert die Bedingungen für das Ausführen der Methode (Maus im Button, Linksklick, keine UI Elemente darüber, ...)
	/// Anwenderseite: Hängt eine Methode an das Event an -> Der eigentliche Code der beim Klicken des Buttons ausgeführt wird
	/// </summary>
	event EventHandler TestEvent;

	event EventHandler<TestEventArgs> ArgsEvent;

	event EventHandler<int> IntEvent;

	static void Main(string[] args) => new Events().Run();

	public void Run()
	{
		TestEvent += Events_TestEvent; //Anwenderseite
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite (this für die Komponente selbst, EventArgs.Empty wenn es keine Daten gibt)

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent?.Invoke(this, new TestEventArgs() { Status = "Erfolg" });

		IntEvent += Events_IntEvent;
		IntEvent?.Invoke(this, 10);
	}

	//Diese Methode übernimmt die Struktur des Delegates
	private void Events_TestEvent(object sender, EventArgs e)
	{
        Console.WriteLine("Keine Ahnung");
	}

	private void Events_ArgsEvent(object sender, TestEventArgs e)
	{
        Console.WriteLine(e.Status);
	}

	private void Events_IntEvent(object sender, int e)
	{
        Console.WriteLine(e);
    }
}

public class TestEventArgs : EventArgs
{
	public string Status { get; set; }
}