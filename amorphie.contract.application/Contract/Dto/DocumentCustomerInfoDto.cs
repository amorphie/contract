namespace amorphie.contract.application.Contract.Dto
{
    public record DocumentCustomerInfoDto
    {
        public Guid DocumentDefinitionId { get; init; }
        public string DocumentCode { get; init; }
        public string SemVer { get; init; }
        public bool IsSigned { get; init; }
        public Guid? DocumentInstanceId { get; set; }
        public DocumentOnlineSignDto DocumentOnlineSign { get; set; }
        public Dictionary<string, string> Titles { get; set; } = default!;

    }
}
