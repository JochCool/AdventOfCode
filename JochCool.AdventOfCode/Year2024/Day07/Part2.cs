namespace JochCool.AdventOfCode.Year2024.Day07;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		return BothParts.Solve(inputReader, IsPossible);
	}

	private static bool IsPossible(BigInteger target, BigInteger[] nums)
	{
		Span<Operator> operators = stackalloc Operator[nums.Length - 1];

		while (true)
		{
			if (ApplyOperations(nums, operators) == target)
			{
				return true;
			}

			if (!MoveNext(operators))
			{
				return false;
			}
		}
	}

	private static bool MoveNext(Span<Operator> operators)
	{
		int operatorI = 0;
		while (true)
		{
			if (operators[operatorI] == Operator.Concat)
			{
				operators[operatorI] = Operator.Add;
				if (++operatorI == operators.Length)
				{
					return false;
				}
			}
			else
			{
				operators[operatorI]++;
				return true;
			}
		}
	}

	private static BigInteger ApplyOperations(BigInteger[] nums, ReadOnlySpan<Operator> operators)
	{
		BigInteger result = nums[0];
		for (int i = 1; i < nums.Length; i++)
		{
			result = operators[i - 1] switch
			{
				Operator.Add => result + nums[i],
				Operator.Multiply => result * nums[i],
				Operator.Concat => Concat(result, nums[i]),
				_ => throw new ArgumentException($"Unknown operator {(int)operators[i-1]}.", nameof(operators))
			};
		}
		return result;
	}

	public static BigInteger Concat(BigInteger left, BigInteger right)
	{
		BigInteger multiplier = 10;
		while (right >= multiplier)
		{
			multiplier *= 10;
		}

		return left * multiplier + right;
	}
}

enum Operator
{
	Add,
	Multiply,
	Concat
}
