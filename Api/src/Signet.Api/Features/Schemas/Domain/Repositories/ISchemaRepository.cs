using Signet.Api.Features.Common.Domain.Entities;
using Signet.Api.Features.Common.Domain.Repositories;

namespace Signet.Api.Features.Schemas.Domain.Repositories
{
    public interface ISchemaRepository : IRepository<Schema>
    {
        Task<IEnumerable<Schema>> GetBySchemasIdAsync(string schemaId, string? version, CancellationToken cancellationToken = default);
    }
}
