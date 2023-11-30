namespace JochCool.AdventOfCode;

/// <summary>
/// Represents a method that solves an Advent of Code puzzle.
/// </summary>
/// <param name="input">A reader from which the puzzle input can be read.</param>
/// <returns>The text to be entered as the solution of the puzzle, or <see langword="null"/> if no solution was found.</returns>
public delegate string? Solution(TextReader input);
