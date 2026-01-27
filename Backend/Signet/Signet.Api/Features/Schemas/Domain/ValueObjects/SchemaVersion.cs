using Signet.Api.Features.Common.Entities;
using Signet.Api.Features.Schemas.Domain.Exceptions;

namespace Signet.Api.Features.Schemas.Domain.ValueObjects
{
    public sealed class SchemaVersion : ValueObject
    {
        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Patch { get; private set; }

        private SchemaVersion() { }

        public static SchemaVersion CreateVersion(string version)
        {
        
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentException("Version should have some content. It's coming as empty.");
            }

            var v = version.Trim();

            // allow a leading 'v' (e.g. v1.0.0)
            if (v.Length > 0 && (v[0] == 'v' || v[0] == 'V'))
            {
                v = v.Substring(1);
            }

            // strip pre-release or build metadata if present (e.g. 1.0.0-alpha+001)
            var idx = v.IndexOfAny(new[] { '-', '+' });
            if (idx >= 0)
            {
                v = v.Substring(0, idx);
            }

            var parts = v.Split('.');

            if (parts.Length != 3)
            {
                throw new NotSupportedVersionFormat(version);
            }

            if (!int.TryParse(parts[0].Trim(), out var major) ||
                !int.TryParse(parts[1].Trim(), out var minor) ||
                !int.TryParse(parts[2].Trim(), out var patch))
            {
                throw new NotSupportedVersionFormat(version);
            }

            return CreateVersion(major, minor, patch);
        }

        public static SchemaVersion CreateVersion(int major = 1, int minor = 0, int patch = 0)
        {
            var newVersion = new SchemaVersion
            {
                Major = major,
                Minor = minor,
                Patch = patch
            };

            newVersion.CheckInvariants();

            return newVersion;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }

        protected override void CheckInvariants()
        {
            // Reject negative components
            if (Major < 0 || Minor < 0 || Patch < 0)
            {
                throw new NotValidVersionException("Schema Version is not supported. Version components must be non-negative.");
            }

            // Reject the all-zero version (0.0.0)
            if (Major == 0 && Minor == 0 && Patch == 0)
            {
                throw new NotValidVersionException("Schema Version is not supported. Version cannot be 0.0.0.");
            }
        }
    }
}
