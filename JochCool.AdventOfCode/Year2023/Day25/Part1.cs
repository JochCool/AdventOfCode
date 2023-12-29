using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2023.Day25;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		Dictionary<string, Node> nodes = [];
		List<Edge> edges = [];

		foreach (string line in inputReader.ReadLines())
		{
			int endI = line.IndexOf(':');
			string name = line[0..endI];

			if (!nodes.TryGetValue(name, out Node? node))
			{
				nodes.Add(name, node = new(nodes.Count));
			}

			foreach (string otherName in line[(endI + 2)..].Split(' '))
			{
				if (!nodes.TryGetValue(otherName, out Node? otherNode))
				{
					nodes.Add(otherName, otherNode = new(nodes.Count));
				}

				edges.Add(Edge.Create(node, otherNode));
			}
		}

		Console.WriteLine($"{nodes.Count} nodes, {edges.Count} edges");

		const int minimumCut = 3;

		// Executes Karger's algorithm repeatedly with varying seeds, until a minimum cut of 3 is found.
		// Choice for initial seed here doesn't matter for the algorithm, but 96 is the seed that works for my puzzle input, so we're starting with that one to speed up unit tests.
		for (int seed = 96; true; seed++)
		{
			// Work with a clone of the original graph so that we're able to retry if needed.
			// A lot of objects are created this way, so there is potential for performance improvements here.
			Node?[] clonedNodes = new Node?[nodes.Count];
			List<Edge> clonedEdges = new(edges.Count);
			foreach (Edge edge in edges)
			{
				int id1 = edge.node1.id;
				Node node1 = clonedNodes[id1] ??= new(id1);

				int id2 = edge.node2.id;
				Node node2 = clonedNodes[id2] ??= new(id2);

				clonedEdges.Add(Edge.Create(node1, node2));
			}

			Random random = new(seed);

			// Each iteration contracts one edge, so at the end exactly 2 nodes will be left.
			int contractionsTotal = clonedNodes.Length - 2;
			for (int i = 0; i < contractionsTotal; i++)
			{
				Edge edgeBeingContracted = clonedEdges[random.Next(clonedEdges.Count)];

				Node nodeBeingContracted = edgeBeingContracted.node1;
				Node nodeBeingDeleted = edgeBeingContracted.node2;

				nodeBeingContracted.size += nodeBeingDeleted.size;
				nodeBeingContracted.edges.AddRange(nodeBeingDeleted.edges);

				foreach (Edge edge in nodeBeingDeleted.edges)
				{
					if (edge.node1 == nodeBeingDeleted)
					{
						edge.node1 = nodeBeingContracted;
					}
					else
					{
						Debug.Assert(edge.node2 == nodeBeingDeleted);
						edge.node2 = nodeBeingContracted;
					}

					if (edge.IsBetweenSameNodes)
					{
						bool success = clonedEdges.Remove(edge);
						Debug.Assert(success);

						success = nodeBeingContracted.edges.Remove(edge);
						Debug.Assert(success);
					}
				}
			}
			if (clonedEdges.Count == 0)
			{
				throw new FormatException("Graph is disjoint?");
			}

			Edge anyEdge = clonedEdges[0];
			int size1 = anyEdge.node1.size;
			int size2 = anyEdge.node2.size;

			Console.WriteLine($"Seed {seed}: cut of {clonedEdges.Count}, partitions sized {size1} by {size2}.");

			if (clonedEdges.Count == minimumCut)
			{
				return (size1 * size2).ToString();
			}
		}
	}
}
