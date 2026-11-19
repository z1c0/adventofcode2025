using System.Collections.Immutable;
using aoc;

using JunctionBox = (long x, long y, long z);

Day.Expect(Mode.Sample, 40, 25272);
Day.Expect(Mode.Full, 122430, 8135565324);
Day.Run(Input.ReadLongLongLongList, Part1, Part2);

static long Part1(ImmutableList<JunctionBox> input) => Solve(input, true);

static long Part2(ImmutableList<JunctionBox> input) => Solve(input, false);

static long Solve(ImmutableList<JunctionBox> input, bool top3Only)
{
	var distances = new List<(double, JunctionBox jb1, JunctionBox jb2)>();
	for (var i = 0; i < input.Count; i++)
	{
		var p1 = input[i];
		for (var j = i + 1; j < input.Count; j++)
		{
			var p2 = input[j];
			var d = Math.Sqrt(
				(p2.x - p1.x) * (p2.x - p1.x) +
				(p2.y - p1.y) * (p2.y - p1.y) +
				(p2.z - p1.z) * (p2.z - p1.z)
			);
			distances.Add((d, p1, p2));
		}
	}
	distances.Sort((d1, d2) => d1.Item1.CompareTo(d2.Item1));

	var circuits = input.Select(jb => new HashSet<JunctionBox> { jb }).ToList();
	HashSet<JunctionBox> GetCircuit(JunctionBox jb)
	{
		foreach (var c in circuits)
		{
			if (c.Contains(jb))
			{
				return c;
			}
		}
		throw new InvalidOperationException();
	}

	if (top3Only)
	{
		distances = [.. distances.Take(Day.Mode == Mode.Full ? 1000: 10)];
	}
	foreach (var d in distances)
	{
		var c1 = GetCircuit(d.jb1);
		var c2 = GetCircuit(d.jb2);
		if (c1 != c2)
		{
			c1.UnionWith(c2);
			circuits.Remove(c2);
		}
		if (!top3Only && circuits.Count == 1)
		{
			return d.jb1.x * d.jb2.x;
		}
	}
	circuits.Sort((c1, c2) => c2.Count.CompareTo(c1.Count));
	return circuits.Take(3).Aggregate(1L, (a, c) => a * c.Count);
}
