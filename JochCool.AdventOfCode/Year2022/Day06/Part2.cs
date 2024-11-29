namespace JochCool.AdventOfCode.Year2022.Day06;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		// When I originally wrote this puzzle solution I used File.OpenRead and didn't bother parsing UTF-8.
		// Now that I've switched to using a TextReader as parameter everywhere, I should probably also update the implementation of this method (todo).
		Stream stream = ((StreamReader)inputReader).BaseStream;

		ShiftingArray<byte> prevChars = new(13);
		stream.ReadExactly(prevChars.Array, 0, 13);

		int toSkip = 0;
		int[] lastSeen = new int[byte.MaxValue + 1];
		for (int i = 0; i < prevChars.Array.Length; i++)
		{
			byte @char = prevChars.Array[i];
			if (lastSeen[@char] != 0 && lastSeen[@char] > toSkip)
			{
				toSkip = lastSeen[@char];
			}
			lastSeen[@char] = i + 1;
		}

		while (true)
		{
			byte @char = (byte)stream.ReadByte();
			int foundI = prevChars.LastIndexOf(@char);
			if (toSkip <= 0 && foundI == -1)
			{
				return stream.Position.ToInvariantString();
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
