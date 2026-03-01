using Signet.Api.Common.Entities;

namespace Signet.Api.Domain.Repositories;

public interface IRepository<T> where T : AggregateRoot
{
    Task CreateAsync(T aggregateRoot, CancellationToken cancellationToken = default);

    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(T aggregateRoot, CancellationToken cancellationToken = default);
}
