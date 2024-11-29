namespace JochCool.AdventOfCode.Year2023.Day02;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;
		foreach (string line in inputReader.ReadLines())
		{
			int startI = "Game ".Length;
			int endI = line.IndexOf(':', startI);

			int gameNum = int.Parse(line.AsSpan(startI, endI - startI));

			Dictionary<string, int> contents = new();

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
						contents[color] = num;
					}
				}
				else contents[color] = num;

				startI = endI + ", ".Length;
				if (startI >= line.Length)
					break;
			}

			int power = contents.GetValueOrDefault("red") * contents.GetValueOrDefault("green") * contents.GetValueOrDefault("blue");

			sum += power;
			Console.WriteLine(power);
		}

		return sum.ToInvariantString();
	}
}
