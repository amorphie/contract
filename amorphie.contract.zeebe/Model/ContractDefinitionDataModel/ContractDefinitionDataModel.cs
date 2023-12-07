using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.zeebe.Model.ContractDefinitionDataModel
{
    public class DocumentGroupList
    {
        public ushort atLeastRequiredDocument { get; set; }
        public string groupName { get; set; }
        public bool required { get; set; }
    }

    public class DocumentsList
    {
        public string minVersiyon { get; set; }
        public string name { get; set; }
        public bool required { get; set; }
        public string useExisting { get; set; }
    }

    public class EntityPropertyList
    {
        public string PropertyName { get; set; }
        public string value { get; set; }
    }

    public class ContractDefinitionDataModel
    {
        public List<EntityPropertyList> EntityPropertyList { get; set; }
        public string code { get; set; }
        public List<DocumentGroupList> documentGroupList { get; set; }
        public List<DocumentsList> documentsList { get; set; }
        public List<string> tags { get; set; }
        public List<ValidationList> validationList { get; set; }
    }

    public class ValidationList
    {
        public string decisionTable { get; set; }
        public string type { get; set; }
    }


}