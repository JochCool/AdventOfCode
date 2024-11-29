namespace JochCool.AdventOfCode.Year2020.Day05;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] lines = inputReader.ReadAllLines();
		int[] ids = new int[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];
			int seatId = 0;
			int bit = 1 << (line.Length - 1);
			foreach (char c in line)
			{
				if (c == 'B' || c == 'R') seatId |= bit;
				bit >>= 1;
			}
			ids[i] = seatId;
		}
		Array.Sort(ids);
		for (int i = 1, num = ids[0] + 1; i < ids.Length; num++)
		{
			if (ids[i] <= num) i++;
			else
			{
				Console.WriteLine($"{num} is missing.");
				return num.ToInvariantString();
			}
		}
		Console.WriteLine("Not found.");
		return null;
	}
}
