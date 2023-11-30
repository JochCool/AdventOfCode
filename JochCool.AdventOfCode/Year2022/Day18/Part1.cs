namespace JochCool.AdventOfCode.Year2022.Day18;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		List<Vector3D<int>> cubes = [];
		int numAdjacent = 0;
		foreach (string line in inputReader.ReadLines())
		{
			int i = 0;
			int x = NumberUtil.ParseAt<int>(line, ref i, ',');
			i++;
			int y = NumberUtil.ParseAt<int>(line, ref i, ',');
			i++;
			int z = NumberUtil.ParseAt<int>(line, ref i, ',');
			Vector3D<int> cube = new(x, y, z);

			foreach (Vector3D<int> otherCube in cubes)
			{
				int totalDiff = int.Abs(cube.X - otherCube.X) + int.Abs(cube.Y - otherCube.Y) + int.Abs(cube.Z - otherCube.Z);
				if (totalDiff == 0) throw new UnexpectedStateException();
				if (totalDiff == 1)
				{
					numAdjacent++;
				}
			}
			cubes.Add(cube);
		}
		int result = cubes.Count * 6 - numAdjacent * 2;
		return result.ToString();
	}
}
