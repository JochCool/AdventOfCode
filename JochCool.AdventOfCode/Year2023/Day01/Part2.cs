namespace JochCool.AdventOfCode.Year2023.Day01;

public static class Part2
{
	static readonly string[] digits = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;
		foreach (string line in inputReader.ReadLines())
		{
			int num1 = GetFirstDigit(line, false);
			int num2 = GetFirstDigit(line, true);
			int num = num1 * 10 + num2;
			//Console.WriteLine(num);
			sum += num;
		}

		return sum.ToInvariantString();
	}

	static int GetFirstDigit(string line, bool reverse) // If reverse is true, then searches from high to low index, instead of from low to high
	{
		int bestI = reverse ? int.MinValue : int.MaxValue;
		int num = -1;
		int i = 0;
		foreach (string digit in digits)
		{
			int index = reverse ? line.LastIndexOf(digit, StringComparison.Ordinal) : line.IndexOf(digit, StringComparison.Ordinal);
			if (index != -1 && (index < bestI ^ reverse))
			{
				bestI = index;
				num = i;
			}
			i++;
		}
		if (num == -1) throw new ArgumentException("Line has no digits.", nameof(line));
		if (num >= 10) num -= 10; // The words are offset by 10 in the array
		return num;
	}
}
