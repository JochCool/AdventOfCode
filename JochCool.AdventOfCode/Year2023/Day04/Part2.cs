namespace JochCool.AdventOfCode.Year2023.Day04;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] lines = inputReader.ReadAllLines();

		int[] copies = new int[lines.Length];
		Array.Fill(copies, 1);

		int totalCopies = lines.Length;

		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];

			int startI = line.IndexOf(": ") + 2;
			int endI = line.IndexOf(" | ", startI);

			int[] winningNumbers = line[startI..endI].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

			startI = endI + 3;
			endI = line.Length;

			int numWinning = 0;
			IEnumerable<int> myNumbers = line[startI..endI].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
			foreach (int myNumber in myNumbers)
			{
				if (winningNumbers.Contains(myNumber))
				{
					numWinning++;
				}
			}

			int myNumCopies = copies[i];
			Console.WriteLine($"{myNumCopies} copies of card {i+1}");

			for (int winningCard = 0; winningCard < numWinning; winningCard++)
			{
				copies[i + winningCard + 1] += myNumCopies;
				totalCopies += myNumCopies;
			}
		}

		return totalCopies.ToString();
	}
}
