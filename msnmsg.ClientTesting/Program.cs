// See https://aka.ms/new-console-template for more information

using System.Net.Security;
using Grpc.Net.Client;
using msnmsg.Protocol;

Console.WriteLine("connecting to server");
using var channel = GrpcChannel.ForAddress("http://10.251.142.210:5151");
var client = new MsnMsgServer.MsnMsgServerClient(channel);

// var serverMsgStream = client.OpenStream(new OpenStreamArgs());

Console.WriteLine("sending message");
await client.SendMessageAsync(new MessageInfo
{
    Message = "you like peen",
    Name = "balls"
});
Console.WriteLine("message sent");

// var reply = await client.SayHelloAsync(
//     new HelloRequest { Name = "GreeterClient" });
// Console.WriteLine("Greeting: " + reply.Message);
// Console.WriteLine("Press any key to exit...");
// Console.ReadKey();