namespace JochCool.AdventOfCode.Year2022.Day21;

class UnknownValueMonkey : IMonkeyJob
{
	public BigInteger GetValue(Dictionary<string, IMonkeyJob> dict)
	{
		throw new InvalidOperationException();
	}

	public LinearExpression GetExpression(Dictionary<string, IMonkeyJob> dict)
	{
		return new LinearExpression() { Coefficient = Fraction.One };
	}
}
