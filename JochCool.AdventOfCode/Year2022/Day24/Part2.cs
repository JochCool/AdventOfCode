namespace JochCool.AdventOfCode.Year2022.Day24;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		List<Blizzard> blizzards = [];
		Vector<int> gridSize = new(0, 0);
		foreach (string line in inputReader.ReadLines().Skip(1).SkipLast(1))
		{
			if (gridSize.X == 0) gridSize.X = line.Length - 2;
			else if (gridSize.X != line.Length - 2) throw new InvalidDataException("Inconsistent line lengths.");

			for (int i = 1; i < line.Length - 1; i++)
			{
				if (line[i] == '.') continue;
				blizzards.Add(new(new(i - 1, gridSize.Y), line[i] switch
				{
					'>' => Vector<int>.ToPositiveX,
					'v' => Vector<int>.ToPositiveY,
					'<' => Vector<int>.ToNegativeX,
					'^' => Vector<int>.ToNegativeY,
					_ => throw new InvalidDataException($"Unexpected character {line[i]}")
				}));
			}

			gridSize.Y++;
		}

		Vector<int> maxCoord = gridSize - new Vector<int>(1, 1);

		Minute? result = FindShortestPath(
			startingMinute: Minute.FromBlizzardsList(blizzards, gridSize),
			startPos: new Vector<int>(0, -1),
			endPos: maxCoord,
			maxCoord: maxCoord
		);
		if (result is null) return null;

		Console.WriteLine($"Reached first point after {result.Number + 2} minutes.");

		result = FindShortestPath(
			startingMinute: result.GetNext().GetNext(), // To step in and out of the starting area
			startPos: gridSize - new Vector<int>(1, 0),
			endPos: Vector<int>.Origin,
			maxCoord: maxCoord
		);
		if (result is null) return null;

		Console.WriteLine($"Reached second point after {result.Number + 2} minutes.");

		result = FindShortestPath(
			startingMinute: result.GetNext().GetNext(),
			startPos: new Vector<int>(0, -1),
			endPos: maxCoord,
			maxCoord: maxCoord
		);
		if (result is null) return null;

		Console.WriteLine($"Reached first point after {result.Number + 2} minutes.");

		return (result.Number + 2).ToString();
	}

	static readonly Vector<int>[] dirs =
	[
		new(1, 0),
		new(0, 1),
		new(0, 0),
		new(-1, 0),
		new(0, -1)
	];

	static Minute? FindShortestPath(Minute startingMinute, Vector<int> startPos, Vector<int> endPos, Vector<int> maxCoord)
	{
		PriorityQueue<Vector<int>, AStarPriority> toVisit = new();
		HashSet<(Vector<int>, int)> considered = [];

		Vector<int> pos = startPos;

		List<Minute> minutes = [startingMinute];

		int minuteNum = 0;
		while (true)
		{
			Minute minute = minutes[minuteNum];
			minuteNum++;

			foreach (Vector<int> dir in dirs)
			{
				Vector<int> newPos = pos + dir;
				if (newPos != startPos && (!newPos.IsInBox(Vector<int>.Origin, maxCoord) || minute.HasBlizzardAt(newPos)))
				{
					continue;
				}

				if (newPos == endPos)
				{
					Console.WriteLine("Found!");
					return minute;
				}

				if (!considered.Add((newPos, minuteNum)))
				{
					continue;
				}
				toVisit.Enqueue(newPos, new AStarPriority(minuteNum, (newPos - endPos).TaxicabMagnitude));
			}

			if (!toVisit.TryDequeue(out pos, out AStarPriority priority))
			{
				Console.WriteLine("There is no path.");
				return null;
			}

			minuteNum = priority.Cost;
			if (minuteNum >= minutes.Count)
			{
				minutes.Add(minutes[^1].GetNext());
			}
		}
	}

	/*
	class Path
	{
		public required Minute Minute { get; set; }

		public required Vector<int> StartPos { get; init; }

		public required Vector<int> EndPos { get; init; }

		public required Vector<int> GridSize { get; init; }

		public int Calculate()
		{

		}
	}
	*/
}
