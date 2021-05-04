using Azure.Messaging.ServiceBus;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace ServiceAppRegistration
{
    public class ServiceBus
    {
        public static async Task SendMessageAsync(string connectionString, string queueName)
        {
            var ctr = 1;
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                Console.WriteLine("Press escape to exit process, else any key to send next message");
                while (!(Console.ReadKey().Key == ConsoleKey.Escape))
                {
                    // create a message that we can send
                    ServiceBusMessage message = new ServiceBusMessage($"message {ctr++}");

                    // send the message
                    await sender.SendMessageAsync(message);
                    Console.WriteLine($"Sent {message.Body} to the queue {queueName}");
                }
            }
        }
    }
}
