namespace JochCool.AdventOfCode;

class UnexpectedStateException : InvalidOperationException
{
	public UnexpectedStateException()
	{
	}

	public UnexpectedStateException(string? message) : base(message)
	{
	}

	public UnexpectedStateException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
