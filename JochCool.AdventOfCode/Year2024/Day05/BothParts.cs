namespace JochCool.AdventOfCode.Year2024.Day05;

static class BothParts
{
	public static PageNumberComparer ParseRules(TextReader inputReader)
	{
		PageNumberComparer comparer = new();

		string? line;
		while (!string.IsNullOrEmpty(line = inputReader.ReadLine()))
		{
			int separatorI = line.IndexOf('|');
			if (separatorI == -1) throw new FormatException();

			int num1 = int.Parse(line.AsSpan(0, separatorI), CultureInfo.InvariantCulture);
			int num2 = int.Parse(line.AsSpan(separatorI + 1), CultureInfo.InvariantCulture);

			comparer.AddRule(num1, num2);
		}

		return comparer;
	}
}
