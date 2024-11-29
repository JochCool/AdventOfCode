namespace JochCool.AdventOfCode.Year2023.Day02;

public static class Part1
{
	readonly static Dictionary<string, int> contents = new()
	{
		{ "red", 12 },
		{ "green", 13 },
		{ "blue", 14 }
	};

	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;
		foreach (string line in inputReader.ReadLines())
		{
			int startI = "Game ".Length;
			int endI = line.IndexOf(':', startI);

			int gameNum = int.Parse(line.AsSpan(startI, endI - startI));

			bool isGood = true;

			startI = endI + ": ".Length;
			while (true)
			{
				endI = line.IndexOf(' ', startI);
				int num = int.Parse(line.AsSpan(startI, endI - startI));

				startI = endI + 1;
				endI = line.IndexOfAny([',', ';'], startI);
				if (endI == -1) endI = line.Length;

				string color = line[startI..endI];

				if (contents.TryGetValue(color, out int value))
				{
					if (num > value)
					{
						isGood = false;
						break;
					}
				}
				startI = endI + ", ".Length;
				if (startI >= line.Length)
					break;
			}

			if (isGood)
			{
				sum += gameNum;
				Console.WriteLine(gameNum);
			}
		}

		return sum.ToInvariantString();
	}
}
