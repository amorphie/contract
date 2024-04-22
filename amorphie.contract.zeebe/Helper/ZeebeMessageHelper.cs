using System.Dynamic;
using System.Text.Json;
using amorphie.contract.zeebe.Model;
using amorphie.contract.zeebe.Model.Static;


public static class ZeebeMessageHelper
{
    public static dynamic CreateMessageVariables(MessageVariables messageVariables)
    {

        messageVariables.Variables.Add("EntityName", messageVariables.Body.GetProperty("EntityName").ToString());
        messageVariables.Variables.Add("InstanceId", messageVariables.InstanceId);
        messageVariables.Variables.Add("LastTransition", messageVariables.TransitionName);
        messageVariables.Variables.Add("Message", messageVariables.Message);

        dynamic targetObject = new System.Dynamic.ExpandoObject();
        targetObject.TriggeredBy = messageVariables.TriggeredBy;
        targetObject.TriggeredByBehalfOf = messageVariables.TriggeredByBehalfOf;

        var data = JsonSerializer.Deserialize<ExpandoObject>(messageVariables.Data);
        data.additionalData = messageVariables.additionalData;
        targetObject.Data = data;

        var TransitionNameR = messageVariables.TransitionName.Replace("-", "");
        messageVariables.Variables.Add($"TRX{TransitionNameR}", targetObject);
        return messageVariables.Variables;
    }
    public static Guid StringToGuid(string data)
    {
        Guid dataGuid;
        if (!Guid.TryParse(data, out dataGuid))
        {
            throw new Exception("ZeebeMessageHelper StringToGuid not provided or not as a GUID data=" + data);
        }
        return dataGuid;
    }

    public static MessageVariables VariablesControl(dynamic body)
    {

        var messageVariables = new MessageVariables();
        var transitionName = body.GetProperty(MessageProp.LastTransition).ToString();
        var transitionNameR = body.GetProperty(MessageProp.LastTransition).ToString().Replace("-", "");
        var instanceIdAsString = body.GetProperty(MessageProp.InstanceId).ToString();

        var data = body.GetProperty($"TRX{transitionNameR}").GetProperty(MessageProp.Data);
        var AdditionalData = body.GetProperty($"TRX{transitionNameR}").GetProperty(MessageProp.Data).GetProperty(MessageProp.additionalData);
        var recordIdAsString = body.GetProperty(MessageProp.RecordId).ToString();
        string triggeredByAsString = body.GetProperty($"TRX{transitionNameR}").GetProperty(MessageProp.TriggeredBy).ToString();
        string triggeredByBehalfOfAsString = body.GetProperty($"TRX{transitionNameR}").GetProperty(MessageProp.TriggeredByBehalfOf).ToString();


        messageVariables.InstanceIdGuid = StringToGuid(instanceIdAsString);

        messageVariables.RecordIdGuid = StringToGuid(recordIdAsString);

        messageVariables.TriggeredByGuid = StringToGuid(triggeredByAsString);

        messageVariables.TriggeredByBehalfOfGuid = StringToGuid(triggeredByBehalfOfAsString);
        // messageVariables bunu dön 
        return new MessageVariables
        {
            Body = body,
            TransitionName = transitionName,
            InstanceId = instanceIdAsString,
            Data = data,
            RecordId = recordIdAsString,
            RecordIdGuid = messageVariables.RecordIdGuid,
            TriggeredBy = triggeredByAsString,
            TriggeredByBehalfOf = triggeredByBehalfOfAsString,

            additionalData = AdditionalData,

            InstanceIdGuid = messageVariables.InstanceIdGuid

        };


    }

    public static T MapToDto<T>(dynamic body)
    {
        var jsonDto = body.GetProperty(typeof(T).Name).ToString();

        var resultDto = JsonSerializer.Deserialize<T>(jsonDto, options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return resultDto;
    }

    public static T MapToDto<T>(dynamic body, string variableName)
    {
        var jsonDto = body.GetProperty(variableName).ToString();

        var resultDto = JsonSerializer.Deserialize<T>(jsonDto, options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return resultDto;
    }
}


