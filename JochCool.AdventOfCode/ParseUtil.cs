namespace JochCool.AdventOfCode;

static class ParseUtil
{
	public static T ParseAt<T>(string input, ref int index, char terminator, IFormatProvider? provider = null) where T : ISpanParsable<T>
	{
		int endIndex = input.IndexOf(terminator, index);
		if (endIndex == -1) endIndex = input.Length;

		ReadOnlySpan<char> toParse = input.AsSpan(index, endIndex - index);

		index = endIndex;
		return T.Parse(toParse, provider);
	}

	public static T[] ParseArray<T>(ReadOnlySpan<char> input, char separator, IFormatProvider? provider = null) where T : ISpanParsable<T>
	{
		List<T> result = [];
		while (true)
		{
			int endI = input.IndexOf(separator);
			if (endI == -1)
			{
				result.Add(T.Parse(input, provider));
				return result.ToArray();
			}
			result.Add(T.Parse(input[..endI], provider));
			input = input[(endI + 1)..];
		}
	}
}
