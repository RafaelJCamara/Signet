using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Schemas.Domain.Repositories;
using Signet.Api.Features.Schemas.Endpoints.GetAllSchemas;

namespace Signet.Api.Features.Schemas.UseCases.GetAllSchemas
{
    public sealed class GetAllSchemasUseCase(ISchemaRepository schemaRepository) : IUseCaseOutputOnly<IEnumerable<GetAllSchemasEndpointResponseDto>>
    {
        public async ValueTask<IEnumerable<GetAllSchemasEndpointResponseDto>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var allSchemas = await schemaRepository.GetAllAsync(cancellationToken);

            return allSchemas.Select(s => new GetAllSchemasEndpointResponseDto
            {
                Name = s.Name,
                Version = s.Version.ToString(),
                Description = s.Description,
                ChangeLog = s.ChangeLog,
                JsonSchema = s.Definition.Content,
                SchemaId = s.SchemaId
            });
        }
    }
}
