using aoc;

Day.Expect(Mode.Sample, 21, 40);
Day.Expect(Mode.Full, 1646, 32451134474991);
Day.Run(Input.ReadCharGrid, Part1, Part2);

static long Part1(Grid<char> grid)
{
	var start = grid.Find('S');
	grid[start] = '|';
	var splitCount = 0;
	for (var y = 0; y < grid.Height - 1; y++)
	{
		var beams = grid.FindAll(c => c.y == y && grid[c] == '|');
		foreach (var b in beams)
		{
			Cell below = (b.x, b.y + 1);
			if (grid[below] == '^')
			{
				splitCount++;
				grid[(below.x - 1, below.y)] = '|';
				grid[(below.x + 1, below.y)] = '|';
			}
			else
			{
				grid[below] = '|';
			}
		}
	}
	return splitCount;
}

static long Part2(Grid<char> grid)
{
	var cache = new Dictionary<Cell, long>();

	long ResolvedCached(Cell cell)
	{
		if (cache.TryGetValue(cell, out long value))
		{
			return value;
		}
		var t = Resolve(cell);
		cache.Add(cell, t);
		return t;
	}

	long Resolve(Cell c)
	{
		Cell below = (c.x, c.y + 1);
		if (below.y == grid.Height - 1)
		{
			return 1;
		}
		if (grid[below] == '^')
		{
			return
				ResolvedCached((below.x - 1, below.y)) +
				ResolvedCached((below.x + 1, below.y));
		}
		else
		{
			return ResolvedCached(below);
		}
	}
	
	return Resolve(grid.Find('S'));
}
