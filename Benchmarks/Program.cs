using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace Benchmarks;

[MemoryDiagnoser(false)]
public class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<JsonBenchmarks>();
	}
}

public class JsonBenchmarks
{
	public string Json;

	[Params(50_000, 100_000, 500_000)]
	public int Amount;

	[GlobalSetup]
	public void Setup()
	{
		List<Fahrzeug> randomFzg = new();
		for (int i = 0; i < Amount; i++)
		{
			randomFzg.Add(new()
			{
				ID = i,
				MaxV = Random.Shared.Next(150, 400),
				Marke = (FahrzeugMarke) Random.Shared.Next(0, 3)
			});
		}
		Json = System.Text.Json.JsonSerializer.Serialize(randomFzg);
	}

	[Benchmark]
	[IterationCount(50)]
	public void SystemJsonDeserialize()
	{
		System.Text.Json.JsonSerializer.Deserialize<List<Fahrzeug>>(Json);
	}

	[Benchmark]
	[IterationCount(50)]
	public void NewtonsoftJsonDeserialize()
	{
		Newtonsoft.Json.JsonConvert.DeserializeObject<List<Fahrzeug>>(Json);
	}

	[Benchmark]
	[IterationCount(50)]
	public void SystemJsonPerHandDeserialize()
	{
		System.Text.Json.JsonDocument doc = System.Text.Json.JsonDocument.Parse(Json);
		List<Fahrzeug> readFzg = new();
		foreach (System.Text.Json.JsonElement element in doc.RootElement.EnumerateArray())
		{
			readFzg.Add(new()
			{
				ID = element.GetProperty("ID").GetInt32(),
				MaxV = element.GetProperty("MaxV").GetInt32(),
				Marke = (FahrzeugMarke) element.GetProperty("Marke").GetInt32()
			});
		}
	}

	[Benchmark]
	[IterationCount(50)]
	public void NewtonsoftJsonPerHandDeserialize()
	{
		JToken doc = JToken.Parse(Json);
		List<Fahrzeug> readFzg = new();
		foreach (JToken token in doc)
		{
			readFzg.Add(new()
			{
				ID = token["ID"].Value<int>(),
				MaxV = token["MaxV"].Value<int>(),
				Marke = (FahrzeugMarke) token["Marke"].Value<int>()
			});
		}
	}
}

public class Fahrzeug
{
	public int ID { get; set; }

	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }
}

public enum FahrzeugMarke { Audi, BMW, VW }