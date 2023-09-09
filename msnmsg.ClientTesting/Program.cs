// See https://aka.ms/new-console-template for more information

using System.Net.Security;
using Grpc.Net.Client;
using msnmsg.Protocol;

const string CLIENT_USERNAME = "ClientUser";

Console.WriteLine("connecting to server");
using var channel = GrpcChannel.ForAddress("http://tantleffbeef.co:5151");
var client = new MsnMsgServer.MsnMsgServerClient(channel);

var serverMsgStream = client.OpenStream(new OpenStreamArgs());

while (true)
{
    string? msg = Console.ReadLine();

    if (msg != null)
    {
        Console.WriteLine("sending message: " + msg);
        client.SendMessage(new MessageInfo
        {
            Message = msg,
            Name = CLIENT_USERNAME
        });
        
        Console.WriteLine("Sent!");
    }

    MessageInfo currentStream = serverMsgStream.ResponseStream.Current;
    Console.WriteLine(currentStream.Name + ":\n" + currentStream.Message);

}


// var reply = await client.SayHelloAsync(
//     new HelloRequest { Name = "GreeterClient" });
// Console.WriteLine("Greeting: " + reply.Message);
// Console.WriteLine("Press any key to exit...");
// Console.ReadKey();