using Grpc.Core;
using msnmsg.Protocol;
using msnmsg.Server;

namespace msnmsg.Server.Services;

public class MessageServerService : MsnMsgServer.MsnMsgServerBase
{
    private readonly ILogger<MessageServerService> _logger;

    public MessageServerService(ILogger<MessageServerService> logger)
    {
        _logger = logger;
    }

    public override Task<SendMessageResult> SendMessage(MessageInfo message, ServerCallContext context)
    {
        Console.WriteLine($"{message.Name}: {message.Message}");
        
        return Task.FromResult(new SendMessageResult
        {
            Succeeded = true
        });
    }
}