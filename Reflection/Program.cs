using System.Reflection;

namespace Reflection;

public class Program
{
	public static void Main(string[] args)
	{
		//Type
		//Jedes Objekt hat einen Typen
		//Dieser kann erfasst werden mit der GetType() Methode
		Program p = new Program();
		Type ot = p.GetType();

		//Mit typeof(...) kann über einen Typnamen ein Type Objekt erzeugt werden
		Type pt = typeof(Program);

		Person person = new Person("Max", "Muster", 30);
		ListProperties(person);
        Console.WriteLine("---------------------------");
        ListMethods(person);

		//Private Methoden über Reflection angreifen
		person
			.GetType()
			.GetMethod("Test", BindingFlags.NonPublic | BindingFlags.Instance)
			.Invoke(person, null);

		//Activator
		//Anhand eines Types ein Object erzeugen
		object o = Activator.CreateInstance(pt);
		object o2 = Activator.CreateInstance(typeof(Person), new object[] { "Max", "Muster", 30 });
		//Convert.ChangeType(o, pt); //Typen eines Objekts ändern (z.B. Cast)

		//Assembly
		//Beinhält die Typen und Codebasis von einem Projekt
		Assembly.GetExecutingAssembly(); //Derzeitiges Projekt
		Assembly a = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_11_13\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll");
		//.dll ist der eigentliche Code, .exe ist ein Wrapper damit das Programm ausgeführt werden kann

		//Eventkomponente per Reflection erstellen
		object comp = Activator.CreateInstance(a.GetType("DelegatesEvents.Component"));
		comp.GetType().GetEvent("ProcessStarted").AddEventHandler(comp, () => Console.WriteLine("Reflection gestartet"));
		comp.GetType().GetEvent("ProcessEnded").AddEventHandler(comp, () => Console.WriteLine("Reflection beendet"));
		comp.GetType().GetEvent("Progress").AddEventHandler(comp, (int e) => Console.WriteLine($"Reflection Fortschritt {e}"));
		comp.GetType().GetMethod("DoWork").Invoke(comp, null);
    }

	public static void ListProperties(object o)
	{
		foreach (PropertyInfo info in o.GetType().GetProperties())
		{
			Console.WriteLine($"{info.Name}: {info.GetValue(o)}"); //GetValue: Gibt den Wert hinter einem Property anhand eines gegebenen Objekts zurück
		}
	}

	public static void ListMethods(object o)
	{
		foreach (MethodInfo info in o.GetType().GetMethods())
		{
            Console.WriteLine($"{info.Name}");
        }
	}
}

public record Person(string Vorname, string Nachname, int Alter)
{
	private void Test() => Console.WriteLine("Ich bin versteckt");
}