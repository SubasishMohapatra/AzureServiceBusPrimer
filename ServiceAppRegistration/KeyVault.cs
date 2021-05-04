using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace ServiceAppRegistration
{
    public class KeyVault
    {
        /// <summary>
        /// GetKeyVaultSecret
        /// </summary>
        /// <param name="clientId">Service Principal Client Id</param>
        /// <param name="secureClientSecret">A secret string that the application uses to prove its identity when requesting a token. Also can be referred to as application password. This is configured in the Certificates and Secrets of ServiceApp</param>
        /// <param name="secretIdentifier">Secret Identifier URL of the secret stored in AzureKeyVault</param>
        /// <returns></returns>
        public static async Task<string> GetKeyVaultSecret(string clientId, string secureClientSecret, string secretIdentifier)
        {
            var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
            {
                var adCredential = new ClientCredential(clientId, secureClientSecret);
                var authenticationContext = new AuthenticationContext(authority, null);
                var result = await authenticationContext.AcquireTokenAsync(resource, adCredential);
                return result.AccessToken;
            });
            var result = await keyVaultClient.GetSecretAsync(secretIdentifier);
            return result.Value;
        }

        public static KeyVaultClient GetKeyVaultClient(string clientId, string secureClientSecret)
        {
            return new KeyVaultClient(async (authority, resource, scope) =>
            {
                var adCredential = new ClientCredential(clientId, secureClientSecret);
                var authenticationContext = new AuthenticationContext(authority, null);
                var result = await authenticationContext.AcquireTokenAsync(resource, adCredential);
                return result.AccessToken;
            });
        }

        /// <summary>
        /// GetKeyVaultSecret
        /// </summary>
        /// <param name="clientId">Service Principal Client Id</param>
        /// <param name="secureClientSecret">A secret string that the application uses to prove its identity when requesting a token. Also can be referred to as application password. This is configured in the Certificates and Secrets of ServiceApp</param>
        /// <param name="secretIdentifier">Secret Identifier URL of the secret stored in AzureKeyVault</param>
        /// <returns></returns>
        public static async Task<string> GetKeyVaultSecret(KeyVaultClient keyVaultClient, string secretIdentifier)
        {
            var result = await keyVaultClient.GetSecretAsync(secretIdentifier);
            return result.Value;
        }
    }
}
