using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Enum;

namespace amorphie.contract.zeebe.Model.ContractDefinitionDataModel
{
    public class ContractDefinitionDataModel //inputDto isimlendirmesi yapılacak
    {
        //[Required] Attribute eklenecek hatalar için
        public string Code { get; set; }
        public EBankEntity RegistrationType { get; set; }
        public List<Guid> Tags { get; set; }
        public Dictionary<string, string> Titles { get; set; }
        public List<EntityProperty> EntityProperties { get; set; }
        public List<DocumentsList> Documents { get; set; }
        public List<DocumentGroupList> DocumentGroups { get; set; }
        public List<ValidationList> Validations { get; set; }
    }

    public class EntityProperty //bu metadata class'ı ile değişecek
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }

    public class DocumentsList
    {
        public string MinVersion { get; set; }
        public string Code { get; set; }
        public bool Required { get; set; }
        public EUseExisting UseExisting { get; set; }
        public short Order { get; set; }
    }

    public class DocumentGroupList
    {
        public ushort AtLeastRequiredDocument { get; set; }
        public Guid Id { get; set; }
        public bool Required { get; set; }
    }

    public class ValidationList
    {
        public string DecisionTable { get; set; }
        public EValidationType Type { get; set; }
    }

}