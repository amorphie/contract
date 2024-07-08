using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.application.MessagingGateway.Dto
{
    public class SendContractDocumentsMailInputDto : BaseHeader
    {
        public string RelatedInstanceId { get; set; }
        public List<string> DocumentCodes { get; set; }
    }
}