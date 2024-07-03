using System;
namespace amorphie.contract.application
{
	public class IntegrationRecordInputDto
	{
        public string EngangmentKind { get; set; }
        public int ReferenceId { get; set; }
        public int ReferenceKey { get; set; }
        public string ReferenceName { get; set; }

        public bool IsDysRecord
        {
            get
            {
                return !string.IsNullOrEmpty(ReferenceName) && ReferenceId != 0;
            }
        }

        public bool IsTsizlRecord
        {
            get
            {
                return !string.IsNullOrEmpty(EngangmentKind);
            }
        }
    }
}

