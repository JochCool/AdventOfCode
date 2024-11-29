namespace JochCool.AdventOfCode.Year2023.Day06;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int[] times = inputReader.ReadLineOrThrow()["Time:".Length..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
		int[] distances = inputReader.ReadLineOrThrow()["Distance:".Length..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

		int result = 1;
		for (int i = 0; i < times.Length; i++)
		{
			int totalTime = times[i];
			int distance = distances[i];

			/*
			distance = buttonTime(totalTime - buttonTime) = totalTime * buttonTime - buttonTime^2
			0 = -buttonTime^2 + totalTime*buttonTime - distance
			buttonTime = ((-totalTime) +- sqrt(totalTime^2 - 4(-1)(-distance)))/(2(-1))
			buttonTime = (-totalTime +- sqrt(totalTime^2 - 4*distance))/(-2)
			buttonTime = (totalTime -+ sqrt(totalTime^2 - 4*distance))/2
			*/

			int discriminant = totalTime * totalTime - 4 * distance;
			double root = double.Sqrt(discriminant); // approximation should be good enough

			double buttonTimeHigh = (totalTime + root) / 2;
			double buttonTimeLow = (totalTime - root) / 2;
			int domain = (int)Math.Ceiling(buttonTimeHigh) - (int)buttonTimeLow - 1;
			Console.WriteLine(domain);
			result *= domain;
		}
		return result.ToInvariantString();
	}
}
