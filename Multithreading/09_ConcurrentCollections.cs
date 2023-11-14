using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;

namespace Multithreading;

internal class _09_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> bag = new();
		bag.Add(1);

		ConcurrentDictionary<int, string> dict = new();
		dict.TryAdd(1, "123");
		dict.AddOrUpdate(1, "123", (key, value) => "");
		dict.GetOrAdd(1, "123");

		SynchronizedCollection<int> list = new(); //Hat einen Index und ist geordnet im Gegensatz zum ConcurrentBag
        Console.WriteLine(list[1]);
	}
}
