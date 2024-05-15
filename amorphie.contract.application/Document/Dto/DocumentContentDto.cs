using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application
{
    public class DocumentContentDto
    {
        [Required]
        public required string ContentType { get; set; } = default!;

        [Required]
        public required string FileContext { get; set; } = default!;

        [Required]
        public required string FileName { get; set; } = default!;
    }

}