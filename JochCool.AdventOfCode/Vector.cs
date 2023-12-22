using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode;

/// <summary>
/// Represents a two-dimentional vector.
/// </summary>
/// <typeparam name="T">The type of number that this is a vector of.</typeparam>
/// <param name="X">The X-component of the vector.</param>
/// <param name="Y">The Y-component of the vector.</param>
/// <seealso cref="Vector3D{T}"/>
public record struct Vector<T>(T X, T Y) : IVector<Vector<T>, T> where T : INumber<T>
{
	public static Vector<T> Origin => new Vector<T>(T.Zero, T.Zero);
	public static Vector<T> ToPositiveX => new Vector<T>(T.One, T.Zero);
	public static Vector<T> ToPositiveY => new Vector<T>(T.Zero, T.One);
	public static Vector<T> ToNegativeX => new Vector<T>(-T.One, T.Zero);
	public static Vector<T> ToNegativeY => new Vector<T>(T.Zero, -T.One);

	public static Vector<T>[] AxisUnitVectors =>
	[
		ToPositiveX,
		ToPositiveY,
		ToNegativeX,
		ToNegativeY
	];

	static IEnumerable<Vector<T>> IVector<Vector<T>, T>.AxisUnitVectors => AxisUnitVectors;

	public readonly T SquareMagnitude => X*X + Y*Y;

	public readonly T TaxicabMagnitude => T.Abs(X) + T.Abs(Y);

	public static Vector<T> AdditiveIdentity => new Vector<T>(T.AdditiveIdentity, T.AdditiveIdentity);
	public static T MultiplicativeIdentity => T.MultiplicativeIdentity;

	public readonly bool IsInBox(Vector<T> min, Vector<T> max)
	{
		return X >= min.X && Y >= min.Y && X <= max.X && Y <= max.Y;
	}

	public static Vector<T> Clamp(Vector<T> vector, Vector<T> min, Vector<T> max)
	{
		return new Vector<T>(T.Clamp(vector.X, min.X, max.X), T.Clamp(vector.Y, min.Y, max.Y));
	}

	public static Vector<T> Swizzle(Vector<T> vector) => new(vector.Y, vector.X);

	public static Vector<T> operator +(Vector<T> value) => new Vector<T>(+value.X, +value.Y);

	public static Vector<T> operator -(Vector<T> value) => new Vector<T>(-value.X, -value.Y);

	public static Vector<T> operator +(Vector<T> left, Vector<T> right)
	{
		return new Vector<T>(left.X + right.X, left.Y + right.Y);
	}

	public static Vector<T> operator -(Vector<T> left, Vector<T> right)
	{
		return new Vector<T>(left.X - right.X, left.Y - right.Y);
	}

	public static Vector<T> operator *(Vector<T> left, T right)
	{
		return new Vector<T>(left.X * right, left.Y * right);
	}

	public static Vector<T> operator *(T left, Vector<T> right)
	{
		return new Vector<T>(left * right.X, left * right.Y);
	}

	public static Vector<T> operator /(Vector<T> left, T right)
	{
		return new Vector<T>(left.X / right, left.Y / right);
	}

	public static T Dot(Vector<T> left, Vector<T> right)
	{
		return left.X * right.X + left.Y * right.Y;
	}

	public static T Determinant(Vector<T> a, Vector<T> b)
	{
		return a.X * b.Y - a.Y * b.X;
	}

	public override readonly string ToString()
	{
		return $"({X}, {Y})";
	}

	public static Vector<T> Parse(string s, IFormatProvider? formatProvider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), formatProvider);
	}

	public static Vector<T> Parse(ReadOnlySpan<char> s, IFormatProvider? formatProvider = null)
	{
		if (!TryParse(s, formatProvider, out Vector<T> result))
		{
			throw new FormatException();
		}
		return result;
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? formatProvider, out Vector<T> result)
	{
		return TryParse(s.AsSpan(), formatProvider, out result);
	}

	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? formatProvider, out Vector<T> result)
	{
		const char separator = ','; // TODO: get this from the format provider?

		int separatorIndex = s.IndexOf(separator);
		if (separatorIndex == -1)
		{
			result = default;
			return false;
		}

		ReadOnlySpan<char> xSpan = s[..separatorIndex];
		ReadOnlySpan<char> ySpan = s[(separatorIndex + 1)..];

		if (ySpan.Contains(separator))
		{
			result = default;
			return false;
		}

		if (!T.TryParse(xSpan, formatProvider, out T? x) ||
			!T.TryParse(ySpan, formatProvider, out T? y))
		{
			result = default;
			return false;
		}

		result = new Vector<T>(x, y);
		return true;
	}
}
