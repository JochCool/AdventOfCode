namespace JochCool.AdventOfCode.Year2023.Day20;

readonly struct Pulse(Module source, Module? destination, bool isHigh)
{
	public Module Source => source;
	public Module? Destination => destination;
	public bool IsHigh => isHigh;

	public override string ToString()
	{
		return $"{source.Name} -{(isHigh ? "high" : "low")}-> {(destination is null ? "(unknown)" : destination.Name)}";
	}
}
