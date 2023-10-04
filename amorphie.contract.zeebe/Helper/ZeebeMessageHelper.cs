using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.zeebe.Model;
using amorphie.contract.zeebe.Model.Static;


public static class ZeebeMessageHelper
{
    public static dynamic CreateMessageVariables(MessageVariables messageVariables)
    {
        dynamic variables = new Dictionary<string, dynamic>();

        variables.Add("EntityName", messageVariables.Body.GetProperty("EntityName").ToString());
        variables.Add("RecordId", messageVariables.RecordId);
        variables.Add("InstanceId", messageVariables.InstanceId);
        variables.Add("LastTransition", messageVariables.LastTransition);
        variables.Add("Message", messageVariables.Message);
        if (messageVariables.Success)
            variables.Add("Status", "OK");
        else
        {
            variables.Add("Status", "NOTOK");
        }
        dynamic targetObject = new System.Dynamic.ExpandoObject();
        targetObject.Data = messageVariables.Data;
        targetObject.TriggeredBy = messageVariables.TriggeredBy;
        targetObject.TriggeredByBehalfOf = messageVariables.TriggeredByBehalfOf;


        variables.Add($"TRX-{messageVariables.TransitionName}", targetObject);
        return variables;
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

            Guid instanceId;
            if (!Guid.TryParse(instanceIdAsString, out instanceId))
            {
                throw new Exception("InstanceId not provided or not as a GUID");
            }
            messageVariables.InstanceIdGuid = instanceId;
            Guid recordId;
            if (!Guid.TryParse(recordIdAsString, out recordId))
            {
                throw new Exception("RecordId not provided or not as a GUID");
            }
            messageVariables.RecordIdGuid = recordId;

            Guid triggeredBy;
            if (!Guid.TryParse(triggeredByAsString, out triggeredBy))
            {
                throw new Exception("triggeredBy not provided or not as a GUID");
            }
            messageVariables.TriggeredByGuid = triggeredBy;

            Guid triggeredByBehalfOf;
            if (!Guid.TryParse(triggeredByBehalfOfAsString, out triggeredByBehalfOf))
            {
                throw new Exception("triggeredBy not provided or not as a GUID");
            }
            messageVariables.TriggeredByBehalfOfGuid = triggeredByBehalfOf;

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
            TriggeredBy = triggeredByAsString,
            TriggeredByBehalfOf = triggeredByBehalfOfAsString,
        };
    }
}
