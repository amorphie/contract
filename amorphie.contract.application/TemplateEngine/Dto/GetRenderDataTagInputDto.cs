using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.application.TemplateEngine.Dto
{
    public class GetRenderDataTagInputDto
    {
        public string DomainName { get; set; }
        public string EntityName { get; set; }
        public string TagName { get; set; }
        public string Reference { get; set; }

        public override string ToString()
        {
            return $"Domain Name: {DomainName}, Entity Name: {EntityName}, Tag Name: {TagName}, Reference: {Reference}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (GetRenderDataTagInputDto)obj;
            return DomainName == other.DomainName &&
                EntityName == other.EntityName &&
                TagName == other.TagName &&
                Reference == other.Reference;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DomainName, EntityName, TagName, Reference);
        }
    }
}