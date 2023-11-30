namespace JochCool.AdventOfCode.Year2022.Day10;

public static class Part2
{
	// Note: this does not actually return the answer that should be entered on the website. Rather, it returns the drawing that was made, which should then be read by a human to get the answer.
	public static string? Solve(TextReader inputReader)
	{
		StringBuilder screen = new();
		BigInteger x = 1;
		int cycle = 0;
		const int lineEnd = 40;

		foreach (string line in inputReader.ReadLines())
		{
			DrawPixel();

			if (line == "noop") continue;
			if (!line.StartsWith("addx "))
			{
				Console.WriteLine("Unknown instruction " + line);
				return null;
			}
			
			DrawPixel();
			x += BigInteger.Parse(line.AsSpan(5));
		}

		string result = screen.ToString();
		Console.WriteLine(result);
		return result;

		void DrawPixel()
		{
			char pixel = cycle >= x - 1 && cycle <= x + 1 ? '#' : '.';
			screen.Append(pixel);
			cycle++;
			if (cycle == lineEnd)
			{
				screen.Append('\n');
				cycle = 0;
			}
		}
	}
}
