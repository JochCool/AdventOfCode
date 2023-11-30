namespace JochCool.AdventOfCode;

/// <summary>
/// Implements the <see cref="IList{T}"/> interface and is mutable, except it has a fixed size. Adding elements shifts the entire array to the left, causing the first elements to be removed.
/// </summary>
/// <remarks>
/// <para>Do not create copies of this struct, as it contains both reference and value types.</para>
/// <para>The size of the array cannot be zero or negative.</para>
/// <para>Using the default value of this struct generally results in <see cref="NullReferenceException"/>s.</para>
/// </remarks>
/// <typeparam name="T">The type to contain in the array.</typeparam>
public struct ShiftingArray<T> : IList<T>, IReadOnlyList<T>
{
	internal readonly T[] Array { get; }

	internal int Position { readonly get; private set; }

	/// <summary>
	/// Constructs a <see cref="ShiftingArray{T}"/> of the specified size.
	/// </summary>
	/// <param name="size">The size of the array. Cannot be zero or negative.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is zero or negative.</exception>
	public ShiftingArray(int size)
	{
		if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be positive.");
		Array = new T[size];
		Position = 0;
	}

	internal ShiftingArray(T[] array)
	{
		Array = array;
		Position = 0;
	}

	/// <summary>
	/// Gets or sets the value at a specific index in the array.
	/// </summary>
	/// <param name="index">The index.</param>
	/// <value>The value at the specified index in the array.</value>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> index is not within the bounds of the array.</exception>
	public readonly T this[int index]
	{
		get
		{
			if ((uint)index >= (uint)Array.Length) throw new ArgumentOutOfRangeException(nameof(index));
			return Array[ToArrayIndex(index)];
		}
		set
		{
			if ((uint)index >= (uint)Array.Length) throw new ArgumentOutOfRangeException(nameof(index));
			Array[ToArrayIndex(index)] = value;
		}
	}

	/// <summary>
	/// Gets the size of the array.
	/// </summary>
	/// <value>The size of the array.</value>
	public readonly int Count => Array.Length;

	readonly bool ICollection<T>.IsReadOnly => false;

	private readonly int ToArrayIndex(int index)
	{
		index += Position;
		if (index >= Array.Length) index -= Array.Length;
		return index;
	}

	private readonly int ToPublicIndex(int index)
	{
		index -= Position;
		if (index < 0) index += Array.Length;
		return index;
	}

	public readonly int IndexOf(T item)
	{
		int i = Position;
		do
		{
			T element = Array[i];
			if (element is null ? item is null : element.Equals(item))
			{
				return ToPublicIndex(i);
			}
			i++;
			if (i == Array.Length) i = 0;
		} while (i != Position);
		return -1;
	}

	public readonly int LastIndexOf(T item)
	{
		int i = Position;
		do
		{
			i--;
			if (i == -1) i = Array.Length - 1;
			T element = Array[i];
			if (element is null ? item is null : element.Equals(item))
			{
				return ToPublicIndex(i);
			}
		} while (i != Position);
		return -1;
	}

	public void Insert(int index, T item)
	{
		throw new NotImplementedException();
	}

	public void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	public void Add(T item)
	{
		Array[Position] = item;
		Shift();
	}

	/// <summary>
	/// Shifts all elements one position to the left (new index = old index - 1); moving the first element (index 0) to the end.
	/// </summary>
	public void Shift()
	{
		Position++;
		if (Position == Array.Length) Position = 0;
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public readonly bool Contains(T item)
	{
		return ((ICollection<T>)Array).Contains(item);
	}

	public readonly void CopyTo(T[] array, int arrayIndex)
	{
		int count = Array.Length - Position;
		System.Array.Copy(Array, Position, array, 0, count);
		System.Array.Copy(Array, 0, array, count, Position);
	}

	public bool Remove(T item)
	{
		throw new NotImplementedException();
	}

	public readonly IEnumerator<T> GetEnumerator() => new Enumerator(this);

	readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public struct Enumerator : IEnumerator<T>
	{
		readonly ShiftingArray<T> array;
		int position;

		public Enumerator(ShiftingArray<T> array)
		{
			this.array = array;
			position = -1;
		}

		public readonly T Current => array[position];

		readonly object? IEnumerator.Current => Current;

		public bool MoveNext()
		{
			position++;
			return position < array.Count;
		}

		public void Reset()
		{
			position = -1;
		}

		public readonly void Dispose() { }
	}
}
