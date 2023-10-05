using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace amorphie.contract.zeebe.Model
{
    public class MessageVariables
    {
        public MessageVariables()
        {
            Variables = new Dictionary<string, dynamic>();
        }
        public Dictionary<string, dynamic> Variables { get; set; }
        public dynamic? Body { get; set; }
        public string? TransitionName { get; set; }
        public string? LastTransition { get; set; }

        public string? TriggeredBy { get; set; }
        public string? TriggeredByBehalfOf { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
        public bool Success { get; set; }
        public string? InstanceId { get; set; }
        public string? RecordId { get; set; }
        public Guid InstanceIdGuid { get; set; }
        public Guid RecordIdGuid { get; set; }
        public Guid TriggeredByGuid { get; set; }
        public Guid TriggeredByBehalfOfGuid { get; set; }

    }
}