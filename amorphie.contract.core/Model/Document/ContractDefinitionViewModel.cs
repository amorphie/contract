using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.core.Model.Document
{
    public class ContractDefinitionViewModel
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Status { get; set; }
        public List<TagsView>? Tags { get; set; }
        public List<EntityPropertyView>? EntityProperties { get; set; }
        public List<ContractDocumentDetailView>? ContractDocumentDetailList { get; set; }
        public List<ContractDocumentGroupDetailView>? ContractDocumentGroupDetailLists { get; set; }
        public List<ValidationView>? ValidationList { get; set; }
    }
    public class ContractDocumentDetailView
    {
        public string? UseExisting { get; set; }
        public string? MinVersion { get; set; }
        public bool Required { get; set; }
        public DocumentDefinitionViewModel DocumentDefinition { get; set; }


    }
    public class ContractDocumentGroupDetailView
    {
        public uint AtLeastRequiredDocument { get; set; }
        public bool Required { get; set; }
        public DocumentGroupViewModel? DocumentGroup { get; set; }
    }
    public class ValidationView
    {
        public string? ValidationDecision { get; set; }
        public string? EValidationType { get; set; }
    }
}