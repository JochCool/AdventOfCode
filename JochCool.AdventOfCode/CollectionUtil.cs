namespace JochCool.AdventOfCode;

static class CollectionUtil
{
	public static T[][] CreateJaggedArray<T>(Vector<int> size)
	{
		T[][] result = new T[size.Y][];
		for (int y = 0; y < size.Y; y++)
		{
			result[y] = new T[size.X];
		}
		return result;
	}

	public static Vector<int> PositionOf<T>(this T[][] grid, T value) where T : IEqualityOperators<T, T, bool>
	{
		for (int y = 0; y < grid.Length; y++)
		{
			T[] line = grid[y];
			for (int x = 0; x < line.Length; x++)
			{
				if (line[x] == value)
				{
					return new(x, y);
				}
			}
		}
		throw new ArgumentException($"{value} is not in the grid.", nameof(value));
	}

	public static Vector<int> PositionOf(this string[] grid, char value)
	{
		for (int y = 0; y < grid.Length; y++)
		{
			string line = grid[y];
			for (int x = 0; x < line.Length; x++)
			{
				if (line[x] == value)
				{
					return new(x, y);
				}
			}
		}
		throw new ArgumentException($"{value} is not in the grid.", nameof(value));
	}

	public static void PrintGrid<T>(T[][] grid, Func<T, char> charSelector)
	{
		foreach (T[] line in grid)
		{
			char[] chars = new char[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				chars[i] = charSelector(line[i]);
			}
			Console.WriteLine(chars);
		}
	}
}
