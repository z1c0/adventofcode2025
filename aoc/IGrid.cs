global using Cell = (int x, int y);
using System.Text;

namespace aoc
{
	public interface IGrid<T> where T : struct
	{	
		public void Print();
		public void PrintTo(StringBuilder sb);
		public T this[Cell p] { get; set; }
		public T this[int x, int y] { get; set; }
		public IEnumerable<Cell> FindAll(T t);
		public IEnumerable<(Cell p, T v)> FindAll(Func<(Cell p, T v), bool> predicate);
		public void ForEach(Action<(Cell p, T v)> callback);
	}
}