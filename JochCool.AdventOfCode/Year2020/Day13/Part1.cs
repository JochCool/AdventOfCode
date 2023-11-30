namespace JochCool.AdventOfCode.Year2020.Day13;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int earliestTime = int.Parse(inputReader.ReadLine());
		
		// Info about the closest bus so far
		int busNum = 0;
		int busTime = int.MaxValue; // wait time

		// Loop through the buses on the next line
		string buses = inputReader.ReadLine();
		int busStrStart = 0;
		int busStrEnd;
		while (true)
		{
			busStrEnd = buses.IndexOf(',', busStrStart);
			string currentBusStr = busStrEnd == -1 ? buses[busStrStart ..] : buses[busStrStart .. busStrEnd];
			if (currentBusStr == "x") goto Continue;

			int currentBusNum = int.Parse(currentBusStr);
			int currentBusTime = currentBusNum - earliestTime % currentBusNum;
			if (currentBusTime < busTime)
			{
				busNum = currentBusNum;
				busTime = currentBusTime;
			}

		Continue:
			if (busStrEnd == -1) break;
			busStrStart = busStrEnd + 1;
		}

		int product = busNum * busTime;
		Console.WriteLine($"Result: {busNum} * {busTime} = {product}");

		return product.ToString();
	}
}

/*

   x     x     x     x     x     x     x     x
x        x        x        x        x


   x     x     x     x     x     x     x     x
x      x      x      x      x      x      x      x
  x  x  x  x  x  x  x  x  x  x  x  x  x  x  x  x  x

*/
