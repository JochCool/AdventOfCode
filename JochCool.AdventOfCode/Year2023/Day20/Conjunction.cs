using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2023.Day20;

class Conjunction : Module
{
	ulong inputsMask;
	ulong memory;

	public Conjunction(int id, string name, string[] destinationNames) : base(id, name, destinationNames) { }

	public override bool IsInInitialState => memory == 0uL;

	public bool RemembersPulseFrom(Module module)
	{
		return (memory & ~(1uL << module.Id)) != 0;
	}

	private protected override void RegisterSource(Module source)
	{
		int id = source.Id;
		if ((uint)id >= 64) throw new NotImplementedException("Only 64 modules are supported.");

		inputsMask |= 1uL << id;
	}

	internal override void ReceivePulse(Pulse pulse, Queue<Pulse> pulsesQueue)
	{
		Debug.Assert(pulse.Destination == this);

		ulong sourceMask = 1uL << pulse.Source.Id;
		if (pulse.IsHigh)
			memory |= sourceMask;
		else
			memory &= ~sourceMask;

		EnqueuePulses((memory & inputsMask) != inputsMask, pulsesQueue);
	}
}
