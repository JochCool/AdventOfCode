namespace JochCool.AdventOfCode.Year2024.Day09;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string? line = inputReader.ReadLine();
		if (string.IsNullOrEmpty(line)) return null;

		char[] diskMap = line.ToCharArray();

		int lastFileI = (diskMap.Length - 1) & ~1; // the last even index

		FilesystemChecksumCalculator calculator = new();
		int fileId = 0;
		for (int i = 0; i < line.Length; i++)
		{
			int fileLength = line[i] - '0';
			ThrowIfNotDigit(fileLength);

			if (diskMap[i] != '0')
			{
				calculator.AddFile(fileId, fileLength);
			}
			else
			{
				calculator.AddFreeSpace(fileLength);
			}

			fileId++;

			if (i == lastFileI) break;
			i++;

			int freeSpace = line[i] - '0';
			ThrowIfNotDigit(freeSpace);

			for (int candidateFileI = lastFileI; candidateFileI > i && freeSpace > 0; candidateFileI -= 2)
			{
				int candidateFileLength = diskMap[candidateFileI] - '0';
				if (candidateFileLength > freeSpace) continue;
				ThrowIfNotDigit(candidateFileLength);

				diskMap[candidateFileI] = '0';
				freeSpace -= candidateFileLength;
				calculator.AddFile(candidateFileI / 2, candidateFileLength);
			}

			calculator.AddFreeSpace(freeSpace);
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
