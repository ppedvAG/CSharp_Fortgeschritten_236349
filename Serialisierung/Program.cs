using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using CsvHelper;
using System.Globalization;

namespace Serialisierung;

public class Program
{
	static void Main(string[] args)
	{
		SystemJson();

		NewtonsoftJson();

		XML();

		CSV();
	}

	public static string CreatePaths()
	{
		//Path, File, Directory
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		return filePath;
	}

	public static void SystemJson()
	{
		//string file = CreatePaths();

		//List<Fahrzeug> fahrzeuge = new()
		//{
		//	new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		//	new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		//	new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		//	new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		//	new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		//	new Fahrzeug(5, 133, FahrzeugMarke.VW),
		//	new Fahrzeug(6, 253, FahrzeugMarke.VW),
		//	new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		//	new Fahrzeug(8, 151, FahrzeugMarke.VW),
		//	new Fahrzeug(9, 250, FahrzeugMarke.VW),
		//	new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		//	new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		//};

		////Teil 2: Options
		//JsonSerializerOptions options = new(); //WICHTIG: Options müssen beim (De-)Serialisieren mitübergeben werden
		//options.WriteIndented = true;
		//options.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Kreise ignorieren

		////Teil 1: Objekte Serialisieren
		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(file, json);

		//string readJson = File.ReadAllText(file);
		//List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson, options);

		////Teil 3: Attribute
		////Werden auf die Klasse angewandt
		////Vererbung mit Json: JsonDerivedType Attribut seit .NET 7.0
		////Benötigt alle Typen in der Vererbungshierarchie als Attribute auf der Klasse/Interface + Typ Diskriminator

		////Teil 4: Json per Hand durchgehen
		////Json Dateien in C# hineinzuladen benötigt normalerweise Datenklassen -> Aufwändig bei großen Json Dateien
		////Json kopieren -> Edit -> Paste Special -> Paste JSON as Classes
		//JsonDocument doc = JsonDocument.Parse(File.ReadAllText("history.city.list.min.json")); //Rechtsklick auf File -> Properties -> Copy to Output Directory
		//ArrayEnumerator ae = doc.RootElement.EnumerateArray();
		//foreach (JsonElement element in ae)
		//{
		//	Console.WriteLine(element.GetProperty("city").GetProperty("name").GetString());
		//	Console.WriteLine(element.GetProperty("city").GetProperty("coord").GetProperty("lon").GetDouble());
		//	Console.WriteLine(element.GetProperty("city").GetProperty("coord").GetProperty("lat").GetDouble());
		//	Console.WriteLine("------------------------");
		//}
	}

	public static void NewtonsoftJson()
	{
		string file = CreatePaths();

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new PKW(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//Teil 2: Settings
		JsonSerializerSettings settings = new(); //WICHTIG: Settings müssen beim (De-)Serialisieren mitübergeben werden
		settings.Formatting = Newtonsoft.Json.Formatting.Indented;
		settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Kreise ignorieren
		settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung ermöglichen

		//Teil 1: Objekte Serialisieren
		string json = JsonConvert.SerializeObject(fahrzeuge, settings);
		File.WriteAllText(file, json);

		string readJson = File.ReadAllText(file);
		List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson, settings);

		//Teil 3: Attribute
		//Werden auf die Klasse angewandt

		//Teil 4: Json per Hand durchgehen
		//Json Dateien in C# hineinzuladen benötigt normalerweise Datenklassen -> Aufwändig bei großen Json Dateien
		//Json kopieren -> Edit -> Paste Special -> Paste JSON as Classes
		JToken doc = JToken.Parse(File.ReadAllText("history.city.list.min.json")); //Rechtsklick auf File -> Properties -> Copy to Output Directory
		foreach (JToken element in doc)
		{
			Console.WriteLine(element["city"]["name"].Value<string>());
			Console.WriteLine(element["city"]["coord"]["lon"].Value<double>());
			Console.WriteLine(element["city"]["coord"]["lat"].Value<double>());
			Console.WriteLine("------------------------");
		}
	}

	public static void XML()
	{

		string file = CreatePaths();

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//Teil 1
		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(file))
		{
			xml.Serialize(sw, fahrzeuge);
		}

		using (StreamReader sr = new StreamReader(file))
		{
			List<Fahrzeug> readfzg = xml.Deserialize(sr) as List<Fahrzeug>; //as-Cast: Gibt null zurück wenn der Cast nicht funktioniert
																			//List<Fahrzeug> readfzg2 = (List<Fahrzeug>) xml.Deserialize(sr); //Der reguläre Cast wirft eine Exception wenn der Cast nicht funktioniert
		}

		//Teil 2: Attribute
		//Vererbung über XmlInclude

		//Teil 3:
		XmlDocument doc = new XmlDocument();
		doc.Load(file);
		foreach (XmlElement element in doc.DocumentElement) //mit .DocumentElement den Header überspringen
		{
			Console.WriteLine(element.GetElementsByTagName("ID")[0].InnerText);
			Console.WriteLine(element.GetAttribute("MaxV"));
			Console.WriteLine(element.GetAttribute("Marke"));
			Console.WriteLine("-----------------------------");
		}
	}

	public static void CSV()
	{
		string file = CreatePaths();

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//TextFieldParser tfp = new TextFieldParser(file);
		//tfp.SetDelimiters(";");
		//List<string[]> lines = new();
		//while (!tfp.EndOfData)
		//{
		//	lines.Add(tfp.ReadFields());
		//}

		using (StreamWriter sw = new StreamWriter(file))
		{
			CsvWriter writer = new CsvWriter(sw, CultureInfo.CurrentCulture);
			writer.WriteRecords(fahrzeuge);
		}

		using (StreamReader sr = new StreamReader(file))
		{
			CsvReader reader = new CsvReader(sr, CultureInfo.CurrentCulture);
			IEnumerable<Fahrzeug> readFzg = reader.GetRecords<Fahrzeug>();
		}
	}
}

//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

//[XmlInclude(typeof(Fahrzeug))]
//[XmlInclude(typeof(PKW))]

[DebuggerDisplay("ID: {ID}, MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug
{
	//System.Text.Json
	//[JsonIgnore]
	//[JsonPropertyName("Identifier")]
	//[XmlAttribute]
	public int ID { get; set; }

	//Newtonsoft.Json
	//[JsonIgnore]
	//[JsonProperty("Maximalgeschwindigkeit")]
	[XmlAttribute]
	public int MaxV { get; set; }

	//[XmlIgnore]
	//[XmlElement(ElementName = "Marke")]
	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int iD, int maxV, FahrzeugMarke marke)
	{
		ID = iD;
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }
}

public class PKW : Fahrzeug
{
	public PKW(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke)
	{
	}

    public PKW() : base()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }