namespace JochCool.AdventOfCode.Year2024.Day04;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] lines = inputReader.ReadAllLines();

		int count = 0;

		// Look for the middle 'A's, then check surrounding characters
		for (int y = 1; y < lines.Length - 1; y++)
		{
			for (int x = 1; x < lines[y].Length - 1; x++)
			{
				if (lines[y][x] == 'A' &&
					AreMS(lines[y - 1][x - 1], lines[y + 1][x + 1]) &&
					AreMS(lines[y - 1][x + 1], lines[y + 1][x - 1]))
				{
					count++;
				}
			}
		}

		return count.ToInvariantString();
	}

	private static bool AreMS(char c1, char c2)
	{
		return c1 == 'M' && c2 == 'S' || c1 == 'S' && c2 == 'M';
	}
}
