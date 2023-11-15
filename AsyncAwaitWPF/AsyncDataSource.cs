using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	public async IAsyncEnumerable<int> GetIntsAsync()
	{
		//List<int> ints = new List<int>();
		//while (true)
		//	ints.Add(...);
		//return ints;

		while (true)
		{
			await Task.Delay(Random.Shared.Next(10, 100));
			yield return Random.Shared.Next(); //yield: Wirft ein Ergebnis in das IEnumerable, bis die Funktion zu Ende ist
		}
	}
}