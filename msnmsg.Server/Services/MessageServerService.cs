using System.Threading.Channels;
using Grpc.Core;
using msnmsg.Protocol;
using msnmsg.Server;

namespace msnmsg.Server.Services;

public class MessageServerService : MsnMsgServer.MsnMsgServerBase
{
    private readonly ILogger<MessageServerService> _logger;
    private List<Channel<MessageInfo>> _userChannels = new();

    public MessageServerService(ILogger<MessageServerService> logger)
    {
        _logger = logger;
    }

    public override async Task<SendMessageResult> SendMessage(MessageInfo message, ServerCallContext context)
    {
        Console.WriteLine($"{message.Name}: {message.Message}");

        foreach (var channel in _userChannels)
        {
            channel.Writer.TryWrite(message);
        }
        
        return new SendMessageResult
        {
            Succeeded = true
        };
    }

    public override async Task OpenStream(OpenStreamArgs request, IServerStreamWriter<MessageInfo> responseStream, ServerCallContext context)
    {
        // create a channel that can be used to send this user messages
        var messageChannel = Channel.CreateUnbounded<MessageInfo>();
        // add to the list of channels
        _userChannels.Add(messageChannel);
        
        await foreach (var message in messageChannel.Reader.ReadAllAsync())
        {
            await responseStream.WriteAsync(message);
        }
    }
}