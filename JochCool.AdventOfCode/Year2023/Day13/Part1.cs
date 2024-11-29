namespace JochCool.AdventOfCode.Year2023.Day13;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;

		foreach (string[] pattern in inputReader.ReadLineGroups())
		{
			int value = FindLineSymmetry(pattern, pattern.Length, HorizontalEquals);
			if (value != 0)
			{
				value *= 100;
			}
			else
			{
				value = FindLineSymmetry(pattern, pattern[0].Length, VerticalEquals);
			}

			Console.WriteLine(value);
			sum += value;
		}

		return sum.ToInvariantString();
	}

	private static int FindLineSymmetry(string[] grid, int size, Func<string[], int, int, bool> lineEqualityComparer)
	{
		for (int i = 1; i < size; i++)
		{
			// Put a mirror here and go outwards in both directions to see if it's actually mirrorred
			bool hasSymmetry = true;
			int lowI = i - 1;
			int highI = i;
			do
			{
				if (!lineEqualityComparer(grid, lowI, highI))
				{
					hasSymmetry = false;
					break;
				}
				lowI--;
				highI++;
			}
			while (lowI >= 0 && highI < size);

			if (hasSymmetry)
				return i;
		}
		return 0;
	}

	private static bool HorizontalEquals(string[] grid, int y1, int y2)
	{
		return grid[y1] == grid[y2];
	}

	private static bool VerticalEquals(string[] grid, int x1, int x2)
	{
		for (int y = 0; y < grid.Length; y++)
		{
			if (grid[y][x1] != grid[y][x2])
			{
				return false;
			}
		}
		return true;
	}
}
