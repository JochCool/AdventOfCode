namespace JochCool.AdventOfCode.Year2022.Day02;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int score = 0;

		foreach (string line in inputReader.ReadLines())
		{
			if (line.Length != 3 || line[1] != ' ')
			{
				Console.WriteLine("Unexpected line:");
				Console.WriteLine(line);
				continue;
			}
			int choice1 = line[0] - 'A';
			int choice2 = line[2] - 'X';

			score += choice2 + 1;

			if (choice1 == choice2)
			{
				score += 3;
			}
			else if (choice1 + 1 == choice2 || choice1 == 2 && choice2 == 0)
			{
				score += 6;
			}
		}

		return score.ToString();
	}
}
