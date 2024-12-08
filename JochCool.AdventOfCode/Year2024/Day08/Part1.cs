namespace JochCool.AdventOfCode.Year2024.Day08;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		return BothParts.Solve(inputReader, AddAntinodes);
	}

	private static void AddAntinodes(HashSet<Vector<int>> set, Vector<int> antenna1, Vector<int> antenna2, Vector<int> mapSize)
	{
		Vector<int> distance = antenna2 - antenna1;

		AddIfInMap(set, antenna2 + distance, mapSize);
		AddIfInMap(set, antenna1 - distance, mapSize);
	}

	private static void AddIfInMap(HashSet<Vector<int>> set, Vector<int> location, Vector<int> mapSize)
	{
		if ((uint)location.X < (uint)mapSize.X && (uint)location.Y < (uint)mapSize.Y)
		{
			set.Add(location);
		}
	}
}
