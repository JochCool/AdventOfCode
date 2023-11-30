using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;

namespace JochCool.AdventOfCode.Test;

class PuzzleDataAttribute : DataAttribute
{
	public string Answer { get; }

	public string InputName { get; }

	public PuzzleDataAttribute(string answer, string inputName)
	{
		Answer = answer;
		InputName = inputName;
	}

	public override IEnumerable<object[]> GetData(MethodInfo testMethod)
	{
		Stream? stream;
		try
		{
			// Gets the file in the Inputs/ folder, relative to the test class.
			Type testClass = testMethod.DeclaringType!;
			stream = testClass.Assembly.GetManifestResourceStream(testClass, $"Inputs{Type.Delimiter}{InputName}");
		}
		catch (FileNotFoundException)
		{
			Debug.WriteLine($"Input file {InputName} was not found.");
			return [];
		}
		if (stream is null)
		{
			Debug.WriteLine($"Input file {InputName} is null.");
			return [];
		}

		return [[Answer, new StreamReader(stream)]];
	}
}
