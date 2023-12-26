using System.Globalization;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.data.Extensions;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Model.Contract;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using amorphie.contract.core.Enum;

namespace amorphie.contract.data.Contexts
{
    public class DbInitializer
    {
        public static void Initialize(ProjectDbContext context)
        {
            // try
            // {

                 
            //     var def = new DocumentDefinition();
            //     def.Code = "test";
            //     def.Status = EStatus.Active;
            
            //     def.Semver = "1.0.0";
            //     def.BaseStatus = EStatus.Active;

            //     var def2 = new DocumentDefinition();
            //     def2.Semver = "1.0.0";
            //     def2.Code = "test2";
            //     def2.Status=EStatus.Active;
            //     def2.BaseStatus=EStatus.Active;
            //     context.DocumentDefinition.Add(def);
            //     context.DocumentDefinition.Add(def2);
            //     context.SaveChanges();
            //     var cont = new ContractDefinition();
            //     cont.Code = "test";
            //     cont.Status=EStatus.Active;
            //     var co = new ContractDocumentDetail
            //     {
            //         ContractDefinitionId = cont.Id,
            //         DocumentDefinitionId = def.Id,
            //         Required = false,
            //         UseExisting = 0
            //     };
            //     var co2 = new ContractDocumentDetail
            //     {
            //         ContractDefinitionId = cont.Id,
            //         DocumentDefinitionId = def2.Id,
            //         Required = false,
            //         UseExisting = 0
            //     };
            //     var a = new List<ContractDocumentDetail>
            // {
            //     co,
            //     co2
            // };

            //     cont.ContractDocumentDetails = a;
            //     context.ContractDefinition.Add(cont);
            //     context.SaveChanges();
            // }
            // catch (Exception e)
            // {
            //     var a = e;
            // }

            var query = context.ContractDefinition

            .FirstOrDefault(x => x.Code == "test");

            if (query != null)
            {
                // var documentdeflist = context.DocumentDefinition
                //     .Where(x => query.ContractDocumentDetails
                //         .Any(a => a.DocumentDefinitionId == x.Id))
                //     .ToList();
                var documentdeflist =  query.ContractDocumentDetails.Select(document=>new DocumentModel
                        {

                            Code = document.DocumentDefinition.Code,
                            Status = "not-started",
                            Required = document.Required,
                            Render = document.DocumentDefinition.DocumentOnlineSing != null,
                            Version = document.DocumentDefinition.Semver,
                            // UseExisting = document.UseExisting

                        }).ToList();
                ContractModel contractModel = new ContractModel();
                // contractModel.Id = query.Id;
                contractModel.Status = "in-progress";
                contractModel.Document = documentdeflist;
            }

            // context.Database.EnsureCreated();
            // context.SaveChanges();

            // if (!context.DocumentSize.Any())
            // {
            //     context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 512 });
            //     context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 1024 });
            //     context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 2048 });
            //     context.DocumentSize.Add(new core.Entity.Document.DocumentSize { KiloBytes = 4096 });
            //     var b = context.SaveChanges();
            // }
            // if (!context.DocumentType.Any())
            // {
            //     context.DocumentType.Add(new core.Entity.Document.DocumentType { Name = "pdf", ContentType = "application/pdf" });
            //     context.DocumentType.Add(new core.Entity.Document.DocumentType { Name = "doc", ContentType = "application/doc" });
            //     context.DocumentType.Add(new core.Entity.Document.DocumentType { Name = "xlsx", ContentType = "application/xlsx" });
            //     var c = context.SaveChanges();
            // }

            // if (!context.LanguageType.Any())
            // {
            //     var tr = context.LanguageType.Add(new core.Entity.Common.LanguageType { Name = "TR" }).Entity.Id;
            //     var en = context.LanguageType.Add(new core.Entity.Common.LanguageType { Name = "EN" }).Entity.Id;
            //     var fr = context.LanguageType.Add(new core.Entity.Common.LanguageType { Name = "FR" }).Entity.Id;
            //     var bb = context.SaveChanges();

            // }

            // // if (!context.MultiLanguage.Any())
            // // {
            // //     var trm = context.MultiLanguage.Add(new core.Entity.Common.MultiLanguage
            // //     { Name = "Nufus Cuzdani", Code = "identification-certificate-nc", LanguageTypeId = tr }).Entity.Id;
            // //     context.MultiLanguage.Add(new core.Entity.Common.MultiLanguage
            // //     { Name = "Birth Certificate", Code = "identification-certificate-nc", LanguageType = en });
            // //     context.MultiLanguage.Add(new core.Entity.Common.MultiLanguage
            // //     { Name = "Certificat de naissance", Code = "identification-certificate-nc", LanguageType = fr });

            // //     if (!context.DocumentDefinitionLanguageDetail.Any())
            // //     {
            // //         context.DocumentDefinitionLanguageDetail.Add(
            // //             new core.Entity.Document.DocumentDefinitionLanguageDetail
            // //             { MultiLanguage = trm,]);

            // //     }
            // // }

            // if (!context.DocumentDefinition.Any())
            // {
            //     var dd = new DocumentDefinition
            //     {
            //         Code = "identification-certificate-nc"
            //     };
            //     var ddld = new DocumentDefinitionLanguageDetail
            //     {
            //         MultiLanguage = new core.Entity.Common.MultiLanguage
            //         {
            //             LanguageType = DBExtensions.LanguageTypeCreateOrGet("TR"),
            //             Code = "identification-certificate-nc",
            //             Name = "Nufus Cuzdani",
            //         },
            //         DocumentDefinition = dd
            //     };
            //     dd.DocumentDefinitionLanguageDetails.Add(ddld);
            //     // dd.DocumentTags.Add(new DocumentTag{

            //     // })
            //     context.DocumentDefinition.Add(dd);

            //     var a = context.SaveChanges();
            // }
        }
    }
}