namespace JochCool.AdventOfCode.Year2020.Day05;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int maxSeatId = 0;
		int maxIndex = 0, currentIndex = 0;
		foreach (string line in inputReader.ReadLines())
		{
			int seatId = 0;
			int bit = 1 << (line.Length - 1);
			foreach (char c in line)
			{
				if (c == 'B' || c == 'R') seatId |= bit;
				bit >>= 1;
			}
			if (seatId > maxSeatId)
			{
				maxSeatId = seatId;
				maxIndex = currentIndex;
			}

			currentIndex++;
		}

		Console.WriteLine($"Largest seat ID: {maxSeatId}");
		Console.WriteLine($"In binary: ${Convert.ToString(maxSeatId, 2)}");
		Console.WriteLine($"Index in file: ${maxIndex}");

		return maxSeatId.ToString();
	}
}
