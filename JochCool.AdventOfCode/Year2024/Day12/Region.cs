namespace JochCool.AdventOfCode.Year2024.Day12;

class Region
{
	public char PlantType { get; }

	public int Area { get; set; }

	public int Perimiter { get; set; }

	private readonly List<Vector<int>> plots = [];

	public Region(char plantType)
	{
		PlantType = plantType;
	}

	public int FencePrice => Area * Perimiter;

	public bool HasLocation(Vector<int> location) => plots.Contains(location);

	public void AddPlot(Vector<int> plot)
	{
		plots.Add(plot);
	}

	public void MergeWith(Region other)
	{
		Area += other.Area;
		Perimiter += other.Perimiter;
		plots.AddRange(other.plots);
	}
}
