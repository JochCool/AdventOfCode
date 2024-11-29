using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode.Year2023.Day08;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string instructions = inputReader.ReadLine() ?? throw new FormatException("Input too short.");

		inputReader.ReadLine(); // empty line

		Dictionary<string, (string, string)> network = new();

		foreach (string line in inputReader.ReadLines())
		{
			network.Add(line[0..3], (line[7..10], line[12..15]));
		}

		string[] startNodes = network.Select(kvp => kvp.Key).Where(node => node[2] == 'A').ToArray();
		int[] stepsCounts = new int[startNodes.Length];
		for (int i = 0; i < startNodes.Length; i++)
		{
			int numSteps = 0;
			int instructionI = 0;

			string currentNode = startNodes[i];
			while (true)
			{
				(string, string) node = network[currentNode];
				currentNode = instructions[instructionI] == 'L' ? node.Item1 : node.Item2;
				numSteps++;

				if (currentNode[2] == 'Z')
				{
					stepsCounts[i] = numSteps;
					break;
				}

				instructionI++;
				if (instructionI >= instructions.Length)
					instructionI = 0;
			}
		}

		Console.WriteLine(string.Join(' ', stepsCounts));

		BigInteger result = stepsCounts[0];
		for (int i = 1; i < stepsCounts.Length; i++)
		{
			result = NumberUtil.Lcm(result, stepsCounts[i]);
		}
		return result.ToInvariantString();
	}
}
