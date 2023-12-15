namespace JochCool.AdventOfCode.Year2023.Day15;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string input = inputReader.ReadToEnd().Replace("\n", "");

		int sum = 0;
		for (int i  = 0; i < input.Length; i++)
		{
			int endI = input.IndexOf(',', i);
			if (endI == -1) endI = input.Length;

			sum += Hash(input.AsSpan(i, endI - i));
			i = endI;
		}
		return sum.ToString();
	}

	public static byte Hash(ReadOnlySpan<char> input)
	{
		byte currentValue = 0;
		foreach (char c in input)
		{
			currentValue = (byte)((currentValue + c) * 17);
		}
		return currentValue;
	}
}
