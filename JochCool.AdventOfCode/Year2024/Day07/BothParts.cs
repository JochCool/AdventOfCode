namespace JochCool.AdventOfCode.Year2024.Day07;

static class BothParts
{
	internal static string? Solve(TextReader inputReader, EquationTester isEquationPossible)
	{
		BigInteger sum = 0;
		foreach (string line in inputReader.ReadAllLines())
		{
			int i = line.IndexOf(": ", StringComparison.Ordinal);
			if (i == -1) throw new FormatException();

			BigInteger testValue = BigInteger.Parse(line.AsSpan(0, i), CultureInfo.InvariantCulture);
			BigInteger[] numbers = StringUtil.ParseInvariantArray<BigInteger>(line.AsSpan(i + 2), ' ');

			if (isEquationPossible(testValue, numbers[0], numbers.AsSpan(1)))
			{
				sum += testValue;
			}
		}
		return sum.ToInvariantString();
	}
}

delegate bool EquationTester(BigInteger testValue, BigInteger accumulator, ReadOnlySpan<BigInteger> remainingNumbers);
