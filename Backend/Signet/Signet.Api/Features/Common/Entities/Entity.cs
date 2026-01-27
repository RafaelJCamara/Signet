namespace Signet.Api.Features.Common.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        protected void SetId(Guid id)
        {
            Id = id;
        }
    }
}
