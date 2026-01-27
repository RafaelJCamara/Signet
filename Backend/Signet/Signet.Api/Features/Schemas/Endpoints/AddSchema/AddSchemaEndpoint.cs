using FastEndpoints;
using Signet.Api.Features.Common.UseCases;

namespace Signet.Api.Features.Schemas.Endpoints.AddSchema
{
    public sealed class AddSchemaEndpoint(IUseCaseVoid<AddSchemaInputDto> addSchemaUseCase) : Endpoint<AddSchemaInputDto>
    {
        public override void Configure()
        {
            Post("/api/schema");
        }

        public override async Task HandleAsync(AddSchemaInputDto request, CancellationToken cancellationToken)
        {
            await addSchemaUseCase.ExecuteAsync(request, cancellationToken);
        }
    }
}
