namespace JochCool.AdventOfCode.Year2024.Day08;

static class BothParts
{
	internal static string? Solve(TextReader inputReader, AntinodeAdder addAntinodes)
	{
		// Maps a frequency to the antennas of that frequency
		Dictionary<char, List<Vector<int>>> antennas = [];

		int width = 0;
		int y = 0;
		foreach (string line in inputReader.ReadLines())
		{
			if (width < line.Length)
			{
				width = line.Length;
			}

			for (int x = 0; x < line.Length; x++)
			{
				char frequency = line[x];
				if (frequency == '.') continue;

				if (!antennas.TryGetValue(frequency, out var list))
				{
					antennas[frequency] = list = [];
				}
				list.Add(new Vector<int>(x, y));
			}

			y++;
		}

		Vector<int> mapSize = new Vector<int>(width, y);

		HashSet<Vector<int>> antinodeLocations = [];

		// Find all pairs of same-frequency antennas
		foreach (List<Vector<int>> antennaGroup in antennas.Values)
		{
			for (int i1 = 0; i1 < antennaGroup.Count; i1++)
			{
				Vector<int> antenna1 = antennaGroup[i1];

				for (int i2 = i1 + 1; i2 < antennaGroup.Count; i2++)
				{
					Vector<int> antenna2 = antennaGroup[i2];

					addAntinodes(antinodeLocations, antenna1, antenna2, mapSize);
				}
			}
		}

		return antinodeLocations.Count.ToInvariantString();
	}
}

delegate void AntinodeAdder(HashSet<Vector<int>> set, Vector<int> antenna1Location, Vector<int> antenna2Location, Vector<int> mapSize);
