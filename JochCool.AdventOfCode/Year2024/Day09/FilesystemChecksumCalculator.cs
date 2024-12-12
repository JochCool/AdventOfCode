namespace JochCool.AdventOfCode.Year2024.Day09;

public struct FilesystemChecksumCalculator
{
	BigInteger checksum;
	BigInteger currentBlockPosition;

	public void AddFile(int fileId, int fileLength)
	{
		/*
		char fileChar = fileId > 9 ? 'X' : (char)(fileId + '0');
		for (int i = 0; i < fileLength; i++)
		{
			Console.Write(fileChar);
		}
		//*/

		checksum += fileId * fileLength * (currentBlockPosition * 2 + fileLength - 1) / 2;
		currentBlockPosition += fileLength;
	}

	public void AddFreeSpace(int length)
	{
		/*
		for (int i = 0; i <  length; i++)
		{
			Console.Write('.');
		}
		//*/

		currentBlockPosition += length;
	}

	public readonly BigInteger Result => checksum;
}
