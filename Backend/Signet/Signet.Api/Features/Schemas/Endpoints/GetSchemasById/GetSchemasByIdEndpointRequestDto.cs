namespace Signet.Api.Features.Schemas.Endpoints.GetSchemaById
{
    public class GetSchemasByIdEndpointRequestDto
    {
        /// <summary>
        /// This schema id can represent either the actual db Id or the schema manual id.
        /// </summary>
        public string SchemaId { get; set; }
    }
}
