using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

namespace amorphie.contract.core.Model.Document
{
   

    public class DocumentContentModel
    {
        public string ContentData { get; set; }
        public string KiloBytesSize { get; set; }
        public string ContentType { get; set; }
        public string ContentTransferEncoding { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class DocumentDefinitionModel
    {
        public string Code { get; set; }
        public List<MultilanguageText> MultilanguageText { get; set; }
        public DocumentOperationsModel DocumentOperations { get; set; }
    }

    public class DocumentOperationsModel
    {
        public bool DocumentManuelControl { get; set; }
        public List<TagModel> DocumentOperationsTagsDetail { get; set; }
    }
    public class TagModel
    {
        public string Code { get; set; }
        public string Contact { get; set; }

    }


    public class RootDocumentModel
    {
        public string DocumentDefinitionId { get; set; }
        public DocumentDefinitionModel DocumentDefinition { get; set; }
        public DocumentContentModel DocumentContent { get; set; }
        public string StatuCode { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}