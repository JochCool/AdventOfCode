namespace JochCool.AdventOfCode.Year2020.Day10;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		// Load & sort input
		List<int> joltages = inputReader.ParseAllLines<int>();
		joltages.Sort();

		// Every value corresponds to the number of valid adapter arrangements that can get you a joltage of <index>.
		// Every index that is not in the joltages list has the value 0 (by default), since there's no way to get there.
		BigInteger[] results = new BigInteger[joltages[^1] + 1];

		results[0] = 1; // We start at 0 so there's 1 way to get there namely by using 0 adapters.

		foreach (int num in joltages)
		{
			BigInteger value = results[num - 1];
			if (num >= 2) value += results[num - 2];
			if (num >= 3) value += results[num - 3];
			results[num] = value;
		}

		BigInteger result = results[^1];
		Console.WriteLine($"Result: {results[^1]}.");
		return result.ToString();

		/* Old code that did not work but I still think it was a good try.
		
		// Base 2 log of the final result.
		int resultPow = 0;

		// Only the last 3 bits are used. Specifies whether the numbers that are 1, 2, or 3 lower than the current number are present in the list. For example, 0b011 means that if the current number is 25, then the numbers 22 and 23 also exist.
		int mask = 0;

		int prevNum = 0; // according to the assignment, it starts with 0 jolts
		foreach (int num in joltages)
		{
			// Add a bit at the front of the mask, then shift it right.
			mask = (mask | 0b1000) >> (num - prevNum);

			// Since we can hop from any bit to any other bit in this range, the number of new steps is 2 ^ (numbits - 1).
			resultPow += CountBits(mask) - 1;

			prevNum = num;
		}

		BigInteger result = BigInteger.One << resultPow;
		Console.WriteLine($"Result: 2 ^ {resultPow} = {result}");
		*/
	}

	// Method was copied from another project of mine. Returns two to the power of the number of bits in num.
	public static int CountBits(int num)
	{
		if (num < 0) throw new ArgumentException("The number can't be negative.", nameof(num));
		int result = 0;
		while (num != 0)
		{
			result += num & 0b1;
			num >>= 1;
		}
		return result;
	}
}
