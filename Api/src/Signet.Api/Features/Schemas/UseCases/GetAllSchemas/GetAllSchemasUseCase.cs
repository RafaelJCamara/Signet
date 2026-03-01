using Signet.Api.Common.UseCases;
using Signet.Api.Domain.Entities;
using Signet.Api.Domain.Repositories;
using Signet.Api.Features.Schemas.Endpoints.GetAllSchemas;

namespace Signet.Api.Features.Schemas.UseCases.GetAllSchemas;

public sealed class GetAllSchemasUseCase(IRepository<SchemaContainer> schemaContainerRepository) : IUseCase<GetAllSchemasEndpointRequestDto, IEnumerable<GetAllSchemasEndpointResponseDto>>
{
    public async ValueTask<IEnumerable<GetAllSchemasEndpointResponseDto>> ExecuteAsync(GetAllSchemasEndpointRequestDto input, CancellationToken cancellationToken = default)
    {
        var schemaContainer = await schemaContainerRepository.GetByIdAsync(input.ContainerId);

        return schemaContainer.Schemas.Select(
            schema => new GetAllSchemasEndpointResponseDto
            {
                SchemaId = schema.Id.ToString(),
                ChangeLog = schema.ChangeLog,
                Version = schema.Version.ToString(),
                JsonSchema = schema.Definition.Content
            }
        );
    }
}
