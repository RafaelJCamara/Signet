using Signet.Api.Features.Common.Entities;
using System.Linq.Expressions;

namespace Signet.Api.Features.Common.Domain.Repositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task CreateAsync(T aggregateRoot, CancellationToken cancellationToken = default);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
