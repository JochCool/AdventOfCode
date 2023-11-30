namespace JochCool.AdventOfCode.Year2022.Day05;

class Crate
{
	public char Name { get; }

	public Crate? Next { get; set; }

	public Crate(char name)
	{
		Name = name;
	}

	public override string ToString()
	{
		return Name.ToString();
	}
}
