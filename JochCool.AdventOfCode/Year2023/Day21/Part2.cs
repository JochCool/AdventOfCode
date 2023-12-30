using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2023.Day21;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		const int totalSteps = 26501365;

		string[] grid = inputReader.ReadAllLines();
		Vector<int> gridSize = new(grid[0].Length, grid.Length);

		if (gridSize.X != gridSize.Y) throw new NotImplementedException("Grid must be square.");
		if (!int.IsOddInteger(gridSize.X)) throw new NotImplementedException("Grid must have an odd size.");
		if (gridSize.TaxicabMagnitude > totalSteps) throw new NotImplementedException("Grid is too large.");

		Vector<int> startPos = grid.PositionOf('S');

		if (startPos.X != startPos.Y || startPos.X != gridSize.X / 2) throw new NotImplementedException("Starting position must be in the middle.");

		int evenCount = BothParts.CountSteps(grid, startPos, totalSteps & ~1);
		int oddCount = BothParts.CountSteps(grid, startPos, totalSteps + (totalSteps | ~1) + 1);

		int fullGridSteps = (totalSteps - startPos.TaxicabMagnitude) / gridSize.X;

		int oddCountMultiplier, evenCountMultiplier;
		if (int.IsEvenInteger(fullGridSteps))
		{
			oddCountMultiplier = fullGridSteps + 1;
			evenCountMultiplier = fullGridSteps;
		}
		else
		{
			oddCountMultiplier = fullGridSteps;
			evenCountMultiplier = fullGridSteps + 1;
		}
		// The bulk of the area
		long result = (long)evenCount * evenCountMultiplier * evenCountMultiplier + (long)oddCount * oddCountMultiplier * oddCountMultiplier;

		// Add the corners of the area
		int stepsToGo = totalSteps - startPos.X - gridSize.X * fullGridSteps - 1;
		result += CountStepsFromEdges(grid, stepsToGo);

		stepsToGo -= gridSize.X;
		result += CountStepsFromEdges(grid, stepsToGo);

		Debug.Assert(stepsToGo - gridSize.X < 0);

		// Add the edges of the area
		int stepsToGoDiagonal = totalSteps - startPos.TaxicabMagnitude - gridSize.X * (fullGridSteps - 1) - 2;
		result += (long)CountStepsFromCorners(grid, stepsToGoDiagonal) * fullGridSteps;

		stepsToGoDiagonal -= gridSize.X;
		result += (long)CountStepsFromCorners(grid, stepsToGoDiagonal) * (fullGridSteps + 1);

		Debug.Assert(stepsToGoDiagonal - gridSize.X < 0);

		return result.ToString();
	}

	private static int CountStepsFromEdges(string[] grid, int maxStepCount)
	{
		int gridSizeY = grid.Length;
		int gridSizeX = grid[0].Length;
		return BothParts.CountSteps(grid, new(0, gridSizeY / 2), maxStepCount)
			+ BothParts.CountSteps(grid, new(gridSizeX / 2, 0), maxStepCount)
			+ BothParts.CountSteps(grid, new(gridSizeX - 1, gridSizeY / 2), maxStepCount)
			+ BothParts.CountSteps(grid, new(gridSizeX / 2, gridSizeY - 1), maxStepCount);
	}

	private static int CountStepsFromCorners(string[] grid, int maxStepCount)
	{
		int gridSizeY = grid.Length;
		int gridSizeX = grid[0].Length;
		return BothParts.CountSteps(grid, new(0, 0), maxStepCount)
			+ BothParts.CountSteps(grid, new(gridSizeX - 1, 0), maxStepCount)
			+ BothParts.CountSteps(grid, new(0, gridSizeY - 1), maxStepCount)
			+ BothParts.CountSteps(grid, new(gridSizeX - 1, gridSizeY - 1), maxStepCount);
	}
}
