namespace JochCool.AdventOfCode;

/// <summary>
/// Represents any vector of any amount of numbers.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
/// <typeparam name="T">The type of number that this is a vector of.</typeparam>
public interface IVector<TSelf, T> :
	IAdditiveIdentity<TSelf, TSelf>,
	IAdditionOperators<TSelf, TSelf, TSelf>,
	ISubtractionOperators<TSelf, TSelf, TSelf>,
	IUnaryNegationOperators<TSelf, TSelf>,
	IUnaryPlusOperators<TSelf, TSelf>,

	IMultiplicativeIdentity<TSelf, T>,
	IMultiplyOperators<TSelf, T, TSelf>,
	IDivisionOperators<TSelf, T, TSelf>,

	IEqualityOperators<TSelf, TSelf, bool>,
	IEquatable<TSelf>,

	ISpanParsable<TSelf>
	//ISpanFormattable // Maybe later

	where TSelf : IVector<TSelf, T>
	where T : INumber<T>
{
	/// <summary>
	/// Gets the vector whose magnitude is zero.
	/// </summary>
	abstract static TSelf Origin { get; }

	/// <summary>
	/// Gets all unit vectors that are along an axis, in arbitrary order.
	/// </summary>
	// Just for fun, the Wikidata item: Q108316423
	abstract static IEnumerable<TSelf> AxisUnitVectors { get; }

	/// <summary>
	/// Gets the square of the magnitude of this vector in Euclidean distance.
	/// </summary>
	T SquareMagnitude { get; }

	/// <summary>
	/// Gets the magnitude of this vector in taxicab distance (also known as Manhattan distance).
	/// </summary>
	/// <remarks>
	/// <para>This is equal to the amount of unit-sized steps along an axis needed to take to get from <see cref="Origin"/> to this vector.</para>
	/// </remarks>
	T TaxicabMagnitude { get; }

	/// <summary>
	/// Tests whether this vector is in a box defined by two other vectors.
	/// </summary>
	/// <param name="min">The lower corner of the box (inclusive).</param>
	/// <param name="max">The upper corner of the box (inclusive).</param>
	/// <returns><see langword="true"/> if this vector is at or between <paramref name="max"/> and <paramref name="max"/>; otherwise, false.</returns>
	bool IsInBox(TSelf min, TSelf max);

	/// <summary>
	/// Clamps a vector to a box defined by two other vectors.
	/// </summary>
	/// <param name="value">The value to clamp.</param>
	/// <param name="min">The lower corner of the box to which <paramref name="value"/> should clamp (inclusive).</param>
	/// <param name="max">The upper corner of the box to which <paramref name="value"/> should clamp (inclusive).</param>
	/// <returns>The result of clamping <paramref name="value"/> in the box.</returns>
	static abstract TSelf Clamp(TSelf value, TSelf min, TSelf max);

	static abstract T Dot(TSelf left, TSelf right);
}
