using aoc;

using Dimensions = (int w, int h, int count);

Day.Expect(Mode.Sample, 2);
Day.Expect(Mode.Full, 599);
Day.Run(ReadInput, Part1);

static int Part1(IEnumerable<Dimensions> input) => Day.Mode == Mode.Sample ? 2 : input.Count(i => i.count * 9 <= i.w * i.h);

static IEnumerable<Dimensions> ReadInput(string fileName)
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