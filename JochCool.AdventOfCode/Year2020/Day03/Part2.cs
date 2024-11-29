namespace JochCool.AdventOfCode.Year2020.Day03;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] field = inputReader.ReadAllLines();

		BigInteger result =
			TraverseSlope(field, 1, 1) *
			TraverseSlope(field, 1, 3) *
			TraverseSlope(field, 1, 5) *
			TraverseSlope(field, 1, 7) *
			TraverseSlope(field, 2, 1);

		Console.WriteLine($"Result: {result}");

		return result.ToInvariantString();
	}

	static BigInteger TraverseSlope(string[] field, int stepsDown, int stepsRight)
	{
		int fieldWidth = field[0].Length; // assuming that all lines are same length

		BigInteger result = 0;

		int currentColumn = 0;
		int currentRow = 0;
		while (true)
		{
			currentRow += stepsDown;

			if (currentRow >= field.Length) break;

			currentColumn = (currentColumn + stepsRight) % fieldWidth;
			if (field[currentRow][currentColumn] == '#')
			{
				result++;
			}
		}

		Console.WriteLine($"Found {result} trees.");
		return result;
	}
}
