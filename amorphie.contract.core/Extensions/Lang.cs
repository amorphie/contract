namespace amorphie.contract.core.Extensions;

public static class Lang
{
    private const string DefaultLang = "tr-TR";
    private const string UndefinedLang = "Undefined_Lang_Key";

    public static string L(this Dictionary<string, string> multilanguages, string langCode)
    {
        if (multilanguages is null || !multilanguages.Any())
            return UndefinedLang;

        if (String.IsNullOrEmpty(langCode))
            langCode = DefaultLang;

        string resultValue = String.Empty;

        if (!multilanguages.TryGetValue(langCode, out resultValue))
        {
            resultValue = UndefinedLang;
        }

        return resultValue;
    }
}