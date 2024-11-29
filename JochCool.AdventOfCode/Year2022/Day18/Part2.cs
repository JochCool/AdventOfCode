namespace JochCool.AdventOfCode.Year2022.Day18;

public static class Part2
{
	const int size = 8;

	static readonly Vector3D<int> minCoords = Vector3D<int>.Origin;
	static readonly Vector3D<int> maxCoords = new(size - 1, size - 1, size - 1);

	public static string? Solve(TextReader inputReader)
	{
		Cube[,,] space = new Cube[size, size, size];
		foreach (string line in inputReader.ReadLines())
		{
			int i = 0;
			int x = StringUtil.ParseAt<int>(line, ref i, ',') + 1;
			i++;
			int y = StringUtil.ParseAt<int>(line, ref i, ',') + 1;
			i++;
			int z = StringUtil.ParseAt<int>(line, ref i, ',') + 1;

			space[x, y, z] = Cube.Lava;
		}

		Stack<Vector3D<int>> toVisit = new();

		int result = 0;
		Vector3D<int> pos = new(0, 0, 0);

		while (true)
		{
			space[pos.X, pos.Y, pos.Z] = Cube.Steam;
			foreach (Vector3D<int> dir in Vector3D<int>.AxisUnitVectors)
			{
				Vector3D<int> newPos = pos + dir;
				if (!newPos.IsInBox(minCoords, maxCoords)) continue;
				switch (space[newPos.X, newPos.Y, newPos.Z])
				{
					case Cube.Lava:
					{
						result++;
						break;
					}

					case Cube.Empty:
					{
						toVisit.Push(newPos);
						break;
					}
				}
			}
			if (toVisit.Count == 0) break;
			pos = toVisit.Pop();
		}

		//int result = FillAt(space, new(0, 0, 0));

		/*
		int emptySpaceCount = 0;
		for (int x = 0; x < size; x++)
		{
			StringBuilder sb = new("X = ");
			sb.Append(x);
			sb.Append(":\n   ");
			for (int z = 0; z < size; z++)
			{
				sb.Append(z % 10);
			}
			sb.Append('\n');
			for (int y = 0; y < size; y++)
			{
				if (y < 10) sb.Append('0');
				sb.Append(y);
				sb.Append(' ');
				for (int z = 0; z < size; z++)
				{
					switch (space[x, y, z])
					{
						case Cube.Empty:
						{
							Console.WriteLine($"Empty space at ({x}, {y}, {z})");
							emptySpaceCount++;
							sb.Append('.');
							break;
						}
						case Cube.Lava:
						{
							sb.Append('#');
							break;
						}
						case Cube.Steam:
						{
							sb.Append('*');
							break;
						}
					}
				}
				sb.Append('\n');
			}
			Console.WriteLine(sb.ToString());
			Console.ReadLine();
		}
		Console.WriteLine($"Total of {emptySpaceCount} empty spaces.");
		*/

		return result.ToInvariantString();
	}

	/*
	static readonly HashSet<Vector3D<int>> visited = new();

	static int FillAt(Cube[,,] space, Vector3D<int> pos)
	{
		if (!pos.IsInBox(minCoords, maxCoords)) return 0;
		switch (space[pos.X, pos.Y, pos.Z])
		{
			case Cube.Steam:
				return 0; // already visited

			case Cube.Lava:
				return 1;

			case Cube.Empty:
			{
				if (visited.Contains(pos))
				{
					throw new UnexpectedStateException();
				}
				visited.Add(pos);
				//Console.WriteLine(pos);
				space[pos.X, pos.Y, pos.Z] = Cube.Steam;
				int total = 0;
				foreach (Vector3D<int> dir in Vector3D<int>.AxisUnitVectors)
				{
					total += FillAt(space, pos + dir);
				}
				return total;
			}

			default:
				throw new UnexpectedStateException();
		}
	}

	static int FillAt(Cube[,,] space, Vector3D<int> pos)
	{
		space[pos.X, pos.Y, pos.Z] = Cube.Steam;
		int total = 0;
		foreach (Vector3D<int> dir in Vector3D<int>.AxisUnitVectors)
		{
			Vector3D<int> newPos = pos + dir;
			if (!newPos.IsInBox(minCoords, maxCoords)) continue;
			switch (space[newPos.X, newPos.Y, newPos.Z])
			{
				case Cube.Lava:
				{
					total++;
					break;
				}

				case Cube.Empty:
				{
					total += FillAt(space, newPos);
					break;
				}
			}
		}
		return total;
	}
	*/

	enum Cube
	{
		Empty,
		Lava,
		Steam
	}
}
