using System;
using System.Collections.Generic;
using System.Text;
using Cocoa;
using LibLastRip;

namespace TheLastRipper
{
    [Register("ApplicationController")]
	class ApplicationController : Cocoa.Object 
    {
		/*TODOs
		 * - Formatting of TrackLabel
		 * - CoverImage
		 * - Skip, Hate and love
		 * - Progressbar/LevelIndicator, use TextField and ProgressIndicator instead
		 * - Proxy settings
		 * - Browse directory
		 * - Save password
		 * - Playlist generator
		 * - Save settings
		 * - AboutBox
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
        #endregion
        
        protected LastManager LastManager;
        protected System.Timers.Timer MetaTimer = new System.Timers.Timer(3000);
        protected System.Timers.Timer ProgressTimer = new System.Timers.Timer();
        //protected Cocoa.Timer ProgressTimer = new Cocoa.Timer();
        
		public ApplicationController(System.IntPtr a)
            : base(a)
        {
			//Create a new manager using default music folder
			this.LastManager = new LastManager("/Users/renejosefsen/Music/Last.fm/");
			
			//Subscribe to events
            this.LastManager.HandshakeReturn += new EventHandler(this.HandshakeReturn);
            this.LastManager.StationChanged += new EventHandler(this.TuneInReturn);
            this.LastManager.OnNewSong += new EventHandler(this.OnNewSong);
            
            //Set timer interval and events
            this.MetaTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
            this.ProgressTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateProgress);
        }
        
        [Export("applicationWillFinishLaunching:")]
        public void FinishLoading(Cocoa.Notification notification)
        {
            //TODO: initialize widgets
        }
        
        
        #region SettingsWindow methods
        public void OnLoginClick( Cocoa.Object Sender)
        {
			
        	this.LastManager.Handshake(this.UsernameField.Value, LastManager.CalculateHash(this.PasswordField.Value));
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
        #endregion
	}
}
