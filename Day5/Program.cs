using System.Collections.Immutable;
using aoc;

Day.Expect(Mode.Sample, 3, 14);
Day.Expect(Mode.Full, 661, 359526404143208);
Day.Run(ReadInput, Part1, Part2);

static long Part1((ImmutableList<(long from, long to)> ranges, ImmutableList<long> ids) input) =>
	input.ids.Count(id => input.ranges.Any(r => id >= r.from && id <= r.to));

static long Part2((ImmutableList<(long from, long to)> ranges, ImmutableList<long>) input)
{
	var intervals = new List<(long value, bool open)>();
	input.ranges.ForEach(r => 
	{
		intervals.Add((r.from, true));
		intervals.Add((r.to, false));
	});
	intervals.Sort((i, j) => 
	{
		var n = i.value.CompareTo(j.value);
		if (n == 0)
		{
			var x = i.open ? 1 : 0;
			var y = j.open ? 1 : 0;
			return y.CompareTo(x);
		}
		return n;
	});
	var open = 0;
	var from = 0L;
	var ranges = new List<(long from, long to)>();
	intervals.ForEach(i =>
	{
		if (i.open)
		{
			if (open == 0)
			{
				from = i.value;
			}
			open++;
		}
		else
		{
			open--;
			if (open == 0)
			{
				if (ranges.Any() && ranges.Last().to == from)
				{
					from++;
				}
				ranges.Add((from, i.value));
			}
		}
	});
	return ranges.Sum(r => r.to - r.from + 1);
}

static (ImmutableList<(long, long)>, ImmutableList<long>) ReadInput(string fileName)
{
	var (block1, block2) = Input.ReadBlocks(fileName);
	return (Input.ParseLongLongList(block1, "-"), Input.ParseLongList(block2));
}
