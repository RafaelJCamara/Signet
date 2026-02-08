namespace Signet.SchemaRegistry.Exceptions
{
    public sealed class SchemaNotValidException : Exception
    {
        public SchemaNotValidException() : base("The current content does not follow its provided schema.")
        {

        }
    }
}
