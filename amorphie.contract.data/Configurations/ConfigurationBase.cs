using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Base;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations
{
    public class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.HasKey(x => x.Id);
            builder.HasIndex(e => e.Code).IsUnique();
            var entitypropNavigationList = builder.Metadata.GetNavigations();
            foreach (var entitypropNavigation in entitypropNavigationList)
            {
                var navigationBuilder = new NavigationBuilder(entitypropNavigation);
                navigationBuilder.AutoInclude();
            }
        }
    }
    public class ConfigurationBaseAudiEntity<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AudiEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.HasKey(x => x.Id);
            var entitypropNavigationList = builder.Metadata.GetNavigations();
            foreach (var entitypropNavigation in entitypropNavigationList)
            {
                var navigationBuilder = new NavigationBuilder(entitypropNavigation);
                navigationBuilder.AutoInclude();
            }
        }
    }
}