using FastEndpoints;
using Signet.Api.Features.Common.UseCases;

namespace Signet.Api.Features.Validation.Endpoints.ValidateSchema
{
    public sealed class ValidateSchemaEndpoint(IUseCase<ValidateSchemaInputDto, bool> validateSchemaUseCase) : Endpoint<ValidateSchemaInputDto>
    {
        public override void Configure()
        {
            Post("/api/validations/schemas/{SchemaId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ValidateSchemaInputDto request, CancellationToken cancellationToken)
        {
            await Send.OkAsync(await validateSchemaUseCase.ExecuteAsync(request, cancellationToken), cancellationToken);
        }
    }
}
