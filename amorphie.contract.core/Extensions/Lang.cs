using amorphie.core.Base;

namespace amorphie.contract.core.Extensions;

public static class Lang
{
    public static string L(this Dictionary<string, string> multilanguages, string langCode)
    {
        if (String.IsNullOrEmpty(langCode))
            langCode = "tr-TR";

        var langLabel = multilanguages?[langCode];

        if (String.IsNullOrEmpty(langLabel))
            return "Undefined Lang";

        return langLabel;
    }
}