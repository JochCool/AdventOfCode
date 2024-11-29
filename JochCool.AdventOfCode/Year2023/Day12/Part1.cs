namespace JochCool.AdventOfCode.Year2023.Day12;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int separatorI = line.IndexOf(' ');

			ReadOnlySpan<char> row = line.AsSpan(0, separatorI);
			ReadOnlySpan<int> nums = StringUtil.ParseArray<int>(line.AsSpan(separatorI + 1), ',');

			int count = GetPossibleArrangementCount(row, nums);
			Console.WriteLine($"{line} - {count}");

			sum += count;
		}

		return sum.ToInvariantString();
	}

	// Uses double recursion
	private static int GetPossibleArrangementCount(ReadOnlySpan<char> row, ReadOnlySpan<int> nums)
	{
		if (nums.Length == 0)
		{
			// Having no more numbers is possible iff there were no more damaged springs anyway
			return row.Contains('#') ? 0 : 1;
		}

		int currentNum = nums[0];
		if (currentNum <= 0) throw new ArgumentException("All numbers must be positive.", nameof(nums));

		// Find the first place where we can fit the current num
		while (true)
		{
			if (row.Length < currentNum) return 0;

			if (!row[0..currentNum].Contains('.'))
			{
				// The num seems to fit here
				if (currentNum == row.Length)
				{
					// We've reached the end of the string, so this is a possible arrangement iff this was the last num
					return nums.Length == 1 ? 1 : 0;
				}
				if (row[currentNum] != '#')
				{
					// It's possible so far, but there are more numbers; continue on!
					int result = GetPossibleArrangementCount(row[(currentNum + 1)..], nums[1..]);

					// We might also be able to move this same number 1 spot to the right, and try the same thing
					if (row[0] == '?') result += GetPossibleArrangementCount(row[1..], nums);

					return result;
				}
			}

			// We failed to fit the num here. But if there did have to be a num here, then this is not a possible arrangement.
			if (row[0] == '#') return 0;

			// Otherwise, try the next starting char
			row = row[1..];
		}
	}
}
