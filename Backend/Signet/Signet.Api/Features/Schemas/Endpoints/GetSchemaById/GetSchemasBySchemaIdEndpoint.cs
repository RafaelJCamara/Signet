using FastEndpoints;
using Signet.Api.Features.Common.UseCases;

namespace Signet.Api.Features.Schemas.Endpoints.GetSchemaById
{
    public class GetSchemasBySchemaIdEndpoint(IUseCase<string, IEnumerable<GetSchemasByIdEndpointResponseDto>> useCase) : Endpoint<GetSchemasByIdEndpointRequestDto, IEnumerable<GetSchemasByIdEndpointResponseDto>>
    {
        public override void Configure()
        {
            Get("/api/schema/{SchemaId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetSchemasByIdEndpointRequestDto request, CancellationToken cancellationToken)
        {
            await Send.OkAsync(await useCase.ExecuteAsync(request.SchemaId, cancellationToken), cancellationToken);
        }
    }
}
