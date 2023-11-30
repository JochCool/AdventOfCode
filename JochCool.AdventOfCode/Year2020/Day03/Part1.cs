namespace JochCool.AdventOfCode.Year2020.Day03;

public static class Part1
{
	const int stepsRightPerStepDown = 3;

	public static string? Solve(TextReader inputReader)
	{
		string[] field = inputReader.ReadAllLines();
		int fieldWidth = field[0].Length; // assuming that all lines are same length

		int result = 0;

		int currentColumn = 0;
		for (int currentRow = 1; currentRow < field.Length; currentRow++)
		{
			currentColumn = (currentColumn + stepsRightPerStepDown) % fieldWidth;
			char[] row = field[currentRow].ToCharArray();
			if (row[currentColumn] == '#')
			{
				result++;
				row[currentColumn] = 'X';
			}
			else
			{
				row[currentColumn] = 'O';
			}
			field[currentRow] = new string(row);
			Console.WriteLine(field[currentRow]);
		}

		Console.WriteLine($"Found {result} trees.");

		return result.ToString();
	}
}
