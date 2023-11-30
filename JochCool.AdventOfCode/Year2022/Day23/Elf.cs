namespace JochCool.AdventOfCode.Year2022.Day23;

class Elf
{
	public Vector<int> Position { get; internal set; }

	public Vector<int> TargetPosition { get; internal set; }

	public Elf(Vector<int> position)
	{
		Position = position;
		TargetPosition = position;
	}
}
