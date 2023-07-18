using System.Globalization;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;

namespace amorphie.contract.data.Contexts
{
    public class DbInitializer
    {
        public static void Initialize(ProjectDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();

            if (!context.DocumentSize.Any())
            {
                context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 512 });
                context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 1024 });
                context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 2048 });
                context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 4096 });
                var b = context.SaveChanges();
            }
            if (!context.DocumentType.Any())
            {
                context.DocumentType.Add(new core.Entity.Document.DocumentType { Name = "pdf", ContentType = "application/pdf" });
                context.DocumentType.Add(new core.Entity.Document.DocumentType { Name = "doc", ContentType = "application/doc" });
                context.DocumentType.Add(new core.Entity.Document.DocumentType { Name = "xlsx", ContentType = "application/xlsx" });
                var c = context.SaveChanges();
            }
            
            if (!context.LanguageType.Any())
            {
                var tr = context.LanguageType.Add(new core.Entity.Common.LanguageType { Name = "TR" }).Entity.Id;
                var en = context.LanguageType.Add(new core.Entity.Common.LanguageType { Name = "EN" }).Entity.Id;
                var fr = context.LanguageType.Add(new core.Entity.Common.LanguageType { Name = "FR" }).Entity.Id;
                var bb = context.SaveChanges();

            }

            // if (!context.MultiLanguage.Any())
            // {
            //     var trm = context.MultiLanguage.Add(new core.Entity.Common.MultiLanguage
            //     { Name = "Nufus Cuzdani", Code = "identification-certificate-nc", LanguageTypeId = tr }).Entity.Id;
            //     context.MultiLanguage.Add(new core.Entity.Common.MultiLanguage
            //     { Name = "Birth Certificate", Code = "identification-certificate-nc", LanguageType = en });
            //     context.MultiLanguage.Add(new core.Entity.Common.MultiLanguage
            //     { Name = "Certificat de naissance", Code = "identification-certificate-nc", LanguageType = fr });

            //     if (!context.DocumentDefinitionLanguageDetail.Any())
            //     {
            //         context.DocumentDefinitionLanguageDetail.Add(
            //             new core.Entity.Document.DocumentDefinitionLanguageDetail
            //             { MultiLanguage = trm,]);

            //     }
            // }

            if (!context.DocumentDefinition.Any())
            {
                var dd = new DocumentDefinition
                {
                    Code = "identification-certificate-nc"
                };
                var ddld = new DocumentDefinitionLanguageDetail
                {
                    MultiLanguage = new core.Entity.Common.MultiLanguage
                    {
                        LanguageType = context.LanguageType.FirstOrDefault(x=>x.Name == "TR") == null ? new core.Entity.Common.LanguageType {
                            Name = "TR"
                        }:context.LanguageType.FirstOrDefault(x=>x.Name == "TR")
                              ,
                        Code = "identification-certificate-nc",
                        Name = "Nufus Cuzdani",
                    }
                          ,
                    DocumentDefinition = dd
                };
                dd.DocumentDefinitionLanguageDetails.Add(ddld);
                context.DocumentDefinition.Add(dd);
                //     DocumentDefinitionLanguageDetails.Add(
                //   

                var a = context.SaveChanges();
            }
        }
    }
}