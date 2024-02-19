using amorphie.contract.data.Contexts;
using Moq;
using amorphie.contract.core.Services;
using amorphie.contract.application.Customer;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using amorphie.contract.application.Customer.Request;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document;

namespace amorphie.contract.test;
public class CustomerAppServiceTests
{
    public CustomerAppServiceTests()
    {
        Environment.SetEnvironmentVariable("ELASTIC_APM_ACTIVE", "false");
        Environment.SetEnvironmentVariable("EnableApm", "false");
    }

    [Fact]
    public void FindTitle_ReturnsLabel_WhenLanguageMatchFound()
    {
        var connectionString = "DataSource=:memory:";
        var options = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseSqlite(connectionString)
            .Options;

        var minioServiceMock = new Mock<IMinioService>();

        using var context = new ProjectDbContext(options, null);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        var customerAppService = new CustomerAppService(context, minioServiceMock.Object, null);

        var texts = new List<MultilanguageText>
            {
                new MultilanguageText { Language = "en-EN", Label = "English" },
                new MultilanguageText { Language = "tr-TR", Label = "Turkish" }
            };
        var expectedLabel = "Turkish";

        var result = customerAppService.FindTitle(texts, "tr-TR");

        Assert.Equal(expectedLabel, result);
    }

    [Fact]
    public void FindTitle_ReturnsFirstLabel_WhenLanguageMatchNotFound()
    {
        var connectionString = "DataSource=:memory:";
        var options = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseSqlite(connectionString)
            .Options;

        var minioServiceMock = new Mock<IMinioService>();

        using var context = new ProjectDbContext(options, null);

        var customerAppService = new CustomerAppService(context, minioServiceMock.Object, null);


        var texts = new List<MultilanguageText>
            {
                new MultilanguageText { Language = "en", Label = "English Default" }
            };

        var expectedLabel = "English Default";

        var result = customerAppService.FindTitle(texts, "fr");

        Assert.Equal(expectedLabel, result);
    }

    [Fact]
    public async Task GetDocumentsByContracts_ReturnsDocumentsAsync()
    {
        var configurationMock = new Mock<IConfiguration>();

        var connectionString = "DataSource=:memory:";
        var options = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseSqlite(connectionString)
            .Options;

        var minioServiceMock = new Mock<IMinioService>();

        using var context = new ProjectDbContext(options, configurationMock.Object);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        var customerAppService = new CustomerAppService(context, minioServiceMock.Object, null);

        var inputDto = new GetCustomerDocumentsByContractInputDto
        {
            Code = "test",
            Reference = "123456",
            Page = 0,
            PageSize = 10
        };

        inputDto.SetHeaderParameters("tr-TR", core.Enum.EBankEntity.on);

        var cancellationToken = new CancellationToken();

        var result = await customerAppService.GetDocumentsByContracts(inputDto, cancellationToken);

        Assert.Empty(result);

    }
}