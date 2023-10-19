using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace amorphie.contract.zeebe.Model.DocumentDefinitionDataModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AllowedClient
    {
        public string select { get; set; }
    }

    public class AllowedFormat
    {
        public string format { get; set; }

        [JsonProperty("max-size-kilobytes")]
        public string maxsizekilobytes { get; set; }
    }

    public class Data
    {
        public string header { get; set; }
        public string name { get; set; }
        public List<Title> titles { get; set; }
        public List<Tag> tags { get; set; }

        [JsonProperty("sca-required")]
        public bool scarequired { get; set; }

        [JsonProperty("allowed-clients")]
        public List<AllowedClient> allowedclients { get; set; }

        [JsonProperty("allowed-formats")]
        public List<AllowedFormat> allowedformats { get; set; }
        public List<Optimize> optimize { get; set; }

        [JsonProperty("starting-transition-name")]
        public string startingtransitionname { get; set; }
        public bool documentManuelControl { get; set; }
        public List<TagsOperation> TagsOperation { get; set; }
        public List<EntityProperty> EntityProperty { get; set; }
        public string status { get; set; }

        [JsonProperty("base-status")]
        public string basestatus { get; set; }
    }



    public class EntityProperty
    {
        public string property { get; set; }
        public string value { get; set; }
    }

    public class Optimize
    {
        [JsonProperty("transform-to")]
        public string transformto { get; set; }
        public bool size { get; set; }
    }

    public class DocumentDefinitionDataModel
    {
        public Data data { get; set; }
    }

    public class Tag
    {
        public string tag { get; set; }
        public string Contact { get; set; }
    }

    public class TagsOperation
    {
        public string tag { get; set; }
        public string Contact { get; set; }
    }

    public class Title
    {
        public string language { get; set; }
        public string title { get; set; }
    }


}