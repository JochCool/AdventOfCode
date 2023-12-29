namespace JochCool.AdventOfCode.Year2023.Day25;

class Node
{
	internal readonly int id;
	internal readonly List<Edge> edges = [];
	internal int size = 1;

	public Node(int id) => this.id = id;
}
