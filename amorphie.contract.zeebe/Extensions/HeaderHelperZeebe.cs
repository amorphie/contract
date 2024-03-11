using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;

public static class HeaderHelperZeebe
{
    public static HeaderFilterModel GetHeader(dynamic body)
    {
        string reference = String.Empty;
        string businessLine = String.Empty;
        long? customerNo = null;
        string langCode = "tr-TR";
        string clientId = String.Empty;

        if (body.ToString().IndexOf("Headers") == -1)
        {
            throw new ArgumentNullException("Headers cannot be null");
        }


        if (body.GetProperty("Headers").ToString().IndexOf(AppHeaderConsts.UserReference.ToLower()) != -1)
        {
            reference = body.GetProperty("Headers").GetProperty(AppHeaderConsts.UserReference.ToLower()).ToString();
        }

        if (body.GetProperty("Headers").ToString().IndexOf(AppHeaderConsts.CustomerNo.ToLower()) != -1)
        {
            customerNo = Convert.ToInt64(body.GetProperty("Headers").GetProperty(AppHeaderConsts.CustomerNo.ToLower()).ToString());
        }

        if (body.GetProperty("Headers").ToString().IndexOf(AppHeaderConsts.AcceptLanguage.Replace("-", String.Empty).ToLower()) != -1)
        {
            langCode = body.GetProperty("Headers").GetProperty(AppHeaderConsts.AcceptLanguage.Replace("-", String.Empty).ToLower()).ToString();
        }

        if (body.GetProperty("Headers").ToString().IndexOf(AppHeaderConsts.BusinessLine.ToLower()) != -1)
        {
            businessLine = body.GetProperty("Headers").GetProperty(AppHeaderConsts.BusinessLine.ToLower()).ToString();
        }

        if (body.GetProperty("Headers").ToString().IndexOf(AppHeaderConsts.ClientId.ToLower()) != -1)
        {
            clientId = body.GetProperty("Headers").GetProperty(AppHeaderConsts.ClientId.ToLower()).ToString();
        }

        ArgumentException.ThrowIfNullOrEmpty(reference, nameof(reference));
        ArgumentException.ThrowIfNullOrEmpty(businessLine, nameof(businessLine));


        return new HeaderFilterModel(businessLine, langCode, clientId, reference, customerNo);
    }

}
