using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2023.Day20;

abstract class Module
{
	private readonly int id;
	private readonly string name;
	private readonly string[] destinationNames;
	private Module?[]? destinations;

	public Module(int id, string name, string[] destinationNames)
	{
		ArgumentNullException.ThrowIfNull(name);
		ArgumentNullException.ThrowIfNull(destinationNames);

		this.id = id;
		this.name = name;
		this.destinationNames = destinationNames;
	}

	public int Id => id;

	public string Name => name;

	public abstract bool IsInInitialState { get; }

	internal void ResolveDestinationNames(Dictionary<string, Module> dictionary)
	{
		string[] destinationNames = this.destinationNames;
		Module?[] destinations = new Module[destinationNames.Length];
		for (int i = 0; i < destinationNames.Length; i++)
		{
			if (dictionary.TryGetValue(destinationNames[i], out Module? destination))
			{
				destinations[i] = destination;
				destination.RegisterSource(this);
			}
		}
		this.destinations = destinations;
	}

	private protected virtual void RegisterSource(Module source) { }

	internal abstract void ReceivePulse(Pulse pulse, Queue<Pulse> pulsesQueue);

	internal void EnqueuePulses(bool isHigh, Queue<Pulse> pulsesQueue)
	{
		Debug.Assert(destinations is not null);

		foreach (Module? module in destinations)
		{
			pulsesQueue.Enqueue(new(this, module, isHigh));
		}
	}
}
