namespace JochCool.AdventOfCode.Year2022.Day11;

public static class Part1
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

		int rounds = 20;
		while (rounds --> 0)
		{
			for (int monkeyI = 0; monkeyI < monkeys.Length; monkeyI++)
			{
				Monkey monkey = monkeys[monkeyI];
				List<IItem> items = monkey.items;
				foreach (Item item in items)
				{
					item.ApplyOperation(monkey.Operation, monkey.Operand);
					item.DivByThree();
					bool testResult = item.PassesTestFor(monkeyI);
					int receivingI = testResult ? monkey.TrueMonkeyIndex : monkey.FalseMonkeyIndex;
					monkeys[receivingI].AddItem(item);
				}
				monkey.totalActivity += items.Count;
				items.Clear();
			}

			for (int monkeyI = 0; monkeyI < monkeys.Length; monkeyI++)
			{
				Console.WriteLine($"Monkey {monkeyI}: {string.Join(", ", monkeys[monkeyI].items)}");
			}
			Console.WriteLine();
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

		int business = highest * highest2;
		Console.WriteLine($"Result: {highest} * {highest2} = {business}");
		return business.ToInvariantString();
	}

	class Item : IItem
	{
		int worryLevel;

		public Item(int worryLevel)
		{
			this.worryLevel = worryLevel;
		}

		public bool PassesTestFor(int monkeyI)
		{
			return worryLevel % Monkey.monkeys[monkeyI].Divider == 0;
		}

		public void ApplyOperation(Operation operation, int operand)
		{
			worryLevel = operation switch
			{
				Operation.Add => worryLevel + operand,
				Operation.Multiply => worryLevel * operand,
				Operation.Square => worryLevel * worryLevel,
				_ => throw new ArgumentException("Unknown operation", nameof(operation))
			};
		}

		public void DivByThree()
		{
			worryLevel /= 3;
		}

		public override string ToString() => ToString(null);

		public string ToString(IFormatProvider? formatProvider)
		{
			return worryLevel.ToString(formatProvider);
		}
	}
}
