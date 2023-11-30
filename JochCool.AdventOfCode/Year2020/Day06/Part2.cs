namespace JochCool.AdventOfCode.Year2020.Day06;

public static class Part2
{
	// Indicates that this element in the currentGroup array should be ignored.
	const char nothing = '_';

	public static string? Solve(TextReader inputReader)
	{
		int result = 0;
		char[]? currentGroup = null; // All questions that everyone answered with 'yes' in this group so far. Elements may be '_' (nothing).
		int currentGroupSize = 0;
		foreach (string line in inputReader.ReadLines())
		{
			// Empty lines mark end of group
			if (line.Length == 0)
			{
				if (currentGroup is null) continue;
				result += currentGroupSize;
				currentGroup = null; // reset group
			}
			else if (currentGroup is null)
			{
				currentGroup = line.ToCharArray();
				currentGroupSize = currentGroup.Length;
			}
			else
			{
				// Sort this line so we can binary search.
				char[] answers = line.ToCharArray();
				Array.Sort(answers);
				// Remove elements in currentGroup that aren't in answers.
				for (int i = 0; i < currentGroup.Length; i++)
				{
					char c = currentGroup[i];
					if (c == nothing) continue;
					if (Array.BinarySearch(answers, c) < 0)
					{
						currentGroupSize--;
						currentGroup[i] = nothing;
					}
				}
			}
		}
		if (currentGroup is not null) result += currentGroupSize;

		Console.WriteLine($"Result: {result}");

		return result.ToString();
	}
}
