namespace JochCool.AdventOfCode.Year2022.Day13;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string? line1 = null;
		string? line2 = null;
		int i = 1;
		int result = 0;

		foreach (string line in inputReader.ReadLines())
		{
			if (line1 is null) line1 = line;
			else if (line2 is null) line2 = line;
			else
			{
				if (line.Length != 0)
				{
					Console.WriteLine("There's a pair of more than two lines");
					Console.WriteLine();
				}
				if (Compair(line1, line2))
				{
					Console.WriteLine($"Line {i} is in the right order.");
					Console.WriteLine();
					result += i;
				}
				else
				{
					Console.WriteLine(line1);
					Console.WriteLine(line2);
					Console.WriteLine();
				}
				i++;
				line1 = null;
				line2 = null;
			}
		}

		return result.ToString();
	}

	static bool Compair(string line1, string line2)
	{
		if (line1[0] != '[' || line2[0] != '[') Console.WriteLine("A line does not start with '['");

		int i1 = 1, i2 = 1;

		while (true)
		{
			// Skip the end of array chars
			while (true)
			{
				if (line1[i1] != ']')
				{
					if (line2[i2] == ']') return false;

					if (line1[i1] == ',') i1++;
					if (line2[i2] == ',') i2++;
					break;
				}
				if (line2[i2] != ']') return true;
				i1++;
				i2++;
			}

			int value1 = GetIntAt(line1, ref i1);
			int value2 = GetIntAt(line2, ref i2);
			bool value1IsArray = value1 == -1;
			bool value2IsArray = value2 == -1;

			if (value1IsArray ^ value2IsArray)
			{
				int depth = 0;

				if (value1IsArray)
				{
					while (true)
					{
						if (line1[i1] == ']') return true;
						value1 = GetIntAt(line1, ref i1);
						depth++;
						if (value1 != -1) break;
					}
				}
				else
				{
					while (true)
					{
						if (line2[i2] == ']') return false;
						value2 = GetIntAt(line2, ref i2);
						depth++;
						if (value2 != -1) break;
					}
				}

				if (value1 != value2)
				{
					return value1 < value2;
				}

				if (value1IsArray)
				{
					while (depth-- > 0)
					{
						if (line1[i1] != ']') return false;
						i1++;
					}
				}
				else
				{
					while (depth-- > 0)
					{
						if (line2[i2] != ']') return true;
						i2++;
					}
				}
			}

			if (value1 != value2)
			{
				return value1 < value2;
			}
		}
	}

	static int GetIntAt(string line, ref int i)
	{
		int startI = i;
		while (true)
		{
			if (line[i] is >= '0' and <= '9')
			{
				i++;
				if (i < line.Length) continue;
			}

			int length = i - startI;
			if (length == 0)
			{
				i++;
				return -1;
			}

			return int.Parse(line.AsSpan(startI, length));
		}
	}
}
