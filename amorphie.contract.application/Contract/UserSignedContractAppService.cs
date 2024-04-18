using amorphie.contract.application.Contract.Dto;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.application.Customer;
using amorphie.contract.core.Response;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.application.Contract
{
    public interface IUserSignedContractAppService
    {

        Task<GenericResult<Guid>> UpsertAsync(UserSignedContractInputDto inputDto);

    }
    public class UserSignedContractAppService : IUserSignedContractAppService
    {
        private readonly ProjectDbContext _projectDbContext;
        private readonly ICustomerAppService _customerAppService;
        private readonly ILogger _logger;
        public UserSignedContractAppService(ProjectDbContext projectDbContext, ICustomerAppService customerAppService, ILogger logger)
        {
            _projectDbContext = projectDbContext;
            _customerAppService = customerAppService;
            _logger = logger;
        }

        public async Task<GenericResult<Guid>> UpsertAsync(UserSignedContractInputDto inputDto)
        {

            var customerResult = await _customerAppService.GetIdByReference(inputDto.UserReference);

            if (!customerResult.IsSuccess)
            {
                _logger.Error("Failed to get customer. {Message} - {UserReference}", customerResult.ErrorMessage, inputDto.UserReference);

                return GenericResult<Guid>.Fail($"Failed to get customer {inputDto.UserReference}");
            }

            var userSignedContract = await _projectDbContext.UserSignedContract.FirstOrDefaultAsync(k => k.ContractCode == inputDto.ContractCode && k.ContractInstanceId == inputDto.ContractInstanceId);

            if (userSignedContract is null)
            {
                userSignedContract = new UserSignedContract
                {
                    ContractCode = inputDto.ContractCode,
                    ContractInstanceId = inputDto.ContractInstanceId,
                    CustomerId = customerResult.Data,
                    UserSignedContractDetails = inputDto.DocumentInstanceIds.Select(docInstanceId => new UserSignedContractDetail
                    {
                        DocumentInstanceId = docInstanceId
                    }).ToList()
                };

                await _projectDbContext.UserSignedContract.AddAsync(userSignedContract);
            }
            else
            {
                foreach (var item in inputDto.DocumentInstanceIds)
                {
                    userSignedContract.UserSignedContractDetails.Add(new UserSignedContractDetail
                    {
                        DocumentInstanceId = item,
                        UserSignedContractId = userSignedContract.Id
                    });
                }

                _projectDbContext.UserSignedContract.Update(userSignedContract);
            }

            await _projectDbContext.SaveChangesAsync();

            return GenericResult<Guid>.Success(userSignedContract.Id);
        }

    }
}
