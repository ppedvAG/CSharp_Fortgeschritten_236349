using System.Diagnostics;

namespace Multitasking;

internal class _06_ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000 };
		for (int i = 0; i < durchgänge.Length; i++)
		{
			int d = durchgänge[i];

			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Durchgänge: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Durchgänge: {d}, {sw2.ElapsedMilliseconds}ms");

			Console.WriteLine("------------------------------------------------------------");
		}

		/*
			For Durchgänge: 1000, 0ms
			ParallelFor Durchgänge: 1000, 1058ms
			------------------------------------------------------------
			For Durchgänge: 10000, 3ms
			ParallelFor Durchgänge: 10000, 2ms
			------------------------------------------------------------
			For Durchgänge: 50000, 16ms
			ParallelFor Durchgänge: 50000, 23ms
			------------------------------------------------------------
			For Durchgänge: 100000, 34ms
			ParallelFor Durchgänge: 100000, 148ms
			------------------------------------------------------------
			For Durchgänge: 250000, 128ms
			ParallelFor Durchgänge: 250000, 96ms
			------------------------------------------------------------
			For Durchgänge: 500000, 237ms
			ParallelFor Durchgänge: 500000, 115ms
			------------------------------------------------------------
			For Durchgänge: 1000000, 316ms
			ParallelFor Durchgänge: 1000000, 180ms
			------------------------------------------------------------
			For Durchgänge: 5000000, 1277ms
			ParallelFor Durchgänge: 5000000, 299ms
			------------------------------------------------------------
			For Durchgänge: 10000000, 1864ms
			ParallelFor Durchgänge: 10000000, 603ms
			------------------------------------------------------------
			For Durchgänge: 100000000, 19287ms
			ParallelFor Durchgänge: 100000000, 9329ms
			------------------------------------------------------------
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i =>
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}
