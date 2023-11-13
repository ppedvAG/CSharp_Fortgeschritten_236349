namespace Generics;

public class Program
{
	static void Main(string[] args)
	{
		//Generisches Typargument (Generic)
		//Platzhalter für einen Typen, der danach überall innerhalb der Klasse/Methode eingesetzt wird
		//Wird häufig mit T (für Type) abgekürzt

		//T wird durch string ersetzt
		List<string> list = new List<string>();
		list.Add("123"); //T wird string

		List<int> ints = new List<int>();
		ints.Add(1); //T wird int

		Dictionary<int, string> dict = new();
		dict.Add(1, "123");

		int x = GetT<int>();
		string s = GetT<string>();
	}

	public static T GetT<T>()
	{
		return default;
	}
}

//Eigene Klasse mit Generic (generische Klasse)
public class DataStore<T> : IProgress<T>
{
	//Feld mit Generic
	private T[] data;

	//Property mit Generic
	public List<T> Data { get => data.ToList(); }

	//Methode mit Generic
	public void Add(T item, int index)
	{
		data[index] = item;
	}

	//T als Rückgabewert
	public T GetIndex(int index)
	{
		return data[index];
	}

	//T bei Vererbung
	public void Report(T value)
	{
		throw new NotImplementedException();
	}

	//Dieses T hier sollte anders benannt werden
	//Das Klassen-T ist hier noch sichtbar
	public void PrintType<T2>()
	{
        Console.WriteLine(typeof(T2)); //Den Typen des Generics herausfinden
        Console.WriteLine(nameof(T2)); //Gibt den Typen als String zurück
        Console.WriteLine(default(T2)); //Gibt den Standardwert zurück (int: 0, bool: false, Program: null, ...)

		T2 o = (T2) Convert.ChangeType(data[0], typeof(T2));
    }
}