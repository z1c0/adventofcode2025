using aoc;

Day.Expect(Mode.Sample, 3, 6);
Day.Expect(Mode.Full, 1150, 6738);
Day.Run(ParseInput, Part1, Part2);

static int Part1(IEnumerable<(Direction direction, int value)> input) => Solve(input).exact;

static int Part2(IEnumerable<(Direction direction, int value)> input) => Solve(input).total;

static (int exact, int total) Solve(IEnumerable<(Direction direction, int value)> input)
{
	var zerosExact = 0;
	var zerosTotal = 0;
	var dial = 50;
	foreach (var line in input)
	{
		zerosTotal += line.value / 100;
		var value = line.value % 100;
		if (line.direction == Direction.Left)
		{
			if (dial != 0 && dial - value < 0)
			{
				zerosTotal++;
			}
			dial = (dial + 100 - value) % 100;
		}
		else
		{
			if (dial != 0 && dial + value > 100)
			{
				zerosTotal++;
			}
			dial = (dial + value) % 100;
		}
		if (dial == 0)
		{
			zerosExact++;
			zerosTotal++;
		}
	}
	return (zerosExact, zerosTotal);
}
static IEnumerable<(Direction direction, int value)> ParseInput(string fileName)
{
	var lines = Input.ReadStringList(fileName);
	foreach (var line in lines)
	{
		yield return (
			(line[0] == 'L') ? Direction.Left : Direction.Right,
			int.Parse(line.Substring(1))
		);
	}
}

enum Direction
{
	Left,
	Right
}