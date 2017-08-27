namespace dwCheckApi.Common
{
    public class CorsConfiguration : ConfigurationBase
    {
        public string GetCorsPolicyName()
        {
            var section = GetConfiguration().GetSection("CorsPolicy:name");

            return section?["name"];

        }
    }
}