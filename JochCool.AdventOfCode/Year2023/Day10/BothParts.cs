namespace JochCool.AdventOfCode.Year2023.Day10;

static class BothParts
{
	private static readonly Dictionary<char, (Vector<int> Direction1, Vector<int> Direction2)> pipeDirs = new()
	{
		{ '|', (Vector<int>.ToNegativeY, Vector<int>.ToPositiveY) },
		{ '-', (Vector<int>.ToNegativeX, Vector<int>.ToPositiveX) },
		{ 'L', (Vector<int>.ToNegativeY, Vector<int>.ToPositiveX) },
		{ 'J', (Vector<int>.ToNegativeY, Vector<int>.ToNegativeX) },
		{ '7', (Vector<int>.ToPositiveY, Vector<int>.ToNegativeX) },
		{ 'F', (Vector<int>.ToPositiveY, Vector<int>.ToPositiveX) },
	};

	// Calls a delegate at every pipe in a potential loop
	// Returns false if it doesn't loop
	public static bool FollowPipes(string[] grid, Vector<int> startPos, Vector<int> startDir, StepAction stepAction)
	{
		Vector<int> maxPos = new(grid[^1].Length - 1, grid.Length - 1);

		Vector<int> dir = startDir;
		Vector<int> pos = startPos;

		while (true)
		{
			pos += dir;

			if (pos == startPos)
			{
				stepAction(pos, dir, startDir);
				Console.WriteLine($"Found a loop!");
				return true;
			}

			if (!pos.IsInBox(new(0, 0), maxPos))
			{
				Console.WriteLine($"Pipe leads to the edge at {pos}.");
				return false;
			}

			if (!pipeDirs.TryGetValue(grid[pos.Y][pos.X], out (Vector<int> Direction1, Vector<int> Direction2) dirs))
			{
				Console.WriteLine($"Pipe leads to nothing at {pos}.");
				return false;
			}

			Vector<int> newDir;
			if (dir == -dirs.Direction1)
			{
				newDir = dirs.Direction2;
			}
			else if (dir == -dirs.Direction2)
			{
				newDir = dirs.Direction1;
			}
			else
			{
				Console.WriteLine($"Pipe leads to non-connected pipe at {pos}.");
				return false;
			}

			stepAction(pos, dir, newDir);

			dir = newDir;
		}
	}
}

delegate void StepAction(Vector<int> position, Vector<int> incomingDirection, Vector<int> outgoingDirection);
