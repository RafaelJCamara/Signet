using Signet.Api.Common.UseCases;
using Signet.Api.Domain.Entities;
using Signet.Api.Domain.Repositories;
using Signet.Api.Features.Schemas.Endpoints.AddSchema;

namespace Signet.Api.Features.Schemas.UseCases.AddSchema;

public sealed class AddSchemaUseCase(IRepository<SchemaContainer> schemaRepository) : IUseCaseVoid<AddSchemaInputDto>
{
    public async ValueTask ExecuteAsync(AddSchemaInputDto input, CancellationToken cancellationToken = default)
    {
        var schemaContainer = await schemaRepository.GetByIdAsync(input.ContainerId);

        await schemaContainer.AddSchemaToContainerAsync(Guid.NewGuid(), input.Version, input.ChangeLog, input.SchemaDefinition);

        await schemaRepository.UpdateAsync(schemaContainer);
    }
}
