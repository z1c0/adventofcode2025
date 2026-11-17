using aoc;

Day.Mode = Mode.Full;
Day.Expect(38158151648, 1);
Day.Run(ParseInput, Part1, Part2);

static long Part1(IEnumerable<(long from, long to)> ranges)
{
	var invalid = 0L;
	foreach (var (from, to) in ranges)
	{
		for (var i = from; i <= to; i++)
		{
			var digits = GetDigits(i);
			if (digits % 2 == 0)
			{
				var n = (long)Math.Pow(10, digits / 2);
				var left = i / n;
				var right = i % n;
				if (left - right == 0)
				{
					invalid += i;
				}
			}
		}
	}
	return invalid;
}

static long Part2(IEnumerable<(long from, long to)> ranges) => 0;

static IEnumerable<(long from, long to)> ParseInput(string fileName) => Input.ReadLongLongList(fileName, ",", "-");

static int GetDigits(long n) =>	n == 0L ? 1 : (n > 0L ? 1 : 2) + (int)Math.Log10(Math.Abs((double)n));