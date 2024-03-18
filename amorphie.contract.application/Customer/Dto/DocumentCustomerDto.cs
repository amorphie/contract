namespace amorphie.contract.application.Customer.Dto
{

    public class DocumentCustomerDto
    {
        public string Code { get; set; }
        public string Semver { get; set; }
        public string Status { get; set; }
        public string MinioUrl { get; set; }
        public string MinioObjectName { get; set; }
        public string Reference { get; set; }
    }
}

