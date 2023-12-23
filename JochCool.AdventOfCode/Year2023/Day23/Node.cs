
namespace JochCool.AdventOfCode.Year2023.Day23;

class Node : DirectedGraphNode<Node, DirectedGraphEdge<Node>>
{
	public int Id { get; }

	public Vector<int> Position { get; }

	public override List<DirectedGraphEdge<Node>> Edges { get; } = [];

	public Node(int id, Vector<int> position)
	{
		Position = position;
	}

	public override int GetHashCode() => Id;
}
