namespace JochCool.AdventOfCode.Year2022.Day24;

class Minute
{
	readonly Blizzard[] blizzards;
	readonly Vector<int> gridSize;
	readonly bool[,] hasBlizzard;

	public int Number { get; }

	private Minute(Blizzard[] blizzards, Vector<int> gridSize, bool[,] hasBlizzard, int number)
	{
		this.blizzards = blizzards;
		this.gridSize = gridSize;
		this.hasBlizzard = hasBlizzard;
		Number = number;
	}

	public bool HasBlizzardAt(Vector<int> pos)
	{
		return hasBlizzard[pos.X, pos.Y];
	}

	public static Minute FromBlizzardsList(IList<Blizzard> blizzards, Vector<int> gridSize)
	{
		return FromBlizzardsList(blizzards, gridSize, 0);
	}

	private static Minute FromBlizzardsList(IList<Blizzard> blizzards, Vector<int> gridSize, int number)
	{
		Blizzard[] newBlizzards = new Blizzard[blizzards.Count];
		bool[,] hasBlizzard = new bool[gridSize.X, gridSize.Y];
		for (int i = 0; i < newBlizzards.Length; i++)
		{
			Blizzard newBlizzard = blizzards[i].Move(gridSize);
			hasBlizzard[newBlizzard.Position.X, newBlizzard.Position.Y] = true;
			newBlizzards[i] = newBlizzard;
		}
		return new Minute(newBlizzards, gridSize, hasBlizzard, number);
	}

	public Minute GetNext() => FromBlizzardsList(blizzards, gridSize, Number + 1);
}
