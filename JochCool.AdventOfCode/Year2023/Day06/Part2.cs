namespace JochCool.AdventOfCode.Year2023.Day06;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		BigInteger totalTime = BigInteger.Parse(inputReader.ReadLineOrThrow()["Time:".Length..].Replace(" ", ""), CultureInfo.InvariantCulture);
		BigInteger distance = BigInteger.Parse(inputReader.ReadLineOrThrow()["Distance:".Length..].Replace(" ", ""), CultureInfo.InvariantCulture);

		BigInteger discriminant = totalTime * totalTime - 4 * distance;
		BigInteger root = NumberUtil.Sqrt(discriminant);

		// Can't reuse the solution of part1 because it relied on the precision of doubles to be good enough, which it isn't for inputs this large.
		// Luckily, the maths can actually be simplified:
		/*
		buttonTimeHigh = (totalTime + root)/2
		buttonTimeLow = (totalTime - root)/2
		domain = buttonTimeHigh - buttonTimeLow = (totalTime + root)/2 - (totalTime - root)/2 = (root + root)/2 = root
		*/

		// However, the above formula assumes a continuous domain, but we're actually dealing with integers, so the actual domain might be up to 1 larger or smaller than the continuous one.
		// Luckily, we know the centre of the parabola: -(totalTime)/(2*(-1)) = totalTime/2
		// Since totalTime is an integer, the centre is either an integer, or exactly in between two integers, depending on the parity of totalTime.
		// In both cases, the amount of integers in the range to the left of the centre is exactly the same as the amount to the right of the centre, so the sum of them is even.
		// But, if the centre is exactly on an integer (so if totalTime is even), there will be that one integer extra, so it's actually odd.
		// Therefore, the parity of the result is the opposite of the parity of totalTime.

		// This code below ensures that the result has the expected parity.
		// Note that the edges don't actually count, so if the domain happens to actually be an exact integer, then it shouldn't actually count.

		BigInteger result = root;
		if (BigInteger.IsEvenInteger(totalTime) == BigInteger.IsEvenInteger(root))
		{
			bool rootIsExact = root * root == discriminant;
			result += rootIsExact ? -1 : 1;
		}

		return result.ToInvariantString();
	}
}
