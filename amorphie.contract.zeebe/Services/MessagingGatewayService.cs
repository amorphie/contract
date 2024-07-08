using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.application.MessagingGateway;
using amorphie.contract.application.MessagingGateway.Dto;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Response;

namespace amorphie.contract.zeebe.Services
{
    public interface IMessagingGatewayService
    {
        Task<GenericResult<bool>> SendDocumentsToCustomer(SendDocumentsToCustomerInputDto inputDto);
        Task<GenericResult<FindMailAttachmentOutputDto>> FindMailAttachmentCodes(FindMailAttachmentDto contractInstanceDto);
    }

    public class MessagingGatewayService : IMessagingGatewayService
    {
        private readonly IMessagingGatewayAppService _messagingGatewayAppService;

        public MessagingGatewayService(IMessagingGatewayAppService messagingGatewayAppService)
        {
            _messagingGatewayAppService = messagingGatewayAppService;
        }

        public async Task<GenericResult<bool>> SendDocumentsToCustomer(SendDocumentsToCustomerInputDto inputDto)
        {
            return await _messagingGatewayAppService.SendMailTaskService(inputDto);
        }

        public async Task<GenericResult<FindMailAttachmentOutputDto>> FindMailAttachmentCodes(FindMailAttachmentDto contractInstanceDto)
        {
            return await _messagingGatewayAppService.FindContractAttachments(contractInstanceDto);
        }
    }
}