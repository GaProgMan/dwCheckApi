using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace dwCheckApi.Common.Tests
{
    public class ConfigurationBaseTests
    {
        private TestableConfigurationBase _configurationBaseMock = Substitute.For<TestableConfigurationBase>();

        [Fact]
        public void GetConfiguration_ReturnsIConfigurationRoot()
        {
            var result = _configurationBaseMock.CallGetConfiguration();
            Assert.IsAssignableFrom<IConfigurationRoot>(result);
        }

        [Fact]
        public void RaiseValueNotFoundException_ThrowsValueNotFoundException()
        {
            const string keyToSearch = "NonExistentKey";
            var exception =
                Record.Exception(() => _configurationBaseMock.CallRaiseValueNotFoundException(keyToSearch));
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<ValueNotFoundException>(exception);
            Assert.Equal($"appsettings key ({keyToSearch}) could not be found.", exception.Message);
        }
    }
}