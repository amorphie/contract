using NetArchTest.Rules;

namespace amorphie.contract.test;

public class DependencyCheck
{
    [Fact]
    public void DataToCoreCheck()
    {
        /*
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("amorphie.contract.core")
            .Should()
            .BeSealed()
            .GetResult()
            .IsSuccessful;

            Assert.True(result);
            */
    }
}