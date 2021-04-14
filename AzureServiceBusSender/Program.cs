using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace AzureServiceBusSender
{
    class Program
    {
        static string connectionString = "ServiceBusNamespaceConnectionString";
        static string queueName = "test";

        static async Task Main()
        {
            Console.WriteLine("Message sender started...");
            await SendMessageAsync();
        }

        static async Task SendMessageAsync()
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
