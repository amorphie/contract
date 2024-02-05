using System;
namespace amorphie.contract.application.Customer.Request
{
    public class GetCustomerDocumentsByContractInputDto
    {
        public string? Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Reference { get; set; }
    }
}

