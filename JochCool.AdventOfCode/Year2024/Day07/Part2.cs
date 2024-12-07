namespace JochCool.AdventOfCode.Year2024.Day07;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		return BothParts.Solve(inputReader, IsPossible);
	}

	private static bool IsPossible(BigInteger testValue, BigInteger accumulator, ReadOnlySpan<BigInteger> remainingNumbers)
	{
		if (remainingNumbers.Length == 0)
		{
			return accumulator == testValue;
		}

		// Optimization, because the accumulator can never decrease
		if (accumulator > testValue)
		{
			return false;
		}

		return IsPossible(testValue, accumulator + remainingNumbers[0], remainingNumbers[1..])
			|| IsPossible(testValue, accumulator * remainingNumbers[0], remainingNumbers[1..])
			|| IsPossible(testValue, Concat(accumulator, remainingNumbers[0]), remainingNumbers[1..]);
	}
	public static BigInteger Concat(BigInteger left, BigInteger right)
	{
		BigInteger multiplier = 10;
		while (right >= multiplier)
		{
			multiplier *= 10;
		}

		return left * multiplier + right;
	}
}
