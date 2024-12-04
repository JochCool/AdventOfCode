using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode;

static class StringUtil
{
	public static string ToInvariantString<T>(this T value) where T : IFormattable
	{
		return value.ToString(null, CultureInfo.InvariantCulture);
	}

	public static T ParseInvariantAt<T>(string input, ref int index, char terminator) where T : ISpanParsable<T>
	{
		int endIndex = input.IndexOf(terminator, index);
		if (endIndex == -1) endIndex = input.Length;

		ReadOnlySpan<char> toParse = input.AsSpan(index, endIndex - index);

		index = endIndex;
		return T.Parse(toParse, CultureInfo.InvariantCulture);
	}

	public static IEnumerable<T> ParseAllInvariant<T>(this IEnumerable<string> inputs) where T : IParsable<T>
	{
		foreach (string input in inputs)
		{
			yield return T.Parse(input, CultureInfo.InvariantCulture);
		}
	}

	public static T[] ParseInvariantArray<T>(ReadOnlySpan<char> input, char separator) where T : ISpanParsable<T>
	{
		CultureInfo culture = CultureInfo.InvariantCulture;

		List<T> result = [];
		while (true)
		{
			int endI = input.IndexOf(separator);
			if (endI == -1)
			{
				result.Add(T.Parse(input, culture));
				return result.ToArray();
			}
			result.Add(T.Parse(input[..endI], culture));
			input = input[(endI + 1)..];
		}
	}

	public static T ParseHexadecimal<T>(ReadOnlySpan<char> digits) where T : IBinaryInteger<T>
	{
		T result = T.Zero;
		while (digits.Length != 0)
		{
			char c = digits[0];
			int digit = c - '0';
			if ((uint)digit >= 10)
			{
				digit = c - 'A';
				if ((uint)digit >= 6)
				{
					digit = c - 'a';
					if ((uint)digit >= 6) throw new FormatException($"'{c}' is not a hexadecimal digit.");
				}
				digit += 10;
			}
			result <<= 4;
			result |= T.CreateChecked(digit);

			digits = digits[1..];
		}
		return result;
	}

	public static int CountSubstring(string value, string substring)
	{
		int result = 0;
		int i = 0;
		while (true)
		{
			i = value.IndexOf(substring, i, StringComparison.Ordinal);
			if (i == -1) return result;
			result++;
			i += substring.Length;
		}
	}

	[return: NotNullIfNotNull(nameof(value))]
	public static string? Reverse(string? value)
	{
		if (value == null) return null;

		Span<char> chars = value.ToCharArray();
		chars.Reverse();
		return new string(chars);
	}
}
