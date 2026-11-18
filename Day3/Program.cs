using System.Collections.Immutable;
using aoc;

Day.Mode = Mode.Sample;
Day.Expect(1, 1);
Day.Run(ParseInput, Part1, Part2);

static int Part1(ImmutableList<ImmutableList<int>> banks)
{
	var total = 0;
	foreach (var bank in banks)
	{
		var jolts = 0;
		Console.WriteLine(jolts);
		total += jolts;
	}
	return total;
}

static int Part2(ImmutableList<ImmutableList<int>> banks) => 0;

static ImmutableList<ImmutableList<int>> ParseInput(string fileName) =>
	[.. Input.ReadStringList(fileName).Select(s => s.Select(c => c - '0').ToImmutableList()).ToImmutableList()];
