using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Contract;
using amorphie.core.Base;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract
{
    public class ContractMapProfile : Profile
    {
        public ContractMapProfile()
        {
            CreateMap<ContractDocumentDetail, DocumentDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => GetDocumentTitle(src, language)))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.DocumentDefinition.Code))
            .ForMember(dest => dest.Render, opt => opt.MapFrom(src => src.DocumentDefinition.DocumentOnlineSing != null))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.DocumentDefinition.Semver))
            .ForMember(dest => dest.OnlineSign, opt => opt.MapFrom(src => MapToOnlineSignModel(src.DocumentDefinition.DocumentOnlineSing, language)));


            CreateMap<DocumentTemplateDto, DocumentTemplate>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.MinVersion)).ReverseMap();
        }


        private string GetDocumentTitle(ContractDocumentDetail documentDetail, string language)
        {
            return documentDetail.DocumentDefinition.DocumentDefinitionLanguageDetails
                .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
                .FirstOrDefault()?.MultiLanguage?.Name ?? documentDetail.DocumentDefinition.DocumentDefinitionLanguageDetails
                .FirstOrDefault()?.MultiLanguage?.Name;
        }

        private string GetDocumentGroupTitle(ContractDocumentGroupDetail documentGroupDetail, string language)
        {
            return documentGroupDetail.DocumentGroup.DocumentGroupLanguageDetail
                .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
                .FirstOrDefault()?.MultiLanguage?.Name ?? documentGroupDetail.DocumentGroup.DocumentGroupLanguageDetail
                .FirstOrDefault()?.MultiLanguage?.Name;
        }

        private OnlineSignDto MapToOnlineSignModel(DocumentOnlineSing documentOnlineSign, string language)
        {
            return new OnlineSignDto
            {
                DocumentModelTemplate = documentOnlineSign?.DocumentTemplateDetails
                    .Where(x => x.DocumentTemplate.LanguageType.Code == language)
                    .Select(b => ObjectMapperApp.Mapper.Map<DocumentTemplateDto>(b))
                    .ToList() ?? documentOnlineSign?.DocumentTemplateDetails
                    .Select(b => ObjectMapperApp.Mapper.Map<DocumentTemplateDto>(b))
                    .Take(1)
                    .ToList(),
                ScaRequired = documentOnlineSign != null && documentOnlineSign.Required,
                AllovedClients = documentOnlineSign?.DocumentAllowedClientDetails
                    .Select(x => x.DocumentAllowedClients.Code)
                    .ToList() ?? new List<string>()
            };
        }
    }

}
