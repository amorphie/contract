using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Documents;
using Elastic.Apm.Api;

namespace amorphie.contract.infrastructure.Contexts
{
    public class DbInitializer
    {
        private static DocumentDefinition MockDocumentDefinition(string code, string semver)
        {
            var mockDocumentDefinition = new DocumentDefinition
            {
                Code = code,
                Semver = semver
            };
            return mockDocumentDefinition;
        }

        private static ContractDefinition MockContractDefinition(string code, EBankEntity eBankEntity)
        {
            var MockContractDefinition = new ContractDefinition
            {
                Code = code,
                BankEntity = eBankEntity
            };
            return MockContractDefinition;
        }
        public static void TemplateCodeReview(ProjectDbContext context)
        {
            var dtd = context.DocumentDefinition.Where(x=>x.DocumentOnlineSing.DocumentTemplateDetails.Any()).ToList();
            foreach (var i in dtd)
            { 
                i.DocumentOnlineSing.Templates = i.DocumentOnlineSing.DocumentTemplateDetails.
                Select(x=>new Template
                {
                    Code = x.DocumentTemplate.Code,
                    LanguageCode = x.DocumentTemplate.LanguageType.Code,
                    Version =x.DocumentTemplate.Version
                }).ToList();
            }
            context.SaveChanges();
        }
        public static void Initialize(ProjectDbContext context)
        {
            // Veritabanı oluşturur.
            //context.Database.EnsureCreated();

            if (!context.DocumentDefinition.Any())
            {
                var mock1 = MockDocumentDefinition("mock-test-01", "1.0.0");
                var mock2 = MockDocumentDefinition("mock-test-01", "1.0.1");
                context.DocumentDefinition.AddRange(new List<DocumentDefinition> { mock1, mock2 });
                context.SaveChanges();

                try
                {
                    context.DocumentDefinition.Add(mock2);
                    context.SaveChanges();
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("Document Code-Semver testi başarılı");
                }
            }

            if (!context.ContractDefinition.Any())
            {
                var mock1 = MockContractDefinition("mock-test-01", EBankEntity.on);
                var mock2 = MockContractDefinition("mock-test-01", EBankEntity.burgan);

                context.ContractDefinition.AddRange(new List<ContractDefinition> { mock1, mock2 });
                context.SaveChanges();

                try
                {
                    context.ContractDefinition.Add(mock2);
                    context.SaveChanges();
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("Contract Code-BankEntity testi başarılı");
                }
            }
        }
    }
}