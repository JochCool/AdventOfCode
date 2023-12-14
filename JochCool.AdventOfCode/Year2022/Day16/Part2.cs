using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2022.Day16;

public static class Part2
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
			int flowRate = ParseUtil.ParseAt<int>(line, ref i, ';');

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
		}

		PrintNodes(remainingValves);
		Console.WriteLine();

		int[,] costMatrix = new int[index, index];

		Stopwatch stopwatch = new();
		stopwatch.Start();
		int result = Explore(startingValve, remainingValves, costMatrix, 0);
		stopwatch.Stop();

		// When I tried it was 158593ms; just under two minutes.
		Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}ms");

		return result.ToString();
	}

	private static int Explore(Valve valve, List<Valve> otherValves, int[,] costMatrix, int startingCost, bool isElephant = false)
	{
		const int maxCost = 26;

		int maxGain = 0;
		foreach (Valve otherValve in otherValves)
		{
			if (valve == otherValve || otherValve.IsOpened) continue;

			int nextCost = valve.FindShortestPathTo(otherValve, costMatrix) + startingCost + 1;
			if (nextCost >= maxCost) continue;

			otherValve.IsOpened = true;
			int gain = Explore(otherValve, otherValves, costMatrix, nextCost, isElephant);
			otherValve.IsOpened = false;

			if (gain > maxGain) maxGain = gain;
		}

		if (maxGain == 0 && !isElephant)
		{
			// Let the elephant go
			maxGain = Explore(otherValves[0], otherValves, costMatrix, 0, true);
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
