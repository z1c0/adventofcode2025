using System.Collections;

namespace aoc
{
	public static class Output
	{
		public static void Print<T>(IEnumerable<T> list) =>
			Console.WriteLine($"[{string.Join(", ", list)}]");
		
		public static void Print(BitArray bits) =>
			System.Console.WriteLine($"[{string.Concat(bits.Cast<bool>().Select(b => b ? '#' : '.'))}]");
	}
}
