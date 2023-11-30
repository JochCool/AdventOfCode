namespace JochCool.AdventOfCode.Year2022.Day05;

public static class Part1
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
		} while (!it.Current.StartsWith(" 1 "));

		it.MoveNext();

		while (it.MoveNext())
		{
			string line = it.Current;
			if (string.IsNullOrEmpty(line)) continue;
			int i = 5;
			int count = int.Parse(line[i..(i = line.IndexOf(' ', i))]);
			int from = int.Parse(line[(i += 6)..(i = line.IndexOf(' ', i))]) - 1;
			int to = int.Parse(line[(i+4)..]) - 1;

			while (count --> 0)
			{
				Crate? moving = topCrates[from];
				if (moving is null) throw new InvalidOperationException();
				topCrates[from] = moving.Next;
				moving.Next = topCrates[to];
				topCrates[to] = moving;
			}
		}

		return string.Join(null, (object?[])topCrates);
	}
}
