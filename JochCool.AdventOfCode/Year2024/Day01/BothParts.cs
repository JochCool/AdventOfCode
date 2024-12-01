using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JochCool.AdventOfCode.Year2024.Day01;

static class BothParts
{
	public static (List<int> Left, List<int> Right) ReadLists(TextReader inputReader)
	{
		List<int> left = [];
		List<int> right = [];

		foreach (string line in inputReader.ReadLines())
		{
			int separatorI = line.IndexOf(' ');
			if (separatorI == -1) throw new FormatException();

			left.Add(int.Parse(line.AsSpan(0, separatorI), CultureInfo.InvariantCulture));
			right.Add(int.Parse(line.AsSpan(separatorI + 1), CultureInfo.InvariantCulture));
		}

		return (left, right);
	}
}
