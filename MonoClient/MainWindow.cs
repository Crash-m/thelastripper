using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	protected const System.Int32 UpdateInterval = 10000;
	public LibLastRip.LastManager LastManager;
	public System.Timers.Timer Timer = new System.Timers.Timer(MainWindow.UpdateInterval);
	
	protected MonoClient.Settings settings;
	
	public MainWindow (): base ("")
	{	
		//Let Stetic generate the GUI
		this.Build();
		
		
		this.settings = MonoClient.Settings.Restore();
		this.LastManager = this.settings.Manager;
		if(this.LastManager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
		{
			this.LastFMStationEntry.Sensitive = true;
			this.ConnectButton.Sensitive = true;
		}
		
		this.LastManager.OnNewSong += new System.EventHandler(this.OnNewSong);
		this.Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.LastManager.UpdateMetaInfo);
		
		//Handle command return
		this.LastManager.CommandReturn += new System.EventHandler(this.OnCommandReturn);
		
		//Handle station change return
		this.LastManager.StationChanged += new System.EventHandler(this.OnStationChanged);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected System.Boolean IsStarted = false;
	
	protected virtual void OnConnectButtonClicked(object sender, System.EventArgs e)
	{
		this.LastManager.ChangeStation(this.LastFMStationEntry.Entry.Text);
		
		//Disable button
		this.ConnectButton.Sensitive = false;
	}
	
	///<summary>Handles callback from a change station command</summary>
	protected virtual void OnStationChanged(System.Object Sender, System.EventArgs Args)
	{
		this.ConnectButton.Sensitive = true;
		LibLastRip.StationChangedEventArgs sArgs = (LibLastRip.StationChangedEventArgs)Args;
		
		if(sArgs.Success)
		{
			this.Timer.Start();
			this.EnableCommands();
			if(!this.IsStarted)
			{
				GLib.Timeout.Add(2000, new GLib.TimeoutHandler(this.UpdateProgressTime));
				this.IsStarted = true;
			}
		}
	}
	
	protected System.Int32 TrackDuration = 0;
	
	protected virtual System.Boolean UpdateProgressTime()
	{
		//Only if CurrentSong exists
		if(this.LastManager.CurrentSong != null)
		{
			this.TrackDuration += 2000/1000;
			System.Double Frac = ((System.Double)this.TrackDuration / System.Convert.ToDouble(this.LastManager.CurrentSong.Trackduration));
			if(Frac <= 1 && Frac >= (1/System.Double.MaxValue))
			{
				this.SongProgressBar.Fraction = Frac;
			}else{
				this.SongProgressBar.Fraction = 0;
			}
		}
		
		return true;
	}
	
	protected LibLastRip.MetaInfo NewSong;
	protected System.Boolean IsNewSong = false;
	
	protected virtual void OnNewSong(System.Object Sender, System.EventArgs Args)
	{
		LibLastRip.MetaInfo Info = (LibLastRip.MetaInfo)Args;
		this.SetStatus(Info);
	}
	
	protected virtual void SetStatus(LibLastRip.MetaInfo Info)
	{
		this.TrackDuration = 0;
		
		if(Info.Streaming)
		{
			System.String StrText = "";
			StrText += "<span size='x-large'>"+ GLib.Markup.EscapeText(Info.Track) + "</span>\n";
			StrText += "<b>By: </b>" + GLib.Markup.EscapeText(Info.Artist) + "\n";
			StrText += "<b>Album: </b>"+ GLib.Markup.EscapeText(Info.Album) + "\n";
			StrText += "<b>Length: </b>"+ GLib.Markup.EscapeText(Info.Trackduration) + " seconds\n";
			StrText += "<i>From: " + GLib.Markup.EscapeText(Info.Station) + "</i>";
			this.StatusLabel.Markup = StrText;
			
			if(Info.AlbumcoverSmall != null && Info.AlbumcoverSmall.StartsWith("http://"))
			{
				try
				{
					System.Net.WebClient Client = new System.Net.WebClient();
					this.CoverBox.FromPixbuf = new Gdk.Pixbuf(Client.DownloadData(Info.AlbumcoverSmall));
				}
				catch
				{}
			}
		}else{
			//TODO: do something when we're not steaming... disable some controls etc...
		}
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
		//Enable commands again
		this.EnableCommands();
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
			this.LastFMStationEntry.Sensitive = true;
			this.ConnectButton.Sensitive = true;
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
}