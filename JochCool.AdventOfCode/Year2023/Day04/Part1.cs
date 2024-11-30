namespace JochCool.AdventOfCode.Year2023.Day04;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int startI = line.IndexOf(": ", StringComparison.Ordinal) + 2;
			int endI = line.IndexOf(" | ", startI, StringComparison.Ordinal);

			int[] winningNumbers = line[startI..endI].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

			startI = endI + 3;
			endI = line.Length;

			int worth = 0;
			IEnumerable<int> myNumbers = line[startI..endI].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
			foreach (int myNumber in myNumbers)
			{
				if (winningNumbers.Contains(myNumber))
				{
					if (worth == 0) worth = 1;
					else worth *= 2;
				}
			}

			sum += worth;
		}

		return sum.ToInvariantString();
	}
}
