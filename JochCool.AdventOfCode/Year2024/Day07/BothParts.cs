namespace JochCool.AdventOfCode.Year2024.Day07;

static class BothParts
{
	internal static string? Solve(TextReader inputReader, EquationPredicate isEquationPossible)
	{
		BigInteger sum = 0;
		foreach (string line in inputReader.ReadAllLines())
		{
			int i = line.IndexOf(": ", StringComparison.Ordinal);
			if (i == -1) throw new FormatException();

			BigInteger testValue = BigInteger.Parse(line.AsSpan(0, i), CultureInfo.InvariantCulture);
			BigInteger[] numbers = StringUtil.ParseInvariantArray<BigInteger>(line.AsSpan(i + 2), ' ');

			if (isEquationPossible(testValue, numbers))
			{
				sum += testValue;
			}
		}
		return sum.ToInvariantString();
	}
}

delegate bool EquationPredicate(BigInteger testValue, BigInteger[] numbers);
