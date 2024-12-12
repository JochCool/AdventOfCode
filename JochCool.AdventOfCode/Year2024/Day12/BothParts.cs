namespace JochCool.AdventOfCode.Year2024.Day12;

public static class BothParts
{
	public static string? Solve(TextReader inputReader, bool countOnlyRegionSides)
	{
		char[,] grid = inputReader.ReadCharGrid();

		int gridSizeY = grid.GetLength(0);
		int gridSizeX = grid.GetLength(1);

		List<Region> regions = [];

		Vector<int>[] directions = Vector<int>.AxisUnitVectors;

		for (int y = 0; y < gridSizeY; y++)
		{
			for (int x = 0; x < gridSizeX; x++)
			{
				char plantType = grid[y, x];

				Region? region = null;

				int perimiter = 0;
				Vector<int> plot = new(x, y);
				foreach (var direction in directions)
				{
					Vector<int> neigbourPlot = plot + direction;
					if (grid.IsInBounds(neigbourPlot) && grid[neigbourPlot.Y, neigbourPlot.X] == plantType)
					{
						Region? foundRegion = FindRegionAt(regions, plantType, neigbourPlot);
						if (foundRegion is null)
						{
							continue;
						}
						if (region is null)
						{
							region = foundRegion;
						}
						else if (foundRegion != region)
						{
							regions.Remove(foundRegion);
							region.MergeWith(foundRegion);
						}
						continue;
					}

					// This is a side plot
					// For part 2, only count the first one of each region side
					if (countOnlyRegionSides && !IsStartOfRegionSide(grid, plantType, plot, direction))
					{
						continue;
					}

					perimiter++;
				}

				if (region is null)
				{
					regions.Add(region = new(plantType));
				}

				region.Area++;
				region.Perimiter += perimiter;
				region.AddPlot(plot);
			}
		}

		int totalPrice = 0;
		foreach (Region region in regions)
		{
			//Console.WriteLine($"A region of {region.PlantType} plants with price {region.Area} * {region.Perimiter} = {region.Area * region.Perimiter}.");
			totalPrice += region.FencePrice;
		}
		return totalPrice.ToInvariantString();
	}

	// Returns true if a plot side is the topmost/leftmost in a region side.
	private static bool IsStartOfRegionSide(char[,] grid, char plantType, Vector<int> plot, Vector<int> sideDirection)
	{
		/*
		In this example grid:
		BBBBB
		BAAAB
		BAAAB
		BBBBB
		The arrows show the four cases for which this function returns true if plantType is 'A'. The start of the arrow is plot, and the direction of the arrow is sideDirection.
		B B B B B
		  ↑      
		B←A A A→B
		         
		B A A A B
		  ↓      
		B B B B B
		*/

		Vector<int> checkDirection = sideDirection.Y == 0 ? new(0, -1) : new(-1, 0);

		Vector<int> checkPlot1 = plot + checkDirection;
		if (!grid.IsInBounds(checkPlot1) || grid[checkPlot1.Y, checkPlot1.X] != plantType)
		{
			return true;
		}

		Vector<int> checkPlot2 = checkPlot1 + sideDirection;
		return grid.IsInBounds(checkPlot2) && grid[checkPlot2.Y, checkPlot2.X] == plantType;
	}

	private static Region? FindRegionAt(List<Region> plots, char plantType, Vector<int> location)
	{
		foreach (Region plot in plots)
		{
			if (plot.PlantType == plantType && plot.HasLocation(location))
			{
				return plot;
			}
		}
		return null;
	}
}
