using Aria2NET;
using CourseRecorder.Core.Aria2;
using System.Collections.ObjectModel;

namespace CourseRecorder;

public partial class MainPage : ContentPage
{
	int count = 0;
	public MainPage()
	{
		InitializeComponent();
		Logs = new ObservableCollection<string>();
        
        this.BindingContext = this;
	}

    private Aria2NetClient client;

    private string rpc = "ws://127.0.0.1:6800/jsonrpc";

	public string Rpc
	{
		get { return rpc; }
		set { rpc = value; OnPropertyChanged(); }
	}

	private string downloadUrl;

	public string DownloadUrl
	{
		get { return downloadUrl; }
		set { downloadUrl = value; OnPropertyChanged(); }
	}

	private string downloadName;

	public string DownloadName
	{
		get { return downloadName; }
		set { downloadName = value; OnPropertyChanged(); }
	}


	private string gid;

	public string Gid
	{
		get { return gid; }
		set { gid = value; OnPropertyChanged(); }
	}

	private string jsonStr;

	public string JsonStr
	{
		get { return jsonStr; }
		set { jsonStr = value; OnPropertyChanged(); }
	}


	private ObservableCollection<string> logs;

	public ObservableCollection<string> Logs
	{
		get { return logs; }
		set { logs = value; }
	}


	private async void ButtonRPC_Clicked(object sender, EventArgs e)
    {
        client = new Aria2NetClient(Rpc);
		Logs.Add("Connect " + (true ? "成功" : "失败"));
    }

    private async void ButtonDownload_Clicked(object sender, EventArgs e)
    {
        var res = await client.AddUriAsync(new List<string>() { DownloadUrl }, new Dictionary<string, object>() { { "out", DownloadName } });
        Logs.Add(res);
    }

    private async void ButtonEnd_Clicked(object sender, EventArgs e)
    {
        var res = await client.RemoveAsync(Gid);
        Logs.Add(res);
    }

    private async void ButtonSend_Clicked(object sender, EventArgs e)
    {
        //var res = await Aria2Rpc.SendAndReceive(JsonStr);
        //Logs.Add(res);
    }
}

