using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Enum;

namespace amorphie.contract.zeebe.Model.ContractDefinitionDataModel
{
    public class DocumentGroupList
    {
        public ushort atLeastRequiredDocument { get; set; }
        public GroupName groupName { get; set; }
        public bool required { get; set; }
    }

    public class GroupName
    {
        public string id { get; set; }
    }
    public class DocumentsList
    {
        public string minVersiyon { get; set; }
        public Name name { get; set; }
        public bool required { get; set; }
        public short order { get; set; }
        public EUseExisting useExisting { get; set; }
    }


    public class Name
    {
        public Guid Id { get; set; }
        public string code { get; set; }
        public List<string> semverList { get; set; }
        public Title title { get; set; }
    }

    public class Title
    {
        public string languageType { get; set; }
        public string name { get; set; }
    }

    public class SelectBoxes
    {
    }
    public class ContractTitle
    {
        public string textField { get; set; }
        public string language { get; set; }
        public string label { get; set; }
        public SelectBoxes selectBoxes { get; set; }
        public string select { get; set; }
        public string title { get; set; }
    }
    public class EntityProperty
    {
        public string PropertyName { get; set; }
        public string property { get; set; }
        public string value { get; set; }
    }


    public class ContractDefinitionDataModel
    {
        public List<EntityProperty>? EntityProperty { get; set; }
        public string code { get; set; }
        public List<ContractTitle> Titles { get; set; }
        public List<DocumentGroupList>? documentGroupList { get; set; }
        public List<DocumentsList>? documentsList { get; set; }
        public List<string> tags { get; set; }
        public List<ValidationList> validationList { get; set; }
        public EBankEntity registrationType { get; set; }
    }

    public class ValidationList
    {
        public string decisionTable { get; set; }
        public EValidationType type { get; set; }
    }


}