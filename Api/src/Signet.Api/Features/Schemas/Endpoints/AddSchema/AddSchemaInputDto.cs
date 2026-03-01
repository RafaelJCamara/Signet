namespace Signet.Api.Features.Schemas.Endpoints.AddSchema;

public sealed record AddSchemaInputDto(Guid ContainerId, string Version, string? ChangeLog, string SchemaDefinition);
