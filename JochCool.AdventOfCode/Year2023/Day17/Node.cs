using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode.Year2023.Day17;

readonly struct Node : IEquatable<Node>, IEqualityOperators<Node, Node, bool>
{
	const int dirIShift = 8;
	const int stepsDoneMask = (1 << dirIShift) - 1;

	readonly Vector<int> pos;
	readonly int data; // includes steps done and direction

	public Node(Vector<int> pos, int stepsDone, int directionIndex)
	{
		this.pos = pos;
		data = stepsDone | directionIndex << dirIShift;
	}

	public Vector<int> Position => pos;

	public int StepsDone => data & stepsDoneMask;

	public int DirectionIndex => data >> dirIShift;

	public override int GetHashCode() => HashCode.Combine(pos, data);

	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Node node && Equals(node);

	public bool Equals(Node other) => this == other;

	public static bool operator ==(Node left, Node right) => left.pos == right.pos && left.data == right.data;

	public static bool operator !=(Node left, Node right) => !(left == right);
}
