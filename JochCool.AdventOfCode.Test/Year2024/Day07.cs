using JochCool.AdventOfCode.Year2024.Day07;
using System.Globalization;
using System.Numerics;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day07
{
	[Theory]
	[PuzzleData("3749", "day07_simple.txt")]
	[PuzzleData("1582598718861", "day07.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("11387", "day07_simple.txt")]
	[PuzzleData("165278151522644", "day07.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}

	public static TheoryData<BigInteger, BigInteger, BigInteger> GetTestConcatArguments()
	{
		TheoryData<BigInteger, BigInteger, BigInteger> arguments = [];
		arguments.Add(0, 0, 0);
		arguments.Add(2, 0, 2);
		arguments.Add(10, 1, 0);
		arguments.Add(12345, 12, 345);
		arguments.Add(1234567890123, 1234567, 890123);
		return arguments;
	}

	[Theory]
	[MemberData(nameof(GetTestConcatArguments))]
	public static void TestConcat(BigInteger expected, BigInteger left, BigInteger right)
	{
		Assert.Equal(expected, Part2.Concat(left, right));
	}
}
