using amorphie.contract.application.Documents.Dto.Zeebe;

namespace amorphie.contract.application
{
    public class GetDocumentsToApproveInputDto : BaseHeader
    {
        public List<DocumentCodesDto> DocumentCodes { get; set; } = default!;
    }

}