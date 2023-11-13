namespace DelegatesEvents;

public class Delegates
{
	/// <summary>
	/// Delegate: Eigener Typ, mit einem Methodenaufbau (Rückgabetyp, Name und Parametern)
	/// Kann instanziert werden wie ein Objekt, und kann Methodenzeiger halten
	/// Das Delegate kann danach ausgeführt werden
	/// </summary>
	public delegate void Vorstellung(string name);

	static void Main(string[] args)
	{
		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung des Delegates mit einer Initialmethode
		v("Max"); //Delegate ausführen

		v += VorstellungDE; //Methode anhängen
		v("Tim");

		v += VorstellungEN;
		v("Uwe");

		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE; //Hier tritt kein Fehler auf, wenn die Methode nicht angehängt ist
		v("Mia");

		v -= VorstellungEN; //Wenn die letzte Methode abgenommen wird, ist das Delegate null
		v("Max");

		if (v is not null)
			v("Max");

		v.Invoke("Max"); //Führt das Delegate aus

		//Null Propagation: Wenn das Objekt nicht null ist, führe die Methode aus
		v?.Invoke("Max");

		foreach (Delegate dg in v.GetInvocationList()) //Hier die Methoden die angehängt sind beobachten
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	static void VorstellungDE(string name) => Console.WriteLine($"Hallo mein Name ist {name}");

	static void VorstellungEN(string name) => Console.WriteLine($"Hello my name ist {name}");
}