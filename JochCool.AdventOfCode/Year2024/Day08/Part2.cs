namespace JochCool.AdventOfCode.Year2024.Day08;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		return BothParts.Solve(inputReader, AddAntinodes);
	}

	private static void AddAntinodes(HashSet<Vector<int>> set, Vector<int> antenna1, Vector<int> antenna2, Vector<int> mapSize)
	{
		Vector<int> delta = antenna2 - antenna1;

		AddWhileInMap(set, antenna2, delta, mapSize);
		AddWhileInMap(set, antenna1, -delta, mapSize);
	}

	private static void AddWhileInMap(HashSet<Vector<int>> set, Vector<int> antennaLocation, Vector<int> delta, Vector<int> mapSize)
	{
		while ((uint)antennaLocation.X < (uint)mapSize.X && (uint)antennaLocation.Y < (uint)mapSize.Y)
		{
			set.Add(antennaLocation);
			antennaLocation += delta;
		}
	}
}
