namespace JochCool.AdventOfCode.Year2022.Day21;

record MonkeyValue(BigInteger Value) : IMonkeyJob
{
	public BigInteger GetValue(Dictionary<string, IMonkeyJob> dict) => Value;

	public LinearExpression GetExpression(Dictionary<string, IMonkeyJob> dict)
	{
		return new LinearExpression() { Constant = Value };
	}
}
