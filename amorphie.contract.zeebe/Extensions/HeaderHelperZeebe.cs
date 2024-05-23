using amorphie.contract.application.Contract.Dto.Zeebe;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;

public static class HeaderHelperZeebe
{
    private const string headerTag = "Headers";
    public static HeaderFilterModel GetHeader(dynamic body, bool exception = false)
    {
        string reference = String.Empty;
        string businessLine = String.Empty;
        long? customerNo = null;
        string langCode = "tr-TR";
        string clientId = String.Empty;

        if (body.ToString().IndexOf(headerTag) == -1)
        {
            throw new ArgumentNullException("Headers cannot be null");
        }


        if (body.GetProperty(headerTag).ToString().IndexOf(AppHeaderConsts.UserReference.ToLower()) != -1)
        {
            reference = body.GetProperty(headerTag).GetProperty(AppHeaderConsts.UserReference.ToLower()).ToString();
        }

        if (body.GetProperty(headerTag).ToString().IndexOf(AppHeaderConsts.CustomerNo.ToLower()) != -1)
        {
            customerNo = Convert.ToInt64(body.GetProperty(headerTag).GetProperty(AppHeaderConsts.CustomerNo.ToLower()).ToString());
        }

        if (body.GetProperty(headerTag).ToString().IndexOf(AppHeaderConsts.AcceptLanguage.Replace("-", String.Empty).ToLower()) != -1)
        {
            langCode = body.GetProperty(headerTag).GetProperty(AppHeaderConsts.AcceptLanguage.Replace("-", String.Empty).ToLower()).ToString();
        }

        if (body.GetProperty(headerTag).ToString().IndexOf(AppHeaderConsts.BusinessLine.ToLower()) != -1)
        {
            businessLine = body.GetProperty(headerTag).GetProperty(AppHeaderConsts.BusinessLine.ToLower()).ToString();
        }

        if (body.GetProperty(headerTag).ToString().IndexOf(AppHeaderConsts.ClientId.ToLower()) != -1)
        {
            clientId = body.GetProperty(headerTag).GetProperty(AppHeaderConsts.ClientId.ToLower()).ToString();
        }
        if (exception)
        {
            ArgumentException.ThrowIfNullOrEmpty(reference, nameof(reference));
            ArgumentException.ThrowIfNullOrEmpty(businessLine, nameof(businessLine));
        }

        return new HeaderFilterModel(businessLine, langCode, clientId, reference, customerNo);
    }

    public static void SetHeaderFromWithoutDto(dynamic body, HeaderFilterModel model)
    {
        var contractWithoutHeaderDto = ZeebeMessageHelper.MapToDto<ContractWithoutHeaderDto>(body, ZeebeConsts.ContractWithoutHeaderDto);
        ArgumentException.ThrowIfNullOrEmpty(contractWithoutHeaderDto.Reference, nameof(contractWithoutHeaderDto.Reference));

        model.UserReference = contractWithoutHeaderDto.Reference;
        model.CustomerNo = contractWithoutHeaderDto.CustomerNo;

        var banktEntity = model.GetBankEntity(contractWithoutHeaderDto.BankEntity);
        model.SetBankEntity(banktEntity);
    }

}
