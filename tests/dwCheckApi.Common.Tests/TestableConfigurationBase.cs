using Microsoft.Extensions.Configuration;

namespace dwCheckApi.Common.Tests
{
    public abstract class TestableConfigurationBase : ConfigurationBase
    {
        public TestableConfigurationBase()
        {
            JsonFileName = "appsettings.Tests.json";
        }
        public IConfigurationRoot CallGetConfiguration()
        {
            return GetConfiguration();
        }

        public void CallRaiseValueNotFoundException(string key)
        {
            RaiseValueNotFoundException(key);
        }
    }
}