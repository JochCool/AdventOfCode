namespace JochCool.AdventOfCode.Year2020.Day13;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		_ = inputReader.ReadLine();

		// How often it happens that the buses (so far) arrive in this way
		int frequency = 1;

		// The first timestamp on which the buses (so far) arrive this way
		int offset = 0;


		// Loop through the buses on the next line
		string buses = inputReader.ReadLine();
		int i = 0;
		int busStrStart = 0;
		int busStrEnd;
		while (true)
		{
			busStrEnd = buses.IndexOf(',', busStrStart);
			string currentBusStr = busStrEnd == -1 ? buses[busStrStart..] : buses[busStrStart..busStrEnd];
			if (currentBusStr == "x") goto Continue;

			int currentBusNum = int.Parse(currentBusStr, CultureInfo.InvariantCulture);
			int gdc = NumberUtil.Gcd(currentBusNum, frequency);

			_ = offset - i;

			frequency = frequency * currentBusNum / gdc;

		Continue:
			if (busStrEnd == -1) break;
			busStrStart = busStrEnd + 1;
			i++;
		}

		Console.WriteLine($"Result: ");

		throw new NotImplementedException();
	}
}

/*

   x     x     x     x     x     x     x     x
x        x        x        x        x


   x     x     x     x     x     x     x     x
x      x      x      x      x      x      x      x
  x  x  x  x  x  x  x  x  x  x  x  x  x  x  x  x  x

*/
