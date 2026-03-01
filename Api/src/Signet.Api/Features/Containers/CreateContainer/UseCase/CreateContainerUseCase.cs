using Signet.Api.Common.UseCases;
using Signet.Api.Domain.Entities;
using Signet.Api.Domain.Repositories;
using Signet.Api.Features.Containers.CreateContainer.Endpoint;

namespace Signet.Api.Features.Containers.CreateContainer.UseCase;

public sealed class CreateContainerUseCase(IRepository<SchemaContainer> repository) : IUseCaseVoid<CreateContainerDto>
{
    public async ValueTask ExecuteAsync(CreateContainerDto input, CancellationToken cancellationToken = default)
    {
        var container = await SchemaContainer.CreateSchemaContainerAsync(
            Guid.NewGuid(),
            input.Name,
            input.ContainerId,
            input.Description,
            input.CompatibilityLevel
        );

        await repository.CreateAsync(container);
    }
}
