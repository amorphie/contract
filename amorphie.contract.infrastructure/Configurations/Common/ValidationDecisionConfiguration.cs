using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Common
{
    public class ValidationDecisionConfiguration : ConfigurationBase<ValidationDecision>,
       IEntityTypeConfiguration<ValidationDecision>
    {
        public void Configure(EntityTypeBuilder<ValidationDecision> builder)
        {

        }
    }
}