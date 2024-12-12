namespace JochCool.AdventOfCode.Year2024.Day11;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string? line = inputReader.ReadLine();
		if (string.IsNullOrEmpty(line)) return null;

		List<BigInteger> stones = [.. StringUtil.ParseInvariantArray<BigInteger>(line, ' ')];

		for (int i = 0; i < 25; i++)
		{
			int stonesCount = stones.Count;
			for (int stoneI = 0; stoneI < stonesCount; stoneI++)
			{
				BigInteger stone = stones[stoneI];

				if (stone.IsZero)
				{
					stones[stoneI] = BigInteger.One;
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
					stones[stoneI] = stone / pow10;
					stones.Add(stone % pow10);
					continue;
				}

				stones[stoneI] = stone * 2024;
			}

			//Console.WriteLine(stones.Count);
			//Console.WriteLine(string.Join(' ', stones));
		}

		return stones.Count.ToInvariantString();
	}
}
