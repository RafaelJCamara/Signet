using Signet.Api.Features.Common.Domain.Repositories;
using Signet.Api.Features.Schemas.Domain.Entities;

namespace Signet.Api.Features.Schemas.Domain.Repositories
{
    public interface ISchemaRepository : IRepository<Schema>
    {
        Task<IEnumerable<Schema>> GetBySchemasIdAsync(string schemaId, string? version, CancellationToken cancellationToken = default);
    }
}
