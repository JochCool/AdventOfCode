namespace JochCool.AdventOfCode.Year2023.Day23;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		Node[] nodes = BothParts.MazeToGraph(inputReader.ReadAllLines(), false);
		
		return nodes[0].FindLongestPathTo(nodes[^1]).ToString();
	}
}
