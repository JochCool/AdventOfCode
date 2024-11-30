namespace JochCool.AdventOfCode.Year2020.Day01;

public static class Part2
{
	const int target = 2020;

	public static string? Solve(TextReader input)
	{
		// read numbers
		List<int> numbers = input.ParseAllLinesInvariant<int>();

		// sort numbers so we can do binary search
		numbers.Sort();

		Console.WriteLine($"Sorted! Lowest is {numbers[0]} and highest is {numbers[^1]}.");

		// for each two numbers, find the third that makes 2020
		for (int i1 = 0; i1 < numbers.Count; i1++)
		{
			for (int i2 = 0; i2 < numbers.Count; i2++)
			{
				if (i1 == i2) continue;

				int num1 = numbers[i1];
				int num2 = numbers[i2];
				int num3 = target - num1 - num2;
				if (numbers.BinarySearch(num3) < 0) continue;

				int product = num1 * num2 * num3;
				Console.WriteLine($"Found! {num1} * {num2} * {num3} = {product}");
				return product.ToInvariantString();
			}
		}
		Console.WriteLine("None found.");
		return null;
	}
}
