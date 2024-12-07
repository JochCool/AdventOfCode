namespace JochCool.AdventOfCode.Year2024.Day07;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		return BothParts.Solve(inputReader, IsPossible);
	}

	internal static bool IsPossible(BigInteger testValue, BigInteger[] numbers)
	{
		// A possible combination of operators is represented by a binary integer.
		// Each bit corresponds to a position between the numbers.
		// A 0 means the operator is addition, a 1 means it is multiplication.

		uint numPossibilities = 1u << (numbers.Length - 1);
		for (uint operators = 0; operators < numPossibilities; operators++)
		{
			if (ApplyOperations(numbers, operators) == testValue)
			{
				//Console.WriteLine(testValue);
				return true;
			}
		}
		return false;
	}

	private static BigInteger ApplyOperations(BigInteger[] numbers, uint operators)
	{
		BigInteger result = numbers[0];
		for (int i = 1; i < numbers.Length; i++)
		{
			uint mask = 1u << (i - 1);
			if ((operators & mask) == 0)
			{
				result += numbers[i];
			}
			else
			{
				result *= numbers[i];
			}
		}
		return result;
	}
}
