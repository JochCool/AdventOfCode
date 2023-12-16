using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode.Year2023.Day16;

struct Beam : IEquatable<Beam>, IEqualityOperators<Beam, Beam, bool>
{
	public Vector<int> Position;
	public Vector<int> Direction;

	public override readonly int GetHashCode() => HashCode.Combine(Position, Direction);

	public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Beam b && Equals(b);

	public readonly bool Equals(Beam other) => Position == other.Position && Direction == other.Direction;

	public static bool operator ==(Beam left, Beam right) => left.Position == right.Position && left.Direction == right.Direction;

	public static bool operator !=(Beam left, Beam right) => !(left == right);
}
