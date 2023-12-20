using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2023.Day20;

class Broadcaster : Module
{
	public Broadcaster(int id, string name, string[] destinationNames) : base(id, name, destinationNames) { }

	public override bool IsInInitialState => true;

	internal override void ReceivePulse(Pulse pulse, Queue<Pulse> pulsesQueue)
	{
		Debug.Assert(pulse.Destination == this);

		EnqueuePulses(pulse.IsHigh, pulsesQueue);
	}
}
