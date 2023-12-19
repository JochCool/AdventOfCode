namespace JochCool.AdventOfCode;

static class TextReaderExtensions
{
	public static string ReadLineOrThrow(this TextReader reader)
	{
		return reader.ReadLine() ?? throw new EndOfStreamException();
	}

	public static IEnumerable<string> ReadLines(this TextReader reader)
	{
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			yield return line;
		}
	}

	public static string[] ReadAllLines(this TextReader reader)
	{
		List<string> lines = [];
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			lines.Add(line);
		}
		return lines.ToArray();
	}

	// Yes I know it's inconsistent that this returns a list and the above method returns an array
	public static List<T> ParseAllLines<T>(this TextReader reader, Func<string, T> parser)
	{
		List<T> result = [];
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			result.Add(parser(line));
		}
		return result;
	}

	public static List<T> ParseAllLines<T>(this TextReader reader, IFormatProvider? formatProvider = null) where T : IParsable<T>
	{
		List<T> result = [];
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			result.Add(T.Parse(line, formatProvider));
		}
		return result;
	}

	// Also reads the empty line, but does not yield it
	public static IEnumerable<string> ReadLinesUntilEmpty(this TextReader reader)
	{
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			if (line.Length == 0) yield break;
			yield return line;
		}
	}

	public static IEnumerable<string[]> ReadLineGroups(this TextReader reader)
	{
		List<string> group = [];
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			if (line.Length == 0)
			{
				if (group.Count != 0)
					yield return group.ToArray();

				group.Clear();
				continue;
			}

			group.Add(line);
		}

		if (group.Count != 0)
			yield return group.ToArray();
	}
}
