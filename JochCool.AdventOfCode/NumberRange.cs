namespace JochCool.AdventOfCode;

public class NumberRange<T> where T : IBinaryInteger<T>
{
	T minInclusive;
	T maxInclusive;

	public NumberRange(T minInclusive, T maxInclusive)
	{
		ThrowIfMinMaxAreWrong(minInclusive, maxInclusive);
		this.minInclusive = minInclusive;
		this.maxInclusive = maxInclusive;
	}

	protected NumberRange(NumberRange<T> other)
	{
		minInclusive = other.minInclusive;
		maxInclusive = other.maxInclusive;
	}

	public T MinInclusive
	{
		get => minInclusive;
		set
		{
			ThrowIfMinMaxAreWrong(value, maxInclusive);
			minInclusive = value;
		}
	}

	public T MaxInclusive
	{
		get => maxInclusive;
		set
		{
			ThrowIfMinMaxAreWrong(minInclusive, value);
			maxInclusive = value;
		}
	}

	public T MaxExclusive
	{
		get => MaxInclusive + T.One;
		set => MaxInclusive = value - T.One;
	}

	public T MinExclusive
	{
		get => MinInclusive - T.One;
		set => MinInclusive = value + T.One;
	}

	public T Size => maxInclusive - minInclusive + T.One;

	public void Shift(T amount)
	{
		checked
		{
			minInclusive += amount;
			maxInclusive += amount;
		}
	}

	public virtual NumberRange<T> Clone() => new(this);

	public override string ToString()
	{
		return $"[{minInclusive},{maxInclusive}]";
	}

	internal static void ThrowIfMinMaxAreWrong(T minInclusive, T maxInclusive, string? paramName = null)
	{
		if (minInclusive > maxInclusive)
		{
			throw new ArgumentException("Maximum must be smaller than minimum.", paramName);
		}
	}
}
