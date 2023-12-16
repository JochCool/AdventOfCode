namespace JochCool.AdventOfCode.Year2023.Day16;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();

		Beam beam;
		beam.Position = new(-1, 0);
		beam.Direction = Vector<int>.ToPositiveX;

		return BothParts.SendBeam(grid, beam).ToString();
	}
}
