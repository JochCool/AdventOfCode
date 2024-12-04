namespace JochCool.AdventOfCode.Year2024.Day04;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] lines = inputReader.ReadAllLines();

		const string word = "XMAS";
		string word2 = StringUtil.Reverse(word);

		int count = 0;

		// Forwards & backwards
		foreach (string line in lines)
		{
			count += StringUtil.CountSubstring(line, word) + StringUtil.CountSubstring(line, word2);
		}

		// Downwards & upwards
		for (int y = 0; y <= lines.Length - word.Length; y++)
		{
			for (int x = 0; x < lines[y].Length; x++)
			{
				if (IsWordDownwardsAt(lines, word, y, x) || IsWordDownwardsAt(lines, word2, y, x))
				{
					count++;
				}
			}
		}

		// Diagonal right-down and left-up
		for (int y = 0; y <= lines.Length - word.Length; y++)
		{
			for (int x = 0; x <= lines[y].Length - word.Length; x++)
			{
				if (IsWordRightDownwardsAt(lines, word, y, x) || IsWordRightDownwardsAt(lines, word2, y, x))
				{
					count++;
				}
			}
		}

		// Diagonal left-down and right-up
		for (int y = 0; y <= lines.Length - word.Length; y++)
		{
			for (int x = word.Length - 1; x < lines[y].Length; x++)
			{
				if (IsWordLeftDownwardsAt(lines, word, y, x) || IsWordLeftDownwardsAt(lines, word2, y, x))
				{
					count++;
				}
			}
		}

		return count.ToInvariantString();
	}

	private static bool IsWordDownwardsAt(string[] lines, string word, int y, int x)
	{
		for (int i = 0; i < word.Length; i++)
		{
			if (lines[y + i][x] != word[i])
			{
				return false;
			}
		}
		return true;
	}

	private static bool IsWordRightDownwardsAt(string[] lines, string word, int y, int x)
	{
		for (int i = 0; i < word.Length; i++)
		{
			if (lines[y + i][x + i] != word[i])
			{
				return false;
			}
		}
		return true;
	}

	private static bool IsWordLeftDownwardsAt(string[] lines, string word, int y, int x)
	{
		for (int i = 0; i < word.Length; i++)
		{
			if (lines[y + i][x - i] != word[i])
			{
				return false;
			}
		}
		return true;
	}
}
