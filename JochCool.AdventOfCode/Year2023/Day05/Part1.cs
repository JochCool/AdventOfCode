using JochCool.AdventOfCode.Year2022.Day16;

namespace JochCool.AdventOfCode.Year2023.Day05;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string seedsLine = inputReader.ReadLine() ?? throw new FormatException("Input too short.");
		List<long> seeds = [];
		for (int i = "seeds: ".Length; i < seedsLine.Length; i++)
		{
			int endI = seedsLine.IndexOf(' ', i);
			if (endI == -1) endI = seedsLine.Length;

			seeds.Add(long.Parse(seedsLine.AsSpan(i, endI - i), CultureInfo.InvariantCulture));

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

		long lowest = long.MaxValue;
		foreach (long seed in seeds)
		{
			long value = seed;
			foreach (Map map in maps)
			{
				value = map[value];
			}
			Console.WriteLine($"Seed {seed} maps to {value}");
			if (value < lowest)
			{
				lowest = value;
			}
		}

		return lowest.ToInvariantString();
	}
}
