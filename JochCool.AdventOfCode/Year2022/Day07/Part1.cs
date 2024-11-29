namespace JochCool.AdventOfCode.Year2022.Day07;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		Directory root = Directory.ParseTree(inputReader.ReadLines());

		Directory.SizeReport sizeReport = root.GetSizeReport();
		Console.WriteLine($"Total size: {sizeReport.TotalSize}\nTotal small dir size: {sizeReport.TotalSmallDirSize}");
		return sizeReport.TotalSmallDirSize.ToInvariantString();
	}
}
