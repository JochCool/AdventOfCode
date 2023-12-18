using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace JochCool.AdventOfCode;

public readonly struct BitGrid : ISet<Vector<int>>, IEquatable<BitGrid>, IEqualityOperators<BitGrid, BitGrid, bool>
{
	readonly uint[] bits;
	readonly Vector<int> size;

	public BitGrid(Vector<int> size)
	{
		if (size.X <= 0 || size.Y <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Both components of size must be positive.");
		int arraySize = checked(size.X * size.Y + 31) >> 5;
		bits = new uint[arraySize];
		this.size = size;
	}

	public int Count
	{
		get
		{
			int result = 0;
			foreach (uint segment in bits)
			{
				result += (int)uint.PopCount(segment);
			}
			return result;
		}
	}

	bool ICollection<Vector<int>>.IsReadOnly => false;

	public bool this[Vector<int> position]
	{
		get => GetAt(GetBitIndex(position));
		set
		{
			int bitIndex = GetBitIndex(position);
			if (value)
				SetAt(bitIndex);
			else
				UnsetAt(bitIndex);
		}
	}

	public void Toggle(Vector<int> position) => ToggleAt(GetBitIndex(position));

	private int GetBitIndex(Vector<int> position, [CallerArgumentExpression(nameof(position))] string? paramName = null)
	{
		if ((uint)position.X >= size.X || (uint)position.Y >= size.Y)
		{
			throw new ArgumentOutOfRangeException(paramName, $"Position {position} is not within the array.");
		}
		return position.X + size.X * position.Y;
	}

	private bool GetAt(int bitIndex)
	{
		return (bits[bitIndex >> 5] & (1u << bitIndex)) != 0;
	}

	private void SetAt(int bitIndex)
	{
		bits[bitIndex >> 5] |= 1u << bitIndex;
	}

	private void UnsetAt(int bitIndex)
	{
		bits[bitIndex >> 5] &= ~(1u << bitIndex);
	}

	private void ToggleAt(int bitIndex)
	{
		bits[bitIndex >> 5] ^= 1u << bitIndex;
	}

	public bool Contains(Vector<int> item) => this[item];

	public bool Add(Vector<int> item)
	{
		int bitIndex = GetBitIndex(item);
		bool existed = GetAt(bitIndex);
		if (!existed) SetAt(bitIndex);
		return !existed;
	}

	void ICollection<Vector<int>>.Add(Vector<int> item) => SetAt(GetBitIndex(item));

	public bool Remove(Vector<int> item)
	{
		int bitIndex = GetBitIndex(item);
		bool existed = GetAt(bitIndex);
		if (existed) UnsetAt(bitIndex);
		return existed;
	}

	public void Clear()
	{
		Array.Clear(bits);
	}

	public void CopyTo(Vector<int>[] array, int arrayIndex)
	{
		CopyTo(array.AsSpan(arrayIndex));
	}

	public void CopyTo(Span<Vector<int>> destination)
	{
		foreach (Vector<int> vector in this)
		{
			if (destination.Length == 0) throw new ArgumentException("The length of the destination span is less than the number of bits set in the grid.", nameof(destination));
			destination[0] = vector;
			destination = destination[1..];
		}
	}

	public int Floodfill(Vector<int> position)
	{
		Stack<Vector<int>> toVisit = new();
		toVisit.Push(position);
		int doneCount = 0;

		Vector<int>[] directions = Vector<int>.AxisUnitVectors;
		Vector<int> maxPos = size - new Vector<int>(1, 1);
		while (toVisit.TryPop(out Vector<int> currentPos))
		{
			if (!Add(currentPos))
				continue;
			this[currentPos] = true;
			doneCount++;

			foreach (Vector<int> direction in directions)
			{
				Vector<int> newPos = currentPos + direction;
				if (!newPos.IsInBox(Vector<int>.Origin, maxPos))
					continue;
				toVisit.Push(newPos);
			}
		}
		return doneCount;
	}

	public void UnionWith(BitGrid other)
	{
		if (size != other.size)
			throw new NotImplementedException();

		for (int i = 0; i < bits.Length; i++)
		{
			bits[i] |= other.bits[i];
		}
	}

	public void UnionWith(IEnumerable<Vector<int>> other)
	{
		foreach (Vector<int> vector in other)
		{
			this[vector] = true;
		}
	}

	public void IntersectWith(BitGrid other)
	{
		if (size != other.size)
			throw new NotImplementedException();

		for (int i = 0; i < bits.Length; i++)
		{
			bits[i] &= other.bits[i];
		}
	}

	public void IntersectWith(IEnumerable<Vector<int>> other)
	{
		throw new NotImplementedException();
	}

	public void ExceptWith(BitGrid other)
	{
		if (size != other.size)
			throw new NotImplementedException();

		for (int i = 0; i < bits.Length; i++)
		{
			bits[i] &= ~other.bits[i];
		}
	}

	public void ExceptWith(IEnumerable<Vector<int>> other)
	{
		foreach (Vector<int> vector in other)
		{
			this[vector] = false;
		}
	}

	public void SymmetricExceptWith(BitGrid other)
	{
		if (size != other.size)
			throw new NotImplementedException();

		for (int i = 0; i < bits.Length; i++)
		{
			bits[i] ^= other.bits[i];
		}
	}

	public void SymmetricExceptWith(IEnumerable<Vector<int>> other)
	{
		foreach (Vector<int> vector in other)
		{
			Toggle(vector);
		}
	}

	public bool IsSubsetOf(BitGrid other)
	{
		throw new NotImplementedException();
	}

	public bool IsSubsetOf(IEnumerable<Vector<int>> other)
	{
		throw new NotImplementedException();
	}

	public bool IsSupersetOf(BitGrid other)
	{
		throw new NotImplementedException();
	}

	public bool IsSupersetOf(IEnumerable<Vector<int>> other)
	{
		foreach (Vector<int> vector in other)
		{
			if (!this[vector])
				return false;
		}
		return true;
	}

	public bool IsProperSubsetOf(BitGrid other)
	{
		throw new NotImplementedException();
	}

	public bool IsProperSubsetOf(IEnumerable<Vector<int>> other)
	{
		throw new NotImplementedException();
	}

	public bool IsProperSupersetOf(BitGrid other)
	{
		throw new NotImplementedException();
	}

	public bool IsProperSupersetOf(IEnumerable<Vector<int>> other)
	{
		throw new NotImplementedException();
	}

	public bool Overlaps(BitGrid other)
	{
		throw new NotImplementedException();
	}

	public bool Overlaps(IEnumerable<Vector<int>> other)
	{
		throw new NotImplementedException();
	}

	public bool SetEquals(BitGrid other)
	{
		throw new NotImplementedException();
	}

	public bool SetEquals(IEnumerable<Vector<int>> other)
	{
		throw new NotImplementedException();
	}

	public bool Equals(BitGrid other) => this == other;

	public override bool Equals([NotNullWhen(true)] object? obj) => obj is BitGrid other && Equals(other);

	public override int GetHashCode()
	{
		HashCode hashCode = new();
		hashCode.Add(size);
		foreach (uint segment in bits)
		{
			hashCode.Add(segment);
		}
		return hashCode.ToHashCode();
	}

	public static bool operator ==(BitGrid left, BitGrid right)
	{
		return left.size == right.size
			&& MemoryExtensions.SequenceEqual<uint>(left.bits, right.bits);
	}

	public static bool operator !=(BitGrid left, BitGrid right) => !(left == right);

	public IEnumerator<Vector<int>> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public override string ToString() => ToString('#', '.');

	public string ToString(char trueChar, char falseChar)
	{
		StringBuilder builder = new((size.X + 1) * size.Y);
		int bitIndex = 0;
		for (int y = 0; y < size.Y; y++)
		{
			for (int x = 0; x < size.X; x++)
			{
				builder.Append(GetAt(bitIndex) ? trueChar : falseChar);
				bitIndex++;
			}
			builder.Append('\n');
		}
		return builder.ToString();
	}
}
