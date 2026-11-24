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

		public static void Expect<T>(Mode mode, T? expected1, T? expected2 = default)
		{
			_expected[mode] = (expected1?.ToString(), expected2?.ToString());
		}

		public static Mode Mode { get; private set; }
		
		public static void Run<RET>(Func<IEnumerable<string>, RET>? part1, Func<IEnumerable<string>, RET>? part2 = null) =>
			Run(Input.ReadStringList, part1, part2);

		public static void Run<INPUT, RET>(Func<string, INPUT> readInput, Func<INPUT, RET>? part1, Func<INPUT, RET>? part2 = null) =>
			Run(readInput, part1, readInput, part2);

		public static void Run<INPUT1, INPUT2, RET>(Func<string, INPUT1> readInput1, Func<INPUT1, RET>? part1, Func<string, INPUT2> readInput2, Func<INPUT2, RET>? part2)
		{
			Run(Mode.Sample, readInput1, part1, readInput2, part2);
			Run(Mode.Full, readInput1, part1, readInput2, part2);
		}

		private static void Run<INPUT1, INPUT2, RET>(Mode mode, Func<string, INPUT1> readInput1, Func<INPUT1, RET>? part1, Func<string, INPUT2> readInput2, Func<INPUT2, RET>? part2)
		{
			if (_expected.TryGetValue(mode, out var e))
			{
				Mode = mode;
				var name = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
				Console.WriteLine();
				Console.WriteLine($"START {name} ({mode} mode)");
				var sw = Stopwatch.StartNew();
				var (expected1, expected2) = _expected[mode];
				Mode = mode;
				Invoke(part1, readInput1(GetFileName(mode, 1)), expected1);
				Invoke(part2, readInput2(GetFileName(mode, 2)), expected2);
				Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
			}
		}

		private static string GetFileName(Mode mode, int part)
		{
			string rootDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
			if (mode == Mode.Full)
			{
				return Path.Combine(rootDirectory, Input.DEFAULT_INPUT_FILENAME);
			}
			var fileName = Path.Combine(rootDirectory, Input.DEFAULT_SAMPLE_INPUT_FILENAME);
			if (!Path.Exists(fileName))
			{
				fileName = Path.ChangeExtension(fileName, null) + part + Path.GetExtension(fileName);
			}
			return fileName;
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