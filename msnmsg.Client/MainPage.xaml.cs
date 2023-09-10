namespace msnmsg.Client;

using Grpc.Core;
using Grpc.Net.Client;
using msnmsg.Protocol;

public partial class MainPage : ContentPage
{

    private ClientBase<MsnMsgServer.MsnMsgServerClient> _client;
    
    public MainPage()
    {
        using var channel = GrpcChannel.ForAddress("http://192.168.137.1:5151");
        _client = new MsnMsgServer.MsnMsgServerClient(channel);
        InitializeComponent();
    }
    
    void OnEntryCompleted(object sender, EventArgs e)
    {
        Entry entry = sender as Entry;
        string text = entry.Text;
        entry.Text = "";

        AddMessage(text);
    }

    private void AddMessage(string msg)
    {
        MessageBox.Text += msg + "\n";
    }
}