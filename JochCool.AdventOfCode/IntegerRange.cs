using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode;

/// <summary>
/// Represents a continuous range of integers, with a minimum and a maximum.
/// </summary>
/// <typeparam name="T">The type of integer.</typeparam>
public struct IntegerRange<T> : IReadOnlySet<T>, IEquatable<IntegerRange<T>>, IEqualityOperators<IntegerRange<T>, IntegerRange<T>, bool>
	where T : IBinaryInteger<T>
{
	private T minInclusive;
	private T maxInclusive;

	public IntegerRange(T minInclusive, T maxInclusive)
	{
		ThrowIfMinMaxAreWrong(minInclusive, maxInclusive);
		this.minInclusive = minInclusive;
		this.maxInclusive = maxInclusive;
	}

	public T MinInclusive
	{
		readonly get => minInclusive;
		set
		{
			ThrowIfMinMaxAreWrong(value, maxInclusive, nameof(value));
			minInclusive = value;
		}
	}

	public T MaxInclusive
	{
		readonly get => maxInclusive;
		set
		{
			ThrowIfMinMaxAreWrong(minInclusive, value, nameof(value));
			maxInclusive = value;
		}
	}

	public T MaxExclusive
	{
		readonly get => MaxInclusive + T.One;
		set => MaxInclusive = value - T.One;
	}

	public T MinExclusive
	{
		readonly get => MinInclusive - T.One;
		set => MinInclusive = value + T.One;
	}

	public readonly T Count => checked(maxInclusive - minInclusive + T.One);

	readonly int IReadOnlyCollection<T>.Count => int.CreateChecked(Count);

	public void Shift(T amount)
	{
		checked
		{
			minInclusive += amount;
			maxInclusive += amount;
		}
	}

	public readonly bool Contains(T item)
	{
		return item >= minInclusive && item <= maxInclusive;
	}

	public readonly bool Overlaps(IEnumerable<T> other)
	{
		foreach (T item in other)
		{
			if (Contains(item)) return true;
		}
		return false;
	}

	public readonly bool Overlaps(IntegerRange<T> other)
	{
		return minInclusive <= other.maxInclusive && maxInclusive >= other.minInclusive;
	}

	public readonly bool IsSupersetOf(IEnumerable<T> other)
	{
		foreach (T item in other)
		{
			if (!Contains(item)) return false;
		}
		return true;
	}

	public readonly bool IsSupersetOf(IntegerRange<T> other)
	{
		return minInclusive <= other.minInclusive && maxInclusive >= other.maxInclusive;
	}

	public readonly bool IsProperSupersetOf(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public readonly bool IsProperSupersetOf(IntegerRange<T> other)
	{
		return minInclusive <= other.minInclusive && maxInclusive >= other.maxInclusive && this != other;
	}

	public readonly bool IsSubsetOf(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public readonly bool IsSubsetOf(IntegerRange<T> other)
	{
		return minInclusive >= other.minInclusive && maxInclusive <= other.maxInclusive;
	}

	public readonly bool IsProperSubsetOf(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public readonly bool IsProperSubsetOf(IntegerRange<T> other)
	{
		return minInclusive >= other.minInclusive && maxInclusive <= other.maxInclusive && this != other;
	}

	public readonly bool SetEquals(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public readonly bool Equals(IntegerRange<T> other) => this == other;

	public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is IntegerRange<T> range && this == range;

	public static bool operator ==(IntegerRange<T> left, IntegerRange<T> right)
	{
		return left.minInclusive == right.minInclusive && left.maxInclusive == right.maxInclusive;
	}

	public static bool operator !=(IntegerRange<T> left, IntegerRange<T> right) => !(left == right);

	public override readonly int GetHashCode() => HashCode.Combine(minInclusive, maxInclusive);

	public override readonly string ToString()
	{
		return $"[{minInclusive},{maxInclusive}]";
	}

	public readonly Enumerator GetEnumerator() => new(this);

	readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

	readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public struct Enumerator : IEnumerator<T>
	{
		private T current;
		private readonly T maxInclusive;

		// Can't start at current = minInclusive - 1, because it might underflow.
		// So use a separate field to see if we're before the first number or not.
		private bool startedEnumerating;

		public Enumerator(IntegerRange<T> range)
		{
			current = range.minInclusive;
			maxInclusive = range.maxInclusive;
		}

		public readonly T Current => current;

		readonly object IEnumerator.Current => Current;

		public bool MoveNext()
		{
			if (!startedEnumerating)
			{
				startedEnumerating = true;
				return true;
			}

			if (current == maxInclusive)
			{
				return false;
			}

			current++;
			return true;
		}

		readonly void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		readonly void IDisposable.Dispose()
		{
		}
	}

	internal static void ThrowIfMinMaxAreWrong(T minInclusive, T maxInclusive, string? paramName = null)
	{
		if (minInclusive > maxInclusive)
		{
			throw new ArgumentException("Maximum must be smaller than minimum.", paramName);
		}
	}
}
