namespace JochCool.AdventOfCode;

static class LinqExtensions
{
	public static int CountEqualTo<T>(this IEnumerable<T> source, T value) where T : IEqualityOperators<T, T, bool>
	{
		int sum = 0;
		foreach (var item in source)
		{
			if (item == value)
			{
				sum++;
			}
		}
		return sum;
	}
}
