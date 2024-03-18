using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.Extensions;

public static class HeaderHelper
{
    public static HeaderFilterModel GetHeader(HttpContext httpContext)
    {
        var model = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;

        ArgumentException.ThrowIfNullOrEmpty(model.UserReference, nameof(model.UserReference));
        ArgumentException.ThrowIfNullOrEmpty(model.LangCode, nameof(model.LangCode));

        if (model is null)
        {
            throw new ArgumentException("HeaderFilterModel cannot be null");
        }

        return model;
    }

    public static string GetHeaderLangCode(HttpContext httpContext)
    {
        var model = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;

        if (model is null)
        {
            throw new ArgumentException("HeaderFilterModel cannot be null");
        }

        return model.LangCode;
    }

}
