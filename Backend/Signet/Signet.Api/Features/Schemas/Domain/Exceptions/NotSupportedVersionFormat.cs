namespace Signet.Api.Features.Schemas.Domain.Exceptions
{
    public class NotSupportedVersionFormat(string currentVersion) : Exception($"Schema Version must follow Semantic Versioning. Example: 1.0.0. Current format: {currentVersion}.")
    {
    }
}
