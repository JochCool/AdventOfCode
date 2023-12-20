namespace JochCool.AdventOfCode.Year2023.Day19;

public static class Part2
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

		return GetAcceptedCount(workflows, "in", PartsRange.AllInRange(new(1, 4000))).ToString();
	}

	private static long GetAcceptedCount(Dictionary<string, string[]> workflows, string workflowName, PartsRange inputParts)
	{
		Console.WriteLine($"{workflowName} has {inputParts.Size:N0} inputs: {inputParts}.");
		long result = 0;
		string[] workflow = workflows[workflowName];
		for (int ruleI = 0; ruleI < workflow.Length; ruleI++)
		{
			string rule = workflow[ruleI];
			int colonI = rule.IndexOf(':');
			string targetWorkflow;
			PartsRange matchedParts = inputParts;
			if (colonI == -1)
			{
				targetWorkflow = rule;
				inputParts = PartsRange.None;
			}
			else
			{
				char operand1Id = rule[0];
				ref NumberRange operand1 = ref inputParts.x; // if (c == 'x')
				if (operand1Id == 'm') operand1 = ref inputParts.m;
				else if (operand1Id == 'a') operand1 = ref inputParts.a;
				else if (operand1Id == 's') operand1 = ref inputParts.s;
				else if (operand1Id != 'x') throw new FormatException();

				int operand2 = int.Parse(rule.AsSpan(2, colonI - 2));

				char operatorId = rule[1];
				switch (operatorId)
				{
					case '>':
					{
						operand1 = operand1.GreaterThan(operand2);
						break;
					}

					case '<':
					{
						operand1 = operand1.LessThan(operand2);
						break;
					}

					default:
						throw new FormatException();
				}

				(matchedParts, inputParts) = (inputParts, matchedParts);

				// Apply the inverse operation to the remaining input parts
				switch (operatorId)
				{
					case '>':
					{
						operand1 = operand1.LessThanOrEqual(operand2);
						break;
					}

					case '<':
					{
						operand1 = operand1.GreaterThanOrEqual(operand2);
						break;
					}

					default:
						throw new FormatException();
				}

				targetWorkflow = rule[(colonI + 1)..];
			}

			if (targetWorkflow == "A")
			{
				result += matchedParts.Size;
				continue;
			}
			if (targetWorkflow == "R")
			{
				continue;
			}

			result += GetAcceptedCount(workflows, targetWorkflow, matchedParts);
		}
		Console.WriteLine($"{workflowName} has {result:N0} accepted.");
		return result;
	}

	struct PartsRange
	{
		public NumberRange x;
		public NumberRange m;
		public NumberRange a;
		public NumberRange s;

		public static PartsRange All => AllInRange(NumberRange.All);

		public static PartsRange None => AllInRange(NumberRange.None);

		public static PartsRange AllInRange(NumberRange range)
		{
			PartsRange result;
			result.x = range;
			result.m = range;
			result.a = range;
			result.s = range;
			return result;
		}

		public readonly long Size => x.Size * m.Size * a.Size * s.Size;

		public override readonly string ToString() => $"{{x={x},m={m},a={a},s={s}}}";
	}

	readonly struct NumberRange
	{
		readonly int minInclusive;
		readonly int maxInclusive;

		public NumberRange(int minInclusive, int maxInclusive)
		{
			this.minInclusive = minInclusive;
			this.maxInclusive = maxInclusive;
		}

		public static NumberRange All => new(int.MinValue, int.MaxValue);

		public static NumberRange None => new(0, -1);

		public long Size => (long)maxInclusive - minInclusive + 1;

		public bool Contains(int value) => value >= minInclusive && value <= maxInclusive;

		public NumberRange GreaterThanOrEqual(int value)
		{
			if (value > maxInclusive) return None;
			return new(int.Max(minInclusive, value), maxInclusive);
		}

		public NumberRange LessThanOrEqual(int value)
		{
			if (value < minInclusive) return None;
			return new(minInclusive, int.Min(maxInclusive, value));
		}

		public NumberRange GreaterThan(int value)
		{
			if (value >= maxInclusive) return None;
			return new(int.Max(minInclusive, value + 1), maxInclusive);
		}

		public NumberRange LessThan(int value)
		{
			if (value <= minInclusive) return None;
			return new(minInclusive, int.Min(maxInclusive, value - 1));
		}

		public override string ToString() => $"({minInclusive}, {maxInclusive})";
	}
}
