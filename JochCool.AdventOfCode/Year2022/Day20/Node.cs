using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2022.Day20;

[DebuggerDisplay("Value = {Value}")]
class Node : BinaryTreeNode<Node>
{
	public BigInteger Value { get; }

	public Node(BigInteger value)
	{
		Value = value;
	}

	public int BalanceFactor
	{
		get
		{
			int heightRight = Right is null ? 0 : Right.Height;
			int heightLeft = Left is null ? 0 : Left.Height;
			return heightRight - heightLeft;
		}
	}

	public int GetIndex()
	{
		int index = NumChildrenLeft;
		Node currentNode = this;
		while (currentNode.Parent is not null)
		{
			if (currentNode.Parent.Right == currentNode) index += currentNode.Parent.NumChildrenLeft + 1;
			currentNode = currentNode.Parent;
		}
		return index;
	}

	public Node Find(int index)
	{
		if (index == NumChildrenLeft) return this;
		if (index < 0 || index >= TotalSize) throw new ArgumentOutOfRangeException(nameof(index));
		if (index < NumChildrenLeft) return Left!.Find(index);
		return Right!.Find(index - NumChildrenLeft - 1);
	}

	// returns the new root of the entire tree
	public Node Insert(Node newNode, int index)
	{
		if (newNode is null) throw new ArgumentNullException(nameof(newNode));
		if (Parent is not null) throw new InvalidOperationException("This method can only be called on the root of the tree.");
		if (newNode == this) throw new ArgumentException("Cannot insert a node into itself.", nameof(newNode));
		if (!newNode.IsAlone) throw new ArgumentException("Only single nodes can be inserted.", nameof(newNode));

		// Step 1: search
		Node currentNode = this;
		while (true)
		{
			if (index <= currentNode.NumChildrenLeft)
			{
				if (currentNode.Left is null)
				{
					currentNode.SetLeft(newNode);
					break;
				}
				currentNode = currentNode.Left;
			}
			else
			{
				if (currentNode.Right is null)
				{
					currentNode.SetRight(newNode);
					break;
				}
				index -= currentNode.NumChildrenLeft + 1;
				currentNode = currentNode.Right;
			}
		}

		// Step 2: rebalance
		return currentNode.Rebalance();
	}
	
	// returns the new root of the entire tree
	public new Node? Delete()
	{
		return base.Delete()?.Rebalance();
	}

	private Node Rebalance()
	{
		Node? currentNode = this;
		while (true)
		{
			Node newNode;
			int balanceFactor = currentNode.BalanceFactor;
			Debug.Assert(balanceFactor is >= -2 and <= 2);
			switch (balanceFactor)
			{
				case -2:
				{
					Debug.Assert(currentNode.Left is not null);

					// Check if double rotation is needed
					Node leftChild = currentNode.Left;
					Debug.Assert(leftChild.BalanceFactor is >= -1 and <= 1);
					if (leftChild.BalanceFactor == 1)
					{
						Debug.Assert(leftChild.Right is not null);

						newNode = leftChild.Right;

						leftChild.SetRight(newNode.Left);
						newNode.SetLeft(leftChild);
					}
					else
					{
						newNode = leftChild;
					}

					currentNode.SetLeft(newNode.Right);
					currentNode.Parent?.ReplaceDirectChild(currentNode, newNode);
					newNode.SetRight(currentNode);
					break;
				}

				case 2:
				{
					Debug.Assert(currentNode.Right is not null);

					// Check if double rotation is needed
					Node rightChild = currentNode.Right;
					Debug.Assert(rightChild.BalanceFactor is >= -1 and <= 1);
					if (rightChild.BalanceFactor == -1)
					{
						Debug.Assert(rightChild.Left is not null);

						newNode = rightChild.Left;

						rightChild.SetLeft(newNode.Right);
						newNode.SetRight(rightChild);
					}
					else
					{
						newNode = rightChild;
					}

					currentNode.SetRight(newNode.Left);
					currentNode.Parent?.ReplaceDirectChild(currentNode, newNode);
					newNode.SetLeft(currentNode);
					break;
				}

				default:
				{
					// No rotations needed
					newNode = currentNode;
					break;
				}
			}

			currentNode = newNode.Parent;
			if (currentNode is null) return newNode;
		}
	}

	public override string ToString() => Value.ToString();
}

/*
[DebuggerDisplay("Value = {Value}")]
class Node : IEnumerable<Node>
{
	public int Value { get; }

	public Node? Parent { get; private set; }

	public Node? Left { get; private set; }

	public Node? Right { get; private set; }

	public int NumChildrenLeft { get; private set; }

	public int NumChildrenRight { get; private set; }

	public int Height { get; private set; }

	public Node(int value)
	{
		Value = value;
	}

	public int TotalSize => NumChildrenLeft + NumChildrenRight + 1;

	public int BalanceFactor
	{
		get
		{
			int heightRight = Right is null ? 0 : Right.Height;
			int heightLeft = Left is null ? 0 : Left.Height;
			return heightRight - heightLeft;
		}
	}

	private bool IsAlone => Parent is null && Left is null && Right is null;

	internal static Node FromList(List<Node> nodes, Node? parent, int lowInclusive, int highExclusive)
	{
		int middle = (lowInclusive + highExclusive) / 2;
		Node middleNode = nodes[middle];
		middleNode.Parent = parent;

		int childrenHeight = 0;
		if (middle > lowInclusive)
		{
			Node leftNode = FromList(nodes, middleNode, lowInclusive, middle);
			middleNode.Left = leftNode;
			middleNode.NumChildrenLeft = leftNode.TotalSize;
			childrenHeight = leftNode.Height;
		}
		if (middle + 1 < highExclusive)
		{
			Node rightNode = FromList(nodes, middleNode, middle + 1, highExclusive);
			middleNode.Right = rightNode;
			middleNode.NumChildrenRight = rightNode.TotalSize;
			childrenHeight = int.Max(childrenHeight, rightNode.Height);
		}
		middleNode.Height = childrenHeight + 1;

		return middleNode;
	}

	public int GetIndex()
	{
		int index = NumChildrenLeft;
		Node currentNode = this;
		while (currentNode.Parent is not null)
		{
			if (currentNode.Parent.Right == currentNode) index += currentNode.Parent.NumChildrenLeft + 1;
			currentNode = currentNode.Parent;
		}
		return index;
	}

	public Node GetMin()
	{
		if (Left is null) return this;
		return Left.GetMin();
	}

	public Node GetMax()
	{
		if (Right is null) return this;
		return Right.GetMax();
	}

	public Node Find(int index)
	{
		if (index == NumChildrenLeft) return this;
		if (index < 0 || index >= TotalSize) throw new ArgumentOutOfRangeException(nameof(index));
		if (index < NumChildrenLeft) return Left!.Find(index);
		return Right!.Find(index - NumChildrenLeft - 1);
	}

	// Make sure to set the parent of oldNode to something else after calling this, and potentially updating Height
	private void ReplaceChild(Node oldNode, Node? newNode, int childrenCountChange = 0)
	{
		if (Left == oldNode)
		{
			Left = newNode;
			NumChildrenLeft += childrenCountChange;
		}
		else if (Right == oldNode)
		{
			Right = newNode;
			NumChildrenRight += childrenCountChange;
		}
		else throw new UnexpectedStateException();

		if (newNode is not null) newNode.Parent = this;
	}

	private void RecalculateHeight()
	{
		int leftHeight = Left is null ? 0 : Left.Height;
		int rightHeight = Right is null ? 0 : Right.Height;
		Height = int.Max(leftHeight, rightHeight) + 1;
	}

	// Returns the new root node
	public Node Insert(Node newNode, int index)
	{
		if (newNode is null) throw new ArgumentNullException(nameof(newNode));
		if (Parent is not null) throw new InvalidOperationException("This method can only be called on the root of the tree.");
		if (newNode == this) throw new ArgumentException("Cannot insert a node into itself.", nameof(newNode));
		if (!newNode.IsAlone) throw new ArgumentException("Only single nodes can be inserted.", nameof(newNode));

		// Step 1: search
		Node currentNode = this;
		while (true)
		{
			if (index <= currentNode.NumChildrenLeft)
			{
				if (currentNode.Left is null)
				{
					currentNode.Left = newNode;
					currentNode.NumChildrenLeft++;
					break;
				}
				currentNode = currentNode.Left;
			}
			else
			{
				if (currentNode.Right is null)
				{
					currentNode.Right = newNode;
					currentNode.NumChildrenRight++;
					break;
				}
				index -= currentNode.NumChildrenLeft + 1;
				currentNode = currentNode.Right;
			}
		}
		newNode.Parent = currentNode;
		if (currentNode.Height == 1) currentNode.Height = 2;

		// Step 2: rebalance
		return Rebalance(currentNode);
	}

	// returns the new root of the entire tree
	public Node? Delete()
	{
		Node? newRoot;
		if (Left is null)
		{
			Parent?.ReplaceChild(this, Right, -1);
			if (Right is not null) newRoot = Right;
			else
			{
				newRoot = Parent;
				if (newRoot is not null) newRoot.Height = 1;
			}
		}
		else if (Right is null)
		{
			Parent?.ReplaceChild(this, Left, -1);
			newRoot = Left;
		}
		else
		{
			if (Left.Height > Right.Height)
			{
				newRoot = Left.GetMax();
				Debug.Assert(newRoot.Parent is not null);
				Debug.Assert(newRoot.Right is null);
				Debug.Assert(newRoot.NumChildrenRight == 0);

				Node oldLeft = Left;

				if (newRoot != oldLeft)
				{
					Debug.Assert(newRoot.Parent.Right == newRoot);
					newRoot.Parent.Right = newRoot.Left;
					if (newRoot.Left is not null) newRoot.Left.Parent = newRoot.Parent;

					// Update size/height stats for nodes between newRoot and this
					Node node = newRoot.Parent;
					do
					{
						node.NumChildrenRight--;
						node.RecalculateHeight();

						Debug.Assert(node.Parent is not null);
						node = node.Parent;
					}
					while (node != this);

					newRoot.Left = oldLeft;
					oldLeft.Parent = newRoot;
				}

				newRoot.Right = Right;
				if (newRoot.Right is not null)
				{
					newRoot.Right.Parent = newRoot;
					newRoot.NumChildrenRight = NumChildrenRight;
					newRoot.RecalculateHeight();
				}
			}
			else
			{
				newRoot = Right.GetMin();
				Debug.Assert(newRoot.Parent is not null);
				Debug.Assert(newRoot.Left is null);
				Debug.Assert(newRoot.NumChildrenLeft == 0);

				Node oldRight = Right;

				if (newRoot != oldRight)
				{
					Debug.Assert(newRoot.Parent.Left == newRoot);
					newRoot.Parent.Left = newRoot.Right;
					if (newRoot.Right is not null) newRoot.Right.Parent = newRoot.Parent;

					// Update size/height stats for nodes between newRoot and this
					Node node = newRoot.Parent;
					do
					{
						node.NumChildrenLeft--;
						node.RecalculateHeight();

						Debug.Assert(node.Parent is not null);
						node = node.Parent;
					}
					while (node != this);

					newRoot.Right = oldRight;
					oldRight.Parent = newRoot;
				}

				newRoot.Left = Left;
				if (newRoot.Left is not null)
				{
					newRoot.Left.Parent = newRoot;
					newRoot.NumChildrenLeft = NumChildrenLeft;
					newRoot.RecalculateHeight();
				}
			}
			if (Parent is null) newRoot.Parent = null;
			else Parent.ReplaceChild(this, newRoot, -1);
		}

		Left = Right = Parent = null;
		NumChildrenLeft = NumChildrenRight = 0;
		Height = 1;

		if (newRoot is null) return null;
		return Rebalance(newRoot);
	}

	private static Node Rebalance(Node startingPoint)
	{
		Node currentNode = startingPoint;
		while (true)
		{
			Node? parent = currentNode.Parent;
			if (parent is null) break;

			int balanceFactor = parent.BalanceFactor;

			switch (balanceFactor)
			{
				case -2:
				{
					Debug.Assert(parent.Left is not null);

					// Check if double rotation is needed
					Node leftChild = parent.Left;
					if (leftChild.Left is null)
					{
						Debug.Assert(parent.Right is null);
						Debug.Assert(leftChild.Right is not null);

						currentNode = leftChild.Right;
						Debug.Assert(currentNode.Left is null);
						Debug.Assert(currentNode.Right is null);

						currentNode.Height = 2;
						currentNode.NumChildrenLeft = currentNode.NumChildrenRight = 1;
						currentNode.Left = leftChild;

						leftChild.Height = 1;
						leftChild.NumChildrenLeft = leftChild.NumChildrenRight = 0;
						leftChild.Right = null;
						leftChild.Parent = currentNode;

						parent.Height = 1;
						parent.NumChildrenLeft = parent.NumChildrenRight = 0;
						parent.Left = null;
					}
					else
					{
						currentNode = leftChild;

						if (currentNode.Right is not null) currentNode.Right.Parent = parent;
						parent.Left = currentNode.Right;

						parent.NumChildrenLeft = currentNode.NumChildrenRight;
						if (parent.Left is not null && (parent.Right is null || parent.Right.Height < parent.Left.Height)) parent.Height = parent.Left.Height;
						currentNode.NumChildrenRight = parent.TotalSize;
						if (parent.Height >= currentNode.Height) currentNode.Height = parent.Height + 1;
					}

					currentNode.Right = parent;

					if (parent.Parent is not null) parent.Parent?.ReplaceChild(parent, currentNode);
					else currentNode.Parent = null;
					parent.Parent = currentNode;

					break;
				}

				case 2:
				{
					Debug.Assert(parent.Right is not null);

					// Check if double rotation is needed
					Node rightChild = parent.Right;
					if (rightChild.Right is null)
					{
						Debug.Assert(parent.Left is null);
						Debug.Assert(rightChild.Left is not null);

						currentNode = rightChild.Left;
						Debug.Assert(currentNode.Left is null);
						Debug.Assert(currentNode.Right is null);

						currentNode.Height = 2;
						currentNode.NumChildrenLeft = currentNode.NumChildrenRight = 1;
						currentNode.Right = rightChild;

						rightChild.Height = 1;
						rightChild.NumChildrenLeft = rightChild.NumChildrenRight = 0;
						rightChild.Left = null;
						rightChild.Parent = currentNode;

						parent.Height = 1;
						parent.NumChildrenLeft = parent.NumChildrenRight = 0;
						parent.Right = null;
					}
					else
					{
						currentNode = rightChild;

						if (currentNode.Left is not null) currentNode.Left.Parent = parent;
						parent.Right = currentNode.Left;

						parent.NumChildrenRight = currentNode.NumChildrenLeft;
						if (parent.Right is not null && (parent.Left is null || parent.Left.Height < parent.Right.Height)) parent.Height = parent.Right.Height;
						currentNode.NumChildrenLeft = parent.TotalSize;
						if (parent.Height >= currentNode.Height) currentNode.Height = parent.Height + 1;
					}

					currentNode.Left = parent;

					if (parent.Parent is not null) parent.Parent.ReplaceChild(parent, currentNode);
					else currentNode.Parent = null;
					parent.Parent = currentNode;

					break;
				}

				default:
				{
					// No rotations needed
					currentNode = parent;

					// Update the stats
					int leftHeight, rightHeight;
					if (parent.Left is null)
					{
						leftHeight = 0;
						parent.NumChildrenLeft = 0;
					}
					else
					{
						leftHeight = parent.Left.Height;
						parent.NumChildrenLeft = parent.Left.TotalSize;
					}
					if (parent.Right is null)
					{
						rightHeight = 0;
						parent.NumChildrenRight = 0;
					}
					else
					{
						rightHeight = parent.Right.Height;
						parent.NumChildrenRight = parent.Right.TotalSize;
					}
					parent.Height = int.Max(leftHeight, rightHeight) + 1;
					break;
				}
			}
		}

		return currentNode;
	}

	public override string ToString() => Value.ToString();

	public IEnumerator<Node> GetEnumerator()
	{
		return new InOrderEnumerator(this);
	}

	class InOrderEnumerator : IEnumerator<Node>
	{
		readonly Node root;
		Node? current;

		internal InOrderEnumerator(Node root)
		{
			this.root = root;
		}

		public Node Current => current ?? throw new InvalidOperationException();

		object IEnumerator.Current => Current;

		public bool MoveNext()
		{
			Node? current = this.current;
			if (current is null)
			{
				this.current = root.GetMin();
				return true;
			}
			if (current.Right is not null)
			{
				this.current = current.Right.GetMin();
				return true;
			}
			Node parent;
			while (true)
			{
				Debug.Assert(current.Parent is not null);
				parent = current.Parent;
				if (parent.Left == current)
				{
					this.current = parent;
					return true;
				}
				Debug.Assert(parent.Right == current);
				if (parent == root)
				{
					this.current = current;
					return false;
				}
				current = parent;
			}
		}

		public void Reset()
		{
			current = null;
		}

		void IDisposable.Dispose() { }
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
*/
