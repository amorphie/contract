using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using amorphie.contract.core.Model.Contract;
using amorphie.contract.core.Model.Document;
using AutoMapper;

namespace amorphie.contract.core.Mapping
{
    public class MappingContractProfile : Profile
    {
        public MappingContractProfile()
        {
            CreateMap<ContractDefinition, ContractDefinition>().ReverseMap();
            CreateMap<ContractDocumentDetail, ContractDocumentDetail>().ReverseMap();
            CreateMap<ContractDocumentGroupDetail, ContractDocumentGroupDetail>().ReverseMap();
            CreateMap<ContractEntityProperty, ContractEntityProperty>().ReverseMap();
            CreateMap<ContractTag, ContractTag>().ReverseMap();
            CreateMap<ContractValidation, ContractValidation>().ReverseMap();
            #region  ContractDefinitionViewModel

            CreateMap<ContractDefinition, ContractDefinitionViewModel>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ToString() : null))
    .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
        src.ContractTags.Select(a => new TagsView
        {
            Code = a.Tags.Code,
            Contact = a.Tags.Contact,
        }).ToList()))
    .ForMember(dest => dest.EntityProperties, opt => opt.MapFrom(src =>
        src.ContractEntityProperty.Select(a => new EntityPropertyView
        {
            Code = a.EntityProperty.Code,
            EntityPropertyValue = a.EntityProperty.EntityPropertyValue != null ? a.EntityProperty.EntityPropertyValue.Data : null
        }).ToList()))
    .ForMember(dest => dest.ContractDocumentDetailList, opt => opt.MapFrom(src =>
        src.ContractDocumentDetails.Select(a => new ContractDocumentDetailView
        {
            UseExisting = ((EUseExisting)a.UseExisting).ToString(),
            MinVersion = a.DocumentDefinitionId.ToString(),
            Required = a.Required,
            DocumentDefinition = ObjectMapper.Mapper.Map<DocumentDefinitionViewModel>(a.DocumentDefinition)
        }).ToList()))
    .ForMember(dest => dest.ContractDocumentGroupDetailLists, opt => opt.MapFrom(src =>
        src.ContractDocumentGroupDetails.Select(a => new ContractDocumentGroupDetailView
        {
            AtLeastRequiredDocument = a.AtLeastRequiredDocument,
            Required = a.Required,
            DocumentGroup = ObjectMapper.Mapper.Map<DocumentGroupViewModel>(a.DocumentGroup)
        }).ToList()))
    .ForMember(dest => dest.ValidationList, opt => opt.MapFrom(src =>
        src.ContractValidations != null ? src.ContractValidations.Select(a => new ValidationView
        {
            ValidationDecision = a.Validations.ValidationDecision != null ? a.Validations.ValidationDecision.Code : null,
            EValidationType = ((EValidationType)a.Validations.EValidationType).ToString(),
        }).ToList() : null));
            #endregion

            #region ContractInstanceModel
            CreateMap<ContractDefinition, ContractModel>().
                ConvertUsing((source, destination, context) => ConvertContractModelToViewModel(source, context.Items["language"]?.ToString()));



            // CreateMap<ContractDocumentDetail, DocumentModel>().ConstructUsing(x => new DocumentModel
            // {
            //     Code = x.DocumentDefinition.Code,
            //     MultilanguageText = x.DocumentDefinition.DocumentDefinitionLanguageDetails.Select(a => ObjectMapper.Mapper.Map<MultilanguageTextModel>(a.MultiLanguage)).ToList(),
            //     // Status = 
            //     Required = x.Required,
            //     Render = x.DocumentDefinition.DocumentOnlineSing != null,
            //     Version = x.DocumentDefinition.Semver,
            //     OnlineSign = x.DocumentDefinition.DocumentOnlineSing.
            // });
            // CreateMap<DocumentOnlineSing, OnlineSignModel>().ConstructUsing(x => new OnlineSignModel
            // {
            //     ScaRequired = x.Required,
            //     DocumentModelTemplate = x.DocumentTemplateDetails.Select(a=>new DocumentModelTemplate{
            //         MinVersion = a.DocumentTemplate.Version,
            //         MultilanguageText =  a.DocumentTemplate.
            //     }).ToList(),

            // });

            #endregion

        }
        private ContractModel ConvertContractModelToViewModel(ContractDefinition x, string? language)
        {
            var viewModel = new ContractModel
            {
                Code = x.Code,
                Status = x.Status.ToString(),// TODO gerek yok modelde enum olsun bakacam
                 
                // DocumentModel = x.ContractDocumentDetails.
                //                 Select(x => ObjectMapper.Mapper.Map<DocumentDefinitionViewModel>(x.DocumentDefinition)).ToList(),
            };
            if (!string.IsNullOrEmpty(language))
            {
                viewModel.Status += language;
            }
            return viewModel;
        }
    }

}