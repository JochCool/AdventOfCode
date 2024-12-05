namespace JochCool.AdventOfCode.Year2024.Day05;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		PageNumberComparer comparer = BothParts.ParseRules(inputReader);

		int sum = 0;

		string? line;
		while ((line = inputReader.ReadLine()) is not null)
		{
			int[] nums = StringUtil.ParseInvariantArray<int>(line, ',');

			if (CollectionUtil.IsSorted(nums, comparer))
			{
				//Console.WriteLine(string.Join(',', nums));

				sum += nums[nums.Length / 2];
			}
		}

		return sum.ToInvariantString();
	}
}
