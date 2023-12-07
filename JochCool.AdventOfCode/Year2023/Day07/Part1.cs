using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode.Year2023.Day07;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		List<Line> lines = inputReader.ParseAllLines<Line>();

		foreach (Line line in lines)
		{
			Console.WriteLine(line.ToString());
		}

		lines.Sort();

		Console.WriteLine("-----");
		foreach (Line line in lines)
		{
			Console.WriteLine(line.ToString());
		}

		Console.WriteLine("-----");
		int total = 0;
		for (int i = 0; i < lines.Count; i++)
		{
			int worth = (i + 1) * lines[i].Bid;
			Console.WriteLine(worth);
			total += worth;
		}

		return total.ToString();
	}
}

readonly struct Line : IParsable<Line>, IComparable<Line>
{
	const int cardTypeCount = 13;
	const int cardCount = 5;
	const int bitShift = 4;

	readonly int cards;
	readonly int bid;

	private Line(int cards, int bid)
	{
		this.cards = cards;
		this.bid = bid;
	}

	public int Bid => bid;

	public static Line Parse(string s)
	{
		if (s[cardCount] != ' ') throw new FormatException();

		int cards = 0;
		for (int i = 0; i < cardCount; i++)
		{
			cards <<= bitShift;
			char c = s[i];
			int cardType = c switch
			{
				'A' => 12,
				'K' => 11,
				'Q' => 10,
				'J' => 9,
				'T' => 8,
				>= '2' and <= '9' => c - '2',
				_ => throw new FormatException()
			};
			cards |= cardType;
		}

		return new Line(cards, int.Parse(s.AsSpan(cardCount + 1)));
	}

	public static Line Parse(string s, IFormatProvider? provider) => Parse(s);

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Line result)
	{
		throw new NotImplementedException();
	}

	public int GetCardTypeAt(int index)
	{
		if ((uint)index >= cardCount) throw new ArgumentOutOfRangeException(nameof(index));
		int shift = (cardCount - 1 - index) * bitShift;
		return (cards >> shift) & ((1 << bitShift) - 1);
	}

	public HandType Type
	{
		get
		{
			Span<int> countByCardType = stackalloc int[cardTypeCount];
			for (int i = 0; i < cardCount; i++)
			{
				int cardType = GetCardTypeAt(i);
				countByCardType[cardType]++;
			}
			int highest = 0;
			int secondHighest = 0;
			for (int cardType = 0;  cardType < cardTypeCount; cardType++)
			{
				int count = countByCardType[cardType];
				if (count > highest)
				{
					secondHighest = highest;
					highest = count;
				}
				else if (count > secondHighest)
				{
					secondHighest = count;
				}
			}
			if (highest == 5) return HandType.FiveOfAKind;
			if (highest == 4) return HandType.FourOfAKind;
			if (highest == 3)
			{
				if (secondHighest == 2) return HandType.FullHouse;
				return HandType.ThreeOfAKind;
			}
			if (highest == 2)
			{
				if (secondHighest == 2) return HandType.TwoPair;
				return HandType.OnePair;
			}
			return HandType.HighCard;
		}
	}

	// sorts the least value first
	public int CompareTo(Line other)
	{
		int comparison = (int)Type - (int)other.Type;
		if (comparison != 0) return comparison;

		return cards - other.cards;
	}

	public override string ToString()
	{
		StringBuilder sb = new();
		for (int i = 0; i < cardCount;  i++)
		{
			int cardType = GetCardTypeAt(i);
			sb.Append(cardType switch
			{
				12 => 'A',
				11 => 'K',
				10 => 'Q',
				9 => 'J',
				8 => 'T',
				_ => (char)(cardType + '2')
			});
		}
		sb.Append(' ');
		sb.Append(bid);
		return sb.ToString();
	}
}

enum HandType
{
	HighCard,
	OnePair,
	TwoPair,
	ThreeOfAKind,
	FullHouse,
	FourOfAKind,
	FiveOfAKind
}
