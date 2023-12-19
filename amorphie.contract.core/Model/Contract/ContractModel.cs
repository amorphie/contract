using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.core.Model.Contract
{

    public class AllowedFormatModel
    {
        public string format { get; set; }
        public int max_size_kilobytes { get; set; }
    }

    public class Upload
    {
        public bool sca_required { get; set; }
        public List<string> allowed_clients { get; set; }
        public List<AllowedFormatModel> allowed_formats { get; set; }
    }

    public class OnlineSignModel
    {
        public string version { get; set; }
        public bool sca_required { get; set; }
        public List<string> alloved_clients { get; set; }
    }

    public class DocumentModel
    {
        public string name { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public bool required { get; set; }
        public Upload upload { get; set; }
        public bool render { get; set; }
        public OnlineSignModel online_sign { get; set; }
    }

    public class DocumentGroupModel
    {
        public string name { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public int at_least_required_document { get; set; }
        public List<DocumentModel> document { get; set; }
    }

    public class ContractModel
    {
        public Guid id { get; set; }
        public string status { get; set; }
        public List<DocumentModel> document { get; set; }
        public List<DocumentGroupModel> document_groups { get; set; }
    }
}