namespace JochCool.AdventOfCode.Year2022.Day11;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		// TODO: actually parse the input lol

		Monkey[] monkeys = Monkey.monkeys;
		/*
		monkeys[0].AddItems(new Item(79), new Item(98));
		monkeys[1].AddItems(new Item(54), new Item(65), new Item(75), new Item(74));
		monkeys[2].AddItems(new Item(79), new Item(60), new Item(97));
		monkeys[3].AddItem(new Item(74));
		/*/
		monkeys[0].AddItems(new Item(89), new Item(95), new Item(92), new Item(64), new Item(87), new Item(68));
		monkeys[1].AddItems(new Item(87), new Item(67));
		monkeys[2].AddItems(new Item(95), new Item(79), new Item(92), new Item(82), new Item(60));
		monkeys[3].AddItems(new Item(67), new Item(97), new Item(56));
		monkeys[4].AddItems(new Item(80), new Item(68), new Item(87), new Item(94), new Item(61), new Item(59), new Item(50), new Item(68));
		monkeys[5].AddItems(new Item(73), new Item(51), new Item(76), new Item(59));
		monkeys[6].AddItem(new Item(92));
		monkeys[7].AddItems(new Item(99), new Item(76), new Item(78), new Item(76), new Item(79), new Item(90), new Item(89));
		//*/

		int rounds = 10_000;
		while (rounds --> 0)
		{
			for (int monkeyI = 0; monkeyI < monkeys.Length; monkeyI++)
			{
				Monkey monkey = monkeys[monkeyI];
				List<IItem> items = monkey.items;
				foreach (Item item in items)
				{
					item.ApplyOperation(monkey.Operation, monkey.Operand);
					bool testResult = item.PassesTestFor(monkeyI);
					int receivingI = testResult ? monkey.TrueMonkeyIndex : monkey.FalseMonkeyIndex;
					monkeys[receivingI].AddItem(item);
				}
				monkey.totalActivity += items.Count;
				items.Clear();
			}

			/*
			for (int monkeyI = 0; monkeyI < monkeys.Length; monkeyI++)
			{
				Console.WriteLine($"Monkey {monkeyI}: {monkeys[monkeyI].totalActivity} times, {string.Join(", ", monkeys[monkeyI].items)}");
			}
			Console.WriteLine();
			*/
		}

		int highest = int.MinValue;
		int highest2 = int.MinValue;
		for (int monkeyI = 0; monkeyI < monkeys.Length; monkeyI++)
		{
			int totalActivity = monkeys[monkeyI].totalActivity;
			Console.WriteLine($"Monkey {monkeyI} inspected items {totalActivity} times.");
			if (totalActivity > highest)
			{
				highest2 = highest;
				highest = totalActivity;
			}
			else if (totalActivity > highest2)
			{
				highest2 = totalActivity;
			}
		}

		long business = Math.BigMul(highest, highest2);
		Console.WriteLine($"Result: {highest} * {highest2} = {business}");
		return business.ToString();
	}

	class Item : IItem
	{
		readonly int[] numbers;

		public Item(int worryLevel)
		{
			Monkey[] monkeys = Monkey.monkeys;
			numbers = new int[monkeys.Length];
			for (int i = 0; i < monkeys.Length; i++)
			{
				numbers[i] = worryLevel % monkeys[i].Divider;
			}
		}

		public bool PassesTestFor(int monkeyI)
		{
			return numbers[monkeyI] == 0;
		}

		public void ApplyOperation(Operation operation, int operand)
		{
			Monkey[] monkeys = Monkey.monkeys;
			for (int i = 0; i < numbers.Length; i++)
			{
				int num = numbers[i];
				int result = operation switch
				{
					Operation.Add => num + operand,
					Operation.Multiply => num * operand,
					Operation.Square => num * num,
					_ => throw new ArgumentException("Unknown operation", nameof(operation))
				};
				numbers[i] = result % monkeys[i].Divider;
			}
		}

		/*
		public void Add(int num)
		{
			Monkey[] monkeys = Monkey.monkeys;
			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = (numbers[i] + num) % monkeys[i].Divider;
			}
		}

		public void Multiply(int num)
		{
			Monkey[] monkeys = Monkey.monkeys;
			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = (numbers[i] * num) % monkeys[i].Divider;
			}
		}

		public void Square()
		{
			Monkey[] monkeys = Monkey.monkeys;
			for (int i = 0; i < numbers.Length; i++)
			{
				int num = numbers[i];
				numbers[i] = (num * num) % monkeys[i].Divider;
			}
		}
		*/

		public override string ToString()
		{
			return string.Join('/', numbers);
		}
	}
}
