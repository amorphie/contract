using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentConfiguration : ConfigurationBaseAudiEntity<core.Entity.Document.Document>
    {
        public void Configure(EntityTypeBuilder<core.Entity.Document.Document> builder)
        {

            //       builder.Entity<Document>().HasIndex(item => item.SearchVector).HasMethod("GIN");
            // builder..Entity<Document>().Property(item => item.SearchVector).HasComputedColumnSql(FullTextSearchHelper.GetTsVectorComputedColumnSql("english", new[] { "hangialanlarsearch olacaksa ekle", "componentName", "PageName", "transitionName" }), true);

        }
    }
}