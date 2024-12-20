namespace JochCool.AdventOfCode.Year2022.Day15;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		const int targetY = 2000000;
		HashSet<int> beaconXs = [];

		//List<TaxicabCircle> sensors = new();
		BrokenIntegerRange<int> xs = new();

		foreach (string line in inputReader.ReadLines())
		{
			int i = "Sensor at x=".Length;
			int x = StringUtil.ParseInvariantAt<int>(line, ref i, ',');

			i += ", y=".Length;
			int y = StringUtil.ParseInvariantAt<int>(line, ref i, ':');

			Vector<int> sensorPos = new(x, y);

			i += ": closest beacon is at x=".Length;
			int beaconX = StringUtil.ParseInvariantAt<int>(line, ref i, ',');

			i += ", y=".Length;
			int beaconY = int.Parse(line.AsSpan(i), CultureInfo.InvariantCulture);

			if (beaconY == targetY) beaconXs.Add(beaconX);

			int radius = (sensorPos - new Vector<int>(beaconX, beaconY)).TaxicabMagnitude;

			int halfWidth = radius - int.Abs(sensorPos.Y - targetY);
			if (halfWidth >= 0) xs.AddRange(sensorPos.X - halfWidth, sensorPos.X + halfWidth);

			/*
			TaxicabCircle newSensor = new(sensorPos, radius);
			int area = newSensor.SurfaceArea;

			for (int sensorI = 0; sensorI < sensors.Count; sensorI++)
			{
				TaxicabCircle oldSensor = sensors[sensorI];
				//if (oldSensor.Contains(newSensor))
				//{
				//	goto Skip;
				//}
				//if (newSensor.Contains(oldSensor))
				//{
				//	sensors[sensorI] = newSensor; // TODO
				//}
				area -= newSensor.GetOverlapWith(oldSensor); // TODO: triple overlap?
			}

			totalArea += area;
			
			sensors.Add(newSensor);
		Skip:
			;
			*/
		}

		int size = xs.Count;
		int numbeacons = beaconXs.Count;
		Console.WriteLine($"{size} squares reached, {numbeacons} beacons are there.");
		return (size - numbeacons).ToInvariantString();
	}
}
