namespace JochCool.AdventOfCode;

static class CollectionUtil
{
	public static T[] Repeat<T>(ReadOnlySpan<T> span, int count)
	{
		if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
		if (count == 0) return [];

		T[] result = new T[count * span.Length];
		int resultI = 0;
		for (int i = 0; i < count; i++)
		{
			span.CopyTo(result.AsSpan(resultI, span.Length));
			resultI += span.Length;
		}
		return result;
	}

	public static T[] Repeat<T>(ReadOnlySpan<T> span, int count, T separator)
	{
		if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
		if (count == 0) return [];

		T[] result = new T[count * (span.Length + 1) - 1];
		int resultI = 0;
		int i = 0;
		while (true)
		{
			span.CopyTo(result.AsSpan(resultI, span.Length));
			resultI += span.Length;

			i++;
			if (i == count) return result;

			result[resultI] = separator;
			resultI++;
		}
	}

	public static bool IsInBounds<T>(this T[,] grid, Vector<int> position)
	{
		unchecked
		{
			return (uint)position.Y < (uint)grid.GetLength(0) && (uint)position.X < (uint)grid.GetLength(1);
		}
	}

	public static bool IsSorted<T>(ReadOnlySpan<T> span, IComparer<T> comparer)
	{
		for (int i = 1; i < span.Length; i++)
		{
			if (comparer.Compare(span[i - 1], span[i]) > 0)
			{
				return false;
			}
		}
		return true;
	}

	public static bool JaggedArraysEqual<T>(T[][] a, T[][] b) where T : IEquatable<T>
	{
		if (a.Length != b.Length) return false;
		for (int i = 0; i < a.Length; i++)
		{
			if (!MemoryExtensions.SequenceEqual<T>(a[i], b[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static T[][] CreateJaggedArray<T>(Vector<int> size)
	{
		T[][] result = new T[size.Y][];
		for (int y = 0; y < size.Y; y++)
		{
			result[y] = new T[size.X];
		}
		return result;
	}

	public static T[][] CopyJaggedArray<T>(T[][] grid)
	{
		T[][] result = new T[grid.Length][];
		for (int i = 0; i < grid.Length; i++)
		{
			T[] oldArray = grid[i];
			T[] newArray = result[i] = new T[oldArray.Length];
			Array.Copy(oldArray, newArray, oldArray.Length);
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

	public static Vector<int> PositionOf(this char[,] grid, char value)
	{
		int height = grid.GetLength(0);
		int width = grid.GetLength(1);
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (grid[y, x] == value)
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

	public static void PrintGrid(char[][] grid)
	{
		foreach (char[] line in grid)
		{
			Console.WriteLine(line);
		}
	}

	public static void AddToKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
		where TKey : notnull
		where TValue : IAdditionOperators<TValue, TValue, TValue>
	{
		if (!dictionary.TryAdd(key, value))
		{
			checked
			{
				dictionary[key] += value;
			}
		}
	}
}
