namespace Signet.Api.Common.Entities;

public abstract class ValueObject
{
    protected abstract ValueTask CheckInvariantsAsync();
}
