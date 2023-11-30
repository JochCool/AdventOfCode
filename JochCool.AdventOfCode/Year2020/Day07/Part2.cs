namespace JochCool.AdventOfCode.Year2020.Day07;

public static class Part2
{
	// Key is a bag name, value is all the bag names that are the possible contents of the key bag.
	// Bag names exclude the word "bags".
	static readonly Dictionary<string, string[]> bags = [];

	public static string? Solve(TextReader inputReader)
	{
		string input = inputReader.ReadToEnd();

		int result = TotalNumBags(input, "shiny gold") - 1;

		Console.WriteLine($"Number of bags in shiny gold (excluding itself): {result}");

		return result.ToString();
	}

	// Counts the number of bags with the specified name, recursively. Includes this bag!
	static int TotalNumBags(string input, string bagName)
	{
		// Find the contents of the bag.
		int i = input.IndexOf($"\n{bagName}") + bagName.Length + 15; // The newline + " bags contain " = 15 characters

		if (input[i] == 'n') return 1; // 'n' of "no other bags"

		int result = 1;

		while (true)
		{
			// Read the number until the next space
			int nextSpace = input.IndexOf(' ', i);
			int num = int.Parse(input[i..nextSpace]);

			// Read next bag and multiply
			i = input.IndexOf(" bag", ++nextSpace);
			result += TotalNumBags(input, input[nextSpace..i]) * num;

			// next
			i = input.IndexOfAny(separators, i);
			if (input[i] == '.') break;
			i += 2;
		}

		return result;
	}

	static readonly char[] separators = [',', '.'];
}
