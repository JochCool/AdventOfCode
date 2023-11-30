#define LOGSTEPS
#define LOGALL

namespace JochCool.AdventOfCode.Year2022.Day19;

static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		/* // Example input blueprint 1
		ResourceType[] resourceTypes1 =
		[
			new("ore", 4, 0),
			new("clay", 2, 0),
			new("obsidian", 3, 14),
			new("geode", 2, 7)
		];
		/*/ // Example input blueprint 2
		ResourceType[] resourceTypes2 =
		[
			new("ore", 2, 0),
			new("clay", 3, 0),
			new("obsidian", 3, 8),
			new("geode", 3, 12)
		];
		//*/

		return FindMostGeodes(resourceTypes2).ToString();

		int result = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int i = "Blueprint ".Length;
			int blueprintNum = NumberUtil.ParseAt<int>(line, ref i, ':');

			ResourceType[] resourceTypes =
			[
				new("ore"),
				new("clay"),
				new("obsidian"),
				new("geode")
			];

			i += ": Each ore robot costs ".Length;
			resourceTypes[0].OreCost = NumberUtil.ParseAt<int>(line, ref i, ' ');

			i += " ore. Each clay robot costs ".Length;
			resourceTypes[1].OreCost = NumberUtil.ParseAt<int>(line, ref i, ' ');

			i += " ore. Each obsidian robot costs ".Length;
			resourceTypes[2].OreCost = NumberUtil.ParseAt<int>(line, ref i, ' ');
			i += " ore and ".Length;
			resourceTypes[2].SecondaryCost = NumberUtil.ParseAt<int>(line, ref i, ' ');

			i += " clay. Each geode robot costs ".Length;
			resourceTypes[3].OreCost = NumberUtil.ParseAt<int>(line, ref i, ' ');
			i += " ore and ".Length;
			resourceTypes[3].SecondaryCost = NumberUtil.ParseAt<int>(line, ref i, ' ');

			int bestGeodeCount = FindMostGeodes(resourceTypes);
			Console.WriteLine($"Blueprint {blueprintNum}: {bestGeodeCount} geodes");

			result += blueprintNum * bestGeodeCount;
		}

		return result.ToString();
	}

	public static int FindMostGeodes(ResourceType[] resourceTypes)
	{
		const int totalMinutes = 24;

		resourceTypes[0].NumActiveRobots = 1;

		int bestGeodeCount = 0;
		int bestOreRobotsTarget = -1;

		int oreCollectingRobotsTarget = 0;

		while (true)
		{
			int oreCollectingRobotsToMake = oreCollectingRobotsTarget;

			for (int minute = 0; minute < totalMinutes; minute++)
			{
#if LOGSTEPS
				Console.WriteLine($"== Minute {minute + 1} ==");
#endif

				// Create robots
				if (oreCollectingRobotsToMake > 0)
				{
					if (minute >= 18)
					{
						// By this time it is too late to be able to make any geode-collecting robots, so stop the search
						return bestGeodeCount;
					}
					if (resourceTypes[0].TryBuyWith(resourceTypes[0], resourceTypes[0]))
					{
						oreCollectingRobotsToMake--;
					}
				}
				else
				{
					int maxOreToSpend = int.MaxValue;
					for (int i = resourceTypes.Length - 1; i > 0; i--)
					{
						if (resourceTypes[i].OreCost > maxOreToSpend) continue;

						if (resourceTypes[i].TryBuyWith(resourceTypes[0], resourceTypes[i - 1])) break;

						if (resourceTypes[i - 1].NumActiveRobots == 0) continue;
						int minutesToProduction = (resourceTypes[i].SecondaryCost - resourceTypes[i - 1].Count) / resourceTypes[i - 1].NumActiveRobots;
						int oreCountByThen = resourceTypes[0].Count + minutesToProduction;
						maxOreToSpend = int.Min(maxOreToSpend, int.Max(oreCountByThen - resourceTypes[i].OreCost, 0));
					}
				}

				// Mine resources
				foreach (ResourceType resourceType in resourceTypes)
				{
					resourceType.Produce();
				}

				// Make previously created robots ready
				foreach (ResourceType resourceType in resourceTypes)
				{
					resourceType.MakeRobotsReady();
				}

#if LOGSTEPS
				Console.WriteLine();
#endif
			}

			if (resourceTypes[^1].Count > bestGeodeCount)
			{
				bestGeodeCount = resourceTypes[^1].Count;
				bestOreRobotsTarget = oreCollectingRobotsTarget;
			}

#if LOGALL
			Console.WriteLine($"Result: {resourceTypes[^1].Count} (with {oreCollectingRobotsTarget} ore-collecting robots); best so far: {bestGeodeCount} (with {bestOreRobotsTarget} ore-collecting robots).\n");
#endif

			foreach (ResourceType resourceType in resourceTypes)
			{
				resourceType.Reset();
			}
			resourceTypes[0].NumActiveRobots = 1;

			oreCollectingRobotsTarget++;
		}
	}
}

class ResourceType
{
	public string Name { get; }

	public int Count { get; internal set; }

	public int NumActiveRobots { get; internal set; }

	public int NumRobotsInProduction { get; internal set; }

	public int OreCost { get; internal set;  }

	public int SecondaryCost { get; internal set; }

	public ResourceType(string name)
	{
		Name = name;
	}

	public ResourceType(string name, int oreCost, int secondaryCost)
	{
		Name = name;
		OreCost = oreCost;
		SecondaryCost = secondaryCost;
	}

	public void Produce()
	{
		Count += NumActiveRobots;
#if LOGSTEPS
		Console.WriteLine($"You now have {Count} {Name} (from {NumActiveRobots} active robots)");
#endif
	}

	public bool TryBuyWith(ResourceType ore, ResourceType secondary)
	{
		if (ore.Count >= OreCost && secondary.Count >= SecondaryCost)
		{
			NumRobotsInProduction++;
			ore.Count -= OreCost;
			secondary.Count -= SecondaryCost;
#if LOGSTEPS
			Console.WriteLine($"Bought a {Name}-collecting robot.");
#endif
			return true;
		}
		return false;
	}

	public void MakeRobotsReady()
	{
		NumActiveRobots += NumRobotsInProduction;
		NumRobotsInProduction = 0;
	}

	public void Reset()
	{
		Count = 0;
		NumActiveRobots = 0;
	}
}

/*
static class Part1
{
	public static string? Main(string inputFilePath)
	{
		//* // Example input blueprint 1
		ResourceType[] resourceTypes1 =
		{
			new("ore", 4, 0),
			new("clay", 2, 0),
			new("obsidian", 3, 14),
			new("geode", 2, 7)
		};
		//* // Example input blueprint 2
		ResourceType[] resourceTypes2 =
		{
			new("ore", 2, 0),
			new("clay", 3, 0),
			new("obsidian", 3, 8),
			new("geode", 3, 12)
		};
		//* /

		int result = 0;

		foreach (string line in File.ReadLines(inputFilePath))
		{
			int i = "Blueprint ".Length;
			int blueprintNum = Util.ParseAt<int>(line, ref i, ':');

			ResourceType[] resourceTypes =
			{
				new("ore"),
				new("clay"),
				new("obsidian"),
				new("geode")
			};

			i += ": Each ore robot costs ".Length;
			resourceTypes[0].OreCost = Util.ParseAt<int>(line, ref i, ' ');

			i += " ore. Each clay robot costs ".Length;
			resourceTypes[1].OreCost = Util.ParseAt<int>(line, ref i, ' ');

			i += " ore. Each obsidian robot costs ".Length;
			resourceTypes[2].OreCost = Util.ParseAt<int>(line, ref i, ' ');
			i += " ore and ".Length;
			resourceTypes[2].SecondaryCost = Util.ParseAt<int>(line, ref i, ' ');

			i += " clay. Each geode robot costs ".Length;
			resourceTypes[3].OreCost = Util.ParseAt<int>(line, ref i, ' ');
			i += " ore and ".Length;
			resourceTypes[3].SecondaryCost = Util.ParseAt<int>(line, ref i, ' ');

			int bestGeodeCount = FindMostGeodes(resourceTypes);
			Console.WriteLine($"Blueprint {blueprintNum}: {bestGeodeCount} geodes");

			result += blueprintNum * bestGeodeCount;
		}

		return result.ToString();
	}

	public static int FindMostGeodes(ResourceType[] resourceTypes)
	{
		const int totalMinutes = 24;

		resourceTypes[0].NumActiveRobots = 1;

		int bestGeodeCount = 0;
		int bestOreRobotsTarget = -1;

		int oreCollectingRobotsTarget = 0;

		while (true)
		{
			int oreCollectingRobotsToMake = oreCollectingRobotsTarget;

			for (int minute = 0; minute < totalMinutes; minute++)
			{
#if LOGSTEPS
				Console.WriteLine($"== Minute {minute + 1} ==");
#endif

				// Create robots
				if (oreCollectingRobotsToMake > 0)
				{
					oreCollectingRobotsToMake -= resourceTypes[0].TryBuyWith(resourceTypes[0], resourceTypes[0], oreCollectingRobotsToMake);
				}
				if (oreCollectingRobotsToMake == 0)
				{
					int maxOreToSpend = int.MaxValue;
					for (int i = resourceTypes.Length - 1; i > 0; i--)
					{
						if (resourceTypes[i].OreCost > maxOreToSpend) continue;

						resourceTypes[i].TryBuyWith(resourceTypes[0], resourceTypes[i - 1]);

						if (resourceTypes[i - 1].NumActiveRobots == 0) continue;
						int minutesToProduction = (resourceTypes[i].SecondaryCost - resourceTypes[i - 1].Count) / resourceTypes[i - 1].NumActiveRobots;
						int oreCountByThen = resourceTypes[0].Count + minutesToProduction;
						maxOreToSpend = int.Min(maxOreToSpend, int.Max(oreCountByThen - resourceTypes[i].OreCost, 0));
					}
				}
				else if (minute >= 18)
				{
					// By this time it is too late to be able to make any geode-collecting robots, so stop the search
					return bestGeodeCount;
				}

				// Mine resources
				foreach (ResourceType resourceType in resourceTypes)
				{
					resourceType.Produce();
				}

				// Make previously created robots ready
				foreach (ResourceType resourceType in resourceTypes)
				{
					resourceType.MakeRobotsReady();
				}

#if LOGSTEPS
				Console.WriteLine();
#endif
			}

			if (resourceTypes[^1].Count > bestGeodeCount)
			{
				bestGeodeCount = resourceTypes[^1].Count;
				bestOreRobotsTarget = oreCollectingRobotsTarget;
			}

#if LOGALL
			Console.WriteLine($"Result: {resourceTypes[^1].Count} (with {oreCollectingRobotsTarget} ore-collecting robots); best so far: {bestGeodeCount} (with {bestOreRobotsTarget} ore-collecting robots).\n");
#endif

			foreach (ResourceType resourceType in resourceTypes)
			{
				resourceType.Reset();
			}
			resourceTypes[0].NumActiveRobots = 1;

			oreCollectingRobotsTarget++;
		}
	}
}

class ResourceType
{
	public string Name { get; }

	public int Count { get; internal set; }

	public int NumActiveRobots { get; internal set; }

	public int NumRobotsInProduction { get; internal set; }

	public int OreCost { get; internal set;  }

	public int SecondaryCost { get; internal set; }

	public ResourceType(string name)
	{
		Name = name;
	}

	public ResourceType(string name, int oreCost, int secondaryCost)
	{
		Name = name;
		OreCost = oreCost;
		SecondaryCost = secondaryCost;
	}

	public void Produce()
	{
		Count += NumActiveRobots;
#if LOGSTEPS
		Console.WriteLine($"You now have {Count} {Name} (from {NumActiveRobots} active robots)");
#endif
	}

	public int TryBuyWith(ResourceType ore, ResourceType secondary, int maxCount = int.MaxValue)
	{
		int numBought = 0;
		while (ore.Count >= OreCost && secondary.Count >= SecondaryCost && numBought < maxCount)
		{
			NumRobotsInProduction++;
			ore.Count -= OreCost;
			secondary.Count -= SecondaryCost;
			numBought++;
#if LOGSTEPS
			Console.WriteLine($"Bought a {Name}-collecting robot.");
#endif
		}
		return numBought;
	}

	public void MakeRobotsReady()
	{
		NumActiveRobots += NumRobotsInProduction;
		NumRobotsInProduction = 0;
	}

	public void Reset()
	{
		Count = 0;
		NumActiveRobots = 0;
	}
}
*/
