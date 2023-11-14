using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace DelegatesEvents;

public class ActionFunc
{
	static List<int> ints = Enumerable.Range(0, 50).ToList();

	static void Main(string[] args)
	{
		//Action, Predicate, Func: Vorgegebene Delegates, die an verschiedenen Stellen in der Sprache vorkommen
		//z.B.: Linq, Multitasking, Reflection, ...
		//Essentiell für die Fortgeschrittene Programmierung
		//Können alles was in dem vorherigen Teil besprochen wurde

		//Action: Delegate mit void als Rückgabetyp und bis zu 16 Parametern
		Action<int, int> action = Addiere;
		action(3, 5);
		action?.Invoke(3, 5);

		//DoAction über den Action Parameter konfigurieren
		DoAction(4, 8, action);
		DoAction(4, 8, new Action<int, int>(Addiere));
		DoAction(4, 8, Subtrahiere); //Hier Methodenzeiger direkt übergeben, ohne new Action(...)

		//Praktisches Beispiel: List.ForEach
		ints.ForEach(Quadriere); //Benötigt eine Action mit einem Parameter (int)
		void Quadriere(int n) => Console.WriteLine($"{n}^2={n * n}");


		//Func: Delegate mit einem Rückgabetyp und bis zu 16 Parametern
		//WICHTIG: Letztes Generic ist der Rückgabetyp
		Func<int, int, double> func = Multipliziere;
		double d = func(4, 5);
		double? d2 = func?.Invoke(4, 5); //Wenn die Func null ist, kommt hier null heraus
		double d3 = func?.Invoke(4, 5) == null ? double.NaN : func.Invoke(4, 5); //Wenn die Func null ist, schreibe NaN in die Variable
		double d4 = func?.Invoke(4, 5) ?? double.NaN; //Null Coalescing Operator

		func += Dividiere;
		double d5 = func(3, 4); //Die letzte angehängte Methode ist das Ergebnis
		//Schleife auf GetInvocationList(), jedes Delegate in diesem Array ausführen

		DoFunc(4, 5, func);
		DoFunc(4, 5, Multipliziere);
		DoFunc(4, 5, Dividiere);

		//Praktisches Beispiel: Linq
		ints.Where(CheckDiv2);
		bool CheckDiv2(int x) => x % 2 == 0;

		//Anonyme Methoden
		//Methoden ohne direkte Initialisierung -> Methoden ohne Name, die direkt nach der Verwendung weggeworfen werden
		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form
		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anonyme Methoden als Parameter
		ints.Where(e => { return e % 2 == 0; });
		ints.Where(e => e % 2 == 0);

		DoAction(4, 8, (x, y) => Console.WriteLine(Math.Pow(x, y)));
		DoFunc(3, 9, (x, y) => x % y);
	}

	#region Action
	static void Addiere(int x, int y) => Console.WriteLine(x + y);

	static void Subtrahiere(int x, int y) => Console.WriteLine(x - y);

	static void DoAction(int x, int y, Action<int, int> action) => action?.Invoke(x, y);
	#endregion

	#region Func
	static double Multipliziere(int x, int y) => x * y;

	static double Dividiere(int x, int y) => x / y;

	static double DoFunc(int x, int y, Func<int, int, double> func) => func?.Invoke(4, 5) ?? double.NaN;
	#endregion
}
