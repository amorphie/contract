using amorphie.contract.core.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace amorphie.contract.data
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: AudiEntity delete }) continue;
                entry.State = EntityState.Modified;
                delete.IsDeleted = true;
                delete.IsActive = false;
            }
            return result;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
         DbContextEventData eventData,
         InterceptionResult<int> result,
         CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: AudiEntity delete }) continue;
                entry.State = EntityState.Modified;
                delete.IsDeleted = true;
                delete.IsActive = false;
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}