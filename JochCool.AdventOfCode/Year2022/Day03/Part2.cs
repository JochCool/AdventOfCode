namespace JochCool.AdventOfCode.Year2022.Day03;

public static class Part2
{
	const int totalLetters = 26;

	public static string? Solve(TextReader inputReader)
	{
		int total = 0;
		int[] timesFound = new int[totalLetters * 2];

		int groupI = 0;
		foreach (string line in inputReader.ReadLines())
		{
			for (int i = 0; i < line.Length; i++)
			{
				int priority = GetPriority(line[i]);
				if (timesFound[priority] == groupI)
				{
					timesFound[priority]++;
				}
			}

			groupI++;
			if (groupI == 3)
			{
				int priority = Array.IndexOf(timesFound, 3);
				if (priority == -1)
				{
					Console.Error.WriteLine("No duplicates found.");
					continue;
				}
				total += priority + 1;
				groupI = 0;
				Array.Clear(timesFound);
			}
		}

		return total.ToInvariantString();
	}

	static int GetPriority(char @char)
	{
		switch (@char)
		{
			case >= 'A' and <= 'Z':
			{
				return @char - 'A' + totalLetters;
			}
			case >= 'a' and <= 'z':
			{
				return @char - 'a';
			}
			default:
			{
				throw new ArgumentException("Unknown character: " + @char, nameof(@char));
			}
		}
	}
}
