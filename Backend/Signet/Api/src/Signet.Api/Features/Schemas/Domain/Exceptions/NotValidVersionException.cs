namespace Signet.Api.Features.Schemas.Domain.Exceptions
{
    public sealed class NotValidVersionException(string message) : Exception(message)
    {
    }
}
