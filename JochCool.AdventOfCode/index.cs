using JochCool.AdventOfCode;
using System.Diagnostics;

if (args.Length < 3)
{
	Console.Error.WriteLine("Three arguments are expected: the year, the day, and the input file path.");
}

// TODO: proper input validation and error messages
(Solution part1, Solution part2) = args[0] switch
{
	"2020" => JochCool.AdventOfCode.Year2020.Solutions.Get(int.Parse(args[1])),
	"2022" => JochCool.AdventOfCode.Year2022.Solutions.Get(int.Parse(args[1])),
	"2023" => JochCool.AdventOfCode.Year2023.Solutions.Get(int.Parse(args[1])),
	_ => throw new ArgumentException(null, nameof(args))
};

string filePath = args[2];
ExecutePart(part1, filePath, 1);
ExecutePart(part2, filePath, 2);

static void ExecutePart(Solution solution, string filePath, int num)
{
	Console.WriteLine($"======== PART {num}");

	using StreamReader inputReader = new(File.OpenRead(filePath));

	Stopwatch stopwatch = new();
	stopwatch.Start();
	string? result = solution(inputReader);
	stopwatch.Stop();

	Console.WriteLine("========");
	Console.WriteLine(result is null ? "There is no result." : $"Result: {result}");
	Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
}
