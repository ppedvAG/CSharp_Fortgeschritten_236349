namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		//https://github.com/dotnet/runtime

		object o = 4;
		if (o is int i)
		{
            Console.WriteLine(i);
        }

		if (o.GetType() == typeof(int))
		{
			int j = (int) o;
			Console.WriteLine(j);
		}

		//Typvergleich mit is beachtet auch Vererbungshierarchien
		//GetType == typeof ist ein genauer Typvergleich

		switch (o)
		{
			//Nicht möglich
			//case typeof(int):
			//	break;
			//case int:
			//	break;

			//Typ Switch
			case int k:
				break;
			case long l:
				break;
		}

		string Convert(string parameter) => parameter + ", ";

		int x = 1_754_321_957;
		double d = 2_8_35_798_327_521.321_598_732_957_321_985;

		//class und struct

		//class
		//Referenztyp
		//Wenn eine Variable von einem Klassentyp angelegt wird, werden Objekte die in dieser Variable gespeichert werden referenziert
		//Wenn zwei Variablen von einem Klassentyp verglichen werden, werden die Speicheradressen verglichen
		Test t = new Test() { Name = "Max" };
		Test t2 = t; //In t2 steht jetzt eine Referenz auf das Objekt hinter t
		t.Name = "Neuer Name"; //Hinter t und t2 befindet sich das gleiche Objekt
        Console.WriteLine(t.GetHashCode());
        Console.WriteLine(t2.GetHashCode());

		//struct
		//Wertetyp
		//Wenn eine Variable von einem Structtyp angelegt wird, werden Objekte die in dieser Variable gespeichert werden die Inhalte kopiert
		//Wenn zwei Variablen von einem Structtyp verglichen werden, werden die Inhalte verglichen
		int original = 5;
		int neu = original; //Inhalt von original wird kopiert
		original = 10;

		//ref struct
		//Referenzierbares Struct
		int original2 = 5;
		ref int neu2 = ref original2; //Hier wird jetzt eine Referenz auf original2 erzeugt
		original2 = 10;

		Test("Max", "Mustermann");
		Test("Max", "", 30);
		Test();

		//Methode mit 20 Parametern -> Anstrengend
		Test(alter: 30, vorname: "Max"); //Beliebige Reihenfolge von Parametern mit dem Bezeichner

		int z = 5;
		ref readonly int referenz = ref z;

		Test t3 = new Test() { Name = "" };
		int abc = t3 switch
		{
			var (nt) => 0
		};

		string name = "ThOMaS";
		string fixName = char.ToUpper(name[0]) + name[1..].ToLower();

		//Null-Coaslescing Operator (??): Wenn die linke Seite null ist, nimm die rechte Seite, sonst links
		List<int> list = new List<int>();

		List<int> list2;
		if (list != null)
			list2 = list;
		else
			list2 = new List<int>();

		list2 = list != null ? list : new List<int>();

		list2 = list ?? new List<int>();

		//Nullable Structs
		//Normalerweise sind Wertetypen nicht nullable
		//int n = null; Nicht möglich
		int? n = null;

		string str = null;
		if (str == "") //str may be null here
		{

		}
		if (str != null) //Dadurch: str is not null here
		{
			if (str == "") //str may be null here
			{

			}
		}

		string tagBezeichnung = DateTime.Now.DayOfWeek switch
		{
			>= DayOfWeek.Monday and <= DayOfWeek.Friday => "Wochentag",
			DayOfWeek.Saturday or DayOfWeek.Sunday => "Wochenende",
			_ => "Fehler"
		};

		Person p = new(0, "Max", 3000000);
		(int id, string vorname, decimal gehalt) = p; //Deconstruct wird beim Record auch erzeugt
													  //p.ID = 2;

		Stopwatch sw;
    }

	public static int Zahl = 5;

	public static int Referenz { get => Zahl; }

	/// <summary>
	/// Methode mit 3 optionalen Parametern
	/// </summary>
	public static void Test(string vorname = "", string nachname = "", int alter = 0)
	{

	}

	public static void Test2(in Test t, in int x)
	{
		t.Name = ""; //Variable kann verändert werden, aber nicht zugewiesen werden
					 //t = new Test();

		//x++;
		//x += 1;
		//x = x + 1;
		int y = x + 1;
	}

	public string Heute() => DateTime.Now.DayOfWeek switch
	{
		DayOfWeek.Monday when DateTime.Now.Year == 2023 => "Montag",
		DayOfWeek.Tuesday => throw new NotImplementedException(),
		DayOfWeek.Wednesday => throw new NotImplementedException(),
		DayOfWeek.Thursday => throw new NotImplementedException(),
		DayOfWeek.Friday => throw new NotImplementedException(),
		DayOfWeek.Saturday => throw new NotImplementedException(),
		DayOfWeek.Sunday => throw new NotImplementedException(),
		_ => throw new NotImplementedException()
	};
}

public class Test
{
	public string Name { get; set; }

	public Test Clone()
	{
		return this.MemberwiseClone() as Test;
	}

	public void Deconstruct(out string Name)
	{
		Name = this.Name;
	}
}

[Test<int>]
public record Person(int ID, string Name, decimal Gehalt)
{
	public void Test() { }
}

public class TestAttribute<T> : Attribute
{

}