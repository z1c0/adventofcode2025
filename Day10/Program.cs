using System.Collections.Immutable;
using aoc;
using Google.OrTools.LinearSolver;

Day.Expect(Mode.Sample, 7, 33);
Day.Expect(Mode.Full, 436, 14999);
Day.Run(ReadInput, Part1, Part2);

static int Part1(IEnumerable<(ImmutableList<int> lights, List<ImmutableList<int>> buttons, ImmutableList<int> joltages)> input)
{
	static int ToInt(ImmutableList<int> bits)
	{
		var i = 0;
		foreach (var b in bits)
		{
			i += 1 << b;
		}
		return i;
	}

	var total = 0;
	foreach (var m in input)
	{
		var minPresses = int.MaxValue;
		foreach (var c in AllCombinations(m.buttons))
		{
			var result = c.Aggregate(0, (acc, b) => acc ^ ToInt(b));
			if (result == ToInt(m.lights))
			{
				minPresses = Math.Min(minPresses, c.Count);
			}
		}
		total += minPresses;
	}
	return total;
}

static int Part2(IEnumerable<(ImmutableList<int> lights, List<ImmutableList<int>> buttons, ImmutableList<int> joltages)> input)
{
	static int SolveIntegerLinearProgramming(double[,] A, double[] r)
	{
		var rows = A.GetLength(0);
		var cols = A.GetLength(1);

		var solver = Solver.CreateSolver("SCIP");
		var v = solver.MakeIntVarArray(cols, 0, double.PositiveInfinity, "v");
		for (var i = 0; i < rows; i++)
		{
			var con = solver.MakeConstraint(r[i], r[i]);
			for (var j = 0; j < cols; j++)
			{
				if (A[i, j] != 0)
				{
					con.SetCoefficient(v[j], A[i, j]);
				}
			}
		}
		var obj = solver.Objective();
		for (var j = 0; j < cols; j++)
		{
			obj.SetCoefficient(v[j], 1);
		}
		obj.SetMinimization();
		solver.Solve();
		return (int)v.Sum(x => x.SolutionValue());
	}
	
	var result = 0;
	foreach (var m in input)
	{
		var A = new double[m.joltages.Count, m.buttons.Count];
		var y = 0;
		foreach (var b in m.buttons)
		{
			foreach (var d in b)
			{
				A[d, y] = 1;
			}
			y++;
		}
		var r = m.joltages.Select(j => (double)j).ToArray();
		result += SolveIntegerLinearProgramming(A, r);
	}
	return result;
}

static IEnumerable<ImmutableList<T>> AllCombinationsOfSize<T>(IEnumerable<T> items, int n) =>
	n == 0
		? new[] { ImmutableList<T>.Empty }
		: items.SelectMany((item, i) =>
			AllCombinationsOfSize(items.Skip(i).ToList(), n - 1)
				.Select(rest => rest.Insert(0, item)));

static List<List<T>> AllCombinations<T>(List<T> input) =>
	input.Aggregate(
		new List<List<T>> { new() },
		(acc, x) => acc.Concat(acc.Select(s => new List<T>(s) { x })).ToList())
	.Skip(1)
	.ToList();

	
static IEnumerable<(ImmutableList<int> lights, List<ImmutableList<int>> buttons, ImmutableList<int> joltages)> ReadInput(string fileName)
{
	foreach (var line in Input.ReadStringList(fileName))
	{
		var tokens = Input.ParseStringList(line);
		var t = tokens.First()[1..^1];
		var lights = Enumerable.Range(0, t.Length)
			.Where(i => t[i] == '#')
			.ToImmutableList();
		var buttons = new List<ImmutableList<int>>();
		foreach (var b in tokens.Skip(1).SkipLast(1))
		{
			buttons.Add(Input.ParseIntList(b[1..^1], ","));
		}
		yield return
		(
			lights,
			buttons,
			Input.ParseIntList(tokens.Last()[1..^1], ",")
		);
	}
}
