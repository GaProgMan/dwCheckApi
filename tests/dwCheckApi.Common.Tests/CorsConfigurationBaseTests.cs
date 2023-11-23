using NSubstitute;

namespace dwCheckApi.Common.Tests
{
    public class CorsConfigurationBaseTests
    {
        private TestableCorsConfiguration _corsConfigurationMock = Substitute.For<TestableCorsConfiguration>();

        [Fact]
        public void GetCorsPolicyName_ValidPolicyNameKey_ReturnsNonEmptyCorsPolicyName()
        {
            var policyName = _corsConfigurationMock.CallGetCorsPolicy();
            Assert.IsAssignableFrom<string>(policyName);
            Assert.NotEmpty(policyName);
        }

        [Fact]
        public void GetCorsPolicyName_InvalidPolicyNameKey_RaisesValueNotFoundException()
        {
            const string nonsensePolicyName = "nonsense";
            _corsConfigurationMock = Substitute.For<TestableCorsConfiguration>();
            var exception =
                Record.Exception(() => _corsConfigurationMock.CallGetCorsPolicy(nonsensePolicyName));
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<ValueNotFoundException>(exception);
        }
    }
}