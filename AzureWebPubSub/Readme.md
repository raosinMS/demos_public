## Publisher-Server - generated messages on hub
## Subscriber-Client - receives messages from hub
## TokenGenerator - generate token to initiate connection


# Connection URI schena:
wss://<awps-endpoint>/client/hubs/<hub>?access_token=<access_token>
URI generated in Azure portal or via TokenTenerator console app

# Reconnection URI schema
wss://<awps-endpoint>/client/hubs/<hub>?awps_connection_id=<connection_id>&awps_reconnection_token=<reconnection_token>
awps_connection_id and awps_reconnection_token are returned in the response of the initial connection request.
Message received after successful connection: 
```json
{
     "type":"system",
     "event":"connected",
     "userId":null,
     "connectionId":"<awps_connection_id>",
     "reconnectionToken":"<reconnection_token>"
 }
```
# DOCS
https://github.com/Azure/azure-webpubsub/blob/main/protocols/client/client-spec.md#22-reconnection-token
https://learn.microsoft.com/en-us/azure/azure-web-pubsub/howto-develop-reliable-clients
