using System;
namespace amorphie.contract.application
{
	public class AllowedUploadFormatInputDto
	{
        public Guid Format { get; set; }
        public Guid MaxSizeKilobytes { get; set; }
    }
}

