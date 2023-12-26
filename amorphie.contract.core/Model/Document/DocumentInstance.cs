using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amorphie.contract.core.Model.Document
{
    public class DocumentInstanceModel
    {
        public Guid Id { get; set; }
        [JsonPropertyName("file-type")]
        public string FileType { get; set; }
        [JsonPropertyName("file-byte-array")]
        private byte[] fileByteArray { get; set; }
        public string FileContext { get; set; }
        public string FileContextType { get; set; }
        [JsonPropertyName("file-name")]
        public string FileName { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentVersion { get; set; }
        public string Reference { get; set; }
        public string Owner { get; set; }
        public override string ToString()
        {
            return Id.ToString() +"##"+ Reference +"##"+ DocumentCode +"##"+DocumentVersion +"##"+ FileName;
        }
    }
}