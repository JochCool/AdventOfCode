using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2022.Day15;

/// <summary>
/// Represents a circle in taxicab distance.
/// </summary>
public class TaxicabCircle
{
	public int Radius { get; }

	public Vector<int> Centre { get; }

	internal Vector<int>[] Corners { get; }

	public TaxicabCircle(Vector<int> centre, int radius)
	{
		Centre = centre;
		Radius = radius;

		Vector<int>[] directions = Vector<int>.AxisUnitVectors;
		Vector<int>[] corners = new Vector<int>[directions.Length];
		for (int i = 0; i < directions.Length; i++)
		{
			corners[i] = centre + directions[i] * radius;
		}
		Corners = corners;
	}

	public int SurfaceArea
	{
		get
		{
			int r = Radius;
			return 1 + 2 * (r * r + r); // 1 + 4 * triangle number
		}
	}

	public bool Contains(Vector<int> point) => (point - Centre).TaxicabMagnitude <= Radius;

	public bool Contains(TaxicabCircle other) => (other.Centre - Centre).TaxicabMagnitude + other.Radius <= Radius;

	public bool ContainsXCoord(int x) => int.Abs(Centre.X - x) <= Radius;

	public bool ContainsYCoord(int y) => int.Abs(Centre.Y - y) <= Radius;

	public int GetOverlapWith(TaxicabCircle other)
	{
		TaxicabCircle larger, smaller;
		if (other.Radius < Radius)
		{
			larger = this;
			smaller = other;
		}
		else
		{
			larger = other;
			smaller = this;
		}

		Vector<int>[] containedCorners = smaller.Corners.Where(larger.Contains).ToArray();
		switch (containedCorners.Length)
		{
			case 0:
			{
				return 0;
			}

			case 1:
			{
				IEnumerable<Vector<int>> myContainedCorners = larger.Corners.Where(smaller.Contains);
				Debug.Assert(myContainedCorners.Count() == 1);
				Vector<int> cornerDiff = containedCorners[0] - myContainedCorners.First();

				return int.Abs(cornerDiff.X * cornerDiff.X - cornerDiff.Y * cornerDiff.Y) / 2 + int.Max(int.Abs(cornerDiff.X), int.Abs(cornerDiff.Y)) + 1;
			}

			case 2:
			{
				int distance = (larger.Centre - smaller.Centre).TaxicabMagnitude;
				int overlapWidth = larger.Radius + smaller.Radius + 1 - distance;
				return smaller.Radius * overlapWidth + (overlapWidth+1)/2;
			}

			case 4:
			{
				return smaller.SurfaceArea;
			}

			default:
			{
				Debug.Fail($"It should not be possible for a total of {containedCorners.Length} corners to be contained in another circle in 2D.");
				throw new InvalidOperationException();
			}
		}
	}
}
