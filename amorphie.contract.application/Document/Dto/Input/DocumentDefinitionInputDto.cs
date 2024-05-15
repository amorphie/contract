using System;
using amorphie.contract.core.Model;

namespace amorphie.contract.application
{
	public class DocumentDefinitionInputDto
	{
		public string DocumentType { get; set; }
        public string Code { get; set; }
        public string StartingTransitionName { get; set; }
        public string Version { get; set; }
        public bool DocumentManuelControl { get; set; }
        public bool SizeOptimize { get; set; }
        public bool ScaRequired { get; set; }
        public Guid TransformTo { get; set; }
        public IntegrationRecordInputDto IntegrationRecord { get; set; }
        public List<Guid> Tags { get; set; }
        public List<Guid> RenderAllowedClients { get; set; }
        public List<Guid> UploadTags { get; set; }
        public List<Guid> UploadAllowedClients { get; set; }
        public Dictionary<string, string> Titles { get; set; }
        public List<Metadata> Metadatas { get; set; }
        public List<TemplateInputDto> Templates { get; set; }
        public List<AllowedUploadFormatInputDto> AllowedUploadFormats { get; set; }

	}
}

