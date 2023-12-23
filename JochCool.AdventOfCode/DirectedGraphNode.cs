namespace JochCool.AdventOfCode;

public abstract class DirectedGraphNode<TSelf, TEdge>
	where TSelf : DirectedGraphNode<TSelf, TEdge>
	where TEdge : IEdge<TSelf>
{
	int cost = int.MaxValue;

	//public abstract string Name { get; }

	public abstract IEnumerable<TEdge> Edges { get; }

	// Uses the Dijkstra algorithm
	// NOTE: this is not thread safe. Do not call this from multiple threads at once for the same graph.
	public int FindShortestPathTo(TSelf target)
	{
		ArgumentNullException.ThrowIfNull(target);

		return FindShortestPathTo(target, null);
	}

	// The cost matrix acts as a cache for distances between nodes.
	// There are several requirements for using a cost matrix:
	// 1. The cost of each edge must be positive.
	// 2. GetHashCode() must return a unique value for every node in the graph, which is never negative (and preferably never a large value).
	// 3. The width and height of the matrix must both be larger than the largest hash code in the graph.
	internal int FindShortestPathTo(TSelf target, int[,]? costMatrix)
	{
		if (this == target) return 0;

		int myHash;
		if (costMatrix is not null)
		{
			myHash = GetHashCode();
			int targetHash = target.GetHashCode();
			int cachedValue = costMatrix[myHash, targetHash];
			if (cachedValue > 0) return cachedValue;
		}
		else myHash = 0; // just to initalise; won't be used

		List<TSelf> toCleanUp = [];
		PriorityQueue<DirectedGraphNode<TSelf, TEdge>, int> dijkstraQueue = new();

		DirectedGraphNode<TSelf, TEdge>? currentNode = this;
		int currentNodeCost = 0;
		currentNode.cost = currentNodeCost;
		try
		{
			while (true)
			{
				int currentNodeHash = costMatrix is null ? 0 : currentNode.GetHashCode();

				foreach (TEdge path in currentNode.Edges)
				{
					int nextNodeCost = currentNodeCost + path.Cost;
					TSelf nextNode = path.To;
					if (nextNode.cost <= nextNodeCost)
					{
						continue; // We've already been here with a shorter path
					}

					nextNode.cost = nextNodeCost;

					if (costMatrix is not null)
					{
						int nextNodeHash = nextNode.GetHashCode();
						costMatrix[currentNodeHash, nextNodeHash] = path.Cost;
						costMatrix[nextNodeHash, currentNodeHash] = path.Cost;
						costMatrix[myHash, nextNodeHash] = nextNodeCost;
						costMatrix[nextNodeHash, myHash] = nextNodeCost;
					}

					toCleanUp.Add(nextNode);
					dijkstraQueue.Enqueue(nextNode, nextNodeCost);
				}

				do
				{
					if (!dijkstraQueue.TryDequeue(out currentNode, out currentNodeCost))
					{
						throw new ArgumentException("Target is not in the graph.", nameof(target));
					}
				}
				while (currentNodeCost > currentNode.cost); // If true, then this node must have been enqueued multiple times, and we've hit it before, so just skip it.

				if (currentNode == target)
				{
					return currentNodeCost;
				}
			}
		}
		finally
		{
			// Reset cost
			cost = int.MaxValue;
			foreach (TSelf node in toCleanUp) node.cost = int.MaxValue;
		}
	}

	// Returns -1 if it is not in the graph.
	// NOTE: this is not thread safe. Do not call this from multiple threads at once for the same graph.
	public int FindLongestPathTo(TSelf target)
	{
		if (target == this) return 0;

		// The cost field is now used to mark that we've been here already (0 means that we have)
		cost = 0;

		int highest = -1;
		foreach (TEdge edge in Edges)
		{
			TSelf nextNode = edge.To;
			if (nextNode.cost == 0) continue;

			int cost = nextNode.FindLongestPathTo(target);
			if (cost == -1) continue;

			cost += edge.Cost;
			if (cost > highest) highest = cost;
		}

		// Reset cost
		cost = int.MaxValue;

		return highest;
	}

	public abstract override int GetHashCode();
}

public interface IEdge<TNode>
{
	TNode To { get; }

	int Cost { get; }
}

public interface IDoubleEdge<TNode> : IEdge<TNode>
{
	TNode From { get; }
}

// Default implementation of IEdge
public readonly struct DirectedGraphEdge<TNode>(TNode to, int cost) : IEdge<TNode>
{
	public TNode To => to;
	public int Cost => cost;
}
