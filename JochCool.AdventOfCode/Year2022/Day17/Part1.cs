namespace JochCool.AdventOfCode.Year2022.Day17;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
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
		bool[,] isOccupied = new bool[chamberWidth, 4096];

		string jetPattern = inputReader.ReadToEnd();

		const int rockStartX = 2;
		int rockStartY = 3;

		int rockY = rockStartY;
		int rockX = rockStartX;
		int jetPatternI = 0;
		int rockCount = 0;
		int rockTypeI = 0;
		bool[,] currentRockType = rockTypes[rockTypeI];
		int currentRockWidth = currentRockType.GetLength(0);
		int currentRockHeight = currentRockType.GetLength(1);
		while (true)
		{
			char jetPush = jetPattern[jetPatternI];
			jetPatternI++;
			if (jetPatternI >= jetPattern.Length) jetPatternI = 0;

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
			if (rockCount >= 2022)
			{
				return (rockStartY - 3).ToInvariantString();
			}

			rockTypeI++;
			if (rockTypeI >= rockTypes.Length) rockTypeI = 0;

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

		/*
		void PrintState()
		{
			StringBuilder sb = new();
			
			for  (int y = rockStartY + currentRockHeight - 1; y >= 0; y--)
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
				sb.Append("|\n");
			}
			sb.Append("+-------+");

			Console.WriteLine(sb.ToString());
			Console.ReadLine();
		}
		//*/
	}
}
