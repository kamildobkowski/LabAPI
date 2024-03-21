namespace LabAPI.Domain.Exceptions;

public sealed class UnauthorizedException : Exception
{
	public UnauthorizedException() : base()
	{ }
	public UnauthorizedException(string message) : base(message)
	{ }
}