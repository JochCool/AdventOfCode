using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode.Year2023.Day08;

public static class Part1
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

		int numSteps = 0;
		int instructionI = 0;
		string currentNode = "AAA";
		const string targetNode = "ZZZ";
		while (true)
		{
			(string, string) node = network[currentNode];
			currentNode = instructions[instructionI] == 'L' ? node.Item1 : node.Item2;
			numSteps++;

			if (currentNode == targetNode)
			{
				return numSteps.ToString();
			}

			instructionI++;
			if (instructionI >= instructions.Length)
				instructionI = 0;
		}
	}
}
