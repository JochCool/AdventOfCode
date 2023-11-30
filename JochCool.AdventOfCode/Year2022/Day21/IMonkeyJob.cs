namespace JochCool.AdventOfCode.Year2022.Day21;

interface IMonkeyJob
{
	BigInteger GetValue(Dictionary<string, IMonkeyJob> dict);

	LinearExpression GetExpression(Dictionary<string, IMonkeyJob> dict);
}
