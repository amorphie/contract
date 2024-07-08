using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.application.Customer;
using amorphie.contract.application.CustomerApi;
using amorphie.contract.application.MessagingGateway.Dto;
using amorphie.contract.core.Entity;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Proxy;
using amorphie.contract.core.Response;
using amorphie.contract.core.Services;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.infrastructure.Services.Refit;
using Serilog;

namespace amorphie.contract.application.MessagingGateway
{
    public interface IMessagingGatewayAppService
    {
        Task<GenericResult<string>> SendTemplatedMail(SendTemplatedMailRequestModel requestModel);
        Task<GenericResult<bool>> SendDocumentsToCustomer(SendDocumentsToCustomerInputDto inputDto);
        Task<GenericResult<FindMailAttachmentOutputDto>> FindContractAttachments(FindMailAttachmentDto contractInstanceDto);
    }

    public class MessagingGatewayAppService : IMessagingGatewayAppService
    {
        private readonly IMessagingGatewayService _messagingGatewayService;
        private readonly ICustomerApiAppService _customerApiAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly IMinioService _minioService;
        private readonly ProjectDbContext _dbContext;
        private readonly ILogger _logger;

        public MessagingGatewayAppService(IMessagingGatewayService messagingGatewayService, ICustomerApiAppService customerApiAppService, ICustomerAppService customerAppService, IMinioService minioService, ProjectDbContext dbContext, ILogger logger)
        {
            _messagingGatewayService = messagingGatewayService;
            _customerApiAppService = customerApiAppService;
            _customerAppService = customerAppService;
            _minioService = minioService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<GenericResult<string>> SendTemplatedMail(SendTemplatedMailRequestModel requestModel)
        {
            var result = await _messagingGatewayService.SendTemplatedMail(requestModel);
            var responseContent = await result.Content.ReadAsStringAsync();

            return GenericResult<string>.Success(responseContent);
        }

        public async Task<GenericResult<FindMailAttachmentOutputDto>> FindContractAttachments(FindMailAttachmentDto contractInstanceDto)
        {
            var contractDefinition = _dbContext.ContractDefinition.Where(x => x.Code == contractInstanceDto.ContractCode).FirstOrDefault();

            var mailAttachDocumentCodesOfContract = contractDefinition.ContractDocumentDetails.Where(x => x.SendMail)
            .Select(x => x.DocumentDefinition.Code)
                .Concat(contractDefinition.ContractDocumentGroupDetails
                    .SelectMany(x => x.DocumentGroup.DocumentGroupDetails.Where(x => x.SendMail))
                    .Select(cd => cd.DocumentDefinition.Code))
            .ToList();

            return GenericResult<FindMailAttachmentOutputDto>.Success(new FindMailAttachmentOutputDto
            {
                RelatedInstanceId = contractInstanceDto.ContractInstanceId.ToString(),
                DocumentCodes = mailAttachDocumentCodesOfContract
            });
        }

        public async Task<GenericResult<bool>> SendDocumentsToCustomer(SendDocumentsToCustomerInputDto inputDto)
        {
            List<SendTemplatedMailAttachmentModel> attachments = new List<SendTemplatedMailAttachmentModel>();

            var documents = _dbContext.Document.Where(x => inputDto.DocumentCodes.Contains(x.DocumentDefinition.Code) && x.Customer.Reference == inputDto.TCKN);

            foreach (var doc in documents)
            {
                var documentContent = await _minioService.DownloadFile(doc.DocumentContent.MinioObjectName, new CancellationToken());

                attachments.Add(new SendTemplatedMailAttachmentModel()
                {
                    Name = documentContent.FileName,
                    Data = documentContent.FileContent
                });
            }

            dynamic templateParams = new
            {
                test = "TestMailBody"
            };

            var emailResponse = await _customerApiAppService.GetCustomerEmail(inputDto.TCKN);

            var templateMailDto = new SendTemplatedMailRequestModel
            {
                Sender = inputDto.Sender,
                Email = emailResponse.Data,
                Template = "mailtest1", //ContractTemplate
                TemplateParams = JsonSerializer.Serialize(templateParams),
                Attachments = attachments,
                CustomerNo = 0,
                CitizenshipNo = inputDto.TCKN,
                IsVerified = false, //SendToOnlyWhitelistMails
                InstantReminder = false, //ThisWorksOnlyTest
                Process = new SendTemplatedMailProcessModel
                {
                    Name = "SendContractDocuments",
                    ItemId = inputDto.RelatedInstanceId,
                    Action = "",
                    Identity = "AmorphieContract"
                }
            };

            var result = await _messagingGatewayService.SendTemplatedMail(templateMailDto);
            // var responseContent = await result.Content.ReadAsStringAsync();

            var customerId = await _customerAppService.GetIdByReference(inputDto.TCKN);
            _dbContext.CustomerCommunication.Add(new CustomerCommunication
            {
                CustomerId = customerId.Data,
                EmailAddress = emailResponse.Data,
                DocumentList = attachments.Select(x => x.Name).ToList(),
                IsSuccess = result.IsSuccessStatusCode
            });

            _dbContext.SaveChanges();

            return GenericResult<bool>.Success(result.IsSuccessStatusCode);
        }
    }
}