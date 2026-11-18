using aoc;

Day.Expect(Mode.Sample, 4277556, 3263827);
Day.Expect(Mode.Full, 6891729672676, 9770311947567);
Day.Run(ReadInput, Part1, Input.ReadCharGrid, Part2);

static long Part1(List<(char operation, List<long> numbers)> problems) =>
	problems.Sum(p => Calculate(p.operation, p.numbers));

static long Part2(Grid<char> grid)
{
	grid = grid.InsertColumn(grid.Width, ' ');
	var grandTotal = 0L;
	var operation = ' ';
	var numbers = new List<long>();
	for (var x = 0; x < grid.Width; x++)
	{
		var o = grid[(x, grid.Height - 1)];
		if (o != ' ')
		{
			operation = o;
		}
		var n = 0L;
		for (var y = 0; y < grid.Height - 1; y++)
		{
			var c = grid[(x, y)];
			if (c != ' ')
			{
				n *= 10;
				n += c - '0';
			}
		}
		if (n > 0)
		{
			numbers.Add(n);
		}
		if (n == 0)
		{
			grandTotal += Calculate(operation, numbers);
			numbers.Clear();
		}
	}
	return grandTotal;
}

static long Calculate(char operation, List<long> numbers) =>
	operation switch
	{
		'+' => numbers.Sum(),
		'*' => numbers.Aggregate((n1, n2) => n1 * n2),
		_ => throw new InvalidOperationException(),
	};

static List<(char operation, List<long> numbers)> ReadInput(string fileName)
{
	var problems = new List<(char operation, List<long> numbers)>();
	var lines = Input.ReadStringList(fileName);
	Input.ParseStringList(lines.Last()).ForEach(t => problems.Add((t[0], new())));
	foreach (var line in lines.SkipLast(1))
	{
		var numbers = Input.ParseIntList(line);
		for (var i = 0; i < numbers.Count; i++)
		{
			problems[i].numbers.Add(numbers[i]);
		}
	}	
	return problems;
}
