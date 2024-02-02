using System;
using amorphie.core.Base;

namespace amorphie.contract.application.Customer.Dto
{
	public class CustomerContractDocumentGroupDto
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string DocumentGroupStatus { get; set; } = AppConsts.NotValid;
		public ushort AtLeastRequiredDocument { get; set; }
        public bool Required { get; set; }
        public List<CustomerContractDocumentDto> CustomerContractGroupDocuments { get; set; }
        public List<MultilanguageText> MultiLanguageText { get; set; }
	}
}

