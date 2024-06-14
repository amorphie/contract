using System;
using amorphie.contract.application.Contract.Dto.Input;
using amorphie.contract.core.CustomException;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.History;
using amorphie.contract.infrastructure.Contexts;
using Serilog;
using amorphie.contract.core.Model;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Response;

namespace amorphie.contract.application.Contract
{
    public interface IContractDefinitionAppService
    {
        Task<GenericResult<ContractDefinition>> CreateContractDefinition(ContractDefinitionInputDto inputDto, Guid id);
        Task<GenericResult<ContractDefinition>> UpdateContractDefinition(ContractDefinitionInputDto inputDto, Guid id);
    }

    public class ContractDefinitionAppService : IContractDefinitionAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly ILogger _logger;

        public ContractDefinitionAppService(ProjectDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<GenericResult<ContractDefinition>> CreateContractDefinition(ContractDefinitionInputDto inputDto, Guid id)
        {
            ContractDefinition contractDefinition = new ContractDefinition();

            foreach (var item in inputDto?.Metadatas)  {
                var check  = item.IsValidTag();

                if (!check.HasValue)
                    continue;

                if (!check.Value)
                    return GenericResult<ContractDefinition>.Fail("Geçersiz tag ifadesi!");
            }

            foreach (var item in inputDto?.DecisionTableMetadatas)  {
                var check  = item.IsValidTag();

                if (!check.HasValue)
                    continue;

                if (!check.Value)
                    return GenericResult<ContractDefinition>.Fail("Geçersiz tag ifadesi!");
            }

            contractDefinition.Id = id;
            contractDefinition.Code = inputDto.Code;
            contractDefinition.Titles = inputDto.Titles;
            contractDefinition.BankEntity = inputDto.RegistrationType;
            contractDefinition.DefinitionMetadata = inputDto.Metadatas;
            contractDefinition.DecisionTableId = inputDto.DecisionTableId;
            contractDefinition.DecisionTableMetadata = inputDto.DecisionTableMetadatas;

            _dbContext.ContractDefinition.Add(contractDefinition);

            CreateContractCategoryDetail(inputDto.CategoryIds, id);
            CreateContractDocumentGroupDetail(inputDto.DocumentGroups, id);
            CreateContractDocumentDetail(inputDto.Documents, id);
            CreateContractTag(inputDto.Tags, id);
            CreateContractValidation(inputDto.Validations, id);

            _dbContext.SaveChanges();

            _logger.Information("Contract Definition Created ({code}).", contractDefinition.Code);

            return GenericResult<ContractDefinition>.Success(contractDefinition);
        }

        public async Task<GenericResult<ContractDefinition>> UpdateContractDefinition(ContractDefinitionInputDto inputDto, Guid id)
        {
            var contractDefinition = _dbContext.ContractDefinition.FirstOrDefault(x => x.Id == id);
            if (contractDefinition == null)
            {
                throw new EntityNotFoundException("Contract Definition", id.ToString());
            }

            foreach (var item in inputDto?.Metadatas)  {
                var check  = item.IsValidTag();

                if (!check.HasValue)
                    continue;

                if (!check.Value)
                    return GenericResult<ContractDefinition>.Fail("Geçersiz tag ifadesi!");
            }

            foreach (var item in inputDto?.DecisionTableMetadatas)  {
                var check  = item.IsValidTag();

                if (!check.HasValue)
                    continue;

                if (!check.Value)
                    return GenericResult<ContractDefinition>.Fail("Geçersiz tag ifadesi!");
            }

            SetContractDefinitionHistory(contractDefinition);

            contractDefinition.Code = inputDto.Code;
            contractDefinition.Titles = inputDto.Titles;
            contractDefinition.ModifiedAt = DateTime.UtcNow;
            contractDefinition.DefinitionMetadata = inputDto.Metadatas;
            contractDefinition.DecisionTableId = inputDto.DecisionTableId;
            contractDefinition.DecisionTableMetadata = inputDto.DecisionTableMetadatas;

            _dbContext.ContractDefinition.Update(contractDefinition);

            UpdateContractCategoryDetail(contractDefinition.ContractCategoryDetails.ToList(), inputDto.CategoryIds, id);
            UpdateContractDocumentGroupDetail(contractDefinition.ContractDocumentGroupDetails.ToList(), inputDto.DocumentGroups, id);
            UpdateContractDocumentDetail(contractDefinition.ContractDocumentDetails.ToList(), inputDto.Documents, id);
            UpdateContractTag(contractDefinition.ContractTags.ToList(), inputDto.Tags, id);
            UpdateContractValidation(contractDefinition.ContractValidations.ToList(), inputDto.Validations, id);

            _dbContext.SaveChanges();

            _logger.Information("Contract Definition Updated ({code}).", contractDefinition.Code);

            return GenericResult<ContractDefinition>.Success(contractDefinition);
        }

        #region CreateMethods
        public void CreateContractCategoryDetail(List<Guid> list, Guid id)
        {
            List<ContractCategoryDetail> entityList = new List<ContractCategoryDetail>();

            foreach (Guid categoryId in list)
            {
                entityList.Add(new ContractCategoryDetail
                {
                    ContractDefinitionId = id,
                    ContractCategoryId = categoryId
                });
            }

            _dbContext.ContractCategoryDetail.AddRange(entityList);
        }

        private void CreateContractDocumentGroupDetail(List<ContractDocumentGroupInputDto> list, Guid contractId)
        {
            List<ContractDocumentGroupDetail> entityList = new List<ContractDocumentGroupDetail>();
            foreach (var group in list)
            {
                entityList.Add(new ContractDocumentGroupDetail
                {
                    ContractDefinitionId = contractId,
                    DocumentGroupId = group.Id,
                    AtLeastRequiredDocument = group.AtLeastRequiredDocument,
                    Required = group.Required
                });
            }

            _dbContext.ContractDocumentGroupDetail.AddRange(entityList);
        }

        private void CreateContractDocumentDetail(List<ContractDocumentInputDto> list, Guid contractId)
        {
            List<ContractDocumentDetail> entityList = new List<ContractDocumentDetail>();
            foreach (var document in list)
            {
                var documentDefinitionId = _dbContext.DocumentDefinition.Where(x => x.Semver == document.MinVersion && x.Code == document.Code).Select(x => x.Id).FirstOrDefault(); //Tek Gidiş?

                if (documentDefinitionId == Guid.Empty)
                {
                    //_logger.Error("Document Definition Not Found. {Code} - {SemanticVersion}", document.Code, document.MinVersion);
                    throw new EntityNotFoundException("Document Definition", document.Code + " - " + document.MinVersion);
                }

                entityList.Add(new ContractDocumentDetail
                {
                    ContractDefinitionId = contractId,
                    DocumentDefinitionId = documentDefinitionId,
                    UseExisting = document.UseExisting,
                    Required = document.Required,
                    Order = document.Order
                });
            }

            _dbContext.ContractDocumentDetail.AddRange(entityList);
        }

        private void CreateContractTag(List<Guid> list, Guid contractId)
        {
            List<ContractTag> entityList = new List<ContractTag>();
            foreach (var tagId in list)
            {
                entityList.Add(new ContractTag
                {
                    ContractDefinitionId = contractId,
                    TagId = tagId
                });
            }

            _dbContext.ContractTag.AddRange(entityList);
        }

        private void CreateContractValidation(List<ContractValidationInputDto> list, Guid contractId)
        {
            List<Validation> entityList = new List<Validation>();

            foreach (var validation in list)
            {
                Guid? existingValidationId = _dbContext.Validation.Where(x => x.EValidationType == validation.Type).Select(x => x.Id).FirstOrDefault();
                if (existingValidationId.HasValue)
                {
                    _dbContext.ContractValidation.Add(new ContractValidation
                    {
                        ContractDefinitionId = contractId,
                        ValidationId = existingValidationId.Value
                    });
                }
                else
                {
                    Guid newValidationId = Guid.NewGuid();
                    _dbContext.Validation.Add(new Validation
                    {
                        Id = newValidationId,
                        EValidationType = validation.Type
                    });

                    _dbContext.ContractValidation.Add(new ContractValidation
                    {
                        ContractDefinitionId = contractId,
                        ValidationId = newValidationId
                    });
                }
            }
        }
        #endregion

        #region UpdateMethods
        private void SetContractDefinitionHistory(ContractDefinition existingContractDefinition)
        {
            var contractHistory = ObjectMapperApp.Mapper.Map<ContractDefinitionHistoryModel>(existingContractDefinition);
            var contractDefinitionHistory = new ContractDefinitionHistory
            {
                ContractDefinitionHistoryModel = contractHistory,
                ContractDefinitionId = existingContractDefinition.Id
            };
            _dbContext.ContractDefinitionHistory.Add(contractDefinitionHistory);
        }

        public void UpdateContractCategoryDetail(List<ContractCategoryDetail> entityList, List<Guid> list, Guid id)
        {
            if (list.Count == 0 && entityList.Count > 0)
            {
                _dbContext.ContractCategoryDetail.RemoveRange(entityList);
            }
            else if (list.Count > 0 && entityList.Count == 0)
            {
                CreateContractCategoryDetail(list, id);
            }
            else
            {
                var removeEntities = entityList.Where(x => !list.Any(z => x.ContractCategoryId == z)).ToList();
                _dbContext.ContractCategoryDetail.RemoveRange(removeEntities);

                var addedList = list.Where(x => !entityList.Any(z => x == z.ContractCategoryId)).ToList();
                CreateContractCategoryDetail(addedList, id);
            }
        }

        private void UpdateContractDocumentGroupDetail(List<ContractDocumentGroupDetail> entityList, List<ContractDocumentGroupInputDto> list, Guid id)
        {
            if (list.Count == 0 && entityList.Count > 0)
            {
                _dbContext.ContractDocumentGroupDetail.RemoveRange(entityList);
            }
            else if (list.Count > 0 && entityList.Count == 0)
            {
                CreateContractDocumentGroupDetail(list, id);
            }
            else
            {
                var removeEntities = entityList.Where(x => !list.Any(z => x.DocumentGroupId == z.Id)).ToList();
                _dbContext.ContractDocumentGroupDetail.RemoveRange(removeEntities);

                var addedList = list.Where(x => !entityList.Any(z => x.Id == z.DocumentGroupId)).ToList();
                CreateContractDocumentGroupDetail(addedList, id);

                var updateEntities = entityList.Where(x => list.Any(z => x.DocumentGroupId == z.Id)).ToList();

                foreach (var entity in updateEntities)
                {
                    var updateObject = list.FirstOrDefault(x => x.Id == entity.DocumentGroupId);
                    entity.AtLeastRequiredDocument = updateObject.AtLeastRequiredDocument;
                    entity.Required = updateObject.Required;

                    _dbContext.ContractDocumentGroupDetail.Update(entity);
                }
            }
        }

        private void UpdateContractDocumentDetail(List<ContractDocumentDetail> entityList, List<ContractDocumentInputDto> list, Guid id)
        {
            if (list.Count == 0 && entityList.Count > 0)
            {
                _dbContext.ContractDocumentDetail.RemoveRange(entityList);
            }
            else if (list.Count > 0 && entityList.Count == 0)
            {
                CreateContractDocumentDetail(list, id);
            }
            else
            {
                var removeEntities = entityList.Where(x => !list.Any(z => x.DocumentDefinition.Code == z.Code && x.DocumentDefinition.Semver == z.MinVersion)).ToList();
                _dbContext.ContractDocumentDetail.RemoveRange(removeEntities);

                var addedList = list.Where(x => !entityList.Any(z => z.DocumentDefinition.Code == x.Code && z.DocumentDefinition.Semver == x.MinVersion)).ToList();
                CreateContractDocumentDetail(addedList, id);

                var updateEntities = entityList.Where(x => list.Any(z => x.DocumentDefinition.Code == z.Code && x.DocumentDefinition.Semver == z.MinVersion)).ToList();

                foreach (var entity in updateEntities)
                {
                    var updateObject = list.FirstOrDefault(x => x.Code == entity.DocumentDefinition.Code && x.MinVersion == entity.DocumentDefinition.Semver);
                    entity.Order = updateObject.Order;
                    entity.Required = updateObject.Required;
                    entity.UseExisting = updateObject.UseExisting;

                    _dbContext.ContractDocumentDetail.Update(entity);
                }
            }
        }

        private void UpdateContractTag(List<ContractTag> entityList, List<Guid> list, Guid id)
        {
            if (list.Count == 0 && entityList.Count > 0)
            {
                _dbContext.ContractTag.RemoveRange(entityList);
            }
            else if (list.Count > 0 && entityList.Count == 0)
            {
                CreateContractTag(list, id);
            }
            else
            {
                var removeEntities = entityList.Where(x => !list.Any(z => x.Id == z)).ToList();
                _dbContext.ContractTag.RemoveRange(removeEntities);

                var addedList = list.Where(x => !entityList.Any(z => x == z.Id)).ToList();
                CreateContractTag(addedList, id);
            }
        }

        private void UpdateContractValidation(List<ContractValidation> entityList, List<ContractValidationInputDto> list, Guid id)
        {
            _dbContext.ContractValidation.RemoveRange(entityList);

            CreateContractValidation(list, id);
        }
        #endregion
    }
}

