namespace JochCool.AdventOfCode.Year2020.Day12;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		// these are relative to the ship
		int waypointEast = 10;
		int waypointNorth = 1;

		// these are relative to the starting point
		int shipEast = 0;
		int shipNorth = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int amount = int.Parse(line[1..]);
			switch (line[0])
			{
				case 'N':
					waypointNorth += amount;
					break;

				case 'S':
					waypointNorth -= amount;
					break;

				case 'E':
					waypointEast += amount;
					break;

				case 'W':
					waypointEast -= amount;
					break;

				case 'L':
					amount = 360 - amount;
					goto case 'R';

				case 'R':
					switch (amount % 360)
					{
						case 0:
							break;

						case 90:
							int tmp1 = waypointEast;
							waypointEast = waypointNorth;
							waypointNorth = -tmp1;
							break;

						case 180:
							waypointEast = -waypointEast;
							waypointNorth = -waypointNorth;
							break;

						case 270:
							int tmp2 = waypointEast;
							waypointEast = -waypointNorth;
							waypointNorth = tmp2;
							break;

						default:
							throw new ArgumentException($"Invalid direction {amount}.");
					}
					break;

				case 'F':
					shipEast += waypointEast * amount;
					shipNorth += waypointNorth * amount;
					break;
			}
		}

		int manhattanDistance = Math.Abs(shipEast) + Math.Abs(shipNorth);
		Console.WriteLine($"Manhattan distance is |{shipEast}| + |{shipNorth}| = {manhattanDistance}.");

		return manhattanDistance.ToInvariantString();
	}
}
