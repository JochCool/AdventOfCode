using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2023.Day20;

class FlipFlop : Module
{
	bool isOn;

	public FlipFlop(int id, string name, string[] destinationNames) : base(id, name, destinationNames) { }

	public bool IsOn => isOn;

	public override bool IsInInitialState => !isOn;

	internal override void ReceivePulse(Pulse pulse, Queue<Pulse> pulsesQueue)
	{
		Debug.Assert(pulse.Destination == this);
		if (pulse.IsHigh) return;

		EnqueuePulses(isOn = !isOn, pulsesQueue);
	}
}
