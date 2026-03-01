using Signet.Api.Common.Entities;

namespace Signet.Api.Domain.ValueObjects;

public sealed class CompatibilityLevel : ValueObject
{
    public string Level { get; private set; }

    private CompatibilityLevel() { }

    public static async Task<CompatibilityLevel> CreateCompatibilityLevelAsync(string compatibilityLevel)
    {
        var a = new CompatibilityLevel();

        //TODO: make proper mappings
        a.Level = compatibilityLevel;

        await a.CheckInvariantsAsync();

        return a; 
    }

    //TODO
    protected override ValueTask CheckInvariantsAsync() => ValueTask.CompletedTask;
}
