using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2022.Day25;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		long total = 0;
		foreach (string line in inputReader.ReadLines())
		{
			long num = 0;
			long multiplier = 1;
			for (int i = line.Length - 1; i >= 0; i--)
			{
				num += line[i] switch
				{
					'=' => -2,
					'-' => -1,
					'0' => 0,
					'1' => 1,
					'2' => 2,
					_ => throw new InvalidDataException($"Unexpected character {line[i]}")
				} * multiplier;
				multiplier *= 5;
			}
			Debug.Assert(num >= 0);
			total += num;
		}
		Debug.Assert(total >= 0);
		Console.WriteLine($"Total: {total}");

		// Determine the length of the number; this may be a slight overestimation
		int length = 1;
		long power = 1;
		while (true)
		{
			if (power * 2 >= total) break;
			power *= 5;
			length++;
		}

		char[] result = new char[length];
		int charI = length - 1;
		while (total != 0)
		{
			total = Math.DivRem(total, 5, out long digit);
			if (digit == 3)
			{
				result[charI] = '=';
				total++;
			}
			else if (digit == 4)
			{
				result[charI] = '-';
				total++;
			}
			else
			{
				result[charI] = (char)('0' + digit);
			}
			charI--;
		}

		ReadOnlySpan<char> resultSpan = result;
		if (resultSpan[0] is '\0' or '0') resultSpan = resultSpan[1..];
		return new string(resultSpan);

		/*
		char[] result = Convert.ToString(total, 5).ToCharArray();
		for (int i = result.Length - 1; i >= 0; i--)
		{
			if (result[i] == '3') result[i] = '=';
			else if (result[i] == '4') result[i] = '-';
			else if (result[i] == '5') result[i] = '0';
			else continue;

			if (i != 0)
			{
				result[i]++;
			}
			else
			{
				char[] newResult = new char[result.Length + 1];
				Array.Copy(result, 0, newResult, 1, result.Length);
				newResult[1] = '1';
			}
		}

		return new string(result);
		*/
	}
}
