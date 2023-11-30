namespace JochCool.AdventOfCode;

public class NumberRange
{
	int minInclusive;
	int maxInclusive;

	public NumberRange(int minInclusive, int maxInclusive)
	{
		ThrowIfMinMaxAreWrong(minInclusive, maxInclusive);
		this.minInclusive = minInclusive;
		this.maxInclusive = maxInclusive;
	}

	public int MinInclusive
	{
		get => minInclusive;
		set
		{
			ThrowIfMinMaxAreWrong(value, maxInclusive);
			minInclusive = value;
		}
	}

	public int MaxInclusive
	{
		get => maxInclusive;
		set
		{
			ThrowIfMinMaxAreWrong(minInclusive, value);
			maxInclusive = value;
		}
	}

	public int MaxExclusive
	{
		get => MaxInclusive + 1;
		set => MaxInclusive = value - 1;
	}

	public int MinExclusive
	{
		get => MinInclusive - 1;
		set => MinInclusive = value + 1;
	}

	public int Size => maxInclusive - minInclusive + 1;

	internal static void ThrowIfMinMaxAreWrong(int minInclusive, int maxInclusive, string? paramName = null)
	{
		if (minInclusive > maxInclusive)
		{
			throw new ArgumentException("Maximum must be smaller than minimum.", paramName);
		}
	}
}
