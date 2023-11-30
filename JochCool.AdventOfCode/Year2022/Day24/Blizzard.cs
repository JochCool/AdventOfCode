namespace JochCool.AdventOfCode.Year2022.Day24;

class Blizzard
{
	public Vector<int> Position { get; }

	public Vector<int> Direction { get; }

	public Blizzard(Vector<int> position, Vector<int> direction)
	{
		Position = position;
		Direction = direction;
	}

	public Blizzard Move(Vector<int> gridSize)
	{
		Vector<int> newPos = Position + Direction;
		
		if (newPos.X < 0) newPos.X += gridSize.X;
		else if (newPos.X >= gridSize.X) newPos.X -= gridSize.X;
		if (newPos.Y < 0) newPos.Y += gridSize.Y;
		else if (newPos.Y >= gridSize.Y) newPos.Y -= gridSize.Y;

		return new Blizzard(newPos, Direction);
	}
}
