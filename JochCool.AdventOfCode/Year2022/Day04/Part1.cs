namespace JochCool.AdventOfCode.Year2022.Day04;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int count = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int pos = line.IndexOf('-');
			int pair1Start = int.Parse(line[..pos]);

			int pair1End = int.Parse(line[++pos..(pos = line.IndexOf(',', pos))]);
			int pair2Start = int.Parse(line[++pos..(pos = line.IndexOf('-', pos))]);
			int pair2End = int.Parse(line[++pos..]);

			if (pair1Start >= pair2Start && pair1End <= pair2End ||
				pair1Start <= pair2Start && pair1End >= pair2End)
			{
				count++;
			}
		}

		return count.ToString();
	}
}
