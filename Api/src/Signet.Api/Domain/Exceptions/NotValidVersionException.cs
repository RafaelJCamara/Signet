namespace Signet.Api.Domain.Exceptions;

public sealed class NotValidVersionException(string message) : Exception(message)
{
}
