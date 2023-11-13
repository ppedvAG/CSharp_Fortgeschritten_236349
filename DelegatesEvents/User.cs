namespace DelegatesEvents;

/// <summary>
/// Anwenderseite
/// </summary>
public class User
{
	static void Main(string[] args)
	{
		//Über die Events kann jetzt angegeben werden, was mit den Meldungen passiert
		//z.B. Ausgabe über GUI, Datenbank, Mobile Benachrichtigung, ...
		Component comp = new Component();
		comp.ProcessStarted += () => Console.WriteLine("Prozess gestartet");
		comp.ProcessEnded += () => Console.WriteLine("Prozess fertig");
		//comp.Progress += (x) => Console.WriteLine($"Fortschritt: {x}");
		comp.DoWork();
	}
}
