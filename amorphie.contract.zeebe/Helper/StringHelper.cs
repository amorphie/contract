public static class StringHelper
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
}