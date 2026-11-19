using aoc;

Day.Expect(Mode.Sample, 13, 43);
Day.Expect(Mode.Full, 1474, 8910);
Day.Run(Input.ReadCharGrid, Part1, Part2);

static int Part1(Grid<char> grid) => FindRollsToRemove(grid).Count();

static int Part2(Grid<char> grid)
{
	var total = 0;
	while (true)
	{
		var rolls = FindRollsToRemove(grid);
		if (!rolls.Any())
		{
			break;
		}
		rolls.ForEach(r => grid[r] = '.');
		total += rolls.Count;
	}
	return total;
}

static List<Cell> FindRollsToRemove(Grid<char> grid) =>
	grid
		.FindAll(((Cell p, char v) t) => t.v == '@' && grid.CountAdjacent8(t.p, '@') < 4)
		.Select(t => t.p)
		.ToList();
