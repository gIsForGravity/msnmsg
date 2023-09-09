using Grpc.Core;
using msnmsg.Protocol;
using msnmsg.Server;

namespace msnmsg.Server.Services;

public class ReceiveMessageService : MsnMsgServer.MsnMsgServerBase
{
    private readonly ILogger<ReceiveMessageService> _logger;

    public ReceiveMessageService(ILogger<ReceiveMessageService> logger)
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