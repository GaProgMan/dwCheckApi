using System;
using Microsoft.Extensions.Configuration;

namespace dwCheckApi.Common
{
    public abstract class ConfigurationBase
    {
        protected string JsonFileName = "appsettings.Production.json";
        protected IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(JsonFileName)
                .Build();
        }

        protected void RaiseValueNotFoundException(string configurationKey)
        {
            throw new ValueNotFoundException($"appsettings key ({configurationKey}) could not be found.");
        }
    }
}