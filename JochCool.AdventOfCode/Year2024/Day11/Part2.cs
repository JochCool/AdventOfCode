namespace JochCool.AdventOfCode.Year2024.Day11;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string? line = inputReader.ReadLine();
		if (string.IsNullOrEmpty(line)) return null;

		// Maps a stone number to the number of stones with that number
		Dictionary<BigInteger, BigInteger> stoneCounts = [];

		foreach (BigInteger stone in StringUtil.ParseInvariantArray<BigInteger>(line, ' '))
		{
			stoneCounts.AddToKey(stone, 1);
		}

		// The state of the stones after the next blink
		Dictionary<BigInteger, BigInteger> nextStoneCounts = [];

		for (int i = 0; i < 75; i++)
		{
			foreach (KeyValuePair<BigInteger, BigInteger> stoneGroup in stoneCounts)
			{
				BigInteger stone = stoneGroup.Key;
				BigInteger count = stoneGroup.Value;

				if (stone.IsZero)
				{
					nextStoneCounts.AddToKey(BigInteger.One, count);
					continue;
				}

				int exponent = 1;
				for (BigInteger pow10 = 10; stone >= pow10; pow10 *= 10)
				{
					exponent++;
				}
				if (int.IsEvenInteger(exponent))
				{
					BigInteger pow10 = BigInteger.Pow(10, exponent / 2);
					nextStoneCounts.AddToKey(stone / pow10, count);
					nextStoneCounts.AddToKey(stone % pow10, count);
					continue;
				}

				nextStoneCounts.AddToKey(stone * 2024, count);
			}

			// recycle dictionary instance
			(stoneCounts, nextStoneCounts) = (nextStoneCounts, stoneCounts);
			nextStoneCounts.Clear();

			//Console.WriteLine(stoneCounts.Values.Sum());
		}

		BigInteger totalStoneCount = BigInteger.Zero;
		foreach (BigInteger count in stoneCounts.Values)
		{
			totalStoneCount += count;
		}
		return totalStoneCount.ToInvariantString();
	}
}
