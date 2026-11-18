using System.Diagnostics;
using System.Reflection;

namespace aoc
{
	public enum Mode
	{
		Sample,
		Full
	}

	public static class Day
	{
		private static readonly Dictionary<Mode, (string? expected1, string? expected2)> _expected = [];

		public static void Expect<T1, T2>(Mode mode, T1? expected1, T2? expected2 = default)
		{
			_expected[mode] = (expected1?.ToString(), expected2?.ToString());
		}
		
		public static void Run<RET1, RET2>(Func<IEnumerable<string>, RET1>? part1, Func<IEnumerable<string>, RET2>? part2 = null) =>
			Run(Input.ReadStringList, part1, part2);

		public static void Run<INPUT, RET1, RET2>(Func<string, INPUT> readInput, Func<INPUT, RET1>? part1, Func<INPUT, RET2>? part2 = null)
		{
			Run(Mode.Sample, readInput, part1, readInput, part2);
		}
		public static void Run<INPUT1, RET1, INPUT2, RET2>(Func<string, INPUT1> readInput1, Func<INPUT1, RET1>? part1, Func<string, INPUT2> readInput2, Func<INPUT2, RET2>? part2)
		{
			Run(Mode.Sample, readInput1, part1, readInput2, part2);
			Run(Mode.Full, readInput1, part1, readInput2, part2);
		}

		private static void Run<INPUT1, RET1, INPUT2, RET2>(Mode mode, Func<string, INPUT1> readInput1, Func<INPUT1, RET1>? part1, Func<string, INPUT2> readInput2, Func<INPUT2, RET2>? part2)
		{
			if (_expected.TryGetValue(mode, out var e))
			{
				var name = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
				string rootDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
				Console.WriteLine();
				Console.WriteLine($"START {name} ({mode} mode)");
				var sw = Stopwatch.StartNew();
				var fileName = Path.Combine(rootDirectory, mode == Mode.Full ? Input.DEFAULT_INPUT_FILENAME : Input.DEFAULT_SAMPLE_INPUT_FILENAME);
				var (expected1, expected2) = _expected[mode];
				Invoke(part1, readInput1(fileName), expected1);
				Invoke(part2, readInput2(fileName), expected2);
				Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
			}
		}

		private static void Invoke<INPUT, RET>(Func<INPUT, RET>? func, INPUT input, string? expected)
		{
			if (func != null)
			{
				try
				{
					var s = func(input)?.ToString();
					if (expected != null)
					{
						if (expected != s)
						{
							ConsoleEx.Error($"ERROR: expected '{expected}', actual '{s}'");
						}
						else
						{
							ConsoleEx.Success($"-> {s}");
						}
					}
					else
					{
						ConsoleEx.Info($"-> {s}");
					}
				}
				catch (Exception e)
				{
					ConsoleEx.Error($"ERROR: {e}");
				}
			}
		}
	}
}