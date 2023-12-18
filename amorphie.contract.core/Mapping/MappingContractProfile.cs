using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;
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
            // CreateMap<ContractDefinition, ContractDefinitionViewModel>()
            //     .ConvertUsing(x => new ContractDefinitionViewModel
            //     {
            //         Id = x.Id,
            //         Code = x.Code,
            //         Status = x.Status != null ? x.Status.Code : null,
            //         Tags = x.ContractTags!.Select(a => new TagsView
            //         {
            //             Code = a.Tags!.Code,
            //             Contact = a.Tags!.Contact,
            //         }).ToList(),
            //         EntityProperties = x.ContractEntityProperty!.Select(a => new EntityPropertyView
            //         {
            //             Code = a.EntityProperty!.Code,
            //             EntityPropertyValue = a.EntityProperty!.EntityPropertyValue!.Data
            //         }).ToList(),
            //         ContractDocumentDetailList = x.ContractDocumentDetails.Select(a => new ContractDocumentDetailView
            //         {
            //             UseExisting = ((EUseExisting)a.UseExisting).ToString(),
            //             Semver = a.Semver,
            //             Required = a.Required,
            //             DocumentDefinition = ObjectMapper.Mapper.Map<DocumentDefinitionViewModel>(a.DocumentDefinition)
            //         }).ToList(),
            //         ContractDocumentGroupDetailLists = x.ContractDocumentGroupDetails.Select(a => new ContractDocumentGroupDetailView
            //         {
            //             AtLeastRequiredDocument = a.AtLeastRequiredDocument,
            //             Required = a.Required,
            //             DocumentGroup = ObjectMapper.Mapper.Map<DocumentGroupViewModel>(a.DocumentGroup)
            //         }).ToList(),
            //         ValidationList = x.ContractValidations!=null?x.ContractValidations.Select(a => new ValidationView
            //         {
            //             ValidationDecision = a.Validations!.ValidationDecision!=null ?a.Validations!.ValidationDecision!.Code:null,
            //             EValidationType = ((EUseExisting)a.Validations.EValidationType).ToString(),
            //         }).ToList():null
            //     });
            CreateMap<ContractDefinition, ContractDefinitionViewModel>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.Code : null))
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
            MinVersion = a.Semver,
            Required = a.Required,
            DocumentDefinitionCode = a.DocumentDefinitionCode
            // DocumentDefinition = ObjectMapper.Mapper.Map<DocumentDefinitionViewModel>(a.DocumentDefinition)
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

        }
    }

}