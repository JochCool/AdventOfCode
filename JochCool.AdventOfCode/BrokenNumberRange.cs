namespace JochCool.AdventOfCode;

public class BrokenNumberRange
{
	Node? firstNode;

	/// <summary>
	/// Constructs an empty number range.
	/// </summary>
	public BrokenNumberRange()
	{
	}

	public BrokenNumberRange(int minInclusive, int maxInclusive)
	{
		firstNode = new(minInclusive, maxInclusive);
	}

	public int Size
	{
		get
		{
			int result = 0;
			Node? node = firstNode;
			while (node is not null)
			{
				result += node.Size;
				node = node.next;
			}
			return result;
		}
	}

	public void Add(int minInclusive, int maxInclusive)
	{
		NumberRange.ThrowIfMinMaxAreWrong(minInclusive, maxInclusive);

		Node? prevNode = null;
		Node? node = firstNode;
		while (node is not null)
		{
			if (node.MaxInclusive >= minInclusive - 1)
			{
				if (node.MinInclusive > maxInclusive + 1)
				{
					goto NewNode;
				}
				while (node.MaxInclusive < maxInclusive)
				{
					Node? nextNode = node.next;
					if (nextNode is null || nextNode.MinInclusive > maxInclusive + 1)
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

	public void Remove(int minInclusive, int maxInclusive)
	{
		NumberRange.ThrowIfMinMaxAreWrong(minInclusive, maxInclusive);
		throw new NotImplementedException();
	}

	class Node : NumberRange
	{
		internal Node? next;

		public Node(int minInclusive, int maxExclusive) : base(minInclusive, maxExclusive)
		{
		}
	}
}
