using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Document.DocumentTypes
{
    public class DocumentUploadConfiguration : ConfigurationBase<DocumentUpload>
    {
        public void Configure(EntityTypeBuilder<DocumentUpload> builder)
        {


        }
    }
}