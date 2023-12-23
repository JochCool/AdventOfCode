namespace JochCool.AdventOfCode.Year2023.Day23;

static class BothParts
{
	static readonly Vector<int>[] allDirs = Vector<int>.AxisUnitVectors;
	static readonly Vector<int>[] right = [Vector<int>.ToPositiveX];
	static readonly Vector<int>[] down = [Vector<int>.ToPositiveY];
	static readonly Vector<int>[] left = [Vector<int>.ToNegativeX];
	static readonly Vector<int>[] up = [Vector<int>.ToNegativeY];

	public static Node[] MazeToGraph(string[] grid, bool isSlippery)
	{
		Vector<int> maxPos = new(grid[0].Length - 1, grid.Length - 1);

		int startX = -1;
		string topLine = grid[0];
		for (int x = 0; x < topLine.Length; x++)
		{
			if (topLine[x] == '.')
			{
				if (startX != -1) throw new FormatException("There are multiple starting tiles.");
				startX = x;
			}
		}
		if (startX == -1) throw new FormatException("There is no starting tile.");

		List<Node> nodes = [];

		Node startNode = new(nodes.Count, new Vector<int>(startX, 0));
		nodes.Add(startNode);

		Node? endNode = null;

		Queue<(Node StartNode, Vector<int> Direction)> toVisit = [];
		toVisit.Enqueue((startNode, Vector<int>.ToPositiveY));

		while (true)
		{
			if (!toVisit.TryDequeue(out (Node, Vector<int>) entry))
			{
				break;
			}
			(Node pathStartNode, Vector<int> direction) = entry;
			Vector<int> position = pathStartNode.Position;
			int pathLength = 0;
			while (true)
			{
				position += direction;
				pathLength++;

				if (position.Y == maxPos.Y)
				{
					if (endNode is not null) throw new FormatException("There are multiple ending tiles.");
					endNode = new(nodes.Count, position);
					pathStartNode.Edges.Add(new(endNode, pathLength));
				}

				Vector<int>[] allowedDirections = grid[position.Y][position.X] switch
				{
					'.' => allDirs,
					'>' => isSlippery ? right : allDirs,
					'v' => isSlippery ? down : allDirs,
					'<' => isSlippery ? left : allDirs,
					'^' => isSlippery ? up : allDirs,
					'#' => [],
					_ => throw new FormatException($"Unknown tile '{grid[position.Y][position.X]}' in position {position}.")
				};

				Vector<int> notAllowedDirection = -direction; // cannot go back

				int possibleDirectionIndices = 0;
				for (int i = 0; i < allowedDirections.Length; i++)
				{
					Vector<int> possibleDirection = allowedDirections[i];
					if (possibleDirection == notAllowedDirection) continue;

					Vector<int> possiblePosition = position + possibleDirection;
					if (possiblePosition.IsInBox(Vector<int>.Origin, maxPos) &&
						grid[possiblePosition.Y][possiblePosition.X] != '#')
					{
						possibleDirectionIndices |= 1 << i;
						direction = possibleDirection;
					}
				}
				if (int.IsPow2(possibleDirectionIndices)) continue;
				if (possibleDirectionIndices == 0) break;

				// If we got here, we're in a junction
				// First check if we've been here before
				bool foundExistingNode = false;
				foreach (Node node in nodes)
				{
					if (node.Position == position)
					{
						pathStartNode.Edges.Add(new(node, pathLength));
						foundExistingNode = true;
						break;
					}
				}
				if (foundExistingNode) break;

				Node pathEndNode = new(nodes.Count, position);
				nodes.Add(pathEndNode);
				pathStartNode.Edges.Add(new(pathEndNode, pathLength));

				for (int i = 0; i < allowedDirections.Length; i++)
				{
					if ((possibleDirectionIndices & ~(1 << i)) != 0)
					{
						toVisit.Enqueue((pathEndNode, allowedDirections[i]));
					}
				}
				break;
			}
		}

		if (endNode is null) throw new FormatException("There is no ending tile.");
		nodes.Add(endNode);

		return nodes.ToArray();
	}
}
