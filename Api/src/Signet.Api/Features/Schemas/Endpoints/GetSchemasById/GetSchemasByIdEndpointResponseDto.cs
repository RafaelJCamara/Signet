namespace Signet.Api.Features.Schemas.Endpoints.GetSchemaById
{
    public sealed class GetSchemasByIdEndpointResponseDto
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string? Description { get; set; }

        public string? ChangeLog { get; set; }

        public string JsonSchema { get; set; }
    }
}
