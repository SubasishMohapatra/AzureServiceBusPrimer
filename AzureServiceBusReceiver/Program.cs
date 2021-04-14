using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace AzureServiceBusReceiver
{
    class Program
    {
        static string connectionString = "ServiceBusNamespaceConnectionString";
        static string queueName = "test";

        static async Task Main()
        {
            await ReceiveMessagesAsync();
        }

        static async Task ReceiveMessagesAsync()
        {
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a processor that we can use to process the messages
                ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Press escape to exit processing messages");
                while (1==1) {
                    var input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                // stop processing 
                Console.WriteLine("Stopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
        }

        // handle received messages
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
