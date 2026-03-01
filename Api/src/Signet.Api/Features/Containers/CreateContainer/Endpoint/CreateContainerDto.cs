namespace Signet.Api.Features.Containers.CreateContainer.Endpoint;

public sealed record CreateContainerDto(string Name, string? ContainerId, string CompatibilityLevel, string? Description);
