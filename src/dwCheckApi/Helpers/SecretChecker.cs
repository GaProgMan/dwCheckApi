using System.Runtime.CompilerServices;

namespace dwCheckApi.Helpers
{
    public static class SecretChecker
    {
        public static bool CheckUserSuppliedSecretValue(string userSuppliedValue, string secretValue)
        {
            if (string.IsNullOrWhiteSpace(userSuppliedValue) || string.IsNullOrWhiteSpace(secretValue))
            {
                return false;
            }
            
            return FixedTimeStringComparison(userSuppliedValue, secretValue);
        }
        
        /// <summary>
        /// Provides constant time comparison of two strings <paramref name="input"/> and <paramref name="expected"/>
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="expected">The string to compare to</param>
        /// <returns>True if the provided strings are equal, false otherwise</returns>
        /// <remarks>Based on the code found at https://vcsjones.dev/fixed-time-equals-dotnet-core/</remarks>
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool FixedTimeStringComparison(string input, string expected)
        {
            var result = 0;
            for (var i = 0; i < input.Length; i++) {
                result |= input[i] ^ expected[i];
            }
            return result == 0;
        }
    }
}