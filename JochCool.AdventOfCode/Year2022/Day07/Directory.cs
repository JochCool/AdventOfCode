using System.Diagnostics.CodeAnalysis;

namespace JochCool.AdventOfCode.Year2022.Day07;

class Directory
{
	const int smallDirSize = 100000;

	public Directory? Parent { get; }

	public int FileSize { get; set; }

	readonly Dictionary<string, Directory> subdirs = [];

	internal int sizeCache = -1;

	public Directory(Directory? parent, int fileSize = 0)
	{
		Parent = parent;
		FileSize = fileSize;
	}

	public bool TryGetSubdir(string name, [NotNullWhen(true)] out Directory? subdir)
	{
		return subdirs.TryGetValue(name, out subdir);
	}

	public Directory ToSubdir(string name)
	{
		if (subdirs.TryGetValue(name, out Directory? subdir))
		{
			return subdir;
		}
		subdir = new Directory(this);
		subdirs[name] = subdir;
		return subdir;
	}

	public void AddFile(int size)
	{
		FileSize += size;
	}

	public SizeReport GetSizeReport()
	{
		SizeReport result = new(0, 0);
		foreach (Directory subdir in subdirs.Values)
		{
			result += subdir.GetSizeReport();
		}
		result.TotalSize += FileSize;
		sizeCache = result.TotalSize;
		if (result.TotalSize <= smallDirSize)
		{
			result.TotalSmallDirSize += result.TotalSize;
		}
		return result;
	}

	public record struct SizeReport(int TotalSize, int TotalSmallDirSize)
	{
		public static SizeReport operator +(SizeReport a, SizeReport b)
		{
			return new SizeReport
			{
				TotalSize = a.TotalSize + b.TotalSize,
				TotalSmallDirSize = a.TotalSmallDirSize + b.TotalSmallDirSize
			};
		}
	}

	internal Directory GetSmallest(int minSize)
	{
		Directory result = this;
		foreach (Directory subdir in subdirs.Values)
		{
			Directory candidate = subdir.GetSmallest(minSize);
			if (candidate.sizeCache >= minSize && candidate.sizeCache <= result.sizeCache)
			{
				result = candidate;
			}
		}
		return result;
	}

	public static Directory ParseTree(IEnumerable<string> cmdlines)
	{
		Directory root = new(null);
		Directory currentDir = root;

		foreach (string line in cmdlines)
		{
			if (line[0] == '$')
			{
				if (line.Length <= 2) continue;
				int spaceI = line.IndexOf(' ', 2);
				if (spaceI == -1) spaceI = line.Length;
				ReadOnlySpan<char> command = line.AsSpan(2, spaceI - 2);
				switch (command)
				{
					case "cd":
					{
						if (++spaceI >= line.Length) continue;
						string value = line[spaceI..];
						switch (value)
						{
							case "/":
							{
								currentDir = root;
								break;
							}

							case "..":
							{
								currentDir = currentDir.Parent ?? throw new Exception();
								break;
							}

							default:
							{
								currentDir = currentDir.ToSubdir(value);
								break;
							}
						}
						continue;
					}

					case "ls":
					{
						continue;
					}

					default:
					{
						Console.WriteLine("Unknown command: ");
						Console.Out.WriteLine(command);
						continue;
					}
				}
			}

			if (line.StartsWith("dir ")) continue;

			int endOfNumber = line.IndexOf(' ');
			if (endOfNumber == -1) throw new Exception();
			int size = int.Parse(line.AsSpan(0, endOfNumber));
			currentDir.AddFile(size);
		}

		return root;
	}
}
