namespace JochCool.AdventOfCode.Year2020.Day04;

public static class Part2
{
	static readonly string[] requiredFields = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];

	public static string? Solve(TextReader inputReader)
	{
		string input = inputReader.ReadToEnd();

		int numValidPasswords = 0;
		int requiredFieldsLeft = requiredFields.Length;
		string? currentFieldName = null;
		int prevI = 0;
		for (int i = 0; i < input.Length;) // i++;
		{
			char c = input[i];
			switch (c)
			{
				case ':':
					currentFieldName = input[prevI..i];
					if (Enumerable.Contains(requiredFields, currentFieldName))
					{
						requiredFieldsLeft--;
					}
					i++;
					prevI = i;
					break;

				case ' ':
				case '\n':
					if (!IsValidInput(currentFieldName, input[prevI..i]))
					{
						// Make sure this will never get valid anymore. I know that this is not the fastest way as we're still unnecessarily parsing the rest of the passport, but whatever
						requiredFieldsLeft = -1;
					}
					currentFieldName = null;

					i++;
					if (i == input.Length || c == '\n' && input[i] == '\n')
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
	}

	static bool IsValidInput(string fieldName, string fieldValue)
	{
		switch (fieldName)
		{
			case "byr":
				return IntInRange(fieldValue, 1920, 2002);

			case "iyr":
				return IntInRange(fieldValue, 2010, 2020);

			case "eyr":
				return IntInRange(fieldValue, 2020, 2030);

			case "hgt":
				if (fieldValue.EndsWith("cm", StringComparison.Ordinal)) return IntInRange(fieldValue[..^2], 150, 193);
				if (fieldValue.EndsWith("in", StringComparison.Ordinal)) return IntInRange(fieldValue[..^2], 59, 76);
				return false;

			case "hcl":
				return colorRegex.IsMatch(fieldValue);

			case "ecl":
				return Enumerable.Contains(eyeColors, fieldValue);

			case "pid":
				return idRegex.IsMatch(fieldValue);

			default:
				return true;
		}
	}

	static bool IntInRange(string integer, int min, int max)
	{
		if (int.TryParse(integer, out int year))
		{
			return year >= min && year <= max;
		}
		return false;
	}

	static readonly Regex colorRegex = new Regex(@"^#[0-9a-f]{6}$");
	static readonly Regex idRegex = new Regex(@"^\d{9}$");

	static readonly string[] eyeColors = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];
}
