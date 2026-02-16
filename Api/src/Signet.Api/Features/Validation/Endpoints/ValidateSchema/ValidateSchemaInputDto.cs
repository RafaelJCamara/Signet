namespace Signet.Api.Features.Validation.Endpoints.ValidateSchema
{
    public sealed record ValidateSchemaInputDto(Guid SchemaId, string ContentToValidate);
}
