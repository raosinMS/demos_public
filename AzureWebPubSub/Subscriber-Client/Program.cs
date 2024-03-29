using Azure.Messaging.WebPubSub;
using System.Net.WebSockets;
using Websocket.Client;

//connection URI schena
// wss://<awps-endpoint>/client/hubs/<hub>?access_token=<access_token>
//URI generated in Azure portal or via TokenTenerator console app

//reconnection URI schema
// wss://<awps-endpoint>/client/hubs/<hub>?awps_connection_id=<connection_id>&awps_reconnection_token=<reconnection_token>
//awps_connection_id and awps_reconnection_token are returned in the response of the initial connection request.
// Message received after successful connection: 
// {
//     "type":"system",
//     "event":"connected",
//     "userId":null,
//     "connectionId":"<awps_connection_id>",
//     "reconnectionToken":"<reconnection_token>"
// }
//DOCS
//https://github.com/Azure/azure-webpubsub/blob/main/protocols/client/client-spec.md#22-reconnection-token
//https://learn.microsoft.com/en-us/azure/azure-web-pubsub/howto-develop-reliable-clients

using (var client = new WebsocketClient(new Uri("wss://<awps-endpoint>/client/hubs/<hub>?access_token=<access_token>"), () =>
{
    var inner = new ClientWebSocket();
    inner.Options.AddSubProtocol("json.reliable.webpubsub.azure.v1"); //reconection token is only returned for reliable protocol
    return inner;
}))
{
    client.ReconnectTimeout = null; //to stay online even if no data comes in
    client.MessageReceived.Subscribe(msg => Console.WriteLine($"Message received: {msg}"));
    await client.StartOrFail();

    Console.WriteLine("Connected.");
    await client.Reconnect();
    
    await client.ReconnectOrFail();
     Console.WriteLine("Reconnected.");
     Console.WriteLine(client.ReconnectionHappened);
    Console.WriteLine(client.IsRunning);
    Console.WriteLine(client.IsStarted);
    await client.Reconnect();
    Console.ReadLine();
}