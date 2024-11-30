namespace JochCool.AdventOfCode.Year2022.Day01;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		int[] highest = [int.MinValue, int.MinValue, int.MinValue];
		int current = 0;

		foreach (string line in inputReader.ReadLines())
		{
			if (line.Length == 0)
			{
				CheckHighest();
				current = 0;
				continue;
			}
			current += int.Parse(line, CultureInfo.InvariantCulture);
		}

		CheckHighest();

		void CheckHighest()
		{
			for (int i = 0; i < highest.Length; i++)
			{
				if (current > highest[i])
				{
					(current, highest[i]) = (highest[i], current);
				}
			}
		}

		int total = 0;
		foreach (int num in highest)
		{
			total += num;
			Console.WriteLine(num);
		}
		return total.ToInvariantString();
	}
}
