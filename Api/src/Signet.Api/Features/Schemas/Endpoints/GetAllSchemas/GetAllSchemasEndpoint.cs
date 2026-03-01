using FastEndpoints;
using Signet.Api.Common.UseCases;

namespace Signet.Api.Features.Schemas.Endpoints.GetAllSchemas;

public sealed class GetAllSchemasEndpoint(IUseCase<GetAllSchemasEndpointRequestDto, IEnumerable<GetAllSchemasEndpointResponseDto>> useCase) : Endpoint<GetAllSchemasEndpointRequestDto, IEnumerable<GetAllSchemasEndpointResponseDto>>
{
    public override void Configure()
    {
        Get("/api/containers/{ContainerId}/schemas");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllSchemasEndpointRequestDto request, CancellationToken cancellationToken)
    {
        await Send.OkAsync(await useCase.ExecuteAsync(request, cancellationToken), cancellationToken);
    }
}
