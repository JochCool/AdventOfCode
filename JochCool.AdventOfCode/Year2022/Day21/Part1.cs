namespace JochCool.AdventOfCode.Year2022.Day21;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		Dictionary<string, IMonkeyJob> dict = [];

		foreach (string line in inputReader.ReadLines())
		{
			string name = line[..4];
			if (line[6] is >= '0' and <= '9')
			{
				dict.Add(name, new MonkeyValue(BigInteger.Parse(line.AsSpan(6))));
			}
			else
			{
				dict.Add(name, new MonkeyOperation(line[6..10], line[13..17], line[11]));
			}
		}

		BigInteger result = dict["root"].GetValue(dict);

		return result.ToString();
	}
}
