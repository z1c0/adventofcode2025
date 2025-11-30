using System.Collections.Immutable;

namespace aoc
{
	public static class Output
	{
		public static void Print<T>(IEnumerable<T> list) =>
			Console.WriteLine($"[{string.Join(", ", list)}]");
	}
}
