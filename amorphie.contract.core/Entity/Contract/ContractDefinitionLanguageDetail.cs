using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.Contract;

namespace amorphie.contract.core.Entity.Contract;

[Table("ContractDefinitionLanguageDetail", Schema = "Cont")]
public class ContractDefinitionLanguageDetail : AudiEntity
{
    [Required]
    public Guid MultiLanguageId { get; set; }
    [ForeignKey("MultiLanguageId")]

    public Entity.Common.MultiLanguage MultiLanguage { get; set; } = default!;
    [Required]
    public Guid ContractDefinitionId { get; set; }
    [ForeignKey("ContractDefinitionId")]

    public ContractDefinition ContractDefinition { get; set; } = default!;
}
