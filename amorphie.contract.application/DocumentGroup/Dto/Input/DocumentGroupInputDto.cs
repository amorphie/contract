using System.ComponentModel.DataAnnotations;
using amorphie.contract.application.Common;

namespace amorphie.contract.application
{
	public class DocumentGroupInputDto
	{
        [Required]
        public string Code { get; set; }
        public List<DocumentGroupDocumentInputDto> Documents { get; set; }
        public Dictionary<string, string> Titles
        {
            get
            {
                return TitleInput.ToDictionary(x => x.Key, x => x.Value);
            }
        }
        public List<TitleInputDto> TitleInput { get; set; }
    }
}

