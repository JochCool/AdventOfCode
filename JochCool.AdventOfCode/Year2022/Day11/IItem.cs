namespace JochCool.AdventOfCode.Year2022.Day11;

interface IItem
{
	public bool PassesTestFor(int monkeyI);

	public void ApplyOperation(Operation operation, int operand);
}
