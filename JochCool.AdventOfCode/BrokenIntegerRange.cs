using System.Diagnostics;

namespace JochCool.AdventOfCode;

public class BrokenIntegerRange<T> : ISet<T>, IEquatable<BrokenIntegerRange<T>> where T : IBinaryInteger<T>
{
	Node? firstNode;

	/// <summary>
	/// Constructs an empty number range.
	/// </summary>
	public BrokenIntegerRange()
	{
	}

	public BrokenIntegerRange(T minInclusive, T maxInclusive)
	{
		firstNode = new(minInclusive, maxInclusive);
	}

	protected BrokenIntegerRange(BrokenIntegerRange<T> other)
	{
		firstNode = other.firstNode?.Clone();
	}

	private BrokenIntegerRange(Node? firstNode)
	{
		this.firstNode = firstNode;
	}

	public T Count
	{
		get
		{
			T result = T.AdditiveIdentity;
			Node? node = firstNode;
			while (node is not null)
			{
				result += node.Count;
				node = node.next;
			}
			return result;
		}
	}

	int ICollection<T>.Count => int.CreateChecked(Count);
	bool ICollection<T>.IsReadOnly => false;

	public T Min => (firstNode ?? throw new InvalidOperationException()).MinInclusive;

	public bool Contains(T item)
	{
		Node? node = firstNode;
		while (node is not null)
		{
			if (node.MinInclusive >= item && node.MaxInclusive <= item)
				return true;

			node = node.next;
		}
		return false;
	}

	public void Shift(T amount)
	{
		Node? node = firstNode;
		while (node is not null)
		{
			node.Shift(amount);
			node = node.next;
		}
	}

	public bool Add(T item)
	{
		// TODO: this can definitely be optimized
		if (Contains(item)) return false;
		AddRange(item, item);
		return true;
	}

	void ICollection<T>.Add(T item) => AddRange(item, item);

	public void AddRange(T minInclusive, T maxInclusive)
	{
		IntegerRange<T>.ThrowIfMinMaxAreWrong(minInclusive, maxInclusive);

		Node? prevNode = null;
		Node? node = firstNode;
		while (node is not null)
		{
			if (node.MaxInclusive >= minInclusive - T.One)
			{
				if (node.MinInclusive > maxInclusive + T.One)
				{
					goto NewNode;
				}
				while (node.MaxInclusive < maxInclusive)
				{
					Node? nextNode = node.next;
					if (nextNode is null || nextNode.MinInclusive > maxInclusive + T.One)
					{
						node.MaxInclusive = maxInclusive;
						break;
					}
					node.MaxInclusive = nextNode.MaxInclusive;
					node.next = nextNode.next;
				}
				if (node.MinInclusive > minInclusive)
				{
					node.MinInclusive = minInclusive;
				}
				return;
			}

			prevNode = node;
			node = node.next;
		}

	NewNode:
		Node newNode = new(minInclusive, maxInclusive) { next = node };
		if (prevNode is null) firstNode = newNode;
		else prevNode.next = newNode;
	}

	public bool Remove(T item)
	{
		// TODO: this can definitely be optimized
		if (!Contains(item)) return false;
		RemoveRange(item, item);
		return true;
	}

	public void RemoveRange(T minInclusive, T maxInclusive)
	{
		IntegerRange<T>.ThrowIfMinMaxAreWrong(minInclusive, maxInclusive);

		Node? prevNode = null;
		Node? node = firstNode;
		while (true)
		{
			if (node is null) return; // specified range is greater than our nodes

			if (minInclusive <= node.MaxInclusive)
			{
				if (maxInclusive < node.MinInclusive) return; // specified range is in between or less than our nodes

				if (node.MinInclusive < minInclusive)
				{
					if (maxInclusive < node.MaxInclusive)
					{
						// specified range is within this node; split this node up
						node.next = new(maxInclusive, node.MaxInclusive) { next = node.next };
						return;
					}

					node.MaxExclusive = minInclusive;
					prevNode = node;
					node = node.next;
				}
				break;
			}

			prevNode = node;
			node = node.next;
		}

		while (node is not null)
		{
			if (maxInclusive < node.MaxInclusive)
			{
				if (node.MinInclusive <= maxInclusive)
				{
					node.MinExclusive = maxInclusive;
				}
				break;
			}

			node = node.next;

			// delete that node; it's entirely within the specified range
			if (prevNode is not null)
			{
				prevNode.next = node;
			}
		}

		if (prevNode is null)
		{
			firstNode = node;
		}
	}

	public void Clear()
	{
		firstNode = null;
	}

	public BrokenIntegerRange<T> CreateSubset(T minInclusive, T maxInclusive)
	{
		// Look for the node containing (or greater than) minInclusive
		Node resultFirstNode;
		Node? node = firstNode;
		while (true)
		{
			if (node is null) return new(); // specified range is greater than our nodes

			if (minInclusive <= node.MaxInclusive)
			{
				if (maxInclusive < node.MinInclusive) return new(); // specified range is in between or less than our nodes

				T newMin = T.Max(minInclusive, node.MinInclusive);
				T newMax = node.MaxInclusive;
				if (maxInclusive <= newMax)
				{
					// specified range is fully within this node
					return new(newMin, maxInclusive);
				}
				resultFirstNode = new Node(newMin, newMax);
				break;
			}

			node = node.next;
		}

		node = node.next;

		// Start creating nodes until we find the one containing (or greater than) maxInclusive
		Node lastCreatedNode = resultFirstNode;
		while (node is not null && node.MinInclusive <= maxInclusive)
		{
			if (maxInclusive <= node.MaxInclusive)
			{
				lastCreatedNode.next = new Node(node.MinInclusive, maxInclusive);
				break;
			}

			lastCreatedNode = lastCreatedNode.next = new Node(node.MinInclusive, node.MaxInclusive);

			node = node.next;
		}
		return new(resultFirstNode);
	}

	public void UnionWith(IEnumerable<T> other)
	{
		// TODO: this can definitely be optimized
		foreach (T value in other)
		{
			Add(value);
		}
	}

	public void UnionWith(BrokenIntegerRange<T> other)
	{
		Node? node = other.firstNode;
		while (node is not null)
		{
			AddRange(node.MinInclusive, node.MaxInclusive);
			node = node.next;
		}
	}

	public void ExceptWith(IEnumerable<T> other)
	{
		foreach (T value in other)
		{
			Remove(value);
		}
	}

	public void ExceptWith(BrokenIntegerRange<T> other)
	{
		// TODO: this can definitely be optimized
		Node? node = other.firstNode;
		while (node is not null)
		{
			RemoveRange(node.MinInclusive, node.MaxInclusive);
			node = node.next;
		}
	}

	public void SymmetricExceptWith(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public void IntersectWith(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool IsProperSubsetOf(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool IsProperSupersetOf(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool IsSubsetOf(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool IsSupersetOf(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool Overlaps(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool SetEquals(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool Equals(BrokenIntegerRange<T>? other)
	{
		throw new NotImplementedException();
	}

	public override bool Equals(object? obj) => Equals(obj as BrokenIntegerRange<T>);

	public override int GetHashCode()
	{
		throw new NotImplementedException();
	}

	public virtual BrokenIntegerRange<T> Clone() => new(this);

	public Enumerator GetEnumerator() => new(this);

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public void CopyTo(T[] array, int arrayIndex)
	{
		CopyTo(array.AsSpan(arrayIndex));
	}

	public void CopyTo(Span<T> destination)
	{
		throw new NotImplementedException();
	}

	public override string ToString()
	{
		StringBuilder builder = new("(");
		Node? node = firstNode;
		bool isFirst = true;
		while (node is not null)
		{
			if (!isFirst) builder.Append(", ");
			else isFirst = false;

			builder.Append(node);
			node = node.next;
		}
		builder.Append(')');
		return builder.ToString();
	}

	class Node
	{
		private IntegerRange<T> range;
		internal Node? next;

		public Node(T minInclusive, T maxInclusive) : this(new IntegerRange<T>(minInclusive, maxInclusive))
		{
		}

		public Node(IntegerRange<T> range)
		{
			this.range = range;
		}

		protected Node(Node other)
		{
			range = other.range;
			next = other.next?.Clone();
		}

		public T MinInclusive
		{
			get => range.MinInclusive;
			set => range.MinInclusive = value;
		}

		public T MaxInclusive
		{
			get => range.MaxInclusive;
			set => range.MaxInclusive = value;
		}

		public T MinExclusive
		{
			get => range.MinExclusive;
			set => range.MinExclusive = value;
		}

		public T MaxExclusive
		{
			get => range.MaxExclusive;
			set => range.MaxExclusive = value;
		}

		public T Count => range.Count;

		public void Shift(T amount)
		{
			range.Shift(amount);
		}

		public Node Clone() => new(this);

		public override string ToString() => range.ToString();
	}

	public struct Enumerator : IEnumerator<T>
	{
		Node? currentNode;
		T? current;

		internal Enumerator(BrokenIntegerRange<T> brokenNumberRange)
		{
			currentNode = brokenNumberRange.firstNode;
			if (currentNode is not null)
			{
				current = currentNode.MinInclusive;
				current--;
			}
		}

		// The nullable warning is suppressed, because getting this value while outside the collection is undefined behavior anyway.
		public readonly T Current => current!;

		readonly object IEnumerator.Current => Current;

		public bool MoveNext()
		{
			if (currentNode is null) return false;
			Debug.Assert(current is not null);

			current++;
			if (current > currentNode.MaxInclusive)
			{
				currentNode = currentNode.next;
				if (currentNode is null)
				{
					return false;
				}

				current = currentNode.MinInclusive;
				return true;
			}

			return true;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		readonly void IDisposable.Dispose()
		{
		}
	}
}
