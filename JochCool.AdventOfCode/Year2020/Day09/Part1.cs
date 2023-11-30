namespace JochCool.AdventOfCode.Year2020.Day09;

public static class Part1
{
	const int preambleSize = 25;

	public static string? Solve(TextReader inputReader)
	{
		string? currentLine;
		int currentNum;

		// Last 25 nums
		int[] possibleNumsSorted = new int[preambleSize];

		// Used to determine which number should be removed from possibleNumsSorted. Make sure to dequeue before enqueueing so that the capacity doesn't need to be increased.
		Queue<int> possibleNums = new Queue<int>(preambleSize);

		// Read preamble
		for (int i = 0; i < preambleSize; i++)
		{
			currentLine = inputReader.ReadLine();
			if (currentLine is null)
			{
				Console.WriteLine($"Found {i} numbers in the file but expected at least {preambleSize}.");
				return null;
			}
			currentNum = int.Parse(currentLine);
			possibleNums.Enqueue(currentNum);
			possibleNumsSorted[i] = currentNum; // we'll sort later
		}

		Array.Sort(possibleNumsSorted);

		while ((currentLine = inputReader.ReadLine()) is not null)
		{
			currentNum = int.Parse(currentLine);

			// For every possible num, check if the other num that you can add to make currentNum exists
			foreach (int possibleNum in possibleNumsSorted)
			{
				if (Array.BinarySearch(possibleNumsSorted, currentNum - possibleNum) >= 0)
				{
					goto ValidNumber;
				}
			}

			// If we got here, the num is not found
			Console.WriteLine($"{currentLine} is not a valid number.");
			return currentLine;

		ValidNumber:
			// The oldest num should be replaced with the new num.
			// Find their locations:
			int numToRemoveIndex = Array.BinarySearch(possibleNumsSorted, possibleNums.Dequeue());
			int numToAddIndex = Array.BinarySearch(possibleNumsSorted, currentNum);
			if (numToAddIndex < 0) numToAddIndex = ~numToAddIndex;

			// Shift the nums that are in between the two indices
			int direction;
			if (numToAddIndex > numToRemoveIndex)
			{
				direction = 1;
				numToAddIndex--;
			}
			else
			{
				direction = -1;
			}
			while (numToRemoveIndex != numToAddIndex)
			{
				possibleNumsSorted[numToRemoveIndex] = possibleNumsSorted[numToRemoveIndex + direction];
				numToRemoveIndex += direction;
			}
			possibleNumsSorted[numToAddIndex] = currentNum;

			possibleNums.Enqueue(currentNum);
		}

		Console.WriteLine("All numbers are valid!");
		return null;
	}
}
