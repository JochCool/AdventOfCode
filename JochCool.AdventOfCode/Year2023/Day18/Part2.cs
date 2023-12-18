using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.Versioning;

namespace JochCool.AdventOfCode.Year2023.Day18;

// TODO: refactor this whole file.
// It's a mess.
// But hey, at least it works...

public static class Part2
{
	// Setting this to true will make the program extemely slow, but you get a nice map!
	// Also this only works on Windows
	const bool drawMap = false;

	public static string? Solve(TextReader inputReader)
	{
		long result = 0;

		// 1. Parsing the input into one polygon

		Vector<long> pos = Vector<long>.Origin;

		PolygonVertex startVertex = new(pos);
		PolygonVertex prevVertex = startVertex;

		int leftTurnCount = 0;
		int rightTurnCount = 0;

		Vector<long> prevDirection = default;
		foreach (string line in inputReader.ReadLines())
		{
			int clrI = line.IndexOf('#') + 1;
			if (clrI == 0) throw new FormatException();
			if (line.Length - clrI < 6) throw new FormatException();

			Vector<long> direction = line[clrI + 5] switch
			{
				'0' => Vector<long>.ToPositiveX,
				'1' => Vector<long>.ToPositiveY,
				'2' => Vector<long>.ToNegativeX,
				'3' => Vector<long>.ToNegativeY,
				_ => throw new FormatException()
			};
			long amount = ParseUtil.ParseHexadecimal<long>(line.AsSpan(clrI, 5));
			result += amount;

			pos += direction * amount;

			PolygonVertex vertex = new(pos);
			prevVertex.Next = vertex;
			vertex.Previous = prevVertex;

			if (prevVertex.Previous is not null)
			{
				bool isLeftTurn;
				if (prevDirection.X == 0 && direction.X != 0)
				{
					isLeftTurn = long.IsPositive(prevDirection.Y) == long.IsPositive(direction.X);
				}
				else if (prevDirection.Y == 0 && direction.Y != 0)
				{
					isLeftTurn = long.IsPositive(prevDirection.X) != long.IsPositive(direction.Y);
				}
				else throw new UnexpectedStateException();

				//prevVertex.IsLeftTurn = isLeftTurn;

				if (isLeftTurn)
					leftTurnCount++;
				else
					rightTurnCount++;
			}

			prevDirection = direction;
			prevVertex = vertex;
		}

		if (prevVertex == startVertex || prevVertex.Position != startVertex.Position) throw new FormatException();
		prevVertex.Previous!.Next = startVertex;
		startVertex.Previous = prevVertex.Previous;

		Console.WriteLine($"Total perimeter: {result}");

		// Map for debugging
		Bitmap? bitmap = null;
		Graphics? graphics = null;
		int bitmapSaveI = 0;
		if (drawMap && OperatingSystem.IsWindowsVersionAtLeast(6, 1))
		{
			Console.WriteLine("Drawing map");
			bitmap = new(3000, 3000);
			graphics = Graphics.FromImage(bitmap);

			PolygonVertex? currentVertex = startVertex;
			do
			{
				PolygonVertex? nextVertex = currentVertex.Next;
				if (nextVertex is null) break;

				DrawLine(bitmap, graphics, currentVertex.Position, nextVertex.Position, true);

				currentVertex = nextVertex;
			}
			while (currentVertex != startVertex);

			bitmap.Save("day18_0.png");
			bitmapSaveI++;
		}

		// 2. Simplifying the polygon
		// This code keeps slicing off rectangles until the remaining polygon is one big rectangle.

		bool isLeft = leftTurnCount > rightTurnCount;

		PolygonVertex? vertexC = startVertex;
		while (true)
		{
			vertexC = vertexC.Next;
			Debug.Assert(vertexC is not null);
			Debug.Assert(vertexC.Next is not null);

			PolygonVertex? vertexB = vertexC.Previous;
			Debug.Assert(vertexB is not null);
			Debug.Assert(vertexB.Previous is not null);

			bool bIsConvex = vertexB.IsLeftTurn == isLeft;
			bool cIsConvex = vertexC.IsLeftTurn == isLeft;

			if (!bIsConvex || !cIsConvex)
			{
				continue;
			}

			PolygonVertex vertexA = vertexB.Previous;
			PolygonVertex vertexD = vertexC.Next;

			long edgeLength;
			long edgeCommonPos;
			long aDist, dDist;
			if (vertexB.Position.X == vertexC.Position.X)
			{
				edgeLength = vertexB.Position.Y - vertexC.Position.Y;
				edgeCommonPos = vertexB.Position.X;
				aDist = edgeCommonPos - vertexA.Position.X;
				dDist = edgeCommonPos - vertexD.Position.X;
			}
			else
			{
				Debug.Assert(vertexB.Position.Y == vertexC.Position.Y);
				edgeLength = vertexB.Position.X - vertexC.Position.X;
				edgeCommonPos = vertexB.Position.Y;
				aDist = edgeCommonPos - vertexA.Position.Y;
				dDist = edgeCommonPos - vertexD.Position.Y;
			}
			edgeLength = long.Abs(edgeLength);
			aDist = long.Abs(aDist);
			dDist = long.Abs(dDist);

			if (aDist == dDist)
			{
				if (vertexA.IsRectangle)
				{
					long rectangleSize = (aDist - 1) * (edgeLength - 1);
					return (result + rectangleSize).ToString();
				}
				if (vertexA.IsLeftTurn == isLeft) continue;
				if (vertexD.IsLeftTurn == isLeft) continue;

				if (vertexD.Next.AnyIntersectsLineSegment(vertexA.Position, vertexD.Position, vertexA.Previous))
					continue;

				if (bitmap is not null && graphics is not null && OperatingSystem.IsWindowsVersionAtLeast(6, 1))
				{
					DrawLine(bitmap, graphics, vertexA.Position, vertexD.Position);
					bitmap.Save($"day18_{bitmapSaveI++}.png");
				}

				// In this case, all four vertices can be deleted
				PolygonVertex vertexAPrev = vertexA.Previous!;
				PolygonVertex vertedDNext = vertexD.Next!;
				vertexAPrev.Next = vertedDNext;
				vertedDNext.Previous = vertexAPrev;

				result += aDist * (edgeLength - 1);
			}
			else if (aDist < dDist)
			{
				if (vertexA.IsLeftTurn == isLeft) continue;

				Vector<long> from = vertexA.Position;
				Vector<long> to;

				if (vertexA.Position.X == vertexB.Position.X)
				{
					to = new(vertexD.Position.X, vertexA.Position.Y);
				}
				else
				{
					Debug.Assert(vertexA.Position.Y == vertexB.Position.Y);
					to = new(vertexA.Position.X, vertexD.Position.Y);
				}

				if (vertexD.Next!.AnyIntersectsLineSegment(from, to, vertexA.Previous))
					continue;

				vertexA.Position = to;

				if (bitmap is not null && graphics is not null && OperatingSystem.IsWindowsVersionAtLeast(6, 1))
				{
					DrawLine(bitmap, graphics, from, to);
					bitmap.Save($"day18_{bitmapSaveI++}.png");
				}

				vertexA.Next = vertexD;
				vertexD.Previous = vertexA;

				result += aDist * (edgeLength - 1);
			}
			else // The code in the above and below blocks are basically the same
			{
				if (vertexD.IsLeftTurn == isLeft) continue;

				Vector<long> from = vertexD.Position;
				Vector<long> to;

				if (vertexD.Position.X == vertexC.Position.X)
				{
					to = new(vertexA.Position.X, vertexD.Position.Y);
				}
				else
				{
					Debug.Assert(vertexD.Position.Y == vertexC.Position.Y);
					to = new(vertexD.Position.X, vertexA.Position.Y);
				}

				if (vertexD.Next.AnyIntersectsLineSegment(from, to, vertexA.Previous!))
					continue;

				vertexD.Position = to;

				if (bitmap is not null && graphics is not null && OperatingSystem.IsWindowsVersionAtLeast(6, 1))
				{
					DrawLine(bitmap, graphics, from, to);
					bitmap.Save($"day18_{bitmapSaveI++}.png");
				}

				vertexA.Next = vertexD;
				vertexD.Previous = vertexA;

				result += dDist * (edgeLength - 1);
			}

			vertexC = vertexD;
		}
	}

	[SupportedOSPlatform("windows6.1")]
	private static void DrawLine(Bitmap bitmap, Graphics graphics, Vector<long> from, Vector<long> to, bool isOriginal = false)
	{
		Vector<long> centre = new(1500, 1500);
		const long scale = 10_000;

		Vector<long> startPos = from / scale + centre;
		Vector<long> endPos = to / scale + centre;
		if (startPos.X > endPos.X) startPos.X--;
		else if (startPos.X < endPos.X) startPos.X++;
		if (startPos.Y > endPos.Y) startPos.Y--;
		else if (startPos.Y < endPos.Y) startPos.Y++;

		Point endPoint = new Point((int)endPos.X, (int)endPos.Y);
		graphics.DrawLine(isOriginal ? Pens.Yellow : Pens.Red, new Point((int)startPos.X, (int)startPos.Y), endPoint);
		bitmap.SetPixel(endPoint.X, endPoint.Y, Color.Cyan);
	}
}

class PolygonVertex
{
	public Vector<long> Position { get; set; }

	public PolygonVertex? Next { get; set; }

	public PolygonVertex? Previous { get; set; }

	//public bool IsLeftTurn { get; set; }

	public PolygonVertex(Vector<long> position)
	{
		Position = position;
	}

	public Vector<long> Edge => Next is null ? Vector<long>.Origin : Next.Position - Position;

	[MemberNotNullWhen(true, nameof(Next))]
	public bool IsRectangle
	{
		get
		{
			PolygonVertex? vertex = Next;
			if (vertex is null) return false;
			vertex = vertex.Next;
			if (vertex is null) return false;
			vertex = vertex.Next;
			if (vertex is null) return false;
			vertex = vertex.Next;
			return vertex == this;
		}
	}

	//*
	[MemberNotNull(nameof(Next))]
	[MemberNotNull(nameof(Previous))]
	public bool IsLeftTurn
	{
		get
		{
			if (Next is null || Previous is null) throw new InvalidOperationException();
			Vector<long> direction = Edge;
			Vector<long> prevDirection = Previous.Edge;
			if (prevDirection.X == 0 && direction.X != 0)
			{
				return long.IsPositive(prevDirection.Y) == long.IsPositive(direction.X);
			}
			if (prevDirection.Y == 0 && direction.Y != 0)
			{
				return long.IsPositive(prevDirection.X) != long.IsPositive(direction.Y);
			}
			throw new NotImplementedException();
		}
	}
	//*/

	// These may be useful for debugging
	/*
	public bool IsCircular
	{
		get
		{
			PolygonVertex? vertex = Next;
			while (vertex is not null)
			{
				if (vertex == this) return true;
				vertex = vertex.Next;
			}
			return false;
		}
	}

	public int Size
	{
		get
		{
			PolygonVertex? vertex = Next;
			int size = 1;
			while (vertex is not null && vertex != this)
			{
				size++;
				vertex = vertex.Next;
			}
			return size;
		}
	}
	*/

	public bool AnyIntersectsLineSegment(Vector<long> start, Vector<long> end, PolygonVertex stopAt)
	{
		PolygonVertex currentVertex = this;
		while (true)
		{
			PolygonVertex? nextVertex = currentVertex.Next;
			if (nextVertex is null) return false;

			Vector<long> myStart = currentVertex.Position;
			Vector<long> myEnd = nextVertex.Position;

			if (LineSegmentsIntersect(myStart, myEnd, start, end))
				return true;

			currentVertex = nextVertex;
			if (currentVertex == stopAt) return false;
		}
	}

	private static bool LineSegmentsIntersect(Vector<long> aStart, Vector<long> aEnd, Vector<long> bStart, Vector<long> bEnd)
	{
		// I can guarantee there is a better way to do this
		// But I've used up all my thinking power for today
		if (aStart.X == aEnd.X)
		{
			if (bStart.X == bEnd.X)
			{
				if (aStart.X != bStart.X) return false;
				if (aStart.Y <= bEnd.Y && aEnd.Y >= bStart.Y) return true;
				if (bStart.Y <= aEnd.Y && bEnd.Y >= aStart.Y) return true;
				return false;
			}
			if (bStart.Y == bEnd.Y)
			{
				if (bStart.Y < long.Min(aStart.Y, aEnd.Y)) return false;
				if (bStart.Y > long.Max(aStart.Y, aEnd.Y)) return false;
				if (long.Min(bStart.X, bEnd.X) > aStart.X) return false;
				if (long.Max(bStart.X, bEnd.X) < aStart.X) return false;
				return true;
			}
			else throw new NotImplementedException();
		}
		else if (aStart.Y == aEnd.Y)
		{
			return LineSegmentsIntersect(
				Vector<long>.Swizzle(aStart),
				Vector<long>.Swizzle(aEnd),
				Vector<long>.Swizzle(bStart),
				Vector<long>.Swizzle(bEnd)
			);
		}
		else throw new NotImplementedException();
	}
}
