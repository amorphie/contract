using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

        if (messageVariables.Success)
            messageVariables.Variables.Add("Status", "OK");
        else
        {
            messageVariables.Variables.Add("Status", "NOTOK");
        }
        dynamic targetObject = new System.Dynamic.ExpandoObject();
        targetObject.Data = messageVariables.Data;
        targetObject.TriggeredBy = messageVariables.TriggeredBy;
        targetObject.TriggeredByBehalfOf = messageVariables.TriggeredByBehalfOf;


        messageVariables.Variables.Add($"TRX-{messageVariables.TransitionName}", targetObject);
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
        var instanceIdAsString = body.GetProperty(MessageProp.InstanceId).ToString();
        var data = body.GetProperty($"TRX-{transitionName}").GetProperty(MessageProp.Data);
        var recordIdAsString = body.GetProperty(MessageProp.RecordId).ToString();
        string triggeredByAsString = body.GetProperty($"TRX-{transitionName}").GetProperty(MessageProp.TriggeredBy).ToString();
        string triggeredByBehalfOfAsString = body.GetProperty($"TRX-{transitionName}").GetProperty(MessageProp.TriggeredByBehalfOf).ToString();
        try
        {

            messageVariables.InstanceIdGuid = StringToGuid(instanceIdAsString);

            messageVariables.RecordIdGuid = StringToGuid(recordIdAsString);

            messageVariables.TriggeredByGuid = StringToGuid(triggeredByAsString);

            messageVariables.TriggeredByBehalfOfGuid = StringToGuid(triggeredByBehalfOfAsString);
            // messageVariables bunu dön 

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
            InstanceIdGuid = messageVariables.InstanceIdGuid
        };
    }
}
