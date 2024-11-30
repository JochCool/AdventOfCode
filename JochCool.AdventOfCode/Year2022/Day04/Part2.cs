namespace JochCool.AdventOfCode.Year2022.Day04;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		int count = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int pos = line.IndexOf('-');
			int pair1Start = int.Parse(line[..pos], CultureInfo.InvariantCulture);

			int pair1End = int.Parse(line[++pos..(pos = line.IndexOf(',', pos))], CultureInfo.InvariantCulture);
			int pair2Start = int.Parse(line[++pos..(pos = line.IndexOf('-', pos))], CultureInfo.InvariantCulture);
			int pair2End = int.Parse(line[++pos..], CultureInfo.InvariantCulture);

			if (pair1Start <= pair2End && pair1End >= pair2End ||
				pair2Start <= pair1End && pair2End >= pair1End)
			{
				count++;
			}
		}

		return count.ToInvariantString();
	}
}
