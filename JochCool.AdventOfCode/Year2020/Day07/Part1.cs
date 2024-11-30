namespace JochCool.AdventOfCode.Year2020.Day07;

public static class Part1
{
	// Key is a bag name, value is all the bag names that are the possible contents of the key bag.
	// Bag names exclude the word "bags".
	static readonly Dictionary<string, string[]> bags = [];

	public static string? Solve(TextReader inputReader)
	{
		string input = inputReader.ReadToEnd();

		// Every time a new valid bag is found, it gets added to the stack AND the set.
		HashSet<string> allFoundBagNames = [];
		Stack<string> bagNamesToSeach = new Stack<string>();
		string currentlySearching = "shiny gold";

		// Keep looping till the stack is empty
		while (true)
		{
			// Start looking for all occurences of the bag name
			int i = input.IndexOf(currentlySearching, StringComparison.Ordinal);
			while (i != -1)
			{
				int startOfLine = input.LastIndexOf('\n', i) + 1; // Find where the line began
				if (i != startOfLine) // We don't care about the bag itself
				{
					string newBagName = input[startOfLine .. input.IndexOf(" bags ", startOfLine, StringComparison.Ordinal)];
					if (allFoundBagNames.Add(newBagName)) // avoid duplicates
					{
						bagNamesToSeach.Push(newBagName);
					}
				}

				// find next, starting from next line to avoid potential duplicates
				i = input.IndexOf(currentlySearching, input.IndexOf('\n', i), StringComparison.Ordinal);
			}

			// Next name
			if (bagNamesToSeach.Count == 0) break;
			currentlySearching = bagNamesToSeach.Pop();
		}

		Console.WriteLine($"Number of valid bags: {allFoundBagNames.Count}");

		return allFoundBagNames.Count.ToInvariantString();
	}
}
