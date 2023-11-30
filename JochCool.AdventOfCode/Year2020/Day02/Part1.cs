namespace JochCool.AdventOfCode.Year2020.Day02;

public static class Part1
{
	static readonly Regex onePassword = new Regex(@"^(\d+)-(\d+) +(\w): *(\w*)\r?$", RegexOptions.Multiline);

	public static string? Solve(TextReader inputReader)
	{
		int result = 0;

		// Find all (properly formatted) lines in the file
		Match match = onePassword.Match(inputReader.ReadToEnd());
		while (match.Success)
		{
			// get params
			int min = int.Parse(match.Groups[1].Value);
			int max = int.Parse(match.Groups[2].Value);
			char expected = match.Groups[3].Value[0];
			string toTest = match.Groups[4].Value;
			Console.WriteLine($"Min: {min}, max: {max}, char: {expected}, to test: {toTest}");

			// count chars
			int num = 0;
			foreach (char c in toTest)
			{
				if (c.Equals(expected))
				{
					num++;
					if (num > max) goto Continue;
				}
			}
			if (num >= min)
			{
				Console.WriteLine("Valid.");
				result++;
			}

		Continue:
			match = match.NextMatch();
		}

		Console.WriteLine($"Number of valids: {result}.");

		return result.ToString();
	}
}
