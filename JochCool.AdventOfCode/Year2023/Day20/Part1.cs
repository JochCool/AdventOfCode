namespace JochCool.AdventOfCode.Year2023.Day20;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		Dictionary<string, Module> dictionary = new();
		Broadcaster? broadcaster = null;

		foreach (string line in inputReader.ReadLines())
		{
			int nameEndI = line.IndexOf(' ', 1);
			if (nameEndI == -1) throw new FormatException();

			string[] destinationNames = line[(nameEndI + " -> ".Length)..].Split(", ");

			string name;
			Module module;
			if (line.AsSpan(0, nameEndI).Equals("broadcaster", StringComparison.OrdinalIgnoreCase))
			{
				name = "broadcaster";
				module = broadcaster = new Broadcaster(dictionary.Count, name, destinationNames);
			}
			else
			{
				name = line[1..nameEndI];
				module = line[0] switch
				{
					'%' => new FlipFlop(dictionary.Count, name, destinationNames),
					'&' => new Conjunction(dictionary.Count, name, destinationNames),
					_ => throw new FormatException("Unknown module type.")
				};
			}

			if (!dictionary.TryAdd(name, module))
			{
				throw new FormatException("Duplicate name.");
			}
		}
		if (broadcaster is null)
		{
			throw new FormatException("No broadcaster.");
		}

		foreach (Module module in dictionary.Values)
		{
			module.ResolveDestinationNames(dictionary);
		}

		Queue<Pulse> pulsesQueue = new();
		int highPulsesCount = 0;
		int lowPulsesCount = 0;

		int buttonPressesCount = 0;
		const int totalButtonPressesCount = 1000;
		while (buttonPressesCount < totalButtonPressesCount)
		{
			buttonPressesCount++;

			//Console.WriteLine($"- Press #{buttonPressesCount}");
			broadcaster.EnqueuePulses(false, pulsesQueue);
			lowPulsesCount++;

			while (pulsesQueue.TryDequeue(out Pulse pulse))
			{
				if (pulse.IsHigh)
					highPulsesCount++;
				else
					lowPulsesCount++;

				//Console.WriteLine(pulse.ToString());

				pulse.Destination?.ReceivePulse(pulse, pulsesQueue);
			}

			// Wrote this code, but turns out the input isn't gonna be cyclical for quite a while...
			/*
			bool allInInitialState = true;
			foreach (Module module in dictionary.Values)
			{
				if (!module.IsInInitialState)
					allInInitialState = false;
			}
			if (allInInitialState)
			{
				int totalCycles = totalButtonPressesCount / buttonPressesCount;
				Console.WriteLine($"-- Found a cycle of length {buttonPressesCount}; there's {totalCycles} of them.");
				buttonPressesCount *= totalCycles;
				highPulsesCount *= totalCycles;
				lowPulsesCount *= totalCycles;
			}
			//*/
		}

		long result = (long)lowPulsesCount * highPulsesCount;
		Console.WriteLine($"{lowPulsesCount} * {highPulsesCount} = {result}");
		return result.ToString();
	}
}
