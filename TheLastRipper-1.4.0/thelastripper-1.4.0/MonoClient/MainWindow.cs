using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public LibLastRip.LastManager LastManager;
	
	protected MonoClient.Settings settings;

	public MainWindow (): base ("")
	{	
		//Let Stetic generate the GUI
		this.Build();
		
		
		this.settings = MonoClient.Settings.Restore();
		this.LastManager = this.settings.Manager;
		if(this.LastManager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
		{
			this.ConnectButton.Sensitive = true;
			this.StationBox.Sensitive = true;
		}
		
		this.LastManager.OnNewSong += new System.EventHandler(this.OnNewSong);

		//TODO: handle this.LastManager.OnScanning
		
		//Handle command return
		this.LastManager.CommandReturn += new System.EventHandler(this.OnCommandReturn);
		
		//Handle station change return
		this.LastManager.StationChanged += new System.EventHandler(this.OnStationChanged);
		
		//Handle update progressbar
		this.LastManager.OnProgress += new System.EventHandler(this.UpdateProgress);
		
		//Handle expected exceptions
		this.LastManager.OnError += new System.EventHandler(this.OnError);
		
		//Handle finised recordings
		this.LastManager.SongCompleted += new EventHandler<LibLastRip.SongCompletedEventArgs>(this.SongCompleted);
	}

	//Process for applying replaygain
	private System.Diagnostics.Process replayGain;
	
	/// <summary>
	/// Handles song completed, e.g. initiate upload and applies mp3gain
	/// </summary>
	private void SongCompleted(System.Object Sender, LibLastRip.SongCompletedEventArgs args){
		//Apply mp3gain
		if(this.settings.ApplyRegain){
			Console.WriteLine("Applies ReplayGain");
			//Kill it if still running, as it shouldn't be!
			if(this.replayGain != null){
				if(!this.replayGain.HasExited)
					this.replayGain.Kill();
				this.replayGain.Close();
			}
			this.replayGain = new System.Diagnostics.Process();
			this.replayGain.StartInfo.FileName = "mp3gain";
			this.replayGain.StartInfo.Arguments = "-T -f \"" + args.Filename + "\"";
			this.replayGain.Start();
		}
		//Upload to mp3tunes
		if(this.settings.UploadToLocker && this.settings.Locker.IsLoggedin){
			Console.WriteLine("Starts uploading");
			this.settings.Locker.PutTrack(args.Filename);
		}
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected virtual void OnError(System.Object Sender, System.EventArgs e)
	{
		Gtk.Application.Invoke( delegate{
		LibLastRip.ErrorEventArgs Args = (LibLastRip.ErrorEventArgs)e;
		Gtk.MessageDialog MD = new Gtk.MessageDialog(
		                                            this,
		                                            Gtk.DialogFlags.DestroyWithParent,
		                                            Args.Exception != null ? Gtk.MessageType.Error : Gtk.MessageType.Warning,
		                                            Gtk.ButtonsType.Ok,
		                                            Args.Message + (Args.Exception != null ? "\nException:\n" + Args.Exception.ToString() : ""));
		MD.Run();
		MD.Destroy();
		});
	}
	
	protected virtual void OnConnectButtonClicked(object sender, System.EventArgs e)
	{
		String param = System.Uri.UnescapeDataString(this.LastFMStationEntry.Text.Replace(' ', '+'));
		String uri = "lastfm://";
		switch(this.StationType.ActiveText){
		 case "Tag:":
			uri += "globaltags/" + param;
			break;
		 case "Similar artist:":
			uri += "artist/" + param + "/similarartists";
			break;
		case "Playlist:":
			uri += "user/" + param + "/playlist";
			break;
		case "Personal:":
			uri += "user/" + param + "/personal";
			break;
		case "Loved:":
			uri += "user/" + param + "/loved";
			break;
		case "Recommended:":
			uri += "user/" + param + "/recommended/100";
			break;
		case "Neighbours:":
			uri += "user/" + param + "/neighbours";
			break;
		case "Group:":
				uri += "group/" + param;
			break;
		case "lastfm://":
			if(!this.LastFMStationEntry.Text.StartsWith("lastfm://"))
				uri += this.LastFMStationEntry.Text;
			else
				uri = this.LastFMStationEntry.Text;
			break;
		}

		this.LastManager.ChangeStation(uri);
		
		//Disable button
		this.ConnectButton.Sensitive = false;
	}
	
	///<summary>Handles callback from a change station command</summary>
	protected virtual void OnStationChanged(System.Object Sender, System.EventArgs Args)
	{
		Gtk.Application.Invoke( delegate {
		this.ConnectButton.Sensitive = true;
		LibLastRip.StationChangedEventArgs sArgs = (LibLastRip.StationChangedEventArgs)Args;
		
		if(sArgs.Success)
		{
			this.EnableCommands();
		}
		});
	}
	
	protected System.Int32 TrackDuration = 0;
	
	//TODO: Use event from LastManager
	protected virtual void UpdateProgress(System.Object Sender, System.EventArgs e)
	{
		Gtk.Application.Invoke( delegate{
		//Avoid zero-division error
		if(this.TrackDuration > 1)
		{
			LibLastRip.ProgressEventArgs Args = (LibLastRip.ProgressEventArgs)e;
			System.Double Frac = ((System.Double) Args.Streamprogress) / ((System.Double)this.TrackDuration);
			if(Frac <= 1 && Frac >= (1/System.Double.MaxValue))
			{
				this.SongProgressBar.Fraction = Frac;
			}else{
				this.SongProgressBar.Fraction = 0;
			}
		}
		});
	}
	
	protected LibLastRip.MetaInfo NewSong;
	protected System.Boolean IsNewSong = false;
	
	private void CoverDownloaded(System.Object Sender, System.Net.DownloadDataCompletedEventArgs args){
		if(!args.Cancelled && args.Error == null){
			Gtk.Application.Invoke(delegate {
				this.CoverBox.FromPixbuf = new Gdk.Pixbuf(args.Result);
			});
		}
		//try{
			System.Net.WebClient waste = (System.Net.WebClient)args.UserState;
			waste.Dispose();
		//}
		//catch{}
	}	
	
	private System.Net.WebClient CoverClient;
	protected virtual void OnNewSong(System.Object Sender, System.EventArgs Args)
	{
		Gtk.Application.Invoke( delegate {
			LibLastRip.MetaInfo Info = (LibLastRip.MetaInfo)Args;
			
			//We don't want an error is string is formatet wrong.
			try{
				this.TrackDuration = System.Convert.ToInt32(Info.Trackduration);
			}
			catch{
				this.TrackDuration = 250; //Just a wild guess ;)
			}
			
			if(!Info.isEmpty())
			{
				System.String StrText = "";
				StrText += "<span size='x-large'>"+ GLib.Markup.EscapeText(Info.Track.Replace("&","and")) + "</span>\n";
				StrText += "<b>By: </b>" + GLib.Markup.EscapeText(Info.Artist.Replace("&","and")) + "\n";
				StrText += "<b>Album: </b>"+ GLib.Markup.EscapeText(Info.Album.Replace("&","and")) + "\n";
				StrText += "<b>Length: </b>"+ GLib.Markup.EscapeText(Info.Trackduration.Replace("&","and")) + " seconds\n";
				StrText += "<i>From: " + GLib.Markup.EscapeText(Info.Station.Replace("&","and")) + "</i>";
				this.StatusLabel.Markup = StrText;
				
				if(Info.Albumcover != null && Info.Albumcover.StartsWith("http://"))
				{
					try
					{
						if(this.CoverClient != null && this.CoverClient.IsBusy)
								this.CoverClient.CancelAsync();
						this.CoverClient = new System.Net.WebClient();
						this.CoverClient.DownloadDataCompleted += new System.Net.DownloadDataCompletedEventHandler(this.CoverDownloaded);
						this.CoverClient.DownloadDataAsync(new System.Uri(Info.Albumcover), this.CoverClient);
					}
					catch
					{}
				}
			}else{
				//TODO: do something when we're not steaming... disable some controls etc...
			}
		});
	}



	protected virtual void OnSkipButtonClicked(object sender, System.EventArgs e)
	{
		//Execute command and disable command buttons to prevent multiple commands
		this.LastManager.SkipSong();
		this.DisableCommands();
	}

	protected virtual void OnLoveButtonClicked(object sender, System.EventArgs e)
	{
		//Execute command and disable command buttons to prevent multiple commands
		this.LastManager.LoveSong();
		this.DisableCommands();
	}

	protected virtual void OnBanButtonClicked(object sender, System.EventArgs e)
	{
		//Execute command and disable command buttons to prevent multiple commands
		this.LastManager.BanSong();
		this.DisableCommands();
	}

	///<summary>
	///Handles callbacks from executed commands
	///</summary>
	protected virtual void OnCommandReturn(System.Object Sender, System.EventArgs Args)
	{
		Gtk.Application.Invoke( delegate {
		//Enable commands again
		this.EnableCommands();
		});
	}
	
	protected virtual void OnOnlineHelpActivated(object sender, System.EventArgs e)
	{
		//TODO: find correct adress once the homepage is up
		System.Diagnostics.Process.Start("xdg-open","http://code.google.com/p/thelastripper/wiki/HelpLinux");
	}

	protected virtual void OnExitActivated(object sender, System.EventArgs e)
	{
		Application.Quit();
	}

	protected virtual void OnAboutActivated(object sender, System.EventArgs e)
	{
		MonoClient.About dlg = new MonoClient.About();
		dlg.Show();
	}

	protected virtual void OnPreferencesActivated(object sender, System.EventArgs e)
	{
		this.settings.LaunchPreferences();
		if(this.LastManager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
		{
			this.ConnectButton.Sensitive = true;
			this.StationBox.Sensitive = true;
		}
	}

	protected virtual void OnLegalNoticeActivated(object sender, System.EventArgs e)
	{
		System.Diagnostics.Process.Start("xdg-open","http://code.google.com/p/thelastripper/wiki/LegalNotice");
	}

	protected virtual void OnGeneratePlaylistClick(object sender, System.EventArgs e)
	{
			this.settings.Generate();
	}
	
	///<summary>
	///Disables all command buttons 
	///</summary>
	protected virtual void DisableCommands()
	{
		this.LoveButton.Sensitive = false;
		this.BanButton.Sensitive = false;
		this.SkipButton.Sensitive = false;
	}
	
	///<summary>
	///Enables all command buttons 
	///</summary>
	protected virtual void EnableCommands()
	{
		this.LoveButton.Sensitive = true;
		this.BanButton.Sensitive = true;
		this.SkipButton.Sensitive = true;
	}

	protected virtual void OnStationTypeChanged (object sender, System.EventArgs e)
	{
		switch(this.StationType.ActiveText){
		 case "Playlist:":
		 case "Personal:":
		 case "Loved:":
		 case "Recommended:":
		 case "Neighbours:":
			this.LastFMStationEntry.Text = this.LastManager.UserName;
			break;
		 case "Tag:":
			this.LastFMStationEntry.Text = "rock";
			break;
		 case "Similar artist:":
			this.LastFMStationEntry.Text = "Artist...";
			break;
		 case "Group:":
			this.LastFMStationEntry.Text = "Group name...";
			break;
		}
	}
}