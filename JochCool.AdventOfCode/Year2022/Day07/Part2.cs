namespace JochCool.AdventOfCode.Year2022.Day07;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		Directory root = Directory.ParseTree(inputReader.ReadLines());

		Directory.SizeReport sizeReport = root.GetSizeReport();

		const int totalDiskSpace = 70000000;
		const int requiredDiskSpace = 30000000;

		int minSize = sizeReport.TotalSize + (requiredDiskSpace - totalDiskSpace);
		Console.WriteLine(minSize);

		Directory result = root.GetSmallest(minSize);
		return result.sizeCache.ToString();
	}
}
