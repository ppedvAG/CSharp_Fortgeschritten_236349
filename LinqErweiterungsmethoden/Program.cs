using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LinqErweiterungsmethoden;

public class Program
{
	static void Main(string[] args)
	{
		#region Listentheorie
		//IEnumerable
		//Interface, welches einer Klasse ermöglicht, Iteriert zu werden
		//-> Den Enumerator solange ansprechen, bis eine bestimmte Anzahl Elemente heraus kommen

		//Ein Objekt vom Typ IEnumerable ist eine Anleitung zum Erstellen der fertigen Liste
		//Ein Objekt vom Typ IEnumerable enthält keine Werte
		IEnumerable<int> zahlen = Enumerable.Range(0, 100); //Rückgabetyp IEnumerable -> Eine Anleitung zum Erstellen der Zahlen von 0 bis 99
		//Expanding the Results View will enumerate the IEnumerable

		IEnumerable<int> vieleZahlen = Enumerable.Range(0, 1_000_000_000); //1ms, nur eine Anleitung
		//Enumerable.Range(0, 1_000_000_000).ToList(); //5.5s, hier wird die Anleitung erzeugt und Resourcen (RAM) werden verbraucht

		//Enumerator
		//Grundkomponente von allen Listentypen, wird verwendet um eine Liste zu iterieren
		//3 Teile:
		//MoveNext(): Bewegt den Zeiger um ein Element weiter
		//Current: Gibt das Element zurück, auf das der Zeiger gerade zeigt
		//Reset(): Schiebt den Zeiger auf die Position 0 zurück

		//foreach per Hand mit Enumerator
		List<int> foreachPerHand = new();
		IEnumerator<int> enumerator = zahlen.GetEnumerator();
		start:
		bool more = enumerator.MoveNext();
		foreachPerHand.Add(enumerator.Current);
		if (more)
			goto start;
		//enumerator.Reset();

		foreach (int x in zahlen) //.GetEnumerator()
		{
			//x = Current
			//...
			//.MoveNext()
		}
		//.Reset()

		//Beim ausiterieren von einem IEnumerable werden die konkreten Werte erzeugt
		// foreach (int y in vieleZahlen) continue;

		vieleZahlen.Where(e => e % 2 == 0); //Erzeugt auch nur eine Anleitung mithilfe des Predicates, die erst erzeugt wird, wenn dieses IEnumerable ausiteriert wird
		#endregion

		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		ints.First(); //Gibt das erste Element zurück, Exception wenn kein Element gefunden wurde
		ints.FirstOrDefault(); //Gibt das erste Element zurück, default wenn kein Element gefunden wurde

		ints.Last(); //Gibt das letzte Element zurück, Exception wenn kein Element gefunden wurde
		ints.LastOrDefault(); //Gibt das letzte Element zurück, default wenn kein Element gefunden wurde

		//Console.WriteLine(ints.First(e => e % 50 == 0)); //Finde das erste Element, dass restlos durch 50 teilbar ist (-> Exception)
		Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0)); //Finde das erste Element, dass restlos durch 50 teilbar ist (-> 0)
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Linq mit Objektliste
		//Predicate und Selector

		//Predicate hat als Ergebnis einen bool -> Bedingung
		//Beispiele: Where, Count, All/Any, ...

		//Selector hat als Ergebnis ein T
		//Beispiele: Select, Sum/Average/Min/Max, OrderBy, ...

		fahrzeuge.Where(e => e.MaxV >= 200); //Predicate weil ==
		fahrzeuge.Select(e => e.MaxV); //Selector weil kein Operator

		fahrzeuge.Select(e => e.MaxV >= 200); //Liste mit bool, entsprechend von MaxV

		//Select
		//Transformiert eine Liste in eine neue Form
		//2 Anwendungsfälle:

		//1. Fall (80%): Einzelnes Feld entnehmen
		fahrzeuge.Select(e => e.MaxV); //Liste der Geschwindigkeiten

		//2. Fall (20%): Liste transformieren
		fahrzeuge.Select(e => $"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV}km/h fahren.");

		//In einem Ordner alle Dateien ohne Pfad und Endung auflisten
		string[] pfade = Directory.GetFiles(@"C:\Windows\System32");
		List<string> pfadeOhneEndung = new();
		foreach (string pfad in pfade)
			pfadeOhneEndung.Add(Path.GetFileNameWithoutExtension(pfad));

		//Mit Linq:
		List<string> pfade2 = Directory.GetFiles(@"C:\Windows\System32")
									   .Select(Path.GetFileNameWithoutExtension)
									   .ToList();
        Console.WriteLine(pfadeOhneEndung.SequenceEqual(pfade2));

		//Summiere alle Zeilenanzahlen in einem Verzeichnis von .txt Dateien auf mit Linq
		Directory
			.GetFiles(@"C:\Windows")
			.Where(e => Path.GetExtension(e) == ".txt")
			.Sum(e => File.ReadAllLines(e).Length);

		//Liste casten
		Enumerable.Range(0, 50).Select(e => (long) e);

		//Top 3 schnellste Fahrzeuge
		fahrzeuge.OrderByDescending(e => e.MaxV).Take(3);

		//Chunk: Teilt die Liste in X große Teile auf
		fahrzeuge.Chunk(5);

		//SelectMany: Glättet eine Liste
		fahrzeuge.Chunk(5).SelectMany(e => e); //IEnumerable<Fahrzeug[]> -> IEnumerable<Fahrzeug>

		//GroupBy: Erzeugt Gruppen anhand eines Kriteriums, und fügt alle Elemente in ihre entsprechende Gruppe ein
		//Das schnellste Fahrzeug pro Marke
		fahrzeuge.GroupBy(e => e.Marke); //Audi-Gruppe, BMW-Gruppe, VW-Gruppe
		fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(e => e.Key, e => e.OrderByDescending(x => x.MaxV).First()); //2 Lambda-Expressions: KeySelector, ValueSelector

		fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(e => e.Key, e => e.ToList()); //IGrouping<TKey, TValue> mit ToList() zu einer einfachen Liste konvertieren


		//Aggregate: Wendet auf jedes Element einer Liste eine Funktion an, und schreibt das Ergebnis in den Aggregator
		fahrzeuge.Aggregate(0, (agg, fzg) => agg + fzg.MaxV); //Das Ergebnis der Lambda-Expression wird bei jedem Element in den Aggregator geschrieben

		//Liste printen
		string.Join(',', fahrzeuge.Select(e => $"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV}km/h fahren.")); //string.Join herumwrappen ist anstrengend

        Console.WriteLine
		(
			fahrzeuge
				.Aggregate(new StringBuilder(), (sb, e) => sb.AppendLine($"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV}km/h fahren."))
				.ToString()
		);
		#endregion

		#region Erweiterungsmethoden
		37529327.Quersumme();
		int z = 43872;
        Console.WriteLine(z.Quersumme());

		//Erweiterungsmethode, die eine Liste randomized
		fahrzeuge.Shuffle();

		//Erweiterungsmethode, die eine Liste printed, mit einem Selektor
        Console.WriteLine(fahrzeuge);
        Console.WriteLine(fahrzeuge.AsString(e => e));
        Console.WriteLine(fahrzeuge.AsString(e => e.Marke));
        Console.WriteLine(fahrzeuge.AsString(e => e.MaxV));
        Console.WriteLine(fahrzeuge.AsString(e => (e.MaxV, e.Marke, e.GetHashCode())));
        Console.WriteLine(fahrzeuge.AsString(e => $"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV}km/h fahren"));
        #endregion
    }
}

public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public enum FahrzeugMarke { Audi, BMW, VW }