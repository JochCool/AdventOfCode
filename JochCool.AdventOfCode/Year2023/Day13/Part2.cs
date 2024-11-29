namespace JochCool.AdventOfCode.Year2023.Day13;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;

		foreach (string[] pattern in inputReader.ReadLineGroups())
		{
			int value = FindLineSymmetry(pattern, pattern.Length, HorizontalDifference);
			if (value != 0)
			{
				value *= 100;
			}
			else
			{
				value = FindLineSymmetry(pattern, pattern[0].Length, VerticalDifference);
			}

			Console.WriteLine(value);
			sum += value;
		}

		return sum.ToInvariantString();
	}

	private static int FindLineSymmetry(string[] grid, int size, Func<string[], int, int, int> lineDifferenceComparer)
	{
		for (int i = 1; i < size; i++)
		{
			// Put a mirror here and go outwards in both directions to see if it's actually mirrorred, with one smudge allowed
			int totalDifference = 0;
			int lowI = i - 1;
			int highI = i;
			do
			{
				totalDifference += lineDifferenceComparer(grid, lowI, highI);
				if (totalDifference > 1)
				{
					break;
				}
				lowI--;
				highI++;
			}
			while (lowI >= 0 && highI < size);

			if (totalDifference == 1)
				return i;
		}
		return 0;
	}

	private static int HorizontalDifference(string[] grid, int y1, int y2)
	{
		int numDifferent = 0;
		string line1 = grid[y1];
		string line2 = grid[y2];
		for (int x = 0; x < line1.Length; x++)
		{
			if (line1[x] != line2[x])
			{
				numDifferent++;
			}
		}
		return numDifferent;
	}

	private static int VerticalDifference(string[] grid, int x1, int x2)
	{
		int numDifferent = 0;
		for (int y = 0; y < grid.Length; y++)
		{
			if (grid[y][x1] != grid[y][x2])
			{
				numDifferent++;
			}
		}
		return numDifferent;
	}
}
