using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.zeebe.Model.DocumentGroupDataModel
{
    public class DocumentGroupDataModel
    {
        public string code { get; set; }
        public List<DocumentGroupDocumentModel> documents { get; set; }
        public List<Title> titles { get; set; }
    }

    public class DocumentGroupDocumentModel
    {
        public DocumentGroupDocumentSubModel document { get; set; }
        public string minVersiyon { get; set; }
    }

    public class DocumentGroupDocumentSubModel
    {
        public Guid Id { get; set; }
        public string code { get; set; }
        public TitleSubModel title { get; set; }
        public List<string> semverList { get; set; }
    }

    public class TitleSubModel
    {
        public string languageType { get; set; }
        public string name { get; set; }
    }

    public class Title
    {
        public string language { get; set; }
        public string title { get; set; }
    }
}