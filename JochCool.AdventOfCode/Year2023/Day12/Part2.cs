namespace JochCool.AdventOfCode.Year2023.Day12;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		BigInteger sum = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int separatorI = line.IndexOf(' ');

			ReadOnlySpan<char> row = line.AsSpan(0, separatorI);
			ReadOnlySpan<int> nums = line[(separatorI + 1)..].Split(',').Select(int.Parse).ToArray();

			const int numRepeats = 5;
			ReadOnlySpan<char> rowUnfolded = CollectionUtil.Repeat(row, numRepeats, '?');
			ReadOnlySpan<int> numsUnfolded = CollectionUtil.Repeat(nums, numRepeats);

			BigInteger count = GetPossibleArrangementCount(rowUnfolded, numsUnfolded, new());
			Console.WriteLine($"{line} - {count}");

			sum += count;
		}

		return sum.ToString();
	}

	// Uses double recursion and memoization
	private static BigInteger GetPossibleArrangementCount(ReadOnlySpan<char> row, ReadOnlySpan<int> nums, Dictionary<int, BigInteger> memory)
	{
		// Because the memory dict is used only for one line, if the remaining lengths of the inputs are the same, then the inputs are the same.
		int memoryKey = row.Length << 16 | nums.Length;
		if (memory.TryGetValue(memoryKey, out BigInteger value))
		{
			return value;
		}

		if (nums.Length == 0)
		{
			// Having no more numbers is possible iff there were no more damaged springs anyway
			return memory[memoryKey] = row.Contains('#') ? 0 : 1;
		}

		int currentNum = nums[0];
		if (currentNum <= 0) throw new ArgumentException("All numbers must be positive.", nameof(nums));

		BigInteger result;

		// Find the first place where we can fit the current num
		while (true)
		{
			if (row.Length < currentNum)
			{
				result = 0;
				break;
			}

			if (!row[0..currentNum].Contains('.'))
			{
				// The num seems to fit here
				if (currentNum == row.Length)
				{
					// We've reached the end of the string, so this is a possible arrangement iff this was the last num
					result = nums.Length == 1 ? 1 : 0;
					break;
				}
				if (row[currentNum] != '#')
				{
					// It's possible so far, but there are more numbers; continue on!
					result = GetPossibleArrangementCount(row[(currentNum + 1)..], nums[1..], memory);

					// We might also be able to move this same number 1 spot to the right, and try the same thing
					if (row[0] == '?') result += GetPossibleArrangementCount(row[1..], nums, memory);

					break;
				}
			}

			// We failed to fit the num here. But if there did have to be a num here, then this is not a possible arrangement.
			if (row[0] == '#')
			{
				result = 0;
				break;
			}

			row = row[1..];
		}

		return memory[memoryKey] = result;
	}
}
