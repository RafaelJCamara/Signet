using FastEndpoints;
using Signet.Api.Features.Common.UseCases;

namespace Signet.Api.Features.Schemas.Endpoints.GetAllSchemas
{
    public sealed class GetAllSchemasEndpoint(IUseCaseOutputOnly<IEnumerable<GetAllSchemasEndpointResponseDto>> useCase) : EndpointWithoutRequest<IEnumerable<GetAllSchemasEndpointResponseDto>>
    {
        public override void Configure()
        {
            Get("/api/schema");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            await Send.OkAsync(await useCase.ExecuteAsync(cancellationToken), cancellationToken);
        }
    }
}
