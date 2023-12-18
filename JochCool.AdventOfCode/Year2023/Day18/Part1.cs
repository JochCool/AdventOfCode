namespace JochCool.AdventOfCode.Year2023.Day18;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		const int gridSize = 800;
		BitGrid dug = new(new(gridSize, gridSize));

		Vector<int> pos = new(gridSize/2, gridSize/2);
		dug[pos] = true;

		int numLeftTurns = 0;
		int numRightTurns = 0;

		Vector<int> prevDirection = new Vector<int>(1, 1);
		foreach (string line in inputReader.ReadLines())
		{
			Vector<int> direction = line[0] switch
			{
				'R' => Vector<int>.ToPositiveX,
				'D' => Vector<int>.ToPositiveY,
				'L' => Vector<int>.ToNegativeX,
				'U' => Vector<int>.ToNegativeY,
				_ => throw new FormatException()
			};
			int amount = int.Parse(line.AsSpan(2, line.IndexOf(' ', 2) - 2));
			for (int i = 0; i < amount; i++)
			{
				pos += direction;
				dug[pos] = true;
			}

			if (prevDirection.X == 0 && direction.X != 0)
			{
				if (int.IsPositive(prevDirection.Y) == int.IsPositive(direction.X))
					numLeftTurns++;
				else
					numRightTurns++;
			}
			else if (prevDirection.Y == 0 && direction.Y != 0)
			{
				if (int.IsPositive(prevDirection.X) == int.IsPositive(direction.Y))
					numRightTurns++;
				else
					numLeftTurns++;
			}

			prevDirection = direction;
		}

		bool isLeft = numLeftTurns > numRightTurns;

		Vector<int> ffPos = pos - prevDirection + Rotate(prevDirection, isLeft);

		Console.WriteLine(dug.ToString());

		int result = dug.Count;
		Console.WriteLine(result);

		result += dug.Floodfill(ffPos);

		Console.WriteLine();
		Console.WriteLine(dug.ToString());

		return result.ToString();
	}

	// Copied over from day 10
	// Rotates a vector 90°
	private static Vector<int> Rotate(Vector<int> vector, bool left)
	{
		return left
			? new(vector.Y, -vector.X)
			: new(-vector.Y, vector.X);
	}
}
