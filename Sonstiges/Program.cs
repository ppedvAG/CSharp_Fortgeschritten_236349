using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace Sonstiges;

public class Program
{
	static void Main(string[] args)
	{
		//Operatoren überladen
		//Verhalten von Operatoren verändern
		//z.B. string +, ==, != oder DateTime +, -, <, <=, >, >=, ==, !=

		Zug z = new Zug();
		z++;
		z++;
		z++;
		z++;

		Wagon w = new Wagon();
		z += w;

		//Zug neu = w; //Implizit
		Zug neu = (Zug) w; //Explizit

		foreach (Wagon item in z)
		{

		}

		z[2] = new Wagon();
		Console.WriteLine(z[2]);
		Console.WriteLine(z[20, "Rot"]);
    }
}

public class Zug : IEnumerable<Wagon>
{
	public List<Wagon> Wagons { get; } = new();

	public static Zug operator +(Zug self, Wagon w)
	{
		self.Wagons.Add(w);
		return self;
	}

	public static Zug operator ++(Zug self)
	{
		self.Wagons.Add(new Wagon());
		return self;
	}

	public IEnumerator<Wagon> GetEnumerator() => Wagons.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

	public Wagon this[int i]
	{
		get => Wagons[i];
		set => Wagons[i] = value;
	}

	public Wagon this[int anzSitze, string farbe]
	{
		get => Wagons.First(e => e.AnzSitze == anzSitze && e.Farbe == farbe);
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		return !(a == b);
	}

	//Implizite Umwandlung überschreiben
	//public static implicit operator Zug(Wagon a)
	//{
	//	return new Zug();
	//}

	//Implizite Umwandlung überschreiben
	public static explicit operator Zug(Wagon a)
	{
		return new Zug();
	}
}