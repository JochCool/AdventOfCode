namespace JochCool.AdventOfCode.Year2023.Day09;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int[] history = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ParseAllInvariant<int>().ToArray();
			int[][] sequences = new int[history.Length][];
			sequences[0] = history;

			int sequenceI = 1;
			for (; sequenceI < sequences.Length; sequenceI++)
			{
				int[] currentSequence = sequences[sequenceI - 1];
				int[] newSequence = new int[currentSequence.Length - 1];
				bool onlyZeroes = true;
				for (int numI = 1; numI < currentSequence.Length; numI++)
				{
					int num = currentSequence[numI] - currentSequence[numI - 1];
					newSequence[numI - 1] = num;
					if (num != 0)
						onlyZeroes = false;
				}
				sequences[sequenceI] = newSequence;

				if (onlyZeroes)
					break;
			}

			int prediction = 0;
			for (; sequenceI >= 0; sequenceI--)
			{
				prediction = sequences[sequenceI][0] - prediction;
			}
			sum += prediction;
		}

		return sum.ToInvariantString();
	}
}
