namespace JochCool.AdventOfCode;

static class NumberUtil
{
	internal static T ProperModulo<T>(T left, T right) where T : INumberBase<T>, IModulusOperators<T, T, T>
	{
		T result = left % right;
		if (T.IsNegative(result)) result += right;
		return result;
	}

	internal static T Sum<T>(params ReadOnlySpan<T> values) where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
	{
		T result = T.AdditiveIdentity;
		foreach (T value in values)
		{
			result += value;
		}
		return result;
	}

	internal static T Sum<T>(IList<T> values, int startI, int length) where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
	{
		T result = T.AdditiveIdentity;
		int endI = startI + length;
		for (int i = startI; i < endI; i++)
		{
			result += values[i];
		}
		return result;
	}

	public static BigInteger Sqrt(BigInteger value)
	{
		BigInteger estimate = value >> (int)BigInteger.Log2(value);
		while (true)
		{
			BigInteger product = estimate * estimate;
			BigInteger difference = value - product;
			if (difference == 0)
			{
				return estimate;
			}

			BigInteger estimateChange = (difference / estimate) >> 1;
			if (estimateChange == 0)
			{
				if (difference < 0) estimate--;
				return estimate;
			}

			estimate += estimateChange;
		}
	}

	// Calculates greates common divisor using the Euclidean algorithm. Inputs should be positive.
	public static T Gcd<T>(T a, T b) where T : INumber<T>
	{
		do
		{
			(a, b) = (b, a % b);
		}
		while (!T.IsZero(b));
		return a;
	}

	public static T Lcm<T>(T a, T b) where T : INumber<T>
	{
		return a / Gcd(a, b) * b;
	}

	public static T Lcm<T>(params ReadOnlySpan<T> values) where T : INumber<T>
	{
		if (values.Length == 0) return T.MultiplicativeIdentity;
		T result = values[0];
		for (int i = 1; i < values.Length; i++)
		{
			result = Lcm(result, values[i]);
		}
		return result;
	}
}
