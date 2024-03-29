using System;
using System.Threading.Tasks;
using Azure.Messaging.WebPubSub;

namespace publisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Endpoint=https://<endpoint>.webpubsub.azure.com;AccessKey=<KEY>;Version=1.0;";
            var hub = "pubsubhub1";

            // Either generate the token or fetch it from server or fetch a temp one from the portal
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                var message = $"Hello, WebPubSub! Message number: {i}";
                await serviceClient.SendToAllAsync(message);
                await Task.Delay(6000);
                Console.WriteLine($"Sent message number: {i} Passed: {watch.Elapsed}");
            }
            watch.Stop();            
        }
    }
}