namespace JochCool.AdventOfCode.Year2024.Day03;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string input = inputReader.ReadToEnd();

		const string prefix = "mul(";
		const char separator = ',';
		const char suffix = ')';

		BigInteger sum = 0;
		int i = 0;
		while (true)
		{
			i = input.IndexOf(prefix, i, StringComparison.Ordinal);
			if (i == -1) break;
			i += prefix.Length;

			int endI = input.IndexOf(separator, i);
			if (endI == -1) break;

			if (!int.TryParse(input.AsSpan(i, endI - i), CultureInfo.InvariantCulture, out int num1))
			{
				continue;
			}

			i = endI + 1;

			endI = input.IndexOf(suffix, i);
			if (endI == -1) break;

			if (!int.TryParse(input.AsSpan(i, endI - i), CultureInfo.InvariantCulture, out int num2))
			{
				continue;
			}

			sum += num1 * num2;

			i = endI + 1;
		}

		return sum.ToInvariantString();
	}
}
