using Microsoft.Extensions.Configuration;

namespace dwCheckApi.Common
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private string DbConnectionKey = "dwCheckApiConnection";
        public string GetDatabaseConnectionString()
        {
            return GetConfiguration().GetConnectionString(DbConnectionKey);
        }
    }
}