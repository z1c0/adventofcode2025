using System.Collections;
using System.Collections.Immutable;
using System.Dynamic;
using aoc;

Day.Expect(Mode.Sample, 7, 1);
//Day.Expect(Mode.Full, 436, 1);
Day.Run(ReadInput, Part1, Part2);

static int Part1(IEnumerable<(BitArray lights, List<BitArray> buttons, ImmutableList<int> joltages)> input)
{
	var total = 0;
	foreach (var m in input)
	{
		var minPresses = int.MaxValue;
		foreach (var c in AllCombinations(m.buttons))
		{
			var result = c.Skip(1).Aggregate((BitArray)c[0].Clone(), (acc, b) => acc.Xor(b));
			if (ToInt(result) == ToInt(m.lights))
			{
				minPresses = Math.Min(minPresses, c.Count);
			}
		}
		total += minPresses;
	}
	return total;
}

static int Part2(IEnumerable<(BitArray lights, List<BitArray> buttons, ImmutableList<int> joltages)> input)
{
	var total = 0;
	foreach (var m in input)
	{
		List<List<int>> candidates = new();
		for (var d = 0; d < m.joltages.Count; d++)
		{
			List<List<int>> combinations = null;
			foreach (var c in combinations)
			{
				if (TestJoltage(c, m.joltages))
				{
					candidates.Add(c);
				}
			}
			//...
		}
	}
	return 0;
}

static int ToInt(BitArray bits)
{
	var arr = new int[1];
	bits.CopyTo(arr, 0);
	return arr[0];
}

static List<List<T>> AllCombinations<T>(List<T> input) =>
	input.Aggregate(
		new List<List<T>> { new() },
		(acc, x) => acc.Concat(acc.Select(s => new List<T>(s) { x })).ToList())
	.Skip(1)
	.ToList();

	
static IEnumerable<(BitArray lights, List<BitArray> buttons, ImmutableList<int> joltages)> ReadInput(string fileName)
{
	foreach (var line in Input.ReadStringList(fileName))
	{
		var tokens = Input.ParseStringList(line);
		var t = tokens.First()[1..^1];
		var lights = new BitArray(t.Length);
		for (var i = 0; i < t.Length; i++)
		{
			lights.Set(i, t[i] == '#');
		}
		var buttons = new List<BitArray>();
		foreach (var b in tokens.Skip(1).SkipLast(1))
		{
			var button = new BitArray(lights.Length);
			foreach (var n in Input.ParseIntList(b[1..^1], ","))
			{
				button.Set(n, true);
			}
			buttons.Add(button);
		}
		yield return
		(
			lights,
			buttons,
			Input.ParseIntList(tokens.Last()[1..^1], ",")
		);
	}
}
