using JochCool.AdventOfCode;
using System.Diagnostics;

if (args.Length == 0)
{
	Console.Error.WriteLine("What do you want?");
	return;
}

switch (args[0])
{
	case "solve":
	{
		if (args.Length < 4)
		{
			Console.Error.WriteLine("Please specify the year, day, and part to solve.");
			return;
		}

		if (!int.TryParse(args[1], out int year) ||
			!int.TryParse(args[2], out int day) ||
			!int.TryParse(args[3], out int part))
		{
			Console.Error.WriteLine("Bad format for year/day/part.");
			return;
		}

		Puzzle? puzzle = Puzzles.Get(year, day, part);
		if (puzzle is null)
		{
			Console.Error.WriteLine("Unknown puzzle.");
			return;
		}
		if (puzzle.Solution is null)
		{
			Console.Error.WriteLine("There is no solution for this puzzle yet.");
			return;
		}

		string fileName;
		if (args.Length == 4)
		{
			fileName = "input.txt";
		}
		else
		{
			fileName = args[4];
		}

		FileStream inputStream;
		try
		{
			inputStream = File.OpenRead(fileName);
		}
		catch (Exception exception)
		{
			Console.Error.WriteLine("Error while reading file:");
			Console.Error.WriteLine(exception);
			return;
		}

		using StreamReader inputReader = new(inputStream);

		// Puzzle solutions should always use the invariant culture.
		// This is here in case I somewhere forgot to explicitly use the invariant culture.
		Thread.CurrentThread.CurrentCulture =
		Thread.CurrentThread.CurrentUICulture =
		CultureInfo.DefaultThreadCurrentCulture =
		CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

		Console.WriteLine($"Executing {year} day {day} part {part}...");

		Stopwatch stopwatch = Stopwatch.StartNew();
		string? result = puzzle.Solution(inputReader);
		stopwatch.Stop();

		Console.WriteLine("========");
		Console.WriteLine(result is null ? "There is no result." : $"Result: {result}");
		Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
		return;
	}

	case "list":
	{
		foreach (Puzzle puzzle in Puzzles.All)
		{
			Console.WriteLine($"{puzzle.Year:D4}/{puzzle.Day:D2}/{puzzle.Part:D1}");
		}
		return;
	}

	default:
	{
		Console.Error.WriteLine($"Unknown option '{args[0]}'.");
		return;
	}
}

/*
args = ["2023", "21", @"C:\Users\JochCool\Documents\Personal\Programming\Repositories\JochCool\AdventOfCode\input.txt"];

if (args.Length < 3)
{
	Console.Error.WriteLine("Three arguments are expected: the year, the day, and the input file path.");
}

// I hate that I have to do this
Thread.CurrentThread.CurrentCulture =
Thread.CurrentThread.CurrentUICulture =
CultureInfo.DefaultThreadCurrentCulture =
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

// TODO: proper input validation and error messages
(Solution part1, Solution part2) = args[0] switch
{
	"2020" => JochCool.AdventOfCode.Year2020.Solutions.Get(int.Parse(args[1])),
	"2022" => JochCool.AdventOfCode.Year2022.Solutions.Get(int.Parse(args[1])),
	"2023" => JochCool.AdventOfCode.Year2023.Solutions.Get(int.Parse(args[1])),
	_ => throw new ArgumentException(null, nameof(args))
};

string filePath = args[2];
//ExecutePart(part1, filePath, 1);
ExecutePart(part2, filePath, 2);

static void ExecutePart(Solution solution, string filePath, int num)
{
	Console.WriteLine($"======== PART {num}");

	using StreamReader inputReader = new(File.OpenRead(filePath));

	Stopwatch stopwatch = new();
	GC.Collect();
	stopwatch.Start();
	string? result = solution(inputReader);
	stopwatch.Stop();

	Console.WriteLine("========");
	Console.WriteLine(result is null ? "There is no result." : $"Result: {result}");
	Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
}
*/
