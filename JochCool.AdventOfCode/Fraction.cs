using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace JochCool.AdventOfCode;

// Rules:
// n/0 is not allowed
// 0/d becomes 0/0 BUT is publicly shown as 0/1
// n/-d becomes -d/n
// If n and d have an integer common denominator, both are divided by that

/// <summary>
/// Represents any rational number.
/// </summary>
/// <remarks>
/// <para>The fraction is always guaranteed to be in its most simplified form.</para>
/// <para>The <c>default</c> value of this struct represents 0.</para>
/// </remarks>
readonly struct Fraction : INumber<Fraction>
{
	readonly BigInteger numerator;
	readonly BigInteger denominator;

	private Fraction(BigInteger numerator, BigInteger denominator)
	{
		this.numerator = numerator;
		this.denominator = denominator;
		/*
		if (denominator.IsZero) throw new DivideByZeroException();
		if (numerator.IsZero)
		{
			Numerator = numerator;
			Denominator = BigInteger.One;
		}
		else
		{
			if (BigInteger.IsNegative(denominator))
			{
				denominator = -denominator;
				numerator = -numerator;
			}
			BigInteger divisor = BigInteger.GreatestCommonDivisor(numerator, denominator);
			Numerator = numerator / divisor;
			Denominator = denominator / divisor;
		}
		*/
	}

	/// <summary>
	/// Gets the numerator of this fraction (in simplified form).
	/// </summary>
	/// <value>The numerator of this fraction; can be any number in the range of <see cref="BigInteger"/>.</value>
	public BigInteger Numerator => numerator;

	/// <summary>
	/// Gets the denominator of this fraction (in simplified form).
	/// </summary>
	/// <value>The denominator of this fraction; is always a positive number.</value>
	public BigInteger Denominator => denominator.IsZero ? BigInteger.One : denominator;

	public static Fraction Zero => new(0, 0);
	public static Fraction One => new(1, 1);
	public static Fraction MinusOne => new(-1, 1);
	public static Fraction Half => new(1, 2);
	public static Fraction AdditiveIdentity => new(0, 0);
	public static Fraction MultiplicativeIdentity => new(1, 1);

	static int INumberBase<Fraction>.Radix => 2;

	public static Fraction Abs(Fraction value)
	{
		return new(BigInteger.Abs(value.numerator), value.denominator);
	}

	public static bool IsPositive(Fraction value) => BigInteger.IsPositive(value.numerator);

	public static bool IsNegative(Fraction value) => BigInteger.IsNegative(value.numerator);

	public static bool IsZero(Fraction value) => value.numerator.IsZero;

	public static bool IsInteger(Fraction value) => value.Denominator.IsOne;

	public static bool IsEvenInteger(Fraction value)
	{
		return value.Denominator.IsOne && BigInteger.IsEvenInteger(value.Numerator);
	}

	public static bool IsOddInteger(Fraction value)
	{
		return value.denominator.IsOne && BigInteger.IsOddInteger(value.numerator);
	}

	public static bool IsPow2(Fraction value)
	{
		if (!IsPositive(value)) return false;
		if (value.denominator.IsOne) return BigInteger.IsPow2(value.numerator);
		if (value.numerator.IsOne) return BigInteger.IsPow2(value.denominator);
		return false;
	}

	public static Fraction MaxMagnitude(Fraction x, Fraction y)
	{
		throw new NotImplementedException();
	}

	public static Fraction MinMagnitude(Fraction x, Fraction y)
	{
		throw new NotImplementedException();
	}

	static Fraction INumberBase<Fraction>.MaxMagnitudeNumber(Fraction x, Fraction y) => MaxMagnitude(x, y);
	static Fraction INumberBase<Fraction>.MinMagnitudeNumber(Fraction x, Fraction y) => MinMagnitude(x, y);

	static bool INumberBase<Fraction>.IsRealNumber(Fraction value) => true;
	static bool INumberBase<Fraction>.IsComplexNumber(Fraction value) => false;
	static bool INumberBase<Fraction>.IsImaginaryNumber(Fraction value) => false;
	static bool INumberBase<Fraction>.IsFinite(Fraction value) => true;
	static bool INumberBase<Fraction>.IsInfinity(Fraction value) => false;
	static bool INumberBase<Fraction>.IsNegativeInfinity(Fraction value) => false;
	static bool INumberBase<Fraction>.IsPositiveInfinity(Fraction value) => false;
	static bool INumberBase<Fraction>.IsNaN(Fraction value) => false;
	static bool INumberBase<Fraction>.IsCanonical(Fraction value) => true;
	static bool INumberBase<Fraction>.IsNormal(Fraction value) => true;
	static bool INumberBase<Fraction>.IsSubnormal(Fraction value) => false;

	public static Fraction Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
	{
		throw new NotImplementedException();
	}

	public static Fraction Parse(string s, NumberStyles style, IFormatProvider? provider)
	{
		throw new NotImplementedException();
	}

	public static Fraction Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
	{
		throw new NotImplementedException();
	}

	public static Fraction Parse(string s, IFormatProvider? provider)
	{
		throw new NotImplementedException();
	}

	public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		throw new NotImplementedException();
	}

	public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		throw new NotImplementedException();
	}

	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		throw new NotImplementedException();
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		throw new NotImplementedException();
	}

	public override string ToString()
	{
		if (IsZero(this)) return "0";
		if (denominator.IsOne) return numerator.ToString();
		return $"{numerator}/{denominator}";
	}

	public string ToString(string? format, IFormatProvider? formatProvider)
	{
		if (format is not null || formatProvider is not null) throw new NotImplementedException();
		return ToString();
	}

	public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
	{
		if (format != default || provider is not null) throw new NotImplementedException();
		if (IsZero(this))
		{
			if (destination.Length == 0)
			{
				charsWritten = 0;
				return false;
			}
			destination[0] = '0';
			charsWritten = 1;
			return true;
		}

		if (!numerator.TryFormat(destination, out int writtenSoFar, default, default))
		{
			charsWritten = writtenSoFar;
			return false;
		}

		if (denominator.IsOne)
		{
			charsWritten = writtenSoFar;
			return true;
		}

		if (writtenSoFar + 1 >= destination.Length)
		{
			charsWritten = writtenSoFar;
			return false;
		}

		destination[writtenSoFar++] = '/';

		bool success = denominator.TryFormat(destination[writtenSoFar..], out int written, default, default);
		
		charsWritten = writtenSoFar + 1 + written;
		return success;
	}

	public static bool TryConvertFromChecked<TOther>(TOther value, out Fraction result) where TOther : INumberBase<TOther>
	{
		throw new NotImplementedException();
	}

	public static bool TryConvertFromSaturating<TOther>(TOther value, out Fraction result) where TOther : INumberBase<TOther>
	{
		throw new NotImplementedException();
	}

	public static bool TryConvertFromTruncating<TOther>(TOther value, out Fraction result) where TOther : INumberBase<TOther>
	{
		throw new NotImplementedException();
	}

	public static bool TryConvertToChecked<TOther>(Fraction value, out TOther result) where TOther : INumberBase<TOther>
	{
		throw new NotImplementedException();
	}

	public static bool TryConvertToSaturating<TOther>(Fraction value, out TOther result) where TOther : INumberBase<TOther>
	{
		throw new NotImplementedException();
	}

	public static bool TryConvertToTruncating<TOther>(Fraction value, out TOther result) where TOther : INumberBase<TOther>
	{
		throw new NotImplementedException();
	}

	public static Fraction Inverse(Fraction value)
	{
		if (IsZero(value)) throw new DivideByZeroException();
		if (BigInteger.IsNegative(value.numerator)) return new(-value.denominator, -value.numerator);
		return new(value.denominator, value.numerator);
	}

	public static Fraction operator +(Fraction value) => value;

	public static Fraction operator -(Fraction value) => new(-value.numerator, value.denominator);

	public static Fraction operator +(Fraction left, Fraction right)
	{
		BigInteger newN = left.Numerator * right.Denominator + right.Numerator * left.Denominator;
		BigInteger newD = left.Denominator * right.Denominator;
		if (newN.IsZero) return Zero;
		BigInteger gcd = BigInteger.GreatestCommonDivisor(newN, newD);
		return new(newN / gcd, newD / gcd);
	}

	public static Fraction operator -(Fraction left, Fraction right) => left + -right;

	public static Fraction operator ++(Fraction value)
	{
		if (IsZero(value)) return One;
		return new(value.numerator + value.denominator, value.denominator);
	}

	public static Fraction operator --(Fraction value)
	{
		if (IsZero(value)) return MinusOne;
		return new(value.numerator - value.denominator, value.denominator);
	}

	public static Fraction operator *(Fraction left, Fraction right)
	{
		if (IsZero(left) || IsZero(right)) return Zero;
		BigInteger gcd = BigInteger.GreatestCommonDivisor(left.numerator, right.denominator);
		gcd *= BigInteger.GreatestCommonDivisor(left.denominator, right.numerator);
		return new(
			left.numerator * right.numerator / gcd,
			left.denominator * right.denominator / gcd
		);
	}

	public static Fraction operator /(Fraction left, Fraction right)
	{
		return left * Inverse(right);
	}

	public static Fraction operator %(Fraction left, Fraction right)
	{
		throw new NotImplementedException();
	}

	public int CompareTo(object? obj)
	{
		throw new NotImplementedException();
	}

	public int CompareTo(Fraction other)
	{
		throw new NotImplementedException();
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(numerator, denominator);
	}

	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is Fraction fraction && Equals(fraction);
	}

	public bool Equals(Fraction other)
	{
		return numerator.Equals(other.numerator) && denominator.Equals(other.denominator);
	}

	public static bool operator ==(Fraction left, Fraction right)
	{
		return left.numerator == right.numerator && left.denominator == right.denominator;
	}

	public static bool operator !=(Fraction left, Fraction right) => !(left == right);

	public static bool operator <(Fraction left, Fraction right)
	{
		throw new NotImplementedException();
	}

	public static bool operator >(Fraction left, Fraction right)
	{
		throw new NotImplementedException();
	}

	public static bool operator <=(Fraction left, Fraction right)
	{
		throw new NotImplementedException();
	}

	public static bool operator >=(Fraction left, Fraction right)
	{
		throw new NotImplementedException();
	}

	public static implicit operator Fraction(BigInteger value) => value.IsZero ? Zero : new(value, BigInteger.One);

	public static explicit operator BigInteger(Fraction value) => value.Numerator / value.Denominator;
}
