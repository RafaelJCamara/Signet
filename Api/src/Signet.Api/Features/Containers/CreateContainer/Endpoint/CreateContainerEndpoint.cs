using FastEndpoints;
using Signet.Api.Common.UseCases;

namespace Signet.Api.Features.Containers.CreateContainer.Endpoint;

public class CreateContainerEndpoint(IUseCaseVoid<CreateContainerDto> createContainerUseCase) : Endpoint<CreateContainerDto>
{
    public override void Configure()
    {
        Post("/api/containers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateContainerDto request, CancellationToken cancellationToken) 
        => await createContainerUseCase.ExecuteAsync(request, cancellationToken);
}
