using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Google.Rpc;

namespace amorphie.contract.core.Model.Document
{
    public class DocumentDefinitionViewModel
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Status { get; set; }
        public string? BaseStatus { get; set; }
        public List<MultilanguageText>? MultilanguageText { get; set; }
        public List<EntityPropertyView>? EntityProperties { get; set; }
        public List<TagsView>? Tags { get; set; }
        public DocumentUploadView? DocumentUpload { get; set; }
        public DocumentOnlineSingView? DocumentOnlineSing { get; set; }
        public DocumentOptimizeView? DocumentOptimize { get; set; }
        public DocumentOperationsView? DocumentOperations { get; set; }

    }
    public class DocumentOperationsView
    {
        public bool DocumentManuelControl { get; set; }
        public List<TagsView>? DocumentOperationsTagsDetail { get; set; }

    }
    public class DocumentOptimizeView
    {
        public bool Size { get; set; }
        public string Code { get; set; }
    }
    public class EntityPropertyView
    {
        public string? Code { get; set; }
        public string? EntityPropertyValue { get; set; }
    }
    public class TagsView
    {
        public string? Code { get; set; }
        public string? Contact { get; set; }
    }
    public class DocumentUploadView
    {
        public bool Required { get; set; }
        public List<DocumentFormatDetailView>? DocumentFormatDetail { get; set; }
        public List<string>? DocumentAllowedClientDetail { get; set; }
    }
    public class DocumentOnlineSingView
    {
        public string Semver { get; set; }
        public List<string>? DocumentAllowedClientDetail { get; set; }
        public List<DocumentTemplateDetailsView>? DocumentTemplateDetails { get; set; }


    }
    public class DocumentTemplateDetailsView
    {
        public string Code { get; set; }
        public string LanguageType { get; set; }
    }
    public class DocumentFormatDetailView
    {
        public string? FormatContentType { get; set; }
        public string? FormatType { get; set; }
        public string? Size { get; set; }
    }
}