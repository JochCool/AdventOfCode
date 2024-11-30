namespace JochCool.AdventOfCode.Year2022.Day05;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		IEnumerable<string> lines = inputReader.ReadLines();
		IEnumerator<string> it = lines.GetEnumerator();
		it.MoveNext();

		int numStacks = (it.Current.Length + 1) / 4;
		Crate?[] topCrates = new Crate?[numStacks];
		Crate?[] bottomCrates = new Crate?[numStacks];

		do
		{
			for (int i = 0; i < numStacks; i++)
			{
				char c = it.Current[i * 4 + 1];
				if (c == ' ') continue;

				Crate crate = new(c);
				if (topCrates[i] is null)
				{
					topCrates[i] = crate;
				}
				else
				{
					bottomCrates[i]!.Next = crate;
				}
				bottomCrates[i] = crate;
			}
			it.MoveNext();
		} while (!it.Current.StartsWith(" 1 ", StringComparison.Ordinal));

		it.MoveNext();

		while (it.MoveNext())
		{
			string line = it.Current;
			if (string.IsNullOrEmpty(line)) continue;
			int i = 5;
			int count = int.Parse(line[i..(i = line.IndexOf(' ', i))], CultureInfo.InvariantCulture);
			int from = int.Parse(line[(i += 6)..(i = line.IndexOf(' ', i))], CultureInfo.InvariantCulture) - 1;
			int to = int.Parse(line[(i + 4)..], CultureInfo.InvariantCulture) - 1;

			Crate? topCrate = topCrates[from];
			Crate? bottomCrate = topCrate;
			if (bottomCrate is null) throw new InvalidOperationException();
			while (count --> 1)
			{
				bottomCrate = bottomCrate.Next;
				if (bottomCrate is null) throw new InvalidOperationException();
			}

			topCrates[from] = bottomCrate.Next;
			bottomCrate.Next = topCrates[to];
			topCrates[to] = topCrate;
		}

		return string.Join(null, (object?[])topCrates);
	}
}
