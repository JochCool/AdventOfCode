namespace JochCool.AdventOfCode.Year2022.Day24;

public static class Part1
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

		Vector<int> end = gridSize - new Vector<int>(1, 1);

		PriorityQueue<Vector<int>, AStarPriority> toVisit = new();

		HashSet<(Vector<int>, int)> considered = [];

		Vector<int> startPos = new(0, -1);
		Vector<int> pos = startPos;
		Vector<int>[] dirs =
		[
			new(1, 0),
			new(0, 1),
			new(0, 0),
			new(-1, 0),
			new(0, -1)
		];

		List<Minute> minutes =
		[
			Minute.FromBlizzardsList(blizzards, gridSize)
		];
		int minuteNum = 0;

		while (true)
		{
			Minute minute = minutes[minuteNum];
			minuteNum++;

			foreach (Vector<int> dir in dirs)
			{
				Vector<int> newPos = pos + dir;
				if (newPos != startPos && (!newPos.IsInBox(Vector<int>.Origin, end) || minute.HasBlizzardAt(newPos)))
				{
					continue;
				}

				if (newPos == end)
				{
					Console.WriteLine("Found!");
					return (minuteNum+1).ToInvariantString();
				}

				if (!considered.Add((newPos, minuteNum)))
				{
					continue;
				}
				toVisit.Enqueue(newPos, new AStarPriority(minuteNum, (newPos - end).TaxicabMagnitude));
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
}
