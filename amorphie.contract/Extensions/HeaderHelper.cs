using amorphie.contract.application.Contract.Dto.Zeebe;
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

    public static HeaderFilterModel GetHeaderWithDto(HttpContext httpContext, ContractWithoutHeaderDto? withoutHeaderDto)
    {
        var model = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;

        if (model is null)
            throw new ArgumentException("HeaderFilterModel cannot be null");

        if (String.IsNullOrWhiteSpace(model.UserReference))
        {
            if (withoutHeaderDto is null || !withoutHeaderDto.CheckForNull())
            {
                throw new ArgumentNullException(nameof(withoutHeaderDto), "WithoutHeader cannot be null.");
            }

            model.UserReference = withoutHeaderDto.Reference;
            model.CustomerNo = Convert.ToInt64(withoutHeaderDto.CustomerNo);
            model.SetBankEntity(model.GetBankEntity(withoutHeaderDto.BankEntity));

            return model;
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
