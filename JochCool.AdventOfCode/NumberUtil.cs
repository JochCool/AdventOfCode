namespace JochCool.AdventOfCode;

static class NumberUtil
{
	internal static T ProperModulo<T>(T left, T right) where T : INumberBase<T>, IModulusOperators<T, T, T>
	{
		T result = left % right;
		if (T.IsNegative(result)) result += right;
		return result;
	}

	internal static T ParseAt<T>(string input, ref int index, char terminator, IFormatProvider? provider = null) where T : ISpanParsable<T>
	{
		int endIndex = input.IndexOf(terminator, index);
		if (endIndex == -1) endIndex = input.Length;
		ReadOnlySpan<char> toParse = input.AsSpan(index, endIndex - index);
		index = endIndex;
		return T.Parse(toParse, provider);
	}

	internal static T Sum<T>(ReadOnlySpan<T> values) where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
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
}
