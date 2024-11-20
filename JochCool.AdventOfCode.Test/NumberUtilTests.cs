using System.Numerics;

namespace JochCool.AdventOfCode.Test;

public static class NumberUtilTests
{
	[Theory]
	[InlineData(0, 0, 1)]
	[InlineData(0, 5, 1)]
	[InlineData(0, -5, 1)]
	[InlineData(0, 0, 2)]
	[InlineData(1, 1, 2)]
	[InlineData(0, 2, 2)]
	[InlineData(1, -1, 2)]
	[InlineData(0, -2, 2)]
	[InlineData(1, -3, 2)]
	[InlineData(0, 0, 3)]
	[InlineData(2, -1, 3)]
	[InlineData(0, -3, 3)]
	[InlineData(0, int.MaxValue, int.MaxValue)]
	[InlineData(1, 1 - int.MaxValue, int.MaxValue)]
	[InlineData(int.MaxValue - 1, int.MaxValue - 1, int.MaxValue)]
	[InlineData(int.MaxValue - 1, int.MinValue, int.MaxValue)]
	public static void TestProperModuloInt32(int expected, int left, int right)
	{
		Assert.Equal(expected, NumberUtil.ProperModulo(left, right));
	}

	[Fact]
	public static void TestSumEmptyInt32()
	{
		Assert.Equal(0, NumberUtil.Sum<int>());
	}

	[Fact]
	public static void TestSumSingleInt32()
	{
		Assert.Equal(-42, NumberUtil.Sum(-42));
	}

	[Fact]
	public static void TestSumDoubleInt32()
	{
		Assert.Equal(32, NumberUtil.Sum(-10, 42));
	}

	public static TheoryData<BigInteger, bool, BigInteger> GetSqrtBigIntegerArguments()
	{
		TheoryData<BigInteger, bool, BigInteger> arguments = [];
		arguments.Add(0, true, 0);
		arguments.Add(1, true, 1);
		arguments.Add(1, false, 2);
		arguments.Add(1, false, 3);
		arguments.Add(2, true, 4);
		arguments.Add(2, false, 5);
		arguments.Add(7, false, 63);
		arguments.Add(BigInteger.One << 32, true, BigInteger.One << 64);
		return arguments;
	}

	[Theory]
	[MemberData(nameof(GetSqrtBigIntegerArguments))]
	public static void TestSqrtBigInteger(BigInteger expectedValue, bool expectedToBeExact, BigInteger value)
	{
		Assert.Equal(expectedValue, NumberUtil.Sqrt(value, out bool isExact));
		Assert.Equal(expectedToBeExact, isExact);
	}

	[Theory]
	[InlineData(0, true, 0)]
	[InlineData(1, true, 1)]
	[InlineData(1, false, 2)]
	[InlineData(1, false, 3)]
	[InlineData(2, true, 4)]
	[InlineData(2, false, 5)]
	[InlineData(7, false, 63)]
	[InlineData(46340, false, int.MaxValue)]
	public static void TestSqrtInt32(int expectedValue, bool expectedToBeExact, int value)
	{
		Assert.Equal(expectedValue, NumberUtil.Sqrt(value, out bool isExact));
		Assert.Equal(expectedToBeExact, isExact);
	}

	[Theory]
	[InlineData(1, 1, 1)]
	[InlineData(1, 1, 2)]
	[InlineData(1, 3, 2)]
	[InlineData(2, 4, 6)]
	[InlineData(9, 18, 27)]
	public static void TestGdcInt32(int expected, int a, int b)
	{
		Assert.Equal(expected, NumberUtil.Gcd(a, b));
	}

	[Theory]
	[InlineData(1, 1, 1)]
	[InlineData(6, 2, 3)]
	[InlineData(5, 5, 5)]
	[InlineData(12, 4, 6)]
	public static void TestLcmInt32(int expected, int a, int b)
	{
		Assert.Equal(expected, NumberUtil.Lcm(a, b));
	}
}
