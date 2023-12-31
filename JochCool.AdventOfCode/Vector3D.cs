using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode;

/// <summary>
/// Represents a three-dimentional vector.
/// </summary>
/// <typeparam name="T">The type of number that this is a vector of.</typeparam>
/// <param name="X">The X-component of the vector.</param>
/// <param name="Y">The Y-component of the vector.</param>
/// <param name="Z">The Z-component of the vector.</param>
/// <seealso cref="Vector{T}"/>
public record struct Vector3D<T>(T X, T Y, T Z) : IVector<Vector3D<T>, T> where T : INumber<T>
{
	/// <summary>
	/// Consructs a 3D vector by augmenting a value to a 2D vector.
	/// </summary>
	/// <param name="xy">The original vector.</param>
	/// <param name="z">The augmented value.</param>
	public Vector3D(Vector<T> xy, T z) : this(xy.X, xy.Y, z) { }

	public static Vector3D<T> Origin => new Vector3D<T>(T.Zero, T.Zero, T.Zero);
	public static Vector3D<T> ToPositiveX => new Vector3D<T>(T.One, T.Zero, T.Zero);
	public static Vector3D<T> ToPositiveY => new Vector3D<T>(T.Zero, T.One, T.Zero);
	public static Vector3D<T> ToPositiveZ => new Vector3D<T>(T.Zero, T.Zero, T.One);
	public static Vector3D<T> ToNegativeX => new Vector3D<T>(-T.One, T.Zero, T.Zero);
	public static Vector3D<T> ToNegativeY => new Vector3D<T>(T.Zero, -T.One, T.Zero);
	public static Vector3D<T> ToNegativeZ => new Vector3D<T>(T.Zero, T.Zero, -T.One);

	public static Vector3D<T>[] AxisUnitVectors =>
	[
		ToPositiveX,
		ToPositiveY,
		ToPositiveZ,
		ToNegativeX,
		ToNegativeY,
		ToNegativeZ
	];

	static IEnumerable<Vector3D<T>> IVector<Vector3D<T>, T>.AxisUnitVectors => AxisUnitVectors;

	public readonly T SquareMagnitude => X * X + Y * Y + Z * Z;

	public readonly T TaxicabMagnitude => T.Abs(X) + T.Abs(Y) + T.Abs(Z);

	public static Vector3D<T> AdditiveIdentity => new Vector3D<T>(T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity);
	public static T MultiplicativeIdentity => T.MultiplicativeIdentity;

	public readonly bool IsInBox(Vector3D<T> min, Vector3D<T> max)
	{
		return X >= min.X && Y >= min.Y && Z >= min.Z && X <= max.X && Y <= max.Y && Z <= max.Z;
	}

	public static Vector3D<T> Clamp(Vector3D<T> vector, Vector3D<T> min, Vector3D<T> max)
	{
		return new Vector3D<T>(T.Clamp(vector.X, min.X, max.X), T.Clamp(vector.Y, min.Y, max.Y), T.Clamp(vector.Z, min.Z, max.Z));
	}

	public static Vector3D<T> operator +(Vector3D<T> value) => new Vector3D<T>(+value.X, +value.Y, +value.Z);

	public static Vector3D<T> operator -(Vector3D<T> value) => new Vector3D<T>(-value.X, -value.Y, -value.Z);

	public static Vector3D<T> operator +(Vector3D<T> left, Vector3D<T> right)
	{
		return new Vector3D<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	}

	public static Vector3D<T> operator -(Vector3D<T> left, Vector3D<T> right)
	{
		return new Vector3D<T>(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	}

	public static Vector3D<T> operator *(Vector3D<T> left, T right)
	{
		return new Vector3D<T>(left.X * right, left.Y * right, left.Z * right);
	}

	public static Vector3D<T> operator /(Vector3D<T> left, T right)
	{
		return new Vector3D<T>(left.X / right, left.Y / right, left.Z / right);
	}

	public static T Dot(Vector3D<T> left, Vector3D<T> right)
	{
		return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
	}

	public override readonly string ToString()
	{
		return $"({X}, {Y}, {Z})";
	}

	public static Vector3D<T> Parse(string s, IFormatProvider? formatProvider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), formatProvider);
	}

	public static Vector3D<T> Parse(ReadOnlySpan<char> s, IFormatProvider? formatProvider = null)
	{
		if (!TryParse(s, formatProvider, out Vector3D<T> result))
		{
			throw new FormatException();
		}
		return result;
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? formatProvider, out Vector3D<T> result)
	{
		return TryParse(s.AsSpan(), formatProvider, out result);
	}

	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? formatProvider, out Vector3D<T> result)
	{
		const char separator = ','; // TODO: get this from the format provider?

		int separator1Index = s.IndexOf(separator);
		if (separator1Index == -1)
		{
			result = default;
			return false;
		}

		ReadOnlySpan<char> xSpan = s[..separator1Index];

		s = s[(separator1Index + 1)..];
		int separator2Index = s.IndexOf(separator);
		if (separator2Index == -1)
		{
			result = default;
			return false;
		}

		ReadOnlySpan<char> ySpan = s[..separator2Index];
		ReadOnlySpan<char> zSpan = s[(separator2Index + 1)..];

		if (zSpan.Contains(separator))
		{
			result = default;
			return false;
		}

		if (!T.TryParse(xSpan, formatProvider, out T? x) ||
			!T.TryParse(ySpan, formatProvider, out T? y) ||
			!T.TryParse(zSpan, formatProvider, out T? z))
		{
			result = default;
			return false;
		}

		result = new Vector3D<T>(x, y, z);
		return true;
	}
}
