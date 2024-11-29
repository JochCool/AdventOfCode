namespace JochCool.AdventOfCode.Year2022.Day02;

public static class Part2
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
			int result = line[2] - 'X';

			int choice2 = (choice1 + result + 2) % 3;
			score += choice2 + 1;

			score += result * 3;
		}

		return score.ToInvariantString();
	}
}
