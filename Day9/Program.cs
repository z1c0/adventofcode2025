using System.Collections.Immutable;
using aoc;

using Point = (int x, int y);

Day.Expect(Mode.Sample, 50, 24);
Day.Expect(Mode.Full, 4729332959, 1474477524);
Day.Run(Input.ReadIntIntList, Part1, Part2);

static long Part1(ImmutableList<Point> input) => Solve(input, false);

static long Part2(ImmutableList<Point> input) => Solve(input, true);

static bool Contains(ImmutableList<Point> poly, Point p)
{
	bool inside = false;
	for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
	{
		var a = poly[j];
		var b = poly[i];
		var cross = (long)(b.x - a.x) * (p.y - a.y) - (long)(b.y - a.y) * (p.x - a.x);

		// on the edge itself -> count as inside
		if (cross == 0 &&
			p.x >= Math.Min(a.x, b.x) && p.x <= Math.Max(a.x, b.x) &&
			p.y >= Math.Min(a.y, b.y) && p.y <= Math.Max(a.y, b.y))
		{
			return true;
		}
		// edge straddles the horizontal ray going right from p, and p is left of the crossing
		if ((a.y > p.y) != (b.y > p.y) && (cross > 0) == (b.y > a.y))
		{
			inside = !inside;
		}
	}
	return inside;
}

static bool CheckBounds(Point a, Point b, ImmutableList<Point> input)
{
	var x1 = Math.Min(a.x, b.x);
	var y1 = Math.Min(a.y, b.y);
	var x2 = Math.Max(a.x, b.x);
	var y2 = Math.Max(a.y, b.y);
	return RectInside(input, x1, y1, x2, y2);
}

static bool RectInside(ImmutableList<Point> poly, int x1, int y1, int x2, int y2)
{
	for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
	{
		if (CrossesInterior(poly[j], poly[i], x1, y1, x2, y2))
		{
			return false;
		}
	}
	return Contains(poly, new Point((x1 + x2) / 2, (y1 + y2) / 2));
}

// does segment a-b pass through the open rectangle?
static bool CrossesInterior(Point a, Point b, int x1, int y1, int x2, int y2)
{
	// separated by one of the rectangle's own axes
	if (Math.Max(a.x, b.x) <= x1 || Math.Min(a.x, b.x) >= x2 ||
		Math.Max(a.y, b.y) <= y1 || Math.Min(a.y, b.y) >= y2)
		return false;

	// separated by the segment's line: all four corners on one (closed) side
	long dx = (long)b.x - a.x, dy = (long)b.y - a.y;
	long Side(int px, int py) => dx * ((long)py - a.y) - dy * ((long)px - a.x);

	long s1 = Side(x1, y1), s2 = Side(x2, y1), s3 = Side(x2, y2), s4 = Side(x1, y2);
	bool anyPos = s1 > 0 || s2 > 0 || s3 > 0 || s4 > 0;
	bool anyNeg = s1 < 0 || s2 < 0 || s3 < 0 || s4 < 0;
	return anyPos && anyNeg;   // corners on both sides -> the segment cuts through
}
static long Solve(ImmutableList<Point> input, bool boundsCheck)
{
	var max = 0L;
	for (var i = 0; i < input.Count; i++)
	{
		for (var j = i + 1; j < input.Count; j++)
		{
			var a = input[i];
			var b = input[j];
			if (!boundsCheck || CheckBounds(a, b, input))
			{
				var n = (Math.Abs(a.x - b.x) + 1L) * (Math.Abs(a.y - b.y) + 1L);
				max = Math.Max(max, n);
			}
		}
	}
	return max;
}
