using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceAppRegistration
{
    public class Constants
    {
        public class ServiceBus
        {
            public const string QUEUE_NAME= "testqueue";            
        }
        public class AADServiceApp_Identifier
        {
            public const string CLIENTID= "Service App Client ID";
            public const string SECURE_CLIENT_SECRET = "Service App Client Secret";
        }

        public class KeyVault_SecretIdentifier
        {
            public const string SERVICEBUS_CONNECTIONSTRING = "address of keyvault secret URL containing service bus connection string";
            public const string QUEUE_NAME = "address of keyvault secret URL containing queuename of service bus";
        }
    }
}
