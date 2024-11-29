namespace JochCool.AdventOfCode.Year2022.Day17;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		//const long totalRocks = 2022;
		const long totalRocks = 1000000000000;

		// every 2D array is indexed by x, y
		// with x going right and y going up

		bool[,] minusRock =
		{
			{ true },
			{ true },
			{ true },
			{ true }
		};

		bool[,] plusRock =
		{
			{ false, true, false },
			{ true, true, true },
			{ false, true, false }
		};

		bool[,] reverseLRock =
		{
			{ true, false, false },
			{ true, false, false },
			{ true, true, true }
		};

		bool[,] iRock =
		{
			{ true, true, true, true }
		};

		bool[,] squareRock =
		{
			{ true, true },
			{ true, true }
		};

		bool[][,] rockTypes =
		[
			minusRock,
			plusRock,
			reverseLRock,
			iRock,
			squareRock
		];

		const int chamberWidth = 7;
		const int arrayHeight = 32768; // somewhat arbitrary
		bool[,] isOccupied = new bool[chamberWidth, arrayHeight];

		string jetPattern = inputReader.ReadToEnd();

		List<Report> reports = [new(0, 0)];

		const int rockStartX = 2;
		int rockStartY = 3;

		bool lookingForPattern = true;
		long skippedHeight = 0;

		int rockY = rockStartY;
		int rockX = rockStartX;
		int jetPatternI = 0;
		long rockCount = 0;
		int rockTypeI = 0;
		bool[,] currentRockType = rockTypes[rockTypeI];
		int currentRockWidth = currentRockType.GetLength(0);
		int currentRockHeight = currentRockType.GetLength(1);
		while (true)
		{
			char jetPush = jetPattern[jetPatternI];
			jetPatternI++;
			if (jetPatternI >= jetPattern.Length)
			{
				jetPatternI = 0;

				if (lookingForPattern)
				{
					PrintState();

					for (int size = 1; 2 * size < reports.Count; size++)
					{
						int currentTotalHeight = reports[^1].TotalHeight;
						int prevTotalHeight = reports[^(size + 1)].TotalHeight;
						int prev2TotalHeight = reports[^(2 * size + 1)].TotalHeight;

						// Check if height difference is equal
						int heightDifference = currentTotalHeight - prevTotalHeight;
						if (heightDifference != prevTotalHeight - prev2TotalHeight)
						{
							continue;
						}

						// Check if array portions are equal
						bool areEqual = true;
						for (int x = 0; x < chamberWidth; x++)
						{
							unsafe
							{
								fixed (bool* oldP = &isOccupied[x, prevTotalHeight], newP = &isOccupied[x, prev2TotalHeight])
								{
									ReadOnlySpan<bool> old = new(oldP, heightDifference);
									ReadOnlySpan<bool> @new = new(newP, heightDifference);
									if (!old.SequenceEqual(@new))
									{
										areEqual = false;
										break;
									}
								}
							}
						}
						if (areEqual)
						{
							long patternRockCount = reports[^1].RockCount - reports[^(size + 1)].RockCount;

							// If we got here, they are equal
							Console.WriteLine($"Found a pattern of {patternRockCount} rocks after a total of {rockCount} rocks; pattern is {heightDifference} tall.");
							long rocksLeft = totalRocks - rockCount;
							long rocksToSkip = rocksLeft - rocksLeft % patternRockCount;
							skippedHeight = heightDifference * rocksToSkip / patternRockCount;
							Console.WriteLine($"Skipping {rocksToSkip} rocks; that would have been {skippedHeight} units tall.");
							rockCount += rocksToSkip;
							lookingForPattern = false;
						}
					}

					reports.Add(new(rockCount, rockStartY - 3));
				}
			}

			switch (jetPush)
			{
				case '>':
				{
					if (rockX + currentRockWidth >= chamberWidth) break;

					// Check if we can move right
					// X and Y are relative to the bottom left corner of the rock
					for (int y = 0; y < currentRockHeight; y++)
					{
						int x = currentRockWidth - 1;
						while (!currentRockType[x, y])
						{
							x--;
							if (x < 0) throw new UnexpectedStateException();
						}
						if (isOccupied[rockX + x + 1, rockY + y])
						{
							goto FallDown;
						}
					}

					rockX++;
					break;
				}

				case '<':
				{
					if (rockX <= 0) break;

					// Check if we can move left
					// X and Y are relative to the bottom left corner of the rock
					for (int y = 0; y < currentRockHeight; y++)
					{
						int x = 0;
						while (!currentRockType[x, y])
						{
							x++;
							if (x >= currentRockWidth) throw new UnexpectedStateException();
						}
						if (isOccupied[rockX + x - 1, rockY + y])
						{
							goto FallDown;
						}
					}

					rockX--;
					break;
				}

				case '\n':
					continue;

				default:
				{
					Console.Error.WriteLine($"Unexpected jet push {jetPush}.");
					throw new UnexpectedStateException();
				}
			}

		FallDown:
			//PrintState();

			if (rockY <= 0) goto NextRock;

			// Check if we can move down
			// X and Y are relative to the bottom left corner of the rock
			for (int x = 0; x < currentRockWidth; x++)
			{
				int y = 0;
				while (!currentRockType[x, y])
				{
					y++;
					if (y >= currentRockHeight) throw new UnexpectedStateException();
				}
				if (isOccupied[rockX + x, rockY + y - 1])
				{
					goto NextRock;
				}
			}

			rockY--;
			//PrintState();
			continue;

		NextRock:
			rockStartY = int.Max(rockStartY, rockY + currentRockHeight + 3);

			rockCount++;
			if (rockCount >= totalRocks)
			{
				return (rockStartY - 3 + skippedHeight).ToInvariantString();
			}

			rockTypeI++;
			if (rockTypeI >= rockTypes.Length)
			{
				rockTypeI = 0;
			}

			for (int x = 0; x < currentRockWidth; x++)
			{
				for (int y = 0; y < currentRockHeight; y++)
				{
					if (currentRockType[x, y])
					{
						isOccupied[x + rockX, y + rockY] = true;
					}
				}
			}

			rockY = rockStartY;
			rockX = rockStartX;

			currentRockType = rockTypes[rockTypeI];
			currentRockWidth = currentRockType.GetLength(0);
			currentRockHeight = currentRockType.GetLength(1);
			//PrintState();
		}

		//*
		void PrintState()
		{
			StringBuilder sb = new();
			
			for (int y = rockStartY + currentRockHeight - 1; y >= 0; y--)
			{
				sb.Append('|');
				for (int x = 0; x < chamberWidth; x++)
				{
					if (isOccupied[x, y])
					{
						sb.Append('#');
						continue;
					}
					int rockTypeX = x - rockX;
					if (rockTypeX >= 0 && rockTypeX < currentRockWidth)
					{
						int rockTypeY = y - rockY;
						if (rockTypeY >= 0 && rockTypeY < currentRockHeight)
						{
							if (currentRockType[rockTypeX, rockTypeY])
							{
								sb.Append('@');
								continue;
							}
						}
					}
					sb.Append('.');
				}
				sb.Append('|');
				if (reports.Any(report => report.TotalHeight == y)) sb.Append('<');
				sb.Append('\n');
			}
			sb.Append("+-------+");

			Console.WriteLine(sb.ToString());
			//Console.ReadLine();
		}
		//*/
	}

	readonly record struct Report(long RockCount, int TotalHeight);
}

/*
static class Part2
{
	const int chamberWidth = 7;

	public static string? Main(string inputFilePath)
	{
		// every 2D array is indexed by x, y
		// with x going right and y going up

		Grid grid = new();

		string jetPattern = File.ReadAllText(inputFilePath);

		if (jetPattern[^1] == '\n')
		{
			jetPattern = jetPattern[..^1];
		}

		const int rockStartX = 2;
		int rockStartY = 3;

		int rockY = rockStartY;
		int rockX = rockStartX;
		int jetPatternI = 0;
		long rockCount = 0;
		int rockTypeI = 0;
		int currentRockWidth = 4;
		int currentRockHeight = 1;
		while (true)
		{
			char jetPush = jetPattern[jetPatternI];
			jetPatternI++;
			if (jetPatternI >= jetPattern.Length) jetPatternI = 0;

			//Console.WriteLine(jetPush);

			switch (jetPush)
			{
				case '>':
				{
					if (rockX + currentRockWidth >= chamberWidth) break;

					// Check if we can move right
					// X and Y are relative to the bottom left corner of the rock
					for (int y = 0; y < currentRockHeight; y++)
					{
						int x;
						if (rockTypeI == 1 && y != 1) x = currentRockWidth - 1;
						else x = currentRockWidth;

						if (grid[rockX + x, rockY + y])
						{
							goto FallDown;
						}
					}

					rockX++;
					break;
				}

				case '<':
				{
					if (rockX <= 0) break;

					// Check if we can move left
					// X and Y are relative to the bottom left corner of the rock
					for (int y = 0; y < currentRockHeight; y++)
					{
						int x;
						if (rockTypeI == 1 && y != 1) x = 0;
						else if (rockTypeI == 2 && y != 0) x = 1;
						else x = -1;

						if (grid[rockX + x, rockY + y])
						{
							goto FallDown;
						}
					}

					rockX--;
					break;
				}
			}

		FallDown:
			//PrintState();

			if (rockY <= 0) goto NextRock;

			// Check if we can move down
			// X and Y are relative to the bottom left corner of the rock
			for (int x = 0; x < currentRockWidth; x++)
			{
				int y;
				if (rockTypeI == 1 && x != 1) y = 0;
				else y = -1;

				if (grid[rockX + x, rockY + y])
				{
					goto NextRock;
				}
			}

			rockY--;
			//PrintState();
			continue;

		NextRock:
			rockStartY = int.Max(rockStartY, rockY + currentRockHeight + 3);

			rockCount++;
			if ((rockCount & int.MaxValue) == 0)
			{
				Console.WriteLine(rockCount);
			}
			if (rockCount >= 1000000000000)
			{
				return (rockStartY - 3).ToString();
			}

			switch (rockTypeI)
			{
				case 0:
				{
					grid[rockX, rockY] = true;
					grid[rockX + 1, rockY] = true;
					grid[rockX + 2, rockY] = true;
					grid[rockX + 3, rockY] = true;
					rockTypeI = 1;
					currentRockHeight = currentRockWidth = 3;
					break;
				}

				case 1:
				{
					grid[rockX + 1, rockY] = true;
					grid[rockX, rockY + 1] = true;
					grid[rockX + 1, rockY + 1] = true;
					grid[rockX + 2, rockY + 1] = true;
					grid[rockX + 1, rockY + 2] = true;
					rockTypeI = 2;
					currentRockHeight = currentRockWidth = 3;
					break;
				}

				case 2:
				{
					grid[rockX, rockY] = true;
					grid[rockX + 1, rockY] = true;
					grid[rockX + 2, rockY] = true;
					grid[rockX + 2, rockY + 1] = true;
					grid[rockX + 2, rockY + 2] = true;
					rockTypeI = 3;
					currentRockHeight = 4;
					currentRockWidth = 1;
					break;
				}

				case 3:
				{
					grid[rockX, rockY] = true;
					grid[rockX, rockY + 1] = true;
					grid[rockX, rockY + 2] = true;
					grid[rockX, rockY + 3] = true;
					rockTypeI = 4;
					currentRockHeight = currentRockWidth = 2;
					break;
				}

				case 4:
				{
					grid[rockX, rockY] = true;
					grid[rockX + 1, rockY] = true;
					grid[rockX, rockY + 1] = true;
					grid[rockX + 1, rockY + 1] = true;
					rockTypeI = 0;
					currentRockHeight = 1;
					currentRockWidth = 4;
					break;
				}
			}

			rockY = rockStartY;
			rockX = rockStartX;

			if (rockStartY + currentRockHeight > Grid.TotalHeight)
			{
				grid.ShiftUp();
				rockStartY -= Grid.ChunkHeight;
				rockY -= Grid.ChunkHeight;
			}
			//PrintState();
		}

		//*
		void PrintState()
		{
			StringBuilder sb = new();

			for (int y = rockStartY + currentRockHeight - 1; y >= 0; y--)
			{
				sb.Append('|');
				for (int x = 0; x < chamberWidth; x++)
				{
					if (grid[x, y])
					{
						sb.Append('#');
					}
					else if (x == rockX && y == rockY)
					{
						sb.Append(rockTypeI);
					}
					else
					{
						sb.Append('.');
					}
				}
				sb.Append("|\n");
			}
			sb.Append("+-------+");

			Console.WriteLine(sb.ToString());
			Console.ReadLine();
		}
		//* /
	}

	class Grid
	{
		public const int ChunkHeight = 1 << 28; // rather arbitrary
		public const int TotalHeight = ChunkHeight * 2;

		//Task<bool[,]>? clearingTask;

		// These arrays will be on the large object heap
		bool[,] lowerChunk = new bool[chamberWidth, ChunkHeight];
		bool[,] upperChunk = new bool[chamberWidth, ChunkHeight];

		public bool this[int x, int y]
		{
			get
			{
				if (y < ChunkHeight)
				{
					return lowerChunk[x, y];
				}
				return upperChunk[x, y - ChunkHeight];
			}

			set
			{
				if (y < ChunkHeight)
				{
					lowerChunk[x, y] = value;
					return;
				}
				upperChunk[x, y - ChunkHeight] = value;
			}
		}

		public void ShiftUp()
		{
			/*
			Console.WriteLine("Shifting up.");
			bool[,] newArray;
			if (clearingTask is null)
			{
				Console.WriteLine("This should only happen once.");
				newArray = new bool[chamberWidth, ChunkHeight];
			}
			else
			{
				newArray = clearingTask.GetAwaiter().GetResult();
			}
			//* /

			bool[,] oldArray = lowerChunk;
			/*
			clearingTask = Task.Run(() =>
			{
				Array.Clear(oldArray);
				Console.WriteLine("Done clearing the array.");
				return oldArray;
			});
			* /
			Array.Clear(oldArray);
			lowerChunk = upperChunk;
			upperChunk = oldArray;
		}
	}
}
*/
