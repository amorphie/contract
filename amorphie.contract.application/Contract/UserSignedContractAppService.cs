using amorphie.contract.application.Contract.Dto;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.application.Customer;
using amorphie.contract.core.Response;
using Serilog;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Contract
{
    public interface IUserSignedContractAppService
    {
        Task<GenericResult<Guid>> UpsertAsync(UserSignedContractInputDto inputDto);
        Task<GenericResult<bool?>> IsUserApprovedContract(IsUserApprovedContractInputDto inputDto);
        Task<GenericResult<bool>> UpdateApprovalStatusToNewVersion(string contractCode, CancellationToken cancellationToken);
    }

    public class UserSignedContractAppService : IUserSignedContractAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly ICustomerAppService _customerAppService;
        private readonly ILogger _logger;
        public UserSignedContractAppService(ProjectDbContext projectDbContext, ICustomerAppService customerAppService, ILogger logger)
        {
            _dbContext = projectDbContext;
            _customerAppService = customerAppService;
            _logger = logger;
        }

        public async Task<GenericResult<Guid>> UpsertAsync(UserSignedContractInputDto inputDto)
        {
            var userReference = inputDto.GetUserReference();
            var customerResult = await _customerAppService.GetIdByReference(userReference);

            if (!customerResult.IsSuccess)
            {
                _logger.Error("Failed to get customer. {Message} - {UserReference}", customerResult.ErrorMessage, userReference);

                return GenericResult<Guid>.Fail($"Failed to get customer {userReference}");
            }

            var userSignedContract = await _dbContext.UserSignedContract.FirstOrDefaultAsync(k => k.ContractCode == inputDto.ContractCode && k.CustomerId == customerResult.Data); //TODO ANYVALID kontrol edilecek.

            if (userSignedContract is null)
            {
                userSignedContract = new UserSignedContract
                {
                    ContractCode = inputDto.ContractCode,
                    ContractInstanceId = inputDto.ContractInstanceId,
                    CustomerId = customerResult.Data,
                    ApprovalStatus = inputDto.ApprovalStatus,
                    UserSignedContractDetails = inputDto.DocumentInstanceIds.Select(docInstanceId => new UserSignedContractDetail
                    {
                        DocumentInstanceId = docInstanceId
                    }).ToList()
                };

                await _dbContext.UserSignedContract.AddAsync(userSignedContract);
            }
            else
            {
                userSignedContract.ApprovalStatus = inputDto.ApprovalStatus;

                foreach (var docInstanceId in inputDto.DocumentInstanceIds.Where(k => !userSignedContract.UserSignedContractDetails.Any(x => x.DocumentInstanceId == k)).ToList())
                {
                    userSignedContract.UserSignedContractDetails.Add(new UserSignedContractDetail
                    {
                        DocumentInstanceId = docInstanceId,
                        UserSignedContractId = userSignedContract.Id
                    });
                }

                _dbContext.UserSignedContract.Update(userSignedContract);
            }

            await _dbContext.SaveChangesAsync();

            return GenericResult<Guid>.Success(userSignedContract.Id);
        }

        public async Task<GenericResult<bool?>> IsUserApprovedContract(IsUserApprovedContractInputDto inputDto)
        {
            var userReference = inputDto.GetUserReference();
            var customerResult = await _customerAppService.GetIdByReference(userReference);

            if (!customerResult.IsSuccess)
            {
                _logger.Error("[IsUserApprovedContract] Failed to get customer. {Message} - {UserReference}", customerResult.ErrorMessage, userReference);

                return GenericResult<bool?>.Fail($"Failed to get customer {userReference}");
            }

            var userSignedContract = await _dbContext.UserSignedContract
                .Where(k => k.ContractCode == inputDto.ContractCode && k.CustomerId == customerResult.Data)
                .Select(x => new { x.ContractCode, x.ApprovalStatus })
                    .FirstOrDefaultAsync();

            if (userSignedContract is not null && userSignedContract.ApprovalStatus == ApprovalStatus.Approved)
            {
                return GenericResult<bool?>.Success(true);
            }
            else if (userSignedContract is not null)
            {
                return GenericResult<bool?>.Success(false);
            }
            else
            {
                return GenericResult<bool?>.Success(null);
            }
        }

        public async Task<GenericResult<bool>> UpdateApprovalStatusToNewVersion(string contractCode, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.UserSignedContract
                     .Where(k => k.ContractCode == contractCode)
                     .ExecuteUpdateAsync(k => k.SetProperty(k => k.ApprovalStatus, ApprovalStatus.HasNewVersion), cancellationToken);

                return GenericResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.Error("[UpdateApprovalStatusToNewVersion] Failed to update UserSignedContract. {Message}", ex.Message);

                return GenericResult<bool>.Fail("Failed to update approval status");
            }
        }

    }
}
