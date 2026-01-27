namespace Signet.Api.Features.Common.Entities
{
    public abstract class AggregateRoot : Entity
    {
        protected abstract void CheckInvariants();
    }
}
