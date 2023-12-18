using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace amorphie.contract.zeebe.Model.DocumentDefinitionDataModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AllowedFormatsUploadList
    {
        public string format { get; set; }

        [JsonProperty("max-size-kilobytes")]
        public string maxsizekilobytes { get; set; }
        public string Format { get; set; }
        public string MaxSizeKilobytes { get; set; }
    }

    public class Data
    {
        public string header { get; set; }
        public string DocumentType { get; set; }
        public string Code { get; set; }
        public List<Title> Titles { get; set; }
        public string StartingTransitionName { get; set; }
        public List<string> Tags { get; set; }
        public List<EntityProperty> EntityProperty { get; set; }
        public string versiyon { get; set; }
        public List<string> RenderAllowedClients { get; set; }
        public List<TemplateList> TemplateList { get; set; }
        public bool DocumentManuelControl { get; set; }
        public List<string> UploadTags { get; set; }
        public bool Size { get; set; }
        public bool ScaRequired { get; set; }
        public List<string> UploadAllowedClients { get; set; }
        public List<AllowedFormatsUploadList> AllowedFormatsUploadList { get; set; }
        public string TransformTo { get; set; }
    }

    public class EntityProperty
    {
        public string property { get; set; }
        public string value { get; set; }
        public string PropertyName { get; set; }
    }


    public class DocumentDefinitionDataModel
    {
        public Data data { get; set; }
    }


    public class RenderTemplate
    {
        public string name { get; set; }
        public List<string> semanticVersions { get; set; }
    }



    public class TemplateList
    {
        public RenderTemplate RenderTemplate { get; set; }
        public string language { get; set; }
        public string version { get; set; }
    }

    public class Title
    {
        public string language { get; set; }
        public string title { get; set; }
    }



}