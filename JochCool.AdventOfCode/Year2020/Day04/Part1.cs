namespace JochCool.AdventOfCode.Year2020.Day04;

public static class Part1
{
	static readonly string[] requiredFields = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];

	public static string? Solve(TextReader inputReader)
	{
		string input = inputReader.ReadToEnd();

		int numValidPasswords = 0;
		int requiredFieldsLeft = requiredFields.Length;
		int prevI = 0;
		for (int i = 0; i < input.Length; ) // i++;
		{
			switch (input[i])
			{
				case ':':
					string fieldName = input[prevI..i];
					if (Enumerable.Contains(requiredFields, fieldName))
					{
						requiredFieldsLeft--;
					}
					i++;
					break;

				case ' ':
					i++;
					prevI = i;
					break;

				case '\n':
					i++;
					if (i == input.Length || input[i] == '\n')
					{
						if (requiredFieldsLeft == 0) numValidPasswords++;
						requiredFieldsLeft = requiredFields.Length;
						i++;
					}
					prevI = i;
					break;

				default:
					i++;
					break;
			}
		}

		return numValidPasswords.ToInvariantString();

		/*
		int i = 0;
		while (true)
		{
			char c = input[i];

			if (c == ':')
			{
				if (Enumerable.Contains(requiredFields, input.Substring(prevId, i - prevId)))
				{
					requiredFieldsLeft--;
				}
			}

			i++;
			if (i == input.Length) break;

			if (c == '\n')
			{
				if (input[i] == '\n')
				{
					if (requiredFieldsLeft == 0) numValidPasswords++;
					requiredFieldsLeft = requiredFields.Length;
					i++;
					if (i == input.Length) break;
				}
				prevId = i;
			}
			else if (c == ' ')
			{
				prevId = i;
			}
		}
		//*/
	}
}
