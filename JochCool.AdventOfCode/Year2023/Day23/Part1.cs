
namespace JochCool.AdventOfCode.Year2023.Day23;

public static class Part1
{
	
	public static string? Solve(TextReader inputReader)
	{
		Node[] nodes = BothParts.MazeToGraph(inputReader.ReadAllLines(), true);

		// TODO: this graph is acyclic, so it should be possible to find the longest path by negating all the costs and finding the shortest path, which has MUCH better time complexity.
		//return (-startNode.FindShortestPathTo(endNode)).ToString();

		return nodes[0].FindLongestPathTo(nodes[^1]).ToInvariantString();
	}
}
