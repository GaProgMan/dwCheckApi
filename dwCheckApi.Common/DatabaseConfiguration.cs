using Microsoft.Extensions.Configuration;

namespace dwCheckApi.Common
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private string CorsPolicyKey = "dwCheckApiConnection";
        public string GetDatabaseConnectionString()
        {
            return GetConfiguration().GetConnectionString(CorsPolicyKey);
        }
    }
}