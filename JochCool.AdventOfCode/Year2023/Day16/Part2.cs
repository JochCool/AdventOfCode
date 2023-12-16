namespace JochCool.AdventOfCode.Year2023.Day16;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();

		int highest = int.MinValue;
		for (int y = 0; y < grid.Length; y++)
		{
			Beam beam;
			beam.Position = new(-1, y);
			beam.Direction = Vector<int>.ToPositiveX;
			int num = BothParts.SendBeam(grid, beam);
			if (num > highest)
			{
				highest = num;
			}
		}

		for (int x = 0; x < grid[0].Length; x++)
		{
			Beam beam;
			beam.Position = new(x, -1);
			beam.Direction = Vector<int>.ToPositiveY;
			int num = BothParts.SendBeam(grid, beam);
			if (num > highest)
			{
				highest = num;
			}
		}

		for (int y = 0; y < grid.Length; y++)
		{
			Beam beam;
			beam.Position = new(grid[0].Length, y);
			beam.Direction = Vector<int>.ToNegativeX;
			int num = BothParts.SendBeam(grid, beam);
			if (num > highest)
			{
				highest = num;
			}
		}

		for (int x = 0; x < grid[0].Length; x++)
		{
			Beam beam;
			beam.Position = new(x, grid.Length);
			beam.Direction = Vector<int>.ToNegativeY;
			int num = BothParts.SendBeam(grid, beam);
			if (num > highest)
			{
				highest = num;
			}
		}

		return highest.ToString();
	}

}
