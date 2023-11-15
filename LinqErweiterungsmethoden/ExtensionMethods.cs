using System.Text;

namespace LinqErweiterungsmethoden;

public static class ExtensionMethods
{
	public static int Quersumme(this int x) => x.ToString().Sum(e => (int) char.GetNumericValue(e));

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> x)
	{
		//Dictionary<T, int> keyValuePairs = new Dictionary<T, int>();
		//foreach (T t in x)
		//	keyValuePairs.Add(t, Random.Shared.Next());
		//return keyValuePairs.OrderBy(e => e.Value).Select(e => e.Key);
		return x.OrderBy(e => Random.Shared.Next());
	}

	public static string AsString<T, TSelector>(this IEnumerable<T> x, Func<T, TSelector> selector)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append($"[{string.Join(", ", x.Select(e => selector(e)))}]"); //Wendet auf jedes Element der Liste den gegebenen Selector an, und verkettet die Elemente mit Komma und Abstand
		return sb.ToString();
	}
}
