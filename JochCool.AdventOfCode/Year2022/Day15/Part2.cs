using System.Drawing;

namespace JochCool.AdventOfCode.Year2022.Day15;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		List<PolygonLine> polygons = [];

		foreach (string line in inputReader.ReadLines())
		{
			int i = "Sensor at x=".Length;
			int x = StringUtil.ParseAt<int>(line, ref i, ',');

			i += ", y=".Length;
			int y = StringUtil.ParseAt<int>(line, ref i, ':');

			Vector<int> sensorPos = new(x, y);

			i += ": closest beacon is at x=".Length;
			int beaconX = StringUtil.ParseAt<int>(line, ref i, ',');

			i += ", y=".Length;
			int beaconY = int.Parse(line.AsSpan(i));

			int radius = (sensorPos - new Vector<int>(beaconX, beaconY)).TaxicabMagnitude;
			TaxicabCircle sensor = new(sensorPos, radius);

			polygons.Add(PolygonLine.CreateCircle(sensor.Corners));
		}

		throw new NotImplementedException();
	}

	public static void GetImages(string filePath)
	{
		if (!OperatingSystem.IsWindows()) throw new NotSupportedException();

		const int puzzleSize = 4000000;
		const int shift = 11;
		const int imageSize = puzzleSize >> shift;
		const int offsetX = 0;
		const int offsetY = 0;

		Bitmap bitmap = new(imageSize, imageSize);
		Graphics graphics = Graphics.FromImage(bitmap);

		Random random = new Random();
		int count = 0;

		foreach (string line in File.ReadLines(filePath))
		{
			int i = "Sensor at x=".Length;
			int x = StringUtil.ParseAt<int>(line, ref i, ',');

			i += ", y=".Length;
			int y = StringUtil.ParseAt<int>(line, ref i, ':');

			Vector<int> sensorPos = new(x, y);

			i += ": closest beacon is at x=".Length;
			int beaconX = StringUtil.ParseAt<int>(line, ref i, ',');

			i += ", y=".Length;
			int beaconY = int.Parse(line.AsSpan(i));

			int radius = (sensorPos - new Vector<int>(beaconX, beaconY)).TaxicabMagnitude;
			TaxicabCircle sensor = new(sensorPos, radius);

			Point[] points = sensor.Corners.Select(vector => new Point((vector.X - offsetX) >> shift, (vector.Y - offsetY) >> shift)).ToArray();
			Console.WriteLine($"Points: {string.Join(';', points)}");
			Brush brush = new SolidBrush(Color.FromArgb(random.Next() | 0x7F000000));
			graphics.FillPolygon(brush, points);

			bitmap.Save($"{count++}.png");
		}
	}
}

/// <summary>
/// Represents a line in a polygon. Can also be used to represent the polygon it is part of as a whole.
/// </summary>
// each polygon should be circular
// and have an even number of points
// and have only lines at 45 degrees from the axes
// and always alternate between going southeast/northwest and going southwest/northeast
// and always have the interior to the right side of a line
class PolygonLine
{
	public Vector<int> StartPos { get; set; }

	public PolygonLine Next { get; set; }

#nullable disable
	private PolygonLine(Vector<int> position)
#nullable restore
	{
		StartPos = position;
	}

	public Vector<int> EndPos => Next.StartPos;

	public Vector<int> AsVector => EndPos - StartPos;

	// The value of this property is always the inverse for the next node
	// This is false when going (+, -) and (-, +), true when going (+, +) and (-, -)
	public bool Direction
	{
		get
		{
			if (Next is null) throw new InvalidOperationException();
			return (StartPos.X < Next.StartPos.X) == (StartPos.Y < Next.StartPos.Y);
		}
	}

	// note: this modifies both this and other
	// returns if a join has happened
	internal bool TryJoinWith(PolygonLine other)
	{
		bool direction = Direction;
		if (other.Direction == direction) other = other.Next; // directions must be different, because parallel lines don't intersect (TODO: actually they could)

		bool result = false;

		PolygonLine bLine = other;
		do
		{
			PolygonLine aLine = this;
			do
			{
				// Make everything relative to aLine.Position, so that a's line starts at the origin
				Vector<int> s2 = bLine.StartPos - aLine.StartPos;

				// Find X-coordinate of intersection (relative to aLine.Position)
				if (direction) s2.Y = -s2.Y;
				int doubleIntersectionX = s2.X + s2.Y;
				if (int.IsOddInteger(doubleIntersectionX))
				{
					// TODO
					throw new NotImplementedException();
				}
				int intersectionX = doubleIntersectionX / 2;

				if (intersectionX == 0 || intersectionX == s2.X)
				{
					throw new NotImplementedException();
				}

				// Check if it is between the start/end points
				Vector<int> aLineVector = aLine.AsVector;
				bool intersectsALine = intersectionX > int.Min(0, aLineVector.X) && intersectionX < int.Max(0, aLineVector.X);
				if (intersectsALine)
				{
					Vector<int> bLineVector = bLine.AsVector;
					int intersectionXFromS2 = intersectionX - s2.X;
					bool intersectsBLine = intersectionXFromS2 > int.Min(0, bLineVector.X) && intersectionXFromS2 < int.Min(0, bLineVector.X);
					if (intersectsBLine)
					{
						// Intersection found!
						Vector<int> intersectionPos = aLineVector / aLineVector.X * intersectionX + aLine.StartPos;

						PolygonLine from;
						PolygonLine to;
						if ((aLineVector.Y > 0) == (bLineVector.X < 0))
						{
							from = aLine;
							to = bLine;
						}
						else
						{
							from = bLine;
							to = aLine;
						}
						PolygonLine newNode = new(intersectionPos);
						from.Next = newNode;
						newNode.Next = to;
						result = true;
					}
				}

				aLine = aLine.Next.Next;
			} while (aLine != this);

			bLine = bLine.Next;
			direction = !direction;
		} while (bLine != other);

		return result;
	}

	internal static PolygonLine CreateCircle(Vector<int>[] points)
	{
		PolygonLine first = new(points[0]);
		PolygonLine last = first;
		for (int i = 1; i < points.Length; i++)
		{
			PolygonLine node = new(points[i]);
			last.Next = node;
			last = node;
		}
		last.Next = first;
		return first;
	}
}
