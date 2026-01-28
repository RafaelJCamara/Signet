using Signet.Api.Features.Common.Entities;

namespace Signet.Api.Features.Common.Domain.Repositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task CreateAsync(T aggregateRoot, CancellationToken cancellationToken = default);
    }
}
