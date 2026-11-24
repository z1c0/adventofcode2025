using aoc;

using Placement = (int w, int h, int count);

Day.Expect(Mode.Sample, 1, 1);
//Day.Expect(Mode.Full, 1, 1);
Day.Run(ReadInput, Part1, Part2);

static int Part1(IEnumerable<Placement> input)
{
	var count = 0;
	foreach (var i in input)
	{
		System.Console.WriteLine(i.w * i.h);
		System.Console.WriteLine(i.count * 7);
	}
	return count;
}

static int Part2(IEnumerable<Placement> input) => 0;

static IEnumerable<Placement> ReadInput(string fileName)
{
	foreach (var line in Input.ReadStringList(fileName))
	{
		if (line.Contains('x'))
		{
			var tokens = line.Split(": ");
			var dims = Input.ParseIntList(tokens.First(), "x");
			yield return (dims.First(), dims.Last(), Input.ParseIntList(tokens.Last()).Sum());
		}
	}
}