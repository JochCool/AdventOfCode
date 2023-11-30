namespace JochCool.AdventOfCode.Year2022.Day21;

class MonkeyOperation : IMonkeyJob
{
	public string Left { get; }

	public string Right { get; }

	public char Operator { get; }

	public MonkeyOperation(string left, string right, char @operator)
	{
		if (left is null) throw new ArgumentNullException(nameof(left));
		if (right is null) throw new ArgumentNullException(nameof(right));
		Left = left;
		Right = right;
		Operator = @operator;
	}

	public BigInteger GetValue(Dictionary<string, IMonkeyJob> dict)
	{
		BigInteger value1 = dict[Left].GetValue(dict);
		BigInteger value2 = dict[Right].GetValue(dict);
		return Operator switch
		{
			'+' => value1 + value2,
			'-' => value1 - value2,
			'/' => value1 / value2,
			'*' => value1 * value2,
			_ => throw new InvalidOperationException(),
		};
	}

	public LinearExpression GetExpression(Dictionary<string, IMonkeyJob> dict)
	{
		LinearExpression value1 = dict[Left].GetExpression(dict);
		LinearExpression value2 = dict[Right].GetExpression(dict);
		switch (Operator)
		{
			case '+':
			{
				value1.Add(value2);
				break;
			}
			case '-':
			{
				value1.Subtract(value2);
				break;
			}
			case '*':
			{
				value1.MultBy(value2);
				break;
			}
			case '/':
			{
				value1.DivBy(value2);
				break;
			}
			default:
			{
				throw new InvalidOperationException();
			}
		}
		return value1;
	}
}
