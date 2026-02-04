using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Schemas.Domain.Entities;
using Signet.Api.Features.Schemas.Domain.Repositories;
using Signet.Api.Features.Schemas.Endpoints.AddSchema;

namespace Signet.Api.Features.Schemas.UseCases.AddSchema
{
    public sealed class AddSchemaUseCase(ISchemaRepository schemaRepository) : IUseCaseVoid<AddSchemaInputDto>
    {
        public async ValueTask ExecuteAsync(AddSchemaInputDto input, CancellationToken cancellationToken = default)
        {
            var newSchema = await Schema.CreateSchemaAsync(
                input.Name,
                input.Description,
                input.Id,
                input.Version,
                input.ChangeLog,
                input.SchemaDefinition
            );

            //TODO: check if schema with manual id exists, and only create if it doesn't

            await schemaRepository.CreateAsync(newSchema);
        }
    }
}
