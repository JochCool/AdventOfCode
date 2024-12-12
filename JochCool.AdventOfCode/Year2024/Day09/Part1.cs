namespace JochCool.AdventOfCode.Year2024.Day09;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string? line = inputReader.ReadLine();
		if (string.IsNullOrEmpty(line)) return null;

		char[] diskMap = line.ToCharArray();

		int lastFileI = (diskMap.Length - 1) & ~1; // the last even index

		FilesystemChecksumCalculator calculator = new();
		int fileId = 0;
		for (int i = 0; i < diskMap.Length; i++)
		{
			int fileLength = diskMap[i] - '0';
			ThrowIfNotDigit(fileLength);

			calculator.AddFile(fileId, fileLength);

			fileId++;

			if (i == lastFileI) break;
			i++;

			int freeSpace = diskMap[i] - '0';
			ThrowIfNotDigit(freeSpace);

			while (freeSpace != 0)
			{
				int lastFileId = lastFileI / 2;

				int lastFileLength = diskMap[lastFileI] - '0';
				ThrowIfNotDigit(lastFileLength);

				int takenSpace;
				if (lastFileLength > freeSpace)
				{
					diskMap[lastFileI] = (char)(lastFileLength - freeSpace + '0');
					takenSpace = freeSpace;
					freeSpace = 0;
				}
				else
				{
					lastFileI -= 2;
					takenSpace = lastFileLength;
					freeSpace -= lastFileLength;
				}

				calculator.AddFile(lastFileId, takenSpace);

				if (lastFileI < i) break;
			}
		}

		return calculator.Result.ToInvariantString();
	}

	private static void ThrowIfNotDigit(int number)
	{
		if ((uint)number >= 10)
		{
			throw new FormatException();
		}
	}
}
