using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentOptimizeTypeConfiguration : ConfigurationBase<DocumentOptimizeType>,
             IEntityTypeConfiguration<DocumentOptimizeType>

    {
        public void Configure(EntityTypeBuilder<DocumentOptimizeType> builder)
        {


        }
    }
}