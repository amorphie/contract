using System;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Dto.Input;
using amorphie.contract.application.Extensions;
using amorphie.contract.core.CustomException;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Response;
using amorphie.contract.infrastructure.Contexts;

namespace amorphie.contract.application.Contract
{
	public interface IContractCategoryAppService
    {
        Task<GenericResult<ContractCategoryDto>> CreateContractCategory(ContractCategoryDto inputDto, Guid id);
        Task<GenericResult<ContractCategoryDto>> UpdateContractCategory(ContractCategoryDto inputDto, Guid id);
        Task ContractCategoryAdd(ContractCategoryDetailInputDto inputDto);
    }

	public class ContractCategoryAppService : IContractCategoryAppService
	{
		private readonly ProjectDbContext _dbContext;

        public ContractCategoryAppService(ProjectDbContext dbContext)
		{
			_dbContext = dbContext;
        }

		public async Task<GenericResult<ContractCategoryDto>> CreateContractCategory(ContractCategoryDto inputDto, Guid id)
		{
			var isExist = _dbContext.ContractCategory.Any(x => x.Code == inputDto.Code);

			if (isExist)
			{
				return GenericResult<ContractCategoryDto>.Fail("Kategori daha önce eklenmiş.");
			}

			ContractCategory contractCategory = new ContractCategory
			{
				Id = id,
                Code = inputDto.Code,
				Titles = inputDto.Titles
			};

			_dbContext.Add(contractCategory);

			CreateCategoryContractDetails(inputDto.ContractCategoryDetails.ToList(), id);

			_dbContext.SaveChanges();

            return GenericResult<ContractCategoryDto>.Success(inputDto);
		}

        public async Task<GenericResult<ContractCategoryDto>> UpdateContractCategory(ContractCategoryDto inputDto, Guid id)
        {
            var contractCategory = _dbContext.ContractCategory.FirstOrDefault(x => x.Code == inputDto.Code);

            if (contractCategory == null)
            {
				throw new EntityNotFoundException("Contract Category", inputDto.Code);
            }

			contractCategory.Titles = inputDto.Titles;

            _dbContext.Update(contractCategory);

            UpdateCategoryContractDetails(contractCategory.ContractCategoryDetails, inputDto.ContractCategoryDetails, id);

            _dbContext.SaveChanges();

            return GenericResult<ContractCategoryDto>.Success(inputDto);
        }

		public async Task DeleteContractCategory(Guid id)
		{
			ContractCategory contractCategory = new ContractCategory { Id = id };
			_dbContext.ContractCategory.Remove(contractCategory);
		}

        public async Task ContractCategoryAdd(ContractCategoryDetailInputDto inputDto)
        {
            if (inputDto.ContractDefinitionId.HasValue && inputDto.ContractCategoryId.HasValue)
            {
                _dbContext.ContractCategoryDetail.Add(new ContractCategoryDetail
                {
                    ContractCategoryId = inputDto.ContractCategoryId.Value,
                    ContractDefinitionId = inputDto.ContractDefinitionId.Value
                });
            }
            else if (inputDto.ContractDefinitionId.HasValue)
            {
                List<ContractCategoryDetail> entityList = new List<ContractCategoryDetail>();
                foreach (var addId in inputDto.AddIdList)
                {
                    entityList.Add(new ContractCategoryDetail
                    {
                        ContractDefinitionId = inputDto.ContractDefinitionId.Value,
                        ContractCategoryId = addId
                    });
                }

                _dbContext.ContractCategoryDetail.AddRange(entityList);
            }
            else if (inputDto.ContractCategoryId.HasValue)
            {
                List<ContractCategoryDetail> entityList = new List<ContractCategoryDetail>();
                foreach (var addId in inputDto.AddIdList)
                {
                    entityList.Add(new ContractCategoryDetail
                    {
                        ContractCategoryId = inputDto.ContractCategoryId.Value,
                        ContractDefinitionId = addId
                    });
                }

                _dbContext.ContractCategoryDetail.AddRange(entityList);
            }
        }

        private async void CreateCategoryContractDetails(List<ContractCategoryDetailDto> list, Guid id)
		{
			List<ContractCategoryDetail> entityList = new List<ContractCategoryDetail>();
			foreach (var item in list)
            {
				ContractCategoryDetail contractCategoryDetail = new ContractCategoryDetail
				{
					ContractCategoryId = id,
					ContractDefinitionId = item.ContractDefinitionId
				};

				entityList.Add(contractCategoryDetail);
            }

			_dbContext.AddRange(entityList);
		}

        private async void UpdateCategoryContractDetails(List<ContractCategoryDetail> entityList, List<ContractCategoryDetailDto> list, Guid id)
        {
            if (list.Count == 0 && entityList.Count > 0)
            {
                _dbContext.ContractCategoryDetail.RemoveRange(entityList);
            }
            else if (list.Count > 0 && entityList.Count == 0)
            {
                CreateCategoryContractDetails(list, id);
            }
            else
            {
                var removeEntities = entityList.Where(x => !list.Any(z => x.ContractDefinitionId == z.ContractDefinitionId)).ToList();
                _dbContext.ContractCategoryDetail.RemoveRange(removeEntities);

                var addedList = list.Where(x => !entityList.Any(z => x.ContractDefinitionId == z.ContractDefinitionId)).ToList();
                CreateCategoryContractDetails(addedList, id);
            }
        }
    }
}

