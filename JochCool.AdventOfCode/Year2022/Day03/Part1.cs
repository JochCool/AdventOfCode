namespace JochCool.AdventOfCode.Year2022.Day03;

public static class Part1
{
	const int totalLetters = 26;

	public static string? Solve(TextReader inputReader)
	{
		int total = 0;
		bool[] found = new bool[totalLetters * 2];

		foreach (string line in inputReader.ReadLines())
		{
			if ((line.Length & 1) == 1)
			{
				Console.Error.WriteLine("Odd number of characters:");
				Console.Error.WriteLine(line);
				continue;
			}

			for (int i = 0; i < line.Length/2; i++)
			{
				found[GetPriority(line[i])] = true;
			}

			char duplicate = '\0';
			for (int i = line.Length/2; i < line.Length; i++)
			{
				if (duplicate == line[i]) continue;
				if (found[GetPriority(line[i])])
				{
					if (duplicate != '\0')
					{
						Console.Error.WriteLine($"There are multiple duplicates: {duplicate} and {line[i]}:");
						Console.Error.WriteLine(line);
						continue;
					}
					duplicate = line[i];
				}
			}
			if (duplicate == '\0')
			{
				Console.Error.WriteLine("No duplicate found:");
				Console.Error.WriteLine(line);
				continue;
			}

			total += GetPriority(duplicate) + 1;
			Array.Clear(found);
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
