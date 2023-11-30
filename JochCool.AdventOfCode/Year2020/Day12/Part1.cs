namespace JochCool.AdventOfCode.Year2020.Day12;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int east = 0;
		int north = 0;
		int dir = 0; // 0 = east, 90 = north, 180 = west, 270 = south

		foreach (string line in inputReader.ReadLines())
		{
			int amount = int.Parse(line[1..]);
			switch (line[0])
			{
				case 'N':
					north += amount;
					break;

				case 'S':
					north -= amount;
					break;

				case 'E':
					east += amount;
					break;

				case 'W':
					east -= amount;
					break;

				case 'L':
					dir += amount;
					while (dir >= 360) dir -= 360;
					break;

				case 'R':
					dir -= amount;
					while (dir < 0) dir += 360;
					break;

				case 'F':
					if (dir == 0) goto case 'E';
					if (dir == 90) goto case 'N';
					if (dir == 180) goto case 'W';
					if (dir == 270) goto case 'S';
					throw new ArgumentException("Invalid direction.");
			}
		}

		int manhattanDistance = east + north;
		Console.WriteLine($"Manhattan distance is {east} + {north} = {manhattanDistance}.");

		return manhattanDistance.ToString();
	}
}
