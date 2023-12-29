namespace JochCool.AdventOfCode.Year2023.Day25;

class Edge
{
	internal Node node1;
	internal Node node2;

	private Edge(Node node1, Node node2)
	{
		this.node1 = node1;
		this.node2 = node2;
	}

	public static Edge Create(Node node1, Node node2)
	{
		Edge edge = new(node1, node2);
		node1.edges.Add(edge);
		node2.edges.Add(edge);
		return edge;
	}

	public bool IsBetweenSameNodes => node1 == node2;
}
