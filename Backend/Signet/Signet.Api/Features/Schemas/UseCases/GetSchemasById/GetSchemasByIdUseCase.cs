using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Schemas.Domain.Repositories;
using Signet.Api.Features.Schemas.Endpoints.GetSchemaById;

namespace Signet.Api.Features.Schemas.UseCases.GetSchemaById
{
    public class GetSchemasByIdUseCase(ISchemaRepository schemaRepository) : IUseCase<string, IEnumerable<GetSchemasByIdEndpointResponseDto>>
    {
        public async ValueTask<IEnumerable<GetSchemasByIdEndpointResponseDto>> ExecuteAsync(string input, CancellationToken cancellationToken = default)
        {
            var schemas = await schemaRepository.GetBySchemaIdAsync(input, cancellationToken);

            return schemas.Select(s => new GetSchemasByIdEndpointResponseDto
            {
                Name = s.Name,
                Version = s.Version.ToString(),
                Description = s.Description,
                ChangeLog = s.ChangeLog,
                JsonSchema = s.Definition.Content
            });
        }
    }
}
