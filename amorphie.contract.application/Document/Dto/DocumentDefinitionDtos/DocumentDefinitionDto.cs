using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Google.Rpc;

namespace amorphie.contract.application
{
    public interface IMultilanguageTextHolder
    {
        List<MultilanguageText> MultilanguageText { get; set; }
        string? Name { get; set; }
    }

    public class DocumentDefinitionDto
    {
        public string? Name { get; set; }
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Status { get; set; }
        public string Semver { get; set; }

        public string? BaseStatus { get; set; }
        public List<MultilanguageText>? MultilanguageText { get; set; }
        public List<EntityPropertyDto>? EntityProperties { get; set; }
        public List<TagDto>? Tags { get; set; }
        public DocumentUploadDto? DocumentUpload { get; set; }


        public DocumentOnlineSingDto? DocumentOnlineSing { get; set; }
        public DocumentOptimizeDto? DocumentOptimize { get; set; }
        public DocumentOperationsDto? DocumentOperations { get; set; }

    }

    public class DocumentOptimizeDto
    {
        public bool Size { get; set; }
        public string Code { get; set; }
    }



    public class EntityPropertyDto
    {
        public string? Code { get; set; }
        public string? EntityPropertyValue { get; set; }
    }

    public class NoteDto
    {
        public string? Note { get; set; }
    }

    public class DocumentUploadDto
    {
        public bool Required { get; set; }
        public List<DocumentFormatDetailDto>? DocumentFormatDetails { get; set; }
        public List<string>? DocumentAllowedClientDetails { get; set; }
    }
    public class DocumentOnlineSingDto
    {
        public List<string>? DocumentAllowedClientDetails { get; set; }
        public List<DocumentTemplateDetailsDto>? DocumentTemplateDetails { get; set; }


    }
    public class DocumentTemplateDetailsDto
    {
        public string Code { get; set; }
        public string LanguageType { get; set; }
        public string Version { get; set; } = default!;

    }
    public class DocumentFormatDetailDto
    {
        public string? FormatContentType { get; set; }
        public string? FormatType { get; set; }
        public ulong? Size { get; set; }
    }


    public class GetAllDocumentDefinitionInputDto : PagedInputDto
    {
        public string LangCode { get; set; }
    }

}