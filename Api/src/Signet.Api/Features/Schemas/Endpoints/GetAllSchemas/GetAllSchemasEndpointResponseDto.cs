namespace Signet.Api.Features.Schemas.Endpoints.GetAllSchemas;

public sealed class GetAllSchemasEndpointResponseDto
{
    public string SchemaId { get; set; }

    /// <summary>
    /// Semantic version string (e.g. 1.0.0)
    /// </summary>
    public string Version { get; set; }

    public string? ChangeLog { get; set; }

    /// <summary>
    /// The JSON schema content
    /// </summary>
    public string JsonSchema { get; set; }
}
