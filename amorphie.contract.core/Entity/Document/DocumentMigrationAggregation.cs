using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Model.Documents;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentMigrationAggregations", Schema = "Doc")]
    public class DocumentMigrationAggregation : EntityBase, ISoftDelete
    {
        public string DocumentCode { get; set; } = default!;
        public IList<DocumentMigrationContractModel> ContractCodes { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }

}