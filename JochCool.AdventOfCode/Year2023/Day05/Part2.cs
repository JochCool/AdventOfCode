namespace JochCool.AdventOfCode.Year2023.Day05;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string seedsLine = inputReader.ReadLine() ?? throw new FormatException("Input too short.");
		BrokenIntegerRange<long> seeds = new();
		for (int i = "seeds: ".Length; i < seedsLine.Length; i++)
		{
			int endI = seedsLine.IndexOf(' ', i);
			if (endI == -1) throw new FormatException("Missing length.");

			long startSeed = long.Parse(seedsLine.AsSpan(i, endI - i), CultureInfo.InvariantCulture);

			i = endI + 1;
			endI = seedsLine.IndexOf(' ', i);
			if (endI == -1) endI = seedsLine.Length;

			long seedsLength = long.Parse(seedsLine.AsSpan(i, endI - i), CultureInfo.InvariantCulture);

			seeds.AddRange(startSeed, startSeed + seedsLength - 1);

			i = endI;
		}

		inputReader.ReadLine(); // empty line

		// Parse the maps
		List<Map> maps = [];
		string? line;
		while ((line = inputReader.ReadLine()) is not null)
		{
			if (!line.EndsWith(" map:", StringComparison.Ordinal)) throw new FormatException("Expected map.");

			maps.Add(Map.Parse(inputReader));
		}

		foreach (Map map in maps)
		{
			seeds = map[seeds];
		}

		return seeds.Min.ToInvariantString();
	}
}
