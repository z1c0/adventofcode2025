using System.Collections.Immutable;
using aoc;

Day.Expect(Mode.Sample, 357, 3121910778619);
Day.Expect(Mode.Full, 17179, 170025781683941);
Day.Run(ParseInput, Part1, Part2);

static long Part1(ImmutableList<ImmutableList<int>> banks) => Solve(banks, 2);

static long Part2(ImmutableList<ImmutableList<int>> banks) => Solve(banks, 12);

static ImmutableList<ImmutableList<int>> ParseInput(string fileName) =>
	[.. Input.ReadStringList(fileName).Select(s => s.Select(c => c - '0').ToImmutableList()).ToImmutableList()];

static long Solve(ImmutableList<ImmutableList<int>> banks, int digits)
{
	var total = 0L;
	foreach (var bank in banks)
	{
		var offset = 0;
		var joltage = 0L;
		for (var d = digits; d > 0; d--)
		{
			joltage *= 10;
			var (value, position) = FindMax(bank, offset, d - 1);
			offset = position + 1;
			joltage += value;
		}
		total += joltage;
	}
	return total;
}

static (int value, int position) FindMax(ImmutableList<int> bank, int from, int count)
{
	for (var n = 9; n >= 1; n--)
	{
		for (var i = from; i < bank.Count - count; i++)
		{
			var v = bank[i];
			if (v == n)
			{
				return (v, i);
			}
		}
	}
	throw new InvalidOperationException();
}