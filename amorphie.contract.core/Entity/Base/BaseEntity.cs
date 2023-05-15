namespace amorphie.contract.core.Entity.Base
{
    public abstract class BaseEntity: BaseEntityWithOutId, IHasKey
    {
        // [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


    }
}