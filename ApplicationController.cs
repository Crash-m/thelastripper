using System;
using System.Collections.Generic;
using System.Text;
using Cocoa;
using LibLastRip;
using System.Net;

namespace TheLastRipper
{
    [Register("ApplicationController")]
	class ApplicationController : Cocoa.Object 
    {
		/*TODOs
		 * - Proxy settings
		 * - Browse directory
		 * - Fix threading issues
		 * */
		
		#region MainWindow
        [Connect]
        public Cocoa.Window MainWindow;
        
        [Connect]
        public Cocoa.TextField RadioField;
        
        [Connect]
        public Cocoa.Button TuneInButton;
        
        [Connect]
        public Cocoa.ProgressIndicator TuneInIndicator;
        
        [Connect]
        public Cocoa.TextField TitleLabel;
        [Connect]
        public Cocoa.TextField AlbumLabel;
        [Connect]
        public Cocoa.TextField ArtistLabel;
        [Connect]
        public Cocoa.TextField DurationLabel;
        [Connect]
        public Cocoa.TextField StationLabel;
        
        [Connect]
        public Cocoa.TextField ProgressLabel;
        [Connect]
        public Cocoa.ProgressIndicator DownloadIndicator;
        
        [Connect]
        public Cocoa.ImageView AlbumImage;
        
        [Connect]
        public Cocoa.Button SkipButton;
        [Connect]
        public Cocoa.Button HateButton;
        [Connect]
        public Cocoa.Button LoveButton;
        #endregion
        
        
        #region Aboutbox
        
        [Connect]
        public Cocoa.Window AboutBox;
        
        [Connect]
        public Cocoa.ImageView AboutImage;
        
        #endregion
        
        
        #region SettingsWindow
        [Connect]
        public Cocoa.Window SettingsWindow;
        
        [Connect]
        public Cocoa.TextField UsernameField;
        [Connect]
        public Cocoa.TextField PasswordField;
        
        [Connect]
        public Cocoa.Button OKButton;
        [Connect]
        public Cocoa.Button LoginButton;
        
        [Connect]
        public Cocoa.TextField MusicFolderField;
        
        [Connect]
        public Cocoa.Button SavePassword;
        
        [Connect]
        public Cocoa.TextField ProxyAddress;
        [Connect]
        public Cocoa.TextField ProxyUsername;
        [Connect]
        public Cocoa.TextField ProxyPassword;
        #endregion
        
        #region Playlist
        
        [Connect]
        public Cocoa.Button PlaylistTopTracks;
        [Connect]
        public Cocoa.Button PlaylistRecentlyLoved;
        [Connect]
        public Cocoa.Button PlaylistWeeklyTrackChart;
        
        [Connect]
        public Cocoa.Button PlaylistTopTracksMixed;
        [Connect]
        public Cocoa.Button PlaylistRecentlyLovedMixed;
        [Connect]
        public Cocoa.Button PlaylistWeeklyTrackChartMixed;
        
        [Connect]
        public Cocoa.Button PlaylistAllListsMixed;
        
        [Connect]
        public Cocoa.Button FormatM3U;
        [Connect]
        public Cocoa.Button FormatPLS;
        [Connect]
        public Cocoa.Button FormatSMIL;
        
        #endregion
        
        protected LastManager LastManager;
        protected System.Timers.Timer MetaTimer = new System.Timers.Timer(3000);
        protected System.Timers.Timer ProgressTimer = new System.Timers.Timer();
        protected Settings Settings;
        protected System.Boolean HasPassword = false;
        //protected Cocoa.Timer ProgressTimer = new Cocoa.Timer();
        
		public ApplicationController(System.IntPtr a)
            : base(a)
        {            
            //Set timer interval and events
            this.MetaTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
            this.ProgressTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateProgress);
        }
        
        [Export("applicationWillFinishLaunching:")]
        public void FinishLoading(Cocoa.Notification notification)
        {
        	this.Settings = Settings.Restore();
        	this.MusicFolderField.Value = this.Settings.MusicPath;
        	this.UsernameField.Value = this.Settings.UserName;
        	this.HasPassword = this.Settings.SavePassword;
        	
        	if(this.HasPassword)
        	{
        		this.PasswordField.Enabled = false;
        	}
        	
        	//Get en instance of LastManager, restored from settings
			this.LastManager = this.Settings.Manager;
			
			//Subscribe to events
            this.LastManager.HandshakeReturn += new EventHandler(this.HandshakeReturn);
            this.LastManager.StationChanged += new EventHandler(this.TuneInReturn);
            this.LastManager.OnNewSong += new EventHandler(this.OnNewSong);
            
            //Load playlist settings
            this.PlaylistTopTracks.Value = this.Settings.TopTracks ? "1" : "0";
        	this.PlaylistRecentlyLoved.Value = this.Settings.RecentLovedTracks ? "1" : "0";
        	this.PlaylistWeeklyTrackChart.Value = this.Settings.WeeklyTrackChart ? "1" : "0";
        	
        	this.PlaylistTopTracksMixed.Value = this.Settings.TopTracksMixed ? "1" : "0";
        	this.PlaylistRecentlyLovedMixed.Value = this.Settings.RecentLovedTracksMixed ? "1" : "0";
        	this.PlaylistWeeklyTrackChartMixed.Value = this.Settings.WeeklyTrackChartMixed ? "1" : "0";
        	
        	this.PlaylistAllListsMixed.Value = this.Settings.Mixed ? "1" : "0";
        	
        	this.FormatM3U.Value = this.Settings.m3u ? "1" : "0";
        	this.FormatPLS.Value = this.Settings.pls ? "1" : "0";
        	this.FormatSMIL.Value = this.Settings.smil ? "1" : "0";
        	
        	//Load proxy settings
        	this.ProxyAddress.Value = this.Settings.ProxyAdress;
        	this.ProxyUsername.Value = this.Settings.ProxyUsername;
        	this.ProxyPassword.Value = this.Settings.ProxyPassword;
        }
        
        
        #region SettingsWindow methods
        public void OnLoginClick( Cocoa.Object Sender)
        {
        	//Apply proxy settings
        	if (this.ProxyAddress.Value.Length > 0) {
				WebProxy iwp = new WebProxy(this.ProxyAddress.Value);
				if (this.ProxyUsername.Value.Length > 0 || this.ProxyPassword.Value.Length > 0) {
					iwp.Credentials = new NetworkCredential(this.ProxyUsername.Value, this.ProxyPassword.Value);
				}
				WebRequest.DefaultWebProxy = iwp;//TODO: reconsider this selections since it's obsolete in .Net 2.0
			} else {
				WebRequest.DefaultWebProxy = GlobalProxySelection.GetEmptyWebProxy ();
			}
        	
        	//Get the right password
        	System.String Password;
			if(this.HasPassword)
				Password = this.Settings.Password;
			else
				Password = LastManager.CalculateHash(this.PasswordField.Value);
			
			//Perform a handshake
        	this.LastManager.Handshake(this.UsernameField.Value, Password);
        	this.AllowLogin(false);
        }
        
        public void OnOKClick(Cocoa.Object Sender)
        {
			try
        	{
        		if(!System.IO.Directory.Exists(this.MusicFolderField.Value))
        			System.IO.Directory.CreateDirectory(this.MusicFolderField.Value);
        		if(System.IO.Directory.Exists(this.MusicFolderField.Value))
        		{
        			this.LastManager.MusicPath = this.MusicFolderField.Value;
        			this.MainWindow.Show();
					this.SettingsWindow.Hide();
				}else{
        			Console.WriteLine("Bad music folder");
        		}
        		
        	}
        	catch(System.Exception e)
        	{
        		Console.WriteLine("Bad music folder, exception:");
        		Console.WriteLine(e.ToString());
        	}
        	//Store settings
        	this.Settings.Password = LastManager.CalculateHash(this.PasswordField.Value);
        	this.Settings.UserName = this.UsernameField.Value;
        	this.Settings.SavePassword = this.SavePassword.Value == "1";
        	
        	//Store playlist settings
        	this.Settings.TopTracks = this.PlaylistTopTracks.Value == "1";
        	this.Settings.RecentLovedTracks = this.PlaylistRecentlyLoved.Value == "1";
        	this.Settings.WeeklyTrackChart = this.PlaylistWeeklyTrackChart.Value == "1";
        	
        	this.Settings.TopTracksMixed = this.PlaylistTopTracksMixed.Value == "1";
        	this.Settings.RecentLovedTracksMixed = this.PlaylistRecentlyLovedMixed.Value == "1";
        	this.Settings.WeeklyTrackChartMixed = this.PlaylistWeeklyTrackChartMixed.Value == "1";
        	
        	this.Settings.Mixed = this.PlaylistAllListsMixed.Value == "1";
        	
        	this.Settings.m3u = this.FormatM3U.Value == "1";
        	this.Settings.pls = this.FormatPLS.Value == "1";
        	this.Settings.smil = this.FormatSMIL.Value == "1";
        	
        	//Store proxy settings
        	this.Settings.ProxyAdress = this.ProxyAddress.Value;
        	this.Settings.ProxyUsername = this.ProxyUsername.Value;
        	this.Settings.ProxyPassword = this.ProxyPassword.Value;
        	
        	//Save settings
        	this.Settings.SaveSettings();
        }

        protected void HandshakeReturn(System.Object Sender, System.EventArgs e)
        {
        	HandshakeEventArgs Args = (HandshakeEventArgs)e;
        	if(Args.Success)
        		this.OKButton.Enabled = true;
        	else
        		this.AllowLogin(true);
        }
        
        /// <summary>
        /// Enables or disables widgets used to perform a login
        /// </summary>
        /// <param name="Value">Whether widgets should be enabled or disabled</param>
        protected void AllowLogin(Boolean Value)
        {
        	this.UsernameField.Enabled = Value;
        	this.PasswordField.Enabled = Value;
        	this.LoginButton.Enabled = Value;
        }
        
        public void OnUsernameEdit(Cocoa.Object Sender)
        {
        	this.PasswordField.Enabled = true;
        	this.HasPassword = false;
        }
        #endregion
        
        
        #region MainWindow methods
        public void OnTuneInClick(Cocoa.Object Sender)
        {
        	this.TuneInIndicator.StartAnimation();
        	this.AllowTuneIn(false);
			this.LastManager.ChangeStation(this.RadioField.Value);
        }
        
        
        /// <summary>
        /// Enables/disables widgets used to change station
        /// </summary>
        /// <param name="Value">Whether widgets should be enabled or disabled.</param>
        protected void AllowTuneIn(Boolean Value)
        {
        	this.RadioField.Enabled = Value;
        	this.TuneInButton.Enabled = Value;
        }
        
        protected void TuneInReturn(System.Object Sender, System.EventArgs e)
        {
        	StationChangedEventArgs Args = (StationChangedEventArgs)e;
        	if(Args.Success)
        	{
        		this.MetaTimer.Start();
        	}
        	this.TuneInIndicator.StopAnimation();
        	this.AllowTuneIn(true);
        }
        
        /// <summary>
        /// Holds value to indicate if this is the first update and interval should be higher.
        /// </summary>
        protected System.Boolean IsFirstUpdate = true;
        
        /// <summary>
        /// Updates MetaInfo, handles timer callback	
        /// </summary>
        protected void UpdateMetaInfo(System.Object Sender, System.Timers.ElapsedEventArgs Args)
        {
        	if(this.IsFirstUpdate)
        	{
        		this.MetaTimer.Interval = 25000;
        		this.IsFirstUpdate = false;
        	}
        	this.LastManager.UpdateMetaInfo();
        }
        
        /// <summary>
        /// Handles a new song event
        /// </summary>
        protected void OnNewSong(System.Object Sender, System.EventArgs e)
        {
        	MetaInfo Args = (MetaInfo)e;
        	if(Args.Streaming)
        	{
        		//Set labels
	        	this.TitleLabel.Value = Args.Track;
	        	System.Threading.Thread.Sleep(100);
	        	this.AlbumLabel.Value = Args.Album;
	        	System.Threading.Thread.Sleep(100);
	        	this.ArtistLabel.Value = Args.Artist;
	        	System.Threading.Thread.Sleep(100);
	        	this.DurationLabel.Value = Args.Trackduration + " sec.";
	        	System.Threading.Thread.Sleep(100);
	        	this.StationLabel.Value = Args.Station;

	        	
	        	//Start progress timer
	        	this.Progress = 0;
	        	try
	        	{
	        		this.MaxProgress = System.Convert.ToDouble(Args.Trackduration) * 1000;
	        	}
	        	catch(System.Exception Exp)
	        	{
	        		Console.WriteLine("Bad duration: " + Exp.ToString());
	        		this.MaxProgress = 200000;
	        	}
	        	this.ProgressTimer.Interval = this.MaxProgress / 100;
	        	this.DownloadIndicator.StartAnimation();
	        	this.ProgressTimer.Enabled = true;
	        	
	        	//Download Image
	        	try
	        	{
	        		if(Args.AlbumcoverMedium.StartsWith("http://"))
	        		{
	        			System.String CoverFileName = System.IO.Path.GetTempPath() + "cover";
	        			if(System.IO.File.Exists(CoverFileName))
	        				System.IO.File.Delete(CoverFileName);
	        			System.Net.WebClient Client = new System.Net.WebClient();
	        			Client.DownloadFile(Args.AlbumcoverMedium, CoverFileName);
	        			this.AlbumImage.Image = new Cocoa.Image(CoverFileName);
	        			System.Threading.Thread.Sleep(100);
	        		}
	        	}
	        	catch(System.Exception Exp)
	        	{
	        		Console.WriteLine("Problem downloading cover: " + Exp.ToString());
	        	} 
        	}
        	
        }
        
        protected System.Double Progress = 0;
        protected System.Double MaxProgress = 1;
        protected void UpdateProgress(System.Object Sender, System.Timers.ElapsedEventArgs Args)
        {
        	this.Progress += this.ProgressTimer.Interval;
        	this.ProgressLabel.Value = ((this.Progress / this.MaxProgress) * 100).ToString() + " %";
        }
        
        /// <summary>
        /// Handles a love button click
        /// </summary>
        public void OnLoveClick(Cocoa.Object Sender)
        {
        	this.AllowCommands(false);
        	this.LastManager.LoveSong();
        }
        
        /// <summary>
        /// Handles a Hate button click
        /// </summary>
        public void OnHateClick(Cocoa.Object Sender)
        {
        	this.AllowCommands(false);
        	this.LastManager.BanSong();
        }
        
        /// <summary>
        /// Handles a skip button click
        /// </summary>
        public void OnSkipClick(Cocoa.Object Sender)
        {
        	this.AllowCommands(false);
        	this.LastManager.SkipSong();
        }
        
        protected void CommandCallback(System.Object Sender, System.EventArgs Args)
        {
        	this.AllowCommands(true);
        }
        
        /// <summary>
        /// Enable or disable love, skip and hate buttons.
        /// </summary>
        /// <param name="Value">Whether the buttons should be disabled or enabled</param>
        protected void AllowCommands(Boolean Value)
        {
        	this.SkipButton.Enabled = Value;
        	this.LoveButton.Enabled = Value;
        	this.HateButton.Enabled = Value;
        }
        
        #endregion
        
        #region Menu, AboutBox etc.
        public void ShowAboutBox(Cocoa.Object Sender)
        {
        	this.AboutImage.Image = new Cocoa.Image("AboutBoxOSX.png");
        	this.AboutBox.Show();
        }
        
        public void ShowSettings(Cocoa.Object Sender)
        {
        	this.SettingsWindow.Show();
        }
        	
        public void GeneratePlaylist(Cocoa.Object Sender)
        {
        	this.Settings.Generate();
        }
        
        #endregion
	}
}
