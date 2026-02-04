namespace Signet.Api.Features.Schemas.Domain.Exceptions
{
    public sealed class JsonSchemaNotValidException : Exception
    {
        public JsonSchemaNotValidException() : base("The current JSON Schema you are try to process is not valid. Possible reasons include wrong format.")
        {
            
        }
    }
}
