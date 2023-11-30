namespace JochCool.AdventOfCode.Year2020.Day06;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int result = 0;
		List<char> currentGroup = []; // All questions answered with 'yes' in this group so far. Can contain duplicates.
		foreach (string line in inputReader.ReadLines())
		{
			// Empty lines mark end of group
			if (line.Length == 0)
			{
				if (currentGroup.Count == 0) continue;
				result += CountUniqueCharactersIn(currentGroup);
				currentGroup = [];
			}
			else currentGroup.AddRange(line);
		}
		if (currentGroup.Count != 0) result += CountUniqueCharactersIn(currentGroup);

		Console.WriteLine($"Result: {result}");

		return result.ToString();
	}

	static int CountUniqueCharactersIn(List<char> list) // should not be empty
	{
		if (list.Count == 1) return 1; // simple case

		list.Sort();
		int result = 1;
		IEnumerator<char> enumerator = list.GetEnumerator();
		_ = enumerator.MoveNext();
		char prev = enumerator.Current;
		while (enumerator.MoveNext())
		{
			char current = enumerator.Current;
			if (current != prev)
			{
				result++;
				prev = current;
			}
		}
		return result;
	}
}
