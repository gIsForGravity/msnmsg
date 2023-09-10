namespace msnmsg.Client;

using Grpc.Core;
using Grpc.Net.Client;
using msnmsg.Protocol;

public partial class MainPage : ContentPage
{

    private const string CLIENT_USERNAME = "User";
    private MsnMsgServer.MsnMsgServerClient _client;
    
    public MainPage()
    {
        var channel = GrpcChannel.ForAddress("http://192.168.137.1:5151");
        _client = new MsnMsgServer.MsnMsgServerClient(channel);
        
        var serverMsgStream = _client.OpenStream(new OpenStreamArgs());
        
        Task.Run(() => LoopRetrieveMessage(serverMsgStream));
        
        InitializeComponent();
    }
    
    void OnEntryCompleted(object sender, EventArgs e)
    {
        Entry entry = sender as Entry;
        string text = entry.Text;
        entry.Text = "";

        _client.SendMessage(new MessageInfo
        {
            Message = text,
            Name = CLIENT_USERNAME
        });
        
    }

    async Task LoopRetrieveMessage(AsyncServerStreamingCall<MessageInfo> messageStream)
    {
        
        MessageInfo? message;

        do
        {
            await messageStream.ResponseStream.MoveNext();
            message = messageStream.ResponseStream.Current;

            if (message != null)
            {
                await MainThread.InvokeOnMainThreadAsync(() => AddMessage($"{message.Name}: {message.Message}"));
            }

        } while (message != null);
    }
    
    private void AddMessage(string msg)
    {
        MessageStack.Children.Add(new Label
        {
            Text = msg,
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Start,
        });
        
        MessageStackView.ScrollToAsync(MessageStack, ScrollToPosition.End, false);
        // MessageBox.Text += msg + "\n";
    }
}