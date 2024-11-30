namespace JochCool.AdventOfCode.Year2020.Day09;

public static class Part2
{
	// This equals the result of part 1.
	const int target = 675280050;

	public static string? Solve(TextReader inputReader)
	{
		// All numbers that are currently being considered as part of the range to find.
		Queue<int> currentNums = new Queue<int>();

		// Sum of all numbers in the queue
		int currentSum = 0;

		foreach (string line in inputReader.ReadLines())
		{
			// add num
			int currentNum = int.Parse(line, CultureInfo.InvariantCulture);
			if (currentNum >= target) break;
			currentNums.Enqueue(currentNum);
			currentSum += currentNum;

			// remove nums
			while (currentSum > target)
			{
				currentSum -= currentNums.Dequeue();
			}

			// found?
			if (currentSum == target)
			{
				int firstNum = currentNums.Dequeue();
				int min = firstNum, max = firstNum;
				foreach (int num in currentNums)
				{
					if (num < min) min = num;
					if (num > max) max = num;
				}
				int weakness = min + max;
				Console.WriteLine($"The numbers in the range {firstNum} - {currentNum} work! The encryption weakness is {min} + {max} = {weakness}.");
				return weakness.ToInvariantString();
			}
		}
		Console.WriteLine("No valid range found.");
		return null;
	}
}
