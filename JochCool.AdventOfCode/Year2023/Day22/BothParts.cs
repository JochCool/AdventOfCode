namespace JochCool.AdventOfCode.Year2023.Day22;

static class BothParts
{
	public static void LetBricksFall(List<Brick> bricks)
	{
		bricks.Sort();

		for (int i = 0; i < bricks.Count; i++)
		{
			Brick brick = bricks[i];
			Vector3D<int> start = brick.Start;
			if (start.Z == 1) continue;

			// These points define the cuboid below the brick, through which the brick will fall
			Vector3D<int> targetPos = new(start.X, start.Y, 0); // Z is one lower here
			Vector3D<int> belowEndPos = new(brick.End.X, brick.End.Y, start.Z - 1);

			List<Brick> supporters = [];
			for (int otherBrickI = 0; otherBrickI < i; otherBrickI++)
			{
				Brick otherBrick = bricks[otherBrickI];
				if (otherBrick.IntersectsCuboid(targetPos, belowEndPos))
				{
					if (targetPos.Z != otherBrick.End.Z)
					{
						supporters.Clear();
						targetPos.Z = otherBrick.End.Z;
					}
					supporters.Add(otherBrick);
				}
			}

			targetPos.Z++;
			brick.MoveTo(targetPos);
			brick.supporters = supporters;
			foreach (Brick otherBrick in supporters)
				otherBrick.beingSupported.Add(brick);
		}
	}
}
