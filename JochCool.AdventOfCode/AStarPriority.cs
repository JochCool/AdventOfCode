using System.Diagnostics;

namespace JochCool.AdventOfCode;

/// <summary>
/// Represents the priority of a node in a graph for the A* algorithm, with lower values being better.
/// </summary>
[DebuggerDisplay("{sum}; {heuristic}")]
readonly struct AStarPriority : IComparable<AStarPriority>, IEquatable<AStarPriority>, IEqualityOperators<AStarPriority, AStarPriority, bool>
{
	readonly int sum;
	readonly int heuristic;

	/// <summary>
	/// Constructs an <see cref="AStarPriority"/>. None of the parameter's values are checked.
	/// </summary>
	/// <param name="cost">The cost to get to this node.</param>
	/// <param name="heuristic">The estimation of the cost to get to the goal; this must never be an overestimation.</param>
	public AStarPriority(int cost, int heuristic)
	{
		sum = cost + heuristic;
		this.heuristic = heuristic;
	}

	public int Sum => sum;

	public int Cost => sum - heuristic;

	public int Heuristic => heuristic;

	public int CompareTo(AStarPriority other)
	{
		int result = sum - other.sum;
		if (result == 0) result = heuristic - other.heuristic;
		return result;
	}

	public override int GetHashCode() => HashCode.Combine(sum, heuristic);

	public override bool Equals(object? obj) => obj is AStarPriority other && this == other;

	public bool Equals(AStarPriority other) => this == other;

	public static bool operator ==(AStarPriority left, AStarPriority right) => left.sum == right.sum && left.heuristic == right.heuristic;

	public static bool operator !=(AStarPriority left, AStarPriority right) => !(left == right);
}
