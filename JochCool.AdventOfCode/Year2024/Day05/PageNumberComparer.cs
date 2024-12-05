namespace JochCool.AdventOfCode.Year2024.Day05;

class PageNumberComparer : IComparer<int>
{
	private readonly Dictionary<int, Page> pages = [];

	public void AddRule(int numBefore, int numAfter)
	{
		if (!pages.TryGetValue(numBefore, out Page? beforePage))
		{
			beforePage = new(numBefore);
			pages.Add(numBefore, beforePage);
		}
		if (!pages.TryGetValue(numAfter, out Page? afterPage))
		{
			afterPage = new(numAfter);
			pages.Add(numAfter, afterPage);
		}

		beforePage.After.Add(numAfter);
		afterPage.Before.Add(numBefore);

		//// Make sure it's transitive
		//beforePage.After.UnionWith(afterPage.After);
		//afterPage.Before.UnionWith(beforePage.Before);

		beforePage.ThrowIfInvalid();
		afterPage.ThrowIfInvalid();
	}

	public int Compare(int x, int y)
	{
		if (x == y) return 0;
		if (pages.TryGetValue(x, out Page? xPage))
		{
			if (xPage.Before.Contains(y)) return +1;
			if (xPage.After.Contains(y)) return -1;
		}
		if (pages.TryGetValue(y, out Page? yPage))
		{
			if (yPage.Before.Contains(x)) return -1;
			if (yPage.After.Contains(x)) return +1;
		}

		throw new ArgumentException($"No rules for sorting {x} and {y}.");
	}
}
