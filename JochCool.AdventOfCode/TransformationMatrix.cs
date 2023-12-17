using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode;

/// <summary>
/// Represents a transposed <see href="https://en.wikipedia.org/wiki/Affine_transformation#Augmented_matrix">affine transformation matrix</see> for 2D vectors. It is stored as a row-major 3x2 matrix, with the third column assumed to be (0, 0, 1). It is the generic version of <see cref="Matrix3x2"/>.
/// </summary>
/// <remarks>
/// <para>Because the matrix is transposed, multiplications should be applied in the opposite order. So, to transform a vector by matrix A then B then C, the matrix multiplication is not CBA, but ABC.</para>
/// </remarks>
public struct TransformationMatrix<T> :
	IMultiplicativeIdentity<TransformationMatrix<T>, TransformationMatrix<T>>,
	IMultiplyOperators<TransformationMatrix<T>, TransformationMatrix<T>, TransformationMatrix<T>>,
	IDivisionOperators<TransformationMatrix<T>, TransformationMatrix<T>, TransformationMatrix<T>>,

	IEqualityOperators<TransformationMatrix<T>, TransformationMatrix<T>, bool>,
	IEquatable<TransformationMatrix<T>>

	where T : INumber<T>
{
	public Vector<T> Row1;
	public Vector<T> Row2;
	public Vector<T> Row3;

	/// <summary>
	/// Initializes a <see cref="TransformationMatrix{T}"/> to the identify matrix.
	/// </summary>
	public TransformationMatrix()
	{
		Row1 = Vector<T>.ToPositiveX;
		Row2 = Vector<T>.ToPositiveY;
		Row3 = Vector<T>.Origin;
	}

	public readonly Vector3D<T> FullRow1 => new(Row1, T.Zero);
	public readonly Vector3D<T> FullRow2 => new(Row2, T.Zero);
	public readonly Vector3D<T> FullRow3 => new(Row3, T.One);

	public readonly T Determinant => Vector<T>.Determinant(Row1, Row2);

	public static TransformationMatrix<T> Identity => new();

	public static TransformationMatrix<T> Zero
	{
		get
		{
			TransformationMatrix<T> result;
			result.Row1 = Vector<T>.Origin;
			result.Row2 = Vector<T>.Origin;
			result.Row3 = Vector<T>.Origin;
			return result;
		}
	}

	/// <summary>
	/// Gets a transformation matrix for swizzling a 2D vector (swapping the X and Y components; reflecting it across the line y=x).
	/// </summary>
	public static TransformationMatrix<T> GetSwizzle()
	{
		TransformationMatrix<T> result;
		result.Row1 = Vector<T>.ToPositiveY;
		result.Row2 = Vector<T>.ToPositiveX;
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Gets a transformation matrix for rotating a 2D vector 90°.
	/// </summary>
	public static TransformationMatrix<T> GetPositiveRotation()
	{
		TransformationMatrix<T> result;
		result.Row1 = Vector<T>.ToNegativeY;
		result.Row2 = Vector<T>.ToPositiveX;
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Gets a transformation matrix for rotating a 2D vector -90°.
	/// </summary>
	public static TransformationMatrix<T> GetNegativeRotation()
	{
		TransformationMatrix<T> result;
		result.Row1 = Vector<T>.ToPositiveY;
		result.Row2 = Vector<T>.ToNegativeX;
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Gets a transformation matrix for negating a 2D vector (reflecting it across the origin).
	/// </summary>
	public static TransformationMatrix<T> GetNegation()
	{
		TransformationMatrix<T> result;
		result.Row1 = Vector<T>.ToNegativeX;
		result.Row2 = Vector<T>.ToNegativeY;
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Gets a transformation matrix for a translation.
	/// </summary>
	/// <param name="amount">The translation vector.</param>
	public static TransformationMatrix<T> GetTranslation(Vector<T> amount)
	{
		TransformationMatrix<T> result;
		result.Row1 = Vector<T>.ToPositiveX;
		result.Row2 = Vector<T>.ToPositiveY;
		result.Row3 = amount;
		return result;
	}

	/// <summary>
	/// Gets a transformation matrix for a translation in the X-direction.
	/// </summary>
	/// <param name="amount">The translation amount in the X-direction.</param>
	public static TransformationMatrix<T> GetTranslationX(T amount)
	{
		TransformationMatrix<T> result;
		result.Row1 = Vector<T>.ToPositiveX;
		result.Row2 = Vector<T>.ToPositiveY;
		result.Row3 = new Vector<T>(amount, T.Zero);
		return result;
	}

	/// <summary>
	/// Gets a transformation matrix for a translation in the Y-direction.
	/// </summary>
	/// <param name="amount">The translation amount in the Y-direction.</param>
	public static TransformationMatrix<T> GetTranslationY(T amount)
	{
		TransformationMatrix<T> result;
		result.Row1 = Vector<T>.ToPositiveX;
		result.Row2 = Vector<T>.ToPositiveY;
		result.Row3 = new Vector<T>(T.Zero, amount);
		return result;
	}

	/// <summary>
	/// Gets a transformation matrix for scaling.
	/// </summary>
	/// <param name="amount">The scalar.</param>
	public static TransformationMatrix<T> GetScaling(T amount)
	{
		TransformationMatrix<T> result;
		result.Row1 = new(amount, T.Zero);
		result.Row2 = new(T.Zero, amount);
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Gets a translation matrix for stretching and scaling.
	/// </summary>
	/// <param name="amount">The amount to scale in each component direction.</param>
	public static TransformationMatrix<T> GetScaling(Vector<T> amount)
	{
		TransformationMatrix<T> result;
		result.Row1 = new(amount.X, T.Zero);
		result.Row2 = new(T.Zero, amount.Y);
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Gets a translation matrix for stretching in the X-direction.
	/// </summary>
	/// <param name="amount">The amount to stretch in the X-direction.</param>
	public static TransformationMatrix<T> GetScalingX(T amount)
	{
		TransformationMatrix<T> result;
		result.Row1 = new(amount, T.Zero);
		result.Row2 = new(T.Zero, T.One);
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Gets a translation matrix for stretching in the Y-direction.
	/// </summary>
	/// <param name="amount">The amount to stretch in the Y-direction.</param>
	public static TransformationMatrix<T> GetScalingY(T amount)
	{
		TransformationMatrix<T> result;
		result.Row1 = new(T.One, T.Zero);
		result.Row2 = new(T.Zero, amount);
		result.Row3 = Vector<T>.Origin;
		return result;
	}

	/// <summary>
	/// Multiplies the current matrix by a swizzle matrix (see <see cref="GetSwizzle()"/>).
	/// </summary>
	public void Swizzle()
	{
		//this *= GetSwizzle();

		(Row1, Row2) = (Row2, Row1);
	}

	/// <summary>
	/// Multiplies the current matrix by a translation matrix (see <see cref="GetTranslation(Vector{T})"/>).
	/// </summary>
	/// <param name="amount">The translation vector.</param>
	public void Translate(Vector<T> amount)
	{
		//this *= GetTranslation(translation);

		Row3 += amount;
	}

	/// <summary>
	/// Multiplies the current matrix by a translation matrix (see <see cref="GetTranslationX(T)"/>).
	/// </summary>
	/// <param name="amount">The translation amount in the X-direction.</param>
	public void TranslateX(T amount)
	{
		//this *= GetTranslationX(amount);

		Row3.X += amount;
	}

	/// <summary>
	/// Multiplies the current matrix by a translation matrix (see <see cref="GetTranslationY(T)"/>).
	/// </summary>
	/// <param name="amount">The translation amount in the Y-direction.</param>
	public void TranslateY(T amount)
	{
		//this *= GetTranslationY(amount);

		Row3.Y += amount;
	}

	/// <summary>
	/// Multiplies the current matrix by a scaling matrix (see <see cref="GetScaling(T)"/>).
	/// </summary>
	/// <param name="amount">The scalar.</param>
	public void Scale(T amount)
	{
		//this *= GetScaling(amount);

		Row1 *= amount;
		Row2 *= amount;
		Row3 *= amount;
	}

	/// <summary>
	/// Multiplies the current matrix by a scaling and stretching matrix (see <see cref="GetScaling(Vector{T})"/>).
	/// </summary>
	/// <param name="amount">The amount to scale in each component direction.</param>
	public void Scale(Vector<T> amount)
	{
		//this *= GetScaling(amount);

		Row1 = new Vector<T>(Row1.X * amount.X, Row1.Y * amount.Y);
		Row2 = new Vector<T>(Row2.X * amount.X, Row2.Y * amount.Y);
		Row3 = new Vector<T>(Row3.X * amount.X, Row3.Y * amount.Y);
	}

	/// <summary>
	/// Multiplies the current matrix by a stretching matrix (see <see cref="GetScalingX(T)"/>).
	/// </summary>
	/// <param name="amount">The amount to stretch in the X-direction.</param>
	public void ScaleX(T amount)
	{
		//this *= GetScalingX(amount);

		Row1.X *= amount;
		Row2.X *= amount;
		Row3.X *= amount;
	}

	/// <summary>
	/// Multiplies the current matrix by a stretching matrix (see <see cref="GetScalingY(T)"/>).
	/// </summary>
	/// <param name="amount">The amount to stretch in the Y-direction.</param>
	public void ScaleY(T amount)
	{
		//this *= GetScalingY(amount);

		Row1.Y *= amount;
		Row2.Y *= amount;
		Row3.Y *= amount;
	}

	static TransformationMatrix<T> IMultiplicativeIdentity<TransformationMatrix<T>, TransformationMatrix<T>>.MultiplicativeIdentity => Identity;

	public static TransformationMatrix<T> Inverse(TransformationMatrix<T> matrix)
	{
		T determinant = matrix.Determinant;

		TransformationMatrix<T> result;
		result.Row1 = new Vector<T>(matrix.Row2.Y, -matrix.Row1.Y) / determinant;
		result.Row2 = new Vector<T>(-matrix.Row2.X, matrix.Row1.X) / determinant;
		result.Row3 = new Vector<T>(
			Vector<T>.Determinant(matrix.Row2, matrix.Row3) / determinant,
			Vector<T>.Determinant(matrix.Row3, matrix.Row1) / determinant
		);

		return result;
	}

	public static TransformationMatrix<T> operator *(TransformationMatrix<T> left, TransformationMatrix<T> right)
	{
		TransformationMatrix<T> result;
		result.Row1 = left.Row1.X * right.Row1 + left.Row1.Y * right.Row2;
		result.Row2 = left.Row2.X * right.Row1 + left.Row2.Y * right.Row2;
		result.Row3 = left.Row3.X * right.Row1 + left.Row3.Y * right.Row2 + right.Row3;
		return result;
	}

	public static TransformationMatrix<T> operator /(TransformationMatrix<T> left, TransformationMatrix<T> right)
	{
		return left * Inverse(right);
	}

	public readonly Vector<T> Transform(Vector<T> vector)
	{
		return new Vector<T>(
			Vector<T>.Dot(Row1, vector) + Row3.X,
			Vector<T>.Dot(Row2, vector) + Row3.Y
		);
	}

	public static bool operator ==(TransformationMatrix<T> left, TransformationMatrix<T> right)
	{
		return left.Row1 == right.Row1
			&& left.Row2 == right.Row2
			&& left.Row3 == right.Row3;
	}

	public static bool operator !=(TransformationMatrix<T> left, TransformationMatrix<T> right) => !(left == right);

	public readonly bool Equals(TransformationMatrix<T> other) => this == other;

	public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is TransformationMatrix<T> matrix && Equals(matrix);

	public override readonly int GetHashCode() => HashCode.Combine(Row1, Row2, Row3);
}
