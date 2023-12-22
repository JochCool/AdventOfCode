using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode.Year2023.Day22;

class Brick : ISpanParsable<Brick>, IComparable<Brick>
{
	Vector3D<int> start;
	Vector3D<int> end;

	internal List<Brick>? supporters;
	internal readonly List<Brick> beingSupported = [];

	public Brick(Vector3D<int> start, Vector3D<int> end)
	{
		if (start.X > end.X)
			(start.X, end.X) = (end.X, start.X);

		if (start.Y > end.Y)
			(start.Y, end.Y) = (end.Y, start.Y);

		if (start.Z > end.Z)
			(start.Z, end.Z) = (end.Z, start.Z);

		this.start = start;
		this.end = end;
	}

	public Vector3D<int> Start => start;
	public Vector3D<int> End => end;

	public bool CanBeDisintegrated
	{
		get
		{
			foreach (Brick supported in beingSupported)
			{
				Debug.Assert(supported.supporters is not null);
				Debug.Assert(supported.supporters.Count != 0);
				if (supported.supporters.Count == 1)
				{
					return false;
				}
			}
			return true;
		}
	}

	public bool ContainsPoint(Vector3D<int> point)
	{
		return point.X >= start.X && point.X <= end.X
			&& point.Y >= start.Y && point.Y <= end.Y
			&& point.Z >= start.Z && point.Z <= end.Z;
	}

	public bool IntersectsCuboid(Vector3D<int> start, Vector3D<int> end)
	{
		return end.X >= this.start.X && start.X <= this.end.X
			&& end.Y >= this.start.Y && start.Y <= this.end.Y
			&& end.Z >= this.start.Z && start.Z <= this.end.Z;
	}

	public void Move(Vector3D<int> displacement)
	{
		start += displacement;
		end += displacement;
	}

	public void MoveTo(Vector3D<int> newStartPos)
	{
		end += newStartPos - start;
		start = newStartPos;
	}

	public int GetTotalBeingSupported() => GetTotalBeingSupported([this]);

	private int GetTotalBeingSupported(HashSet<Brick> counted)
	{
		int result = 0;
		foreach (Brick brick in beingSupported)
		{
			Debug.Assert(brick.supporters is not null);
			if (counted.IsSupersetOf(brick.supporters) && counted.Add(brick))
			{
				result += brick.GetTotalBeingSupported(counted) + 1;
			}
		}
		return result;
	}

	public static Brick Parse(string s, IFormatProvider? formatProvider)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), formatProvider);
	}

	public static Brick Parse(ReadOnlySpan<char> s, IFormatProvider? formatProvider)
	{
		if (!TryParse(s, formatProvider, out Brick? result))
		{
			throw new FormatException();
		}
		return result;
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? formatProvider, [MaybeNullWhen(false)] out Brick result)
	{
		return TryParse(s.AsSpan(), formatProvider, out result);
	}

	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? formatProvider, [MaybeNullWhen(false)] out Brick result)
	{
		int separatorI = s.IndexOf('~');
		if (separatorI == -1)
		{
			result = null;
			return false;
		}

		if (!Vector3D<int>.TryParse(s[..separatorI], formatProvider, out Vector3D<int> start) ||
			!Vector3D<int>.TryParse(s[(separatorI + 1)..], formatProvider, out Vector3D<int> end))
		{
			result = null;
			return false;
		}

		result = new Brick(start, end);
		return true;
	}

	public int CompareTo(Brick? other)
	{
		if (other is null) return 1;
		return start.Z.CompareTo(other.start.Z);
	}
}
