using System.Diagnostics.CodeAnalysis;
using amorphie.core.Zeebe.dapr;

namespace amorphie.contract.core.Extensions;

public static class Versioning
{
    public static string FindLargestVersion(string[] versions)
    {
        if (versions == null || versions.Length == 0)
        {
            return String.Empty;
        }

        Version largestVersion = null;

        foreach (string versionString in versions)
        {
            if (Version.TryParse(versionString, out Version version))
            {
                largestVersion = largestVersion == null || version > largestVersion ? version : largestVersion;
            }
            else
            {
                throw new ArgumentException($"Invalid version format: {versionString}");
            }
        }

        return largestVersion?.ToString() ?? String.Empty;
    }

    public static bool CompareVersion(string version1, string version2)
    {
        if (String.IsNullOrWhiteSpace(version1) || String.IsNullOrWhiteSpace(version2))
        {
            return false;
        }

        if (Version.TryParse(version1, out Version _version1) && Version.TryParse(version2, out Version _version2))
        {
            // largestVersion = largestVersion == null || version > largestVersion ? version : largestVersion;

            return _version1 >= _version2;
        }
        else
        {
            throw new ArgumentException($"Invalid version format: {_version1} {version2}");
        }
    }

}