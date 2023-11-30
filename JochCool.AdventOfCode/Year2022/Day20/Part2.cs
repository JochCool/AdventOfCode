namespace JochCool.AdventOfCode.Year2022.Day20;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		List<Node> nodes = [];
		Node? zero = null;

		const int decryptionKey = 811589153;

		foreach (string line in inputReader.ReadLines())
		{
			BigInteger value = BigInteger.Parse(line) * decryptionKey;
			Node node = new(value);
			if (value == 0) zero = node;
			nodes.Add(node);
		}
		if (zero is null) throw new InvalidDataException();

		// Convert to binary search tree
		Node root = Node.FromList(nodes, null, 0, nodes.Count);

		Console.WriteLine("Initial arrangement:");
		Console.WriteLine(string.Join(", ", root));
		Console.WriteLine();

		int mixingRounds = 10;
		while (mixingRounds --> 0)
		{
			foreach (Node node in nodes)
			{
				int oldI = node.GetIndex();
				int newI = (int)NumberUtil.ProperModulo(oldI + node.Value, nodes.Count - 1);
				//Console.WriteLine($"Moving number {node.Value} from {oldI} to {newI}.");

				root = node.Delete()!;
				root = root.Insert(node, newI);
				//Console.WriteLine(string.Join(", ", root));
				//Console.WriteLine();
			}

			/*
			Console.WriteLine("\nRound of mixing done:");
			Console.WriteLine(string.Join(", ", root));
			Console.WriteLine();
			*/
		}

		int zeroI = zero.GetIndex();
		BigInteger num1 = root.Find(NumberUtil.ProperModulo(zeroI + 1000, nodes.Count)).Value;
		BigInteger num2 = root.Find(NumberUtil.ProperModulo(zeroI + 2000, nodes.Count)).Value;
		BigInteger num3 = root.Find(NumberUtil.ProperModulo(zeroI + 3000, nodes.Count)).Value;
		BigInteger result = num1 + num2 + num3;
		Console.WriteLine($"{num1} + {num2} + {num3} = {result}");
		return result.ToString();
	}
}
