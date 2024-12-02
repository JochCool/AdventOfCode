namespace JochCool.AdventOfCode.Year2024.Day02;

static class BothParts
{
	public static bool IsSafe(string report, bool canTolerateOneBadLevel)
	{
		var levels = StringUtil.ParseInvariantArray<int>(report, ' ');

		return AreLevelDiffsInRange(levels, new(1, 3), canTolerateOneBadLevel) // check ascending
			|| AreLevelDiffsInRange(levels, new(-3, -1), canTolerateOneBadLevel); // check descending
	}

	private static bool AreLevelDiffsInRange(ReadOnlySpan<int> levels, IntegerRange<int> range, bool canTolerateOneBadLevel)
	{
		// We need only the differences between the levels.
		Span<int> diffs = stackalloc int[levels.Length - 1];
		for (int i = 1; i < levels.Length; i++)
		{
			diffs[i - 1] = levels[i] - levels[i - 1];
		}

		// Check if any of the diffs are outside the range.
		for (int i = 0; i < diffs.Length; i++)
		{
			int diff = diffs[i];
			if (range.Contains(diff))
			{
				continue;
			}

			if (!canTolerateOneBadLevel)
			{
				return false;
			}

			if (i != diffs.Length - 1 && (i != 0 || !range.Contains(diffs[i + 1])))
			{
				// Tolerating a level (removing it from the array) is the same as "merging" two adjacent diffs.
				diffs[i + 1] += diff;
			}

			canTolerateOneBadLevel = false;
		}

		return true;
	}
}
