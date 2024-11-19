namespace JochCool.AdventOfCode.Year2022.Day11;

class Monkey
{
	internal static Monkey[] monkeys =
	[
		/*
		new(23, Operation.Multiply, 19, 2, 3),
		new(19, Operation.Add, 6, 2, 0),
		new(13, Operation.Square, default, 1, 3),
		new(17, Operation.Add, 3, 0, 1)
		/*/
		new(2, Operation.Multiply, 11, 7, 4),
		new(13, Operation.Add, 1, 3, 6),
		new(3, Operation.Add, 6, 1, 6),
		new(17, Operation.Square, default, 7, 0),
		new(19, Operation.Multiply, 7, 5, 2),
		new(7, Operation.Add, 8, 2, 1),
		new(11, Operation.Add, 5, 3, 0),
		new(5, Operation.Add, 7, 4, 5)
		//*/
	];

	internal readonly List<IItem> items = [];

	public int Divider { get; }

	public Operation Operation { get; }

	public int Operand { get; }

	public int TrueMonkeyIndex { get; }

	public int FalseMonkeyIndex { get; }

	internal int totalActivity = 0;

	public Monkey(int divider, Operation operation, int operand, int trueMonkeyIndex, int falseMonkeyIndex)
	{
		Divider = divider;
		Operation = operation;
		Operand = operand;
		TrueMonkeyIndex = trueMonkeyIndex;
		FalseMonkeyIndex = falseMonkeyIndex;
	}

	public void AddItem(IItem item)
	{
		items.Add(item);
	}

	public void AddItems(params ReadOnlySpan<IItem> items)
	{
		this.items.AddRange(items);
	}
}
