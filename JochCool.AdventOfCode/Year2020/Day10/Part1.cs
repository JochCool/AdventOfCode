namespace JochCool.AdventOfCode.Year2020.Day10;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		// Load & sort input
		List<int> joltages = inputReader.ParseAllLines<int>();
		joltages.Sort();

		int numDiff1 = 0;
		int numDiff3 = 1; // according to the assignment, there is another diff 3 at the end
		int prevNum = 0; // according to the assignment, it starts with 0 jolts
		foreach (int num in joltages)
		{
			int diff = num - prevNum;
			if (diff == 1) numDiff1++;
			if (diff == 3) numDiff3++;
			prevNum = num;
		}

		int product = numDiff1 * numDiff3;
		Console.WriteLine($"There's {numDiff1} one-jolt differences and {numDiff3} three-jolt differences (product: {product}).");

		return product.ToInvariantString();
	}
}
