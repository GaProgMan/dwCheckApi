namespace dwCheckApi.Common.Tests
{
    public abstract class TestableCorsConfiguration : CorsConfiguration
    {
        public string CallGetCorsPolicy(string? corsPolicyName = null)
        {
            if (!string.IsNullOrEmpty(corsPolicyName))
            {
                CorsPolicyKey = corsPolicyName;
            }

            return GetCorsPolicyName();
        }
    }
}