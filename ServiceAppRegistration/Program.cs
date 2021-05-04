using System;
using System.Threading.Tasks;

namespace ServiceAppRegistration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Get keyvault secret value by getting the authorization token from key vault and passing the particular secret identifier url
            //var serviceBusConnectionString= await KeyVault.GetKeyVaultSecret(Constants.AADServiceApp_Identifier.CLIENTID, Constants.AADServiceApp_Identifier.SECURE_CLIENT_SECRET, Constants.KeyVault_SecretIdentifier.SERVICEBUS_CONNECTIONSTRING);

            var keyVaultClient = KeyVault.GetKeyVaultClient(Constants.AADServiceApp_Identifier.CLIENTID, Constants.AADServiceApp_Identifier.SECURE_CLIENT_SECRET);
            var serviceBusConnectionString = await KeyVault.GetKeyVaultSecret(keyVaultClient, Constants.KeyVault_SecretIdentifier.SERVICEBUS_CONNECTIONSTRING);
            var queueName = await KeyVault.GetKeyVaultSecret(keyVaultClient, Constants.KeyVault_SecretIdentifier.QUEUE_NAME);

            await ServiceBus.SendMessageAsync(serviceBusConnectionString, queueName);
            Console.WriteLine("End of program");
        }        
    }
}
