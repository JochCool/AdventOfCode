namespace JochCool.AdventOfCode.Year2023.Day17;

static class BothParts
{
	public static string? Solve(TextReader inputReader, int minSteps, int maxSteps)
	{
		string[] grid = inputReader.ReadAllLines();

		Vector<int> endPos = new Vector<int>(grid[0].Length - 1, grid.Length - 1);

		Node currentNode = default;
		int heatLost = 0;

		Vector<int>[] dirs = Vector<int>.AxisUnitVectors;

		PriorityQueue<Node, AStarPriority> priorityQueue = new();

		Dictionary<Node, int> knownHeatLosts = new();

		while (true)
		{
			for (int i = 0; i < dirs.Length; i++)
			{
				int prevMovingDirI = currentNode.DirectionIndex;
				int stepsDone = currentNode.StepsDone;
				int newStepsDone;
				if (stepsDone == 0) // initial position
				{
					newStepsDone = 1;
				}
				else if (prevMovingDirI == i)
				{
					if (stepsDone >= maxSteps) continue;
					newStepsDone = stepsDone + 1;
				}
				else if (prevMovingDirI == (i >= 2 ? i - 2 : i + 2)) // can't go backwards
				{
					continue;
				}
				else
				{
					if (stepsDone < minSteps) continue;
					newStepsDone = 1;
				}

				Vector<int> newPos = currentNode.Position + dirs[i];
				if (!newPos.IsInBox(Vector<int>.Origin, endPos))
					continue;

				Node newNode = new(newPos, newStepsDone, i);
				int newHeatLost = heatLost + (grid[newPos.Y][newPos.X] - '0');
				if (knownHeatLosts.TryGetValue(newNode, out int knownHeatLost) && knownHeatLost <= newHeatLost)
				{
					continue;
				}
				knownHeatLosts[newNode] = newHeatLost;

				if (newPos == endPos && newStepsDone >= minSteps)
				{
					return newHeatLost.ToString();
				}

				priorityQueue.Enqueue(newNode, new AStarPriority(newHeatLost, (endPos - newPos).TaxicabMagnitude));
			}

			do
			{
				if (!priorityQueue.TryDequeue(out currentNode, out AStarPriority priority))
				{
					return null;
				}
				heatLost = priority.Cost;
			}
			while (knownHeatLosts[currentNode] != heatLost); // This would mean we must have been here before
		}
	}
}
