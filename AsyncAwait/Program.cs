using System.Diagnostics;

namespace AsyncAwait;

public class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();

		//Toast();
		//Geschirr();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s, Synchron

		//sw.Restart();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Geschirr);
		//Task t3 = Task.Run(Kaffee);
		//Task.WaitAll(t1, t2, t3); //WaitAll ist eine blockierende Operation -> Unpraktisch
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//sw.Restart();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Geschirr).ContinueWith(t => Kaffee());
		//Task.WaitAll(t1, t2); //WaitAll ist eine blockierende Operation -> Unpraktisch
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Async und Await
		//await blockiert nicht den Main Thread, im Hintergrund wird vom Compiler ein Task.Run und ContinueWith Konstrukt aufgebaut

		//Wenn eine Async Methode gestartet wird, die einen Task zurückgibt, wird diese als Task gestartet

		//Es gibt drei verschiedene Aufbauten von Async Methoden:
		//async void: Kann selbst await benutzen, kann aber nicht awaited werden
		//async Task: Kann selbst await benutzen und kann von außen awaited werden
		//async Task<T>: Kann selbst await benutzen, kann von außen awaited werden und hat zusätzlich ein Ergebnis in Form eines Objekts

		//sw.Restart();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Geschirr).ContinueWith(t => Kaffee());
		//Task.WaitAll(t1, t2); //WaitAll ist eine blockierende Operation -> Unpraktisch
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//sw.Restart();
		//Task t1 = ToastAsync(); //Hier wird direkt der Toast gestartet
		//Task t2 = GeschirrAsync(); //Hier wird die Tasse geholt
		//await t2; //Warte hier bis t2 fertig ist
		//Task t3 = KaffeeAsync(); //Hier wird der Kaffee gestartet, muss auf Tasse warten durch await
		//await t3; //Warte hier auf den Kaffee
		//await t1; //Warte hier auf den Toast
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//sw.Restart();
		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Tasse> t2 = GeschirrObjectAsync();
		//Tasse tasse = await t2; //await kann auch Objekte zurückgeben, wenn der Task ein Objekt als Ergebnis produziert
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse); //Hier das fertige Tasse Objekt entnehmen
		//Kaffee kaffee = await t3; //Hier kommt ein Kaffee Objekt zurück
		//Toast toast = await t1; //Hier kommt ein Toast Objekt zurück
		//Fruehstueck f = new Fruehstueck(toast, kaffee);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//await kann auch in Expressions verwendet werden
		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Kaffee> t3 = KaffeeObjectAsync(await GeschirrObjectAsync());
		//Fruehstueck f = new Fruehstueck(await t1, await t3);

		//Task.Run
		//Task.Run ist awaitable, und kann dadurch jeden beliebigen C# Code asynchron machen
		Task<List<int>> t = Task.Run(() => Enumerable.Range(0, 1_000_000_000).ToList()); //11.5s
		//...
		List<int> ints = await t; //11.5s im Hintergrund

		//Task.WhenAll & Task.WhenAny
		Task<int> t1 = null, t2 = null, t3 = null;
		//await t1, t2, t3; //Nicht möglich, await kann immer nur einen Task awaiten
		await Task.WhenAll(t1, t2, t3); //Auf beliebig viele Tasks warten
		int[] x = await Task.WhenAll(t1, t2, t3); //Auf beliebig viele Tasks mit Ergebnis warten

		Task<int> ergebnisTask = await Task.WhenAny(t1, t2, t3); //Auf den ersten Task warten, das erste Ergebnis kommt hier heraus

		//Aufbau:
		//Starte die Aufgabe -> Task t = ...
		//Zwischenschritte
		//Warte auf das Ende der Aufgabe: await t
	}

	#region Synchron
	public static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	public static void Geschirr()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr fertig");
	}

	public static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	public static async Task ToastAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		//Hier ist Task als Rückgabetyp definiert, aber es wird kein return benötigt
	}

	public static async Task GeschirrAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr fertig");
	}

	public static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron mit Ergebnis
	public static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	public static async Task<Tasse> GeschirrObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr fertig");
		return new Tasse();
	}

	public static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public class Toast { }

public class Tasse { }

public record Kaffee(Tasse t);

public record Fruehstueck(Toast toast, Kaffee kaffee);