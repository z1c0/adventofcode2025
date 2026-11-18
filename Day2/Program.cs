using aoc;

Day.Expect(Mode.Sample, 1227775554, 4174379265);
Day.Expect(Mode.Full, 38158151648, 45283684555);
Day.Run(ParseInput, Part1, Part2);

static long Part1(IEnumerable<(long from, long to)> ranges) => Solve(ranges, true);

static long Part2(IEnumerable<(long from, long to)> ranges) => Solve(ranges, false);

static long Solve(IEnumerable<(long from, long to)> ranges, bool symmetricOnly)
{
	var invalid = 0L;
	foreach (var (from, to) in ranges)
	{
		for (var id = from; id <= to; id++)
		{
			if (IsInvalidId(id, symmetricOnly))
			{
				invalid += id;
			}
		}
	}
	return invalid;
}

static IEnumerable<(long from, long to)> ParseInput(string fileName) => Input.ReadLongLongList(fileName, ",", "-");

static int GetDigits(long n) =>	n == 0L ? 1 : (n > 0L ? 1 : 2) + (int)Math.Log10((double)n);

static bool IsInvalidId(long id, bool symmetricOnly)
{
	var digits = GetDigits(id);
	for (var groupSize = 1; groupSize < digits; groupSize++)
	{
		if (digits % groupSize == 0)
		{
			if (symmetricOnly && (digits % 2 != 0 || groupSize != digits / 2)) // only even ids
			{
				continue;
			}
			var chunks = digits / groupSize;
			var equal = true;
			var prev = 0L;
			var tmpId = id;
			for (var i = 0; i < chunks; i++)
			{
				var val = 0L;
				for (var j = 0; j < groupSize; j++)
				{
					val *= 10;
					val += tmpId % 10;
					tmpId /= 10;
				}
				if (i > 0)
				{
					if (val !=  prev)
					{
						equal = false;
						break;
					}
				}
				prev = val;
			}
			if (equal)
			{
				return true;
			}
		}
	}
	return false;
}