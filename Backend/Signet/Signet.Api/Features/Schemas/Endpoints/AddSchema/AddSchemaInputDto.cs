namespace Signet.Api.Features.Schemas.Endpoints.AddSchema
{
    public sealed record AddSchemaInputDto(string Name, string Version, string? Description, string? Id, string? ChangeLog, string SchemaDefinition);
}
