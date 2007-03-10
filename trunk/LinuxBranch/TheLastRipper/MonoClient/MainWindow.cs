using System;
using Gtk;

public class MainWindow: Gtk.Window
{	
	public Gtk.ProgressBar SongProgressBar;
	public Gtk.Entry UserNameEntry;
	public Gtk.Entry PasswordEntry;
	public Gtk.ComboBoxEntry LastFMStationEntry;
	public Gtk.Label StatusLabel;
	public Gtk.Label GtkLabel5;
	public Gtk.Image CoverBox;
	public Gtk.Button LoginButton;
	public Gtk.Button ConnectButton;
	public Gtk.Button SkipButton;
	public Gtk.Button LoveButton;
	public Gtk.Button BanButton;
	public Gtk.Expander LoginExpander;
	
	protected const System.Int32 UpdateInterval = 6000;
	public LibLastRip.LastManager LastManager;
	public System.Timers.Timer Timer = new System.Timers.Timer(MainWindow.UpdateInterval);
	
	protected MonoClient.Settings settings;
	
	public MainWindow (): base ("")
	{
		Stetic.Gui.Build (this, typeof(MainWindow));
		
		this.settings = MonoClient.Settings.Restore();
		this.LastManager = this.settings.Manager;
		if(this.LastManager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
		{
			this.LastFMStationEntry.Sensitive = true;
			this.ConnectButton.Sensitive = true;
		}
		
		this.LastManager.OnNewSong += new System.EventHandler(this.OnNewSong);
		this.Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.LastManager.UpdateMetaInfo);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected System.Boolean IsStarted = false;
	
	protected virtual void OnConnectButtonClicked(object sender, System.EventArgs e)
	{
		this.Timer.Stop();
		if(this.LastManager.ChangeStation(this.LastFMStationEntry.Entry.Text))
		{
			this.Timer.Start();
			this.SetStatus(this.LastManager.CurrentSong);
			this.LoveButton.Sensitive = true;
			this.BanButton.Sensitive = true;
			this.SkipButton.Sensitive = true;
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
		this.TrackDuration += 2000/1000;
		System.Double Frac = ((System.Double)this.TrackDuration / System.Convert.ToDouble(this.LastManager.CurrentSong.Trackduration));
		if(Frac <= 1 && Frac >= (1/System.Double.MaxValue))
		{
			this.SongProgressBar.Fraction = Frac;
		}else{
			this.SongProgressBar.Fraction = 0;
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
			StrText += "<span size='x-large'>"+ Info.Track + "</span>\n";
			StrText += "<b>By: </b>" + LibLastRip.LastManager.RemoveIllegalChars(Info.Artist) + "\n";
			StrText += "<b>Album: </b>"+LibLastRip.LastManager.RemoveIllegalChars(Info.Album) + "\n";
			StrText += "<b>Length: </b>"+LibLastRip.LastManager.RemoveIllegalChars(Info.Trackduration) + " seconds\n";
			StrText += "<i>From: " + LibLastRip.LastManager.RemoveIllegalChars(Info.Station) + "</i>";
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
		this.LastManager.SkipSong();
	}

	protected virtual void OnLoveButtonClicked(object sender, System.EventArgs e)
	{
		this.LastManager.LoveSong();
	}

	protected virtual void OnBanButtonClicked(object sender, System.EventArgs e)
	{
		this.LastManager.BanSong();
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
	
}