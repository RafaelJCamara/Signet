namespace Signet.Api.Features.Validation.UseCases.ValidateSchema
{
    public sealed record ValidateSchemaUseCaseInputDto(Guid SchemaId, string SchemaToValidate);
}
