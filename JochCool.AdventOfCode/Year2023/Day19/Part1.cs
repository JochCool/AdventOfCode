namespace JochCool.AdventOfCode.Year2023.Day19;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		Dictionary<string, string[]> workflows = new();

		foreach (string line in inputReader.ReadLinesUntilEmpty())
		{
			int ruleStartI = line.IndexOf('{');
			if (ruleStartI == -1) throw new FormatException();

			int ruleEndI = line.IndexOf('}', ruleStartI + 1);
			if (ruleEndI == -1) throw new FormatException();

			workflows[line[0..ruleStartI]] = line[(ruleStartI + 1)..ruleEndI].Split(',');
		}

		int acceptedRatingSum = 0;
		foreach (string line in inputReader.ReadLines())
		{
			int i = "{x=".Length;
			int x = StringUtil.ParseInvariantAt<int>(line, ref i, ',');
			i += ",m=".Length;
			int m = StringUtil.ParseInvariantAt<int>(line, ref i, ',');
			i += ",a=".Length;
			int a = StringUtil.ParseInvariantAt<int>(line, ref i, ',');
			i += ",s=".Length;
			int s = StringUtil.ParseInvariantAt<int>(line, ref i, '}');

			string[] workflow = workflows["in"];
			int ruleI = 0;
			while (true)
			{
				string targetWorkflow;

				string rule = workflow[ruleI];
				int colonI = rule.IndexOf(':');
				if (colonI == -1)
				{
					targetWorkflow = rule;
				}
				else
				{
					int operand1 = rule[0] switch
					{
						'x' => x,
						'm' => m,
						'a' => a,
						's' => s,
						_ => throw new FormatException()
					};
					int operand2 = int.Parse(rule.AsSpan(2, colonI - 2), CultureInfo.InvariantCulture);
					bool success = rule[1] switch
					{
						'>' => operand1 > operand2,
						'<' => operand1 < operand2,
						'=' => operand1 == operand2,
						_ => throw new FormatException()
					};
					if (!success)
					{
						ruleI++;
						continue;
					}
					targetWorkflow = rule[(colonI + 1)..];
				}

				if (targetWorkflow == "A")
				{
					acceptedRatingSum += x + m + a + s;
					break;
				}
				if (targetWorkflow == "R")
				{
					break;
				}

				workflow = workflows[targetWorkflow];
				ruleI = 0;
			}
		}

		return acceptedRatingSum.ToInvariantString();
	}
}
