using System.Diagnostics;

namespace JochCool.AdventOfCode;

/// <summary>
/// Represents a node in a binary tree.
/// </summary>
/// <remarks>
/// <para>The <see cref="IEnumerable{TSelf}"/> implementation does in-order traversal.</para>
/// </remarks>
class BinaryTreeNode<TSelf> : IEnumerable<TSelf>
	where TSelf : BinaryTreeNode<TSelf>
{
	public TSelf? Parent { get; private set; }

	public TSelf? Left { get; private set; }

	public TSelf? Right { get; private set; }

	public int NumChildrenLeft { get; private set; }

	public int NumChildrenRight { get; private set; }

	public int Height { get; private set; } = 1;

	protected BinaryTreeNode()
	{
		if (!typeof(TSelf).IsAssignableFrom(GetType()))
		{
			throw new InvalidOperationException();
		}
	}

	/// <summary>
	/// Gets the total number of nodes in this subtree.
	/// </summary>
	public int TotalSize => NumChildrenLeft + NumChildrenRight + 1;

	/// <summary>
	/// Gets whether this is the only node in the entire tree.
	/// </summary>
	/// <value><see langword="true"/> if this is the only node; <see langword="false"/> if there are other nodes in the tree.</value>
	public bool IsAlone => Parent is null && Left is null && Right is null;

	public TSelf GetMin()
	{
		if (Left is null) return (TSelf)this;
		return Left.GetMin();
	}

	public TSelf GetMax()
	{
		if (Right is null) return (TSelf)this;
		return Right.GetMax();
	}

	private void UpdateNumbers(BinaryTreeNode<TSelf>? stopAt = null)
	{
		if (this == stopAt) return;

		int leftHeight, rightHeight;
		if (Left is null)
		{
			NumChildrenLeft = 0;
			leftHeight = 0;
		}
		else
		{
			NumChildrenLeft = Left.TotalSize;
			leftHeight = Left.Height;
		}
		if (Right is null)
		{
			NumChildrenRight = 0;
			rightHeight = 0;
		}
		else
		{
			NumChildrenRight = Right.TotalSize;
			rightHeight = Right.Height;
		}
		Height = int.Max(leftHeight, rightHeight) + 1;

		Parent?.UpdateNumbers();
	}

	protected void RemoveDirectChild(TSelf child)
	{
		int otherChildHeight;
		if (child == Left)
		{
			Left = null;
			NumChildrenLeft = 0;
			otherChildHeight = Right is null ? 0 : Right.Height;
		}
		else if (child == Right)
		{
			Right = null;
			NumChildrenRight = 0;
			otherChildHeight = Left is null ? 0 : Left.Height;
		}
		else throw new ArgumentException("Argument is not a direct child of this node.", nameof(child));

		child.Parent = null;
		
		int newHeight = otherChildHeight + 1;
		if (newHeight != Height)
		{
			Height = newHeight;
			Parent?.UpdateNumbers();
		}
	}

	protected void ReplaceDirectChild(TSelf oldChild, TSelf? newChild)
	{
		if (oldChild == Left)
		{
			SetLeft(newChild);
		}
		else if (oldChild == Right)
		{
			SetRight(newChild);
		}
		else throw new ArgumentException("Argument is not a direct child of this node.", nameof(oldChild));
	}

	protected void SetLeft(TSelf? newNode)
	{
		if (Left is not null) Left.Parent = null;
		Left = newNode;
		if (newNode is not null) MakeParentOf(newNode);
		UpdateNumbers();
	}

	protected void SetRight(TSelf? newNode)
	{
		if (Right is not null) Right.Parent = null;
		Right = newNode;
		if (newNode is not null) MakeParentOf(newNode);
		UpdateNumbers();
	}

	private void MakeParentOf(TSelf newNode)
	{
		TSelf? parent = newNode.Parent;
		if (parent is not null)
		{
			if (parent.Left == newNode) parent.Left = null;
			else parent.Right = null;
			parent.UpdateNumbers(this);
		}
		newNode.Parent = (TSelf)this;
	}

	// Returns the lowest node in the tree that had their values changed
	private protected TSelf? Delete()
	{
		TSelf? toUpdate; // Value that will be returned in the end

		if (Left is null)
		{
			toUpdate = Parent;
			if (toUpdate is null)
			{
				if (Right is not null)
				{
					Right.Parent = null;
					toUpdate = Right;
				}
			}
			else
			{
				toUpdate.ReplaceDirectChild((TSelf)this, Right);
			}
		}
		else if (Right is null)
		{
			toUpdate = Parent;
			if (toUpdate is null)
			{
				Left.Parent = null;
				toUpdate = Left;
			}
			else
			{
				toUpdate.ReplaceDirectChild((TSelf)this, Left);
			}
		}
		else
		{
			TSelf newRoot;

			if (Left.Height > Right.Height)
			{
				newRoot = Left.GetMax();

				Debug.Assert(newRoot.Parent is not null);
				Debug.Assert(newRoot.Right is null);
				Debug.Assert(newRoot.NumChildrenRight == 0);
				if (newRoot != Left)
				{
					Debug.Assert(newRoot.Parent.Right == newRoot);
					newRoot.Parent.Right = newRoot.Left;
					if (newRoot.Left is not null) newRoot.Left.Parent = newRoot.Parent;

					newRoot.Left = Left;
					Left.Parent = newRoot;

					toUpdate = newRoot.Parent;
				}
				else toUpdate = newRoot;

				newRoot.Right = Right;
				Right.Parent = newRoot;
			}
			else
			{
				newRoot = Right.GetMin();
				Debug.Assert(newRoot.Parent is not null);
				Debug.Assert(newRoot.Left is null);
				Debug.Assert(newRoot.NumChildrenLeft == 0);
				if (newRoot != Right)
				{
					Debug.Assert(newRoot.Parent.Left == newRoot);
					newRoot.Parent.Left = newRoot.Right;
					if (newRoot.Right is not null) newRoot.Right.Parent = newRoot.Parent;

					newRoot.Right = Right;
					Right.Parent = newRoot;

					toUpdate = newRoot.Parent;
				}
				else toUpdate = newRoot;

				newRoot.Left = Left;
				Left.Parent = newRoot;
			}

			newRoot.Parent = Parent;
			if (Parent is not null)
			{
				if (Parent.Left == this) Parent.Left = newRoot;
				else Parent.Right = newRoot;

				Parent = null;
			}

			toUpdate.UpdateNumbers();
		}

		Left = Right = null;
		NumChildrenLeft = NumChildrenRight = 0;
		Height = 1;

		return toUpdate;
	}

	internal static TSelf FromList(List<TSelf> nodes, TSelf? parent, int lowInclusive, int highExclusive)
	{
		int middle = (lowInclusive + highExclusive) / 2;
		TSelf middleNode = nodes[middle];
		middleNode.Parent = parent;

		int childrenHeight = 0;
		if (middle > lowInclusive)
		{
			TSelf leftNode = FromList(nodes, middleNode, lowInclusive, middle);
			middleNode.Left = leftNode;
			middleNode.NumChildrenLeft = leftNode.TotalSize;
			childrenHeight = leftNode.Height;
		}
		if (middle + 1 < highExclusive)
		{
			TSelf rightNode = FromList(nodes, middleNode, middle + 1, highExclusive);
			middleNode.Right = rightNode;
			middleNode.NumChildrenRight = rightNode.TotalSize;
			childrenHeight = int.Max(childrenHeight, rightNode.Height);
		}
		middleNode.Height = childrenHeight + 1;

		return middleNode;
	}

	public IEnumerator<TSelf> GetEnumerator()
	{
		return new InOrderEnumerator((TSelf)this);
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	class InOrderEnumerator : IEnumerator<TSelf>
	{
		readonly TSelf root;
		TSelf? current;

		internal InOrderEnumerator(TSelf root)
		{
			this.root = root;
		}

		public TSelf Current => current ?? throw new InvalidOperationException();

		object IEnumerator.Current => Current;

		public bool MoveNext()
		{
			TSelf? current = this.current;
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
			TSelf parent;
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
}
