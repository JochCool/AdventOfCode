namespace JochCool.AdventOfCode.Year2020.Day01;

public static class Part1
{
	const int target = 2020;

	public static string? Solve(TextReader inputReader)
	{
		// read numbers
		List<int> numbers = inputReader.ParseAllLines<int>();

		// sort numbers so we can do binary search
		numbers.Sort();

		Console.WriteLine($"Sorted! Lowest is {numbers[0]} and highest is {numbers[^1]}.");

		// for each number, find the other that makes 2020
		foreach (int number in numbers)
		{
			int other = target - number;
			if (numbers.BinarySearch(other) < 0) continue;

			int product = number * other;
			Console.WriteLine($"Found! {number} * {other} = {product}");
			return product.ToInvariantString();
		}
		Console.WriteLine("None found.");
		return null;
	}
}
