using FastEndpoints;
using Signet.Api.Features.Common.UseCases;

namespace Signet.Api.Features.Validation.Endpoints.ValidateSchema
{
    public sealed class ValidateSchemaEndpoint(IUseCaseVoid<ValidateSchemaInputDto> validateSchemaUseCase) : Endpoint<ValidateSchemaInputDto>
    {
        public override void Configure()
        {
            Post("/api/schema/{SchemaId}/validate");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ValidateSchemaInputDto request, CancellationToken cancellationToken)
        {
            await validateSchemaUseCase.ExecuteAsync(request, cancellationToken);
        }
    }
}
