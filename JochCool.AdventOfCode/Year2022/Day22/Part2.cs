using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2022.Day22;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();
		string path = grid[^1];

		/*
		char[][] charGrid = new char[grid.Length - 2][];
		for (int i = 0; i < charGrid.Length; i++)
		{
			charGrid[i] = grid[i].ToCharArray();
		}
		char[] dirChars = { '>', 'v', '<', '^' };
		//*/

		Vector<int> pos = new(grid[0].IndexOf('.'), 0);

		Vector<int>[] directions =
		[
			Vector<int>.ToPositiveX,
			Vector<int>.ToPositiveY,
			Vector<int>.ToNegativeX,
			Vector<int>.ToNegativeY
		];

		const int regionSize = 50;

		int pathI = 0;
		int dirI = 0;
		while (pathI < path.Length)
		{
			int numberEndI = pathI;
			while (path[numberEndI] is >= '0' and <= '9')
			{
				numberEndI++;
				if (numberEndI >= path.Length) break;
			}

			if (numberEndI != pathI)
			{
				int toMove = int.Parse(path.AsSpan(pathI, numberEndI - pathI));

				Vector<int> dir = directions[dirI];
				while (toMove --> 0)
				{
					Vector<int> newPos = pos + dir;
					int newDirI = dirI;
					char square;
					if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= grid[newPos.Y].Length || (square = grid[newPos.Y][newPos.X]) == ' ')
					{
						Vector<int> region = pos / regionSize;
						switch (region.Y)
						{
							case 0:
							{
								switch (dirI)
								{
									case 0:
									{
										Debug.Assert(region.X == 2);
										newPos = new(2 * regionSize - 1, 3 * regionSize - 1 - newPos.Y);
										newDirI = 2;
										break;
									}
									case 1:
									{
										Debug.Assert(region.X == 2);
										newPos = new(2 * regionSize - 1, newPos.X - regionSize);
										newDirI = 2;
										break;
									}
									case 2:
									{
										Debug.Assert(region.X == 1);
										newPos = new(0, 3 * regionSize - 1 - newPos.Y);
										newDirI = 0;
										break;
									}
									case 3:
									{
										if (region.X == 1)
										{
											newPos = new(0, newPos.X + 2 * regionSize);
											newDirI = 0;
										}
										else
										{
											Debug.Assert(region.X == 2);
											newPos = new(newPos.X - 2 * regionSize, 4 * regionSize - 1);
											newDirI = 3;
										}
										break;
									}
									default:
									{
										Debug.Fail("No such direction.");
										return null;
									}
								}
								break;
							}
							case 1:
							{
								Debug.Assert(region.X == 1);
								switch (dirI)
								{
									case 0:
									{
										newPos = new(newPos.Y + regionSize, regionSize - 1);
										newDirI = 3;
										break;
									}
									case 2:
									{
										newPos = new(newPos.Y - regionSize, 2 * regionSize);
										newDirI = 1;
										break;
									}
									case 1:
									case 3:
									{
										Debug.Fail("No edge in this direction.");
										return null;
									}
									default:
									{
										Debug.Fail("No such direction.");
										return null;
									}
								}
								break;
							}
							case 2:
							{
								switch (dirI)
								{
									case 0:
									{
										Debug.Assert(region.X == 1);
										newPos = new(3 * regionSize - 1, 3 * regionSize - 1 - newPos.Y);
										newDirI = 2;
										break;
									}
									case 1:
									{
										Debug.Assert(region.X == 1);
										newPos = new(regionSize - 1, newPos.X + 2 * regionSize);
										newDirI = 2;
										break;
									}
									case 2:
									{
										Debug.Assert(region.X == 0);
										newPos = new(regionSize, 3 * regionSize - 1 - newPos.Y);
										newDirI = 0;
										break;
									}
									case 3:
									{
										Debug.Assert(region.X == 0);
										newPos = new(regionSize, newPos.X + regionSize);
										newDirI = 0;
										break;
									}
									default:
									{
										Debug.Fail("No such direction.");
										return null;
									}
								}
								break;
							}
							case 3:
							{
								Debug.Assert(region.X == 0);
								switch (dirI)
								{
									case 0:
									{
										newPos = new(newPos.Y - 2 * regionSize, 3 * regionSize - 1);
										newDirI = 3;
										break;
									}
									case 1:
									{
										newPos = new(newPos.X + 2 * regionSize, 0);
										newDirI = 1;
										break;
									}
									case 2:
									{
										newPos = new(newPos.Y - 2 * regionSize, 0);
										newDirI = 1;
										break;
									}
									case 3:
									{
										Debug.Fail("No edge in this direction.");
										return null;
									}
									default:
									{
										Debug.Fail("No such direction.");
										return null;
									}
								}
								break;
							}
							default:
							{
								Debug.Fail("Nothing here.");
								return null;
							}
						}
						square = grid[newPos.Y][newPos.X];
					}
					if (square == '#')
					{
						break;
					}
					else if (square == '.')
					{
						pos = newPos;
						dirI = newDirI;
						dir = directions[dirI];
						//charGrid[pos.Y][pos.X] = dirChars[dirI];
					}
					else throw new InvalidDataException($"Unknown character '{square}' in grid at line {newPos.Y} column {newPos.X}.");
				}

				/*
				if (pathI != 0) Console.Write(path[pathI - 1]);
				Console.Out.WriteLine(path.AsSpan(pathI, numberEndI - pathI));
				bool skip = false;
				foreach (char[] chars in charGrid)
				{
					if (skip) Console.WriteLine(chars);
					else
					{
						Console.Write(chars);
						if (Console.ReadLine()!.Length != 0) skip = true;
					}
				}
				//*/

				pathI = numberEndI;
			}
			else
			{
				switch (path[pathI])
				{
					case 'L':
					{
						dirI--;
						if (dirI == -1) dirI = directions.Length - 1;
						break;
					}
					case 'R':
					{
						dirI++;
						if (dirI == directions.Length) dirI = 0;
						break;
					}
					default:
					{
						throw new InvalidDataException($"Unknown character '{path[pathI]}' in path at position {pathI}.");
					}
				}
				pathI++;

				//charGrid[pos.Y][pos.X] = dirChars[dirI];
			}
		}

		Console.WriteLine($"Landed at {pos} facing {dirI}");
		int result = 4 * (pos.X + 1) + 1000 * (pos.Y + 1) + dirI;

		return result.ToString();
	}
}
