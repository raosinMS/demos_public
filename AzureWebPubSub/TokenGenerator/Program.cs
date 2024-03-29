using Azure;
using Azure.Messaging.WebPubSub;

Console.WriteLine("Hello, World!");


var serviceClient = new WebPubSubServiceClient(
        new Uri("https://<endpoint>.webpubsub.azure.com"), 
        "pubsubhub1", 
        new AzureKeyCredential("<KEY>"));


var url = serviceClient.GetClientAccessUri(userId: "client1");
url = serviceClient.GetClientAccessUri(expiresAfter: TimeSpan.FromMinutes(5));
//var url = service.GetClientAccessUri(roles: new string[] { "webpubsub.joinLeaveGroup.group1" });
Console.WriteLine(url);