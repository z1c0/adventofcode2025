using aoc;

Day.Expect(Mode.Sample, 5, 2);
Day.Expect(Mode.Full, 500, 287039700129600);
Day.Run(ReadInput, Part1, Part2);

static long Part1(Dictionary<string, List<string>> lines) => Solve([], lines, "you", true, true);

static long Part2(Dictionary<string, List<string>> lines) => Solve([], lines, "svr", false, false);

static long Solve(Dictionary<string, long> cache, Dictionary<string, List<string>> lines, string from, bool fft, bool dac)
{
	 var count = 0L;
	 var key = from + fft + dac;
	if (cache.TryGetValue(key, out var value))
	{
		return value;
	}
	foreach (var l in lines[from])
	{
		if (l == "out")
		{
			if (fft && dac)
			{
				count++;
			}
		}
		else
		{
			count += Solve(cache, lines, l, fft || l == "fft", dac || l  == "dac");
		}
	}
	cache[key] = count;
	return count;
}

static Dictionary<string, List<string>> ReadInput(string fileName)
{
	var lines = new Dictionary<string, List<string>>();
	foreach (var line in Input.ReadStringList(fileName))
	{
		var tokens = line.Split(": ");
		lines.Add(tokens.First(), [.. tokens.Last().Split(' ')]);
	}
	return lines;
}
