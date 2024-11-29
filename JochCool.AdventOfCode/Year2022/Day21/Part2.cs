namespace JochCool.AdventOfCode.Year2022.Day21;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		Dictionary<string, IMonkeyJob> dict = [];

		foreach (string line in inputReader.ReadLines())
		{
			string name = line[..4];
			if (line[6] is >= '0' and <= '9')
			{
				dict.Add(name, new MonkeyValue(BigInteger.Parse(line.AsSpan(6))));
			}
			else
			{
				dict.Add(name, new MonkeyOperation(line[6..10], line[13..17], line[11]));
			}
		}

		dict["humn"] = new UnknownValueMonkey();

		if (dict["root"] is not MonkeyOperation root) throw new UnexpectedStateException();

		LinearExpression left = dict[root.Left].GetExpression(dict);
		LinearExpression right = dict[root.Right].GetExpression(dict);
		Console.WriteLine($"{left} = {right}");

		if (right.HasCoefficient)
		{
			if (left.HasCoefficient) throw new NotImplementedException();
			(left, right) = (right, left);
		}
		else if (!left.HasCoefficient) throw new NotImplementedException();

		right.Subtract(left.Constant);
		left.Constant = Fraction.Zero;
		Console.WriteLine($"{left} = {right}");

		right.DivBy(left.Coefficient);
		left.Coefficient = Fraction.One;
		Console.WriteLine($"{left} = {right}");

		return right.ToString(CultureInfo.InvariantCulture);
	}
}
