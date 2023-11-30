namespace JochCool.AdventOfCode.Year2022.Day06;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		// When I originally wrote this puzzle solution I used File.OpenRead and didn't bother parsing UTF-8.
		// Now that I've switched to using a TextReader as parameter everywhere, I should probably also update the implementation of this method (todo).
		Stream stream = ((StreamReader)inputReader).BaseStream;

		ShiftingArray<byte> prevChars = new(3);
		stream.ReadExactly(prevChars.Array, 0, 3);

		int toSkip;
		if (prevChars[1] == prevChars[2])
		{
			toSkip = 2;
		}
		else if (prevChars[0] == prevChars[1] || prevChars[0] == prevChars[2])
		{
			toSkip = 1;
		}
		else
		{
			toSkip = 0;
		}

		while (true)
		{
			byte @char = (byte)stream.ReadByte();
			int foundI = prevChars.LastIndexOf(@char);
			if (toSkip <= 0 && foundI == -1)
			{
				return stream.Position.ToString();
			}
			if (foundI >= toSkip)
			{
				toSkip = foundI;
			}
			else toSkip--;

			prevChars.Add(@char);
		}
	}
}
