namespace JochCool.AdventOfCode.Year2023.Day15;

public static class Part2
{
	static readonly char[] operations = ['=', '-'];

	public static string? Solve(TextReader inputReader)
	{
		string input = inputReader.ReadToEnd().Replace("\n", "");

		List<Lens>?[] boxes = new List<Lens>[256];

		int endI;
		for (int i = 0; i < input.Length; i = endI + 1)
		{
			endI = input.IndexOf(',', i);
			if (endI == -1) endI = input.Length;

			int operationI = input.IndexOfAny(operations, i, endI - i);
			if (operationI == -1) throw new FormatException("Missing operation.");

			ReadOnlyMemory<char> label = input.AsMemory(i, operationI - i);
			int boxNum = Part1.Hash(label.Span);
			List<Lens> box = boxes[boxNum] ??= [];

			Console.WriteLine($"{input.AsSpan(i, endI - i)} - {label} hash {boxNum}");

			switch (input[operationI])
			{
				case '-':
				{
					for (int lensI = 0; lensI < box.Count; lensI++)
					{
						if (box[lensI].LabelEquals(label))
						{
							box.RemoveAt(lensI);
							break;
						}
					}
					break;
				}

				case '=':
				{
					int focalLength = int.Parse(input.AsSpan(operationI + 1, endI - operationI - 1));

					bool found = false;
					for (int lensI = 0; lensI < box.Count; lensI++)
					{
						Lens lens = box[lensI];
						if (lens.LabelEquals(label))
						{
							lens.focalLength = focalLength;
							box[lensI] = lens;
							found = true;
							break;
						}
					}
					if (found) break;

					Lens newLens;
					newLens.label = label;
					newLens.focalLength = focalLength;
					box.Add(newLens);
					break;
				}

				default:
					throw new UnexpectedStateException();
			}
		}
		Console.WriteLine("----");

		int sum = 0;
		for (int i = 0; i < boxes.Length; i++)
		{
			List<Lens>? box = boxes[i];
			if (box is null) continue;
			for (int lensI = 0; lensI < box.Count; lensI++)
			{
				Lens lens = box[lensI];
				int power = (i + 1) * (lensI + 1) * lens.focalLength;
				Console.WriteLine($"{lens.label} - {i+1}*{lensI+1}*{lens.focalLength} = {power}");
				sum += power;
			}
		}

		return sum.ToString();
	}

	struct Lens
	{
		public ReadOnlyMemory<char> label;
		public int focalLength;

		public readonly bool LabelEquals(ReadOnlyMemory<char> other) => LabelEquals(other.Span);

		public readonly bool LabelEquals(ReadOnlySpan<char> other) => label.Span.SequenceEqual(other);
	}
}
