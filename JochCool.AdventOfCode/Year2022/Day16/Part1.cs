using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2022.Day16;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		// In case this method is run a second time (like what the tests do).
		// TODO: even better, just don't make it static
		Array.Clear(BothParts.valves);

		foreach (string line in inputReader.ReadLines())
		{
			int i = "Valve ".Length;
			int name = BothParts.IdFromName(line.AsSpan(i, 2));

			i = "Valve AA has flow rate=".Length;
			int flowRate = StringUtil.ParseInvariantAt<int>(line, ref i, ';');

			i += "; tunnel lead to valve".Length;
			i = line.IndexOf(' ', i) + 1;

			int numPaths = (line.Length - i - 2) / 4 + 1; // should be always positive
			PathToValve[] paths = new PathToValve[numPaths];

			for (int pathI = 0; pathI < numPaths; pathI++)
			{
				paths[pathI] = new PathToValve(BothParts.IdFromName(line.AsSpan(i, 2)), 1);
				i += 4;
			}

			BothParts.valves[name] = new Valve(name, flowRate, paths);
		}

		List<Valve> remainingValves = [];

		Valve startingValve = BothParts.valves[0] ?? throw new Exception();
		startingValve.Index = 0;
		remainingValves.Add(startingValve);
		int index = 1;

		// Eliminate useless valve with flow rate 0
		for (int valveId = 1; valveId < BothParts.valves.Length; valveId++)
		{
			Valve? valve = BothParts.valves[valveId];
			if (valve is null) continue;
			if (valve.FlowRate != 0)
			{
				valve.Index = index++;
				remainingValves.Add(valve);
				continue;
			}

			PathToValve[] paths = valve.Edges;
			if (paths.Length != 2)
			{
				throw new NotImplementedException();
			}

			Valve? valve1 = BothParts.valves[paths[0].TargetName];
			Valve? valve2 = BothParts.valves[paths[1].TargetName];
			Debug.Assert(valve1 is not null && valve2 is not null);

			SimplifyPath(valve1, valve, valve2);
			SimplifyPath(valve2, valve, valve1);
			//valve.IsDeleted = true;
			BothParts.valves[valveId] = null;

			/*
			for (int valve1I = 0; valve1I < paths.Length; valve1I++)
			{
				Valve? valve1 = valves[paths[valve1I].TargetName];
				Debug.Assert(valve1 is not null);

				if (paths.Length != 2)
				{
					throw new NotImplementedException();
				}
				PathToValve[] valve1Paths = valve1.Paths;
				int replaceI;
				for (int i = 0; true; i++)
				{
					if (i == valve1Paths.Length) throw new InvalidOperationException();
					if (valve1Paths[i].TargetName == valve.Name)
					{
						replaceI = i;
						break;
					}
				}

				for (int valve2I = 0; valve2I < paths.Length; valve2I++)
				{
					if (valve1I == valve2I) continue;
					Valve? valve2 = valves[paths[valve2I].TargetName];
					Debug.Assert(valve2 is not null);
				}
			}
			*/
		}

		PrintNodes(remainingValves);
		Console.WriteLine();

		int[,] costMatrix = new int[index, index];

		startingValve.IsOpened = true;
		int result = Explore(startingValve, remainingValves, costMatrix, 0);

		// Btw, the solution opens these valves in this order:
		// DY HF RC XJ DP RU KG EF

		return result.ToInvariantString();
		/*
		Valve currentValve = valves[0] ?? throw new Exception();
		int totalGain = 0;
		int cost = 0;
		while (cost < maxCost)
		{
			// For every other valve, find shortest distance to currentValve using dijkstra.
			// Then calculate how much we'd gain from opening it ASAP.
			// Find the one that would gain the most.
			int maxGain = 0;
			Valve? bestCandidate = null;

			PriorityQueue<Valve, int> dijkstraQueue = new();
			Valve candidate = currentValve;
			int candidateCost = cost;
			candidate.PotentialCost = candidateCost;
			do
			{
				// Check if this is the best candidate so far
				if (!candidate.IsOpened)
				{
					int remainingTime = maxCost - candidateCost - 1; // - 1 because it takes 1min to open it
					if (remainingTime > 0)
					{
						int potentialGain = remainingTime * candidate.FlowRate;
						if (potentialGain > maxGain)
						{
							maxGain = potentialGain;
							bestCandidate = candidate;
						}
					}
				}

				foreach (PathToValve path in candidate.Paths)
				{
					int nextCandidateCost = candidateCost + path.Cost;
					if (nextCandidateCost >= maxCost) continue;
					Valve nextCandidate = valves[path.TargetName] ?? throw new Exception();
					if (nextCandidate.PotentialCost <= nextCandidateCost)
					{
						continue;
					}
					nextCandidate.PotentialCost = nextCandidateCost;
					dijkstraQueue.Enqueue(nextCandidate, nextCandidateCost);
				}
			}
			while (dijkstraQueue.TryDequeue(out candidate, out candidateCost));

			if (bestCandidate is null)
			{
				Console.WriteLine("We're done I guess?");
				break;
			}
			totalGain += maxGain;
			cost = bestCandidate.PotentialCost + 1; // + 1 because it takes 1min to open it
			currentValve = bestCandidate;
			currentValve.IsOpened = true;
			Console.WriteLine($"Go to valve {NameStringFromName(currentValve.Name)} (gains {maxGain}); now spent {cost}min and total gain is {totalGain}.");

			// Reset potential cost
			foreach (Valve valve in remainingValves) valve.PotentialCost = int.MaxValue;
		}
		return totalGain.ToString();
		*/
	}

	private static int Explore(Valve valve, List<Valve> otherValves, int[,] costMatrix, int startingCost)
	{
		const int maxCost = 30;

		int maxGain = 0;
		foreach (Valve otherValve in otherValves)
		{
			if (valve == otherValve || otherValve.IsOpened) continue;

			int nextCost = valve.FindShortestPathTo(otherValve, costMatrix) + startingCost + 1;
			if (nextCost >= maxCost) continue;

			otherValve.IsOpened = true;
			int gain = Explore(otherValve, otherValves, costMatrix, nextCost);
			otherValve.IsOpened = false;

			if (gain > maxGain) maxGain = gain;
		}
		return (maxCost - startingCost) * valve.FlowRate + maxGain;
	}

	private static void SimplifyPath(Valve valve1, Valve toRemove, Valve valve2)
	{
		int valve1PathI = valve1.GetPathIndex(toRemove);
		int toRemovePathI = toRemove.GetPathIndex(valve2);
		int newCost = valve1.Edges[valve1PathI].Cost + toRemove.Edges[toRemovePathI].Cost;
		valve1.Edges[valve1PathI] = new PathToValve(valve2.Id, newCost);
	}

	private static void PrintNodes(IEnumerable<Valve?> valves)
	{
		foreach (Valve? valve in valves)
		{
			if (valve is null) continue;
			/*
			if (valve.IsDeleted)
			{
				ConsoleColor oldColor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine(valve);
				Console.ForegroundColor = oldColor;
			}
			else
			{
				Console.WriteLine(valve);
			}
			*/
			Console.WriteLine(valve);
		}
	}
}

static class BothParts
{
	internal const int numLetters = 'Z' - 'A' + 1;

	internal readonly static Valve?[] valves = new Valve?[numLetters * numLetters];

	internal static int IdFromName(ReadOnlySpan<char> letters)
	{
		return (letters[0] - 'A') * numLetters + (letters[1] - 'A');
	}

	internal static string NameFromId(int id)
	{
		int letter1 = id / numLetters;
		int letter2 = id % numLetters;
		return $"{(char)('A' + letter1)}{(char)('A' + letter2)}";
	}
}

class Valve : DirectedGraphNode<Valve, PathToValve>
{
	public int Id { get; }

	public int FlowRate { get; }

	public override PathToValve[] Edges { get; }

	//public bool IsDeleted { get; internal set; }

	public bool IsOpened { get; internal set; }

	//internal int PotentialCost { get; set; } = int.MaxValue;

	internal int Index { get; set; }

	internal Valve(int id, int flowRate, PathToValve[] edges)
	{
		Id = id;
		FlowRate = flowRate;
		Edges = edges;
	}

	internal int GetPathIndex(Valve target)
	{
		PathToValve[] paths = Edges;
		for (int i = 0; true; i++)
		{
			if (i == paths.Length) throw new InvalidOperationException();
			if (paths[i].TargetName == target.Id)
			{
				return i;
			}
		}
	}

	public override string ToString()
	{
		return $"Valve {BothParts.NameFromId(Id)} has flow rate={FlowRate}; tunnels lead to valves {string.Join(", ", Edges)}";
	}

	public override int GetHashCode() => Index;
}

readonly record struct PathToValve(int TargetName, int Cost) : IEdge<Valve>
{
	public Valve To
	{
		get
		{
			return BothParts.valves[TargetName] ?? throw new InvalidOperationException();
		}
	}

	public override string ToString()
	{
		return $"{BothParts.NameFromId(TargetName)} ({Cost})";
	}
}
