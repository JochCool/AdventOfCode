namespace JochCool.AdventOfCode.Year2022.Day13;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] lines = inputReader.ReadAllLines();

		Array.Sort(lines, Compair);

		foreach (string line in lines)
		{
			Console.WriteLine(line);
		}
		Console.WriteLine();

		int divider1Pos = ~Array.BinarySearch(lines, "[[2]]", new Compairer()) + 1;
		int divider2Pos = ~Array.BinarySearch(lines, "[[6]]", new Compairer()) + 2;

		Console.WriteLine("#1 at " + divider1Pos);
		Console.WriteLine("#2 at " + divider2Pos);

		int decorderKey = divider1Pos * divider2Pos;
		Console.WriteLine("Total: " + decorderKey);
		return decorderKey.ToString();
	}

	class Compairer : IComparer<string>
	{
		public int Compare(string? x, string? y)
		{
			return Compair(x, y);
		}
	}

	static int Compair(string line1, string line2)
	{
		if (line1.Length == 0) return line2.Length;
		if (line2.Length == 0) return -1;
		if (line1[0] != '[' || line2[0] != '[') Console.WriteLine("A line does not start with '['");

		int i1 = 1, i2 = 1;

		while (true)
		{
			// Skip the end of array chars
			while (true)
			{
				if (line1[i1] != ']')
				{
					if (line2[i2] == ']') return 1;

					if (line1[i1] == ',') i1++;
					if (line2[i2] == ',') i2++;
					break;
				}
				if (line2[i2] != ']') return -1;
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
						if (line1[i1] == ']') return -1;
						value1 = GetIntAt(line1, ref i1);
						depth++;
						if (value1 != -1) break;
					}
				}
				else
				{
					while (true)
					{
						if (line2[i2] == ']') return 1;
						value2 = GetIntAt(line2, ref i2);
						depth++;
						if (value2 != -1) break;
					}
				}

				if (value1 != value2)
				{
					return value1 - value2;
				}

				if (value1IsArray)
				{
					while (depth-- > 0)
					{
						if (line1[i1] != ']') return 1;
						i1++;
					}
				}
				else
				{
					while (depth-- > 0)
					{
						if (line2[i2] != ']') return -1;
						i2++;
					}
				}
			}

			if (value1 != value2)
			{
				return value1 - value2;
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
