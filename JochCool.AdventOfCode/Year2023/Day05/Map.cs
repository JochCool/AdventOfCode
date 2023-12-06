namespace JochCool.AdventOfCode.Year2023.Day05;

class Map
{
	readonly List<Line> lines = [];

	private Map()
	{
	}

	// mapping logic for part 1
	public long this[long value]
	{
		get
		{
			foreach (Line line in lines)
			{
				long mapped = value - line.sourceRangeStart;
				if ((ulong)mapped >= (ulong)line.rangeLength) continue;
				return line.destinationRangeStart + mapped;
			}
			return value;
		}
	}

	// mapping logic for part 2
	public BrokenNumberRange<long> this[BrokenNumberRange<long> values]
	{
		get
		{
			BrokenNumberRange<long> valuesClone = values.Clone();
			BrokenNumberRange<long> result = new();
			foreach (Line line in lines)
			{
				BrokenNumberRange<long> beingMapped = valuesClone.CreateSubset(line.sourceRangeStart, line.sourceRangeStart + line.rangeLength - 1);
				valuesClone.ExceptWith(beingMapped);
				beingMapped.Shift(line.destinationRangeStart - line.sourceRangeStart);
				result.UnionWith(beingMapped);
			}
			result.UnionWith(valuesClone);
			return result;
		}
	}

	public static Map Parse(TextReader inputReader)
	{
		Map map = new();
		string? line;
		while ((line = inputReader.ReadLine()) is not null && line.Length != 0)
		{
			map.lines.Add(Line.Parse(line));
		}
		return map;
	}

	struct Line
	{
		internal long destinationRangeStart;
		internal long sourceRangeStart;
		internal long rangeLength;

		public static Line Parse(string line)
		{
			Line result;

			int space1 = line.IndexOf(' ');
			if (space1 == -1) throw new FormatException();
			result.destinationRangeStart = long.Parse(line.AsSpan(0, space1));

			++space1;
			int space2 = line.IndexOf(' ', space1);
			if (space2 == -1) throw new FormatException();
			result.sourceRangeStart = long.Parse(line.AsSpan(space1, space2 - space1));

			++space2;
			result.rangeLength = long.Parse(line.AsSpan(space2));

			return result;
		}
	}
}
