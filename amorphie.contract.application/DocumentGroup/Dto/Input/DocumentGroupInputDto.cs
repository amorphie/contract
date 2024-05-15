using System.ComponentModel.DataAnnotations;
namespace amorphie.contract.application
{
	public class DocumentGroupInputDto
	{
        [Required]
        public string Code { get; set; }
        public List<DocumentGroupDocumentInputDto> Documents { get; set; }
        public Dictionary<string, string> Titles { get; set; }
    }
}

