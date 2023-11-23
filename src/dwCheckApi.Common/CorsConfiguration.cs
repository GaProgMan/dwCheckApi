namespace dwCheckApi.Common
{
    public class CorsConfiguration : ConfigurationBase
    {
        protected string CorsPolicyKey = "CorsPolicy:name";
        public string GetCorsPolicyName()
        {
            var section = GetConfiguration().GetSection(CorsPolicyKey);
            if (section == null || string.IsNullOrEmpty(section.Value))
            {
                RaiseValueNotFoundException(CorsPolicyKey);
            }
            return section!.Value;
        }
    }
}