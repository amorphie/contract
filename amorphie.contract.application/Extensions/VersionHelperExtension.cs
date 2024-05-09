using System;
namespace amorphie.contract.application.Extensions
{
	public static class VersionHelperExtension
	{
        public static int CompareVersions(string version1, string version2)
        {
            string[] parts1 = version1.Split('.');
            string[] parts2 = version2.Split('.');
            int length = Math.Max(parts1.Length, parts2.Length);
            for (int i = 0; i < length; i++)
            {
                int num1 = (i < parts1.Length) ? int.Parse(parts1[i]) : 0;
                int num2 = (i < parts2.Length) ? int.Parse(parts2[i]) : 0;
                if (num1 < num2)
                {
                    return -1;
                }
                else if (num1 > num2)
                {
                    return 1;
                }
            }
            return 0;
        }

        public static string GetHighestVersion(string[] versions)
        {
            if (versions == null || versions.Length == 0)
            {
                throw new ArgumentException("Version list cannot be null or empty.");
            }

            string highestVersion = versions[0];
            foreach (var version in versions)
            {
                if (CompareVersions(version, highestVersion) > 0)
                {
                    highestVersion = version;
                }
            }

            return highestVersion;
        }
    }
}

