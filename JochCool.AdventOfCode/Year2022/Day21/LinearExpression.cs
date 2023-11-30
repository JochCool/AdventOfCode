namespace JochCool.AdventOfCode.Year2022.Day21;

/// <summary>
/// Represents an expression containing a variable multiplied by a rational coefficient and added to a rational constant.
/// </summary>
class LinearExpression
{
	public Fraction Coefficient { get; set; }

	public Fraction Constant { get; set; }

	public bool HasCoefficient => !Fraction.IsZero(Coefficient);

	public void Add(LinearExpression expression)
	{
		Coefficient += expression.Coefficient;
		Constant += expression.Constant;
	}

	public void Add(Fraction value)
	{
		Constant += value;
	}

	public void Subtract(LinearExpression expression)
	{
		Coefficient -= expression.Coefficient;
		Constant -= expression.Constant;
	}

	public void Subtract(Fraction value)
	{
		Constant -= value;
	}

	public void SubtractFrom(Fraction value)
	{
		Coefficient = -Coefficient;
		Constant = value - Constant;
	}

	public void MultBy(LinearExpression expression)
	{
		if (HasCoefficient)
		{
			if (expression.HasCoefficient) throw new NotImplementedException();
			MultBy(expression.Constant);
		}
		else
		{
			Coefficient = expression.Coefficient * Constant;
			Constant *= expression.Constant;
		}
	}

	public void MultBy(Fraction amount)
	{
		Coefficient *= amount;
		Constant *= amount;
	}

	public void DivBy(LinearExpression expression)
	{
		if (expression.HasCoefficient) throw new NotImplementedException();
		DivBy(expression.Constant);
	}

	public void DivBy(Fraction divisor)
	{
		Coefficient /= divisor;
		Constant /= divisor;
	}

	public override string ToString()
	{
		if (!HasCoefficient) return Constant.ToString();
		return $"{Coefficient}*x+{Constant}";
	}
}
