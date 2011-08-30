
using System;
using System.Net;
using System.Windows.Forms;

namespace WinFormsMultipleStreamsClient
{
	[SerializableAttribute]
	public class Settings : LibLastRip.PlayListGenerator
	{
		private System.String _password = "";
        private LibLastRip.LastManager manager;

        public LibLastRip.LastManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }
		public LockerPut.Locker locker = null;
		private System.Boolean savePassword;
		private System.String _Comment;
		private System.String _FileNamePattern;
		private System.String _AfterRipCommand;
		private System.String _NewSongCommand;
		private System.Boolean _SaveMode;
		private System.Int32 _PortNum;
		
		private static System.String TlrDataFile = getTlrDataFile();
		
		private static System.String getTlrDataFile() {
			System.String AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			System.String TlrDataDir = AppData + "\\TheLastRipper";
			System.String TlrDataFile = TlrDataDir + "\\TheLastRipper.xml";

			if(!System.IO.Directory.Exists(TlrDataDir)) {
				System.IO.Directory.CreateDirectory(TlrDataDir);
			}			
			return TlrDataFile;
		}
		
		public Settings()
		{			
			this._musicPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
			this.manager = new LibLastRip.LastManager(this._musicPath);
			
			this.LaunchPreferences();
		}
        public string Genre { get; set; }
        public string SearchText { get; set; }
        public Settings(MultipleStreamSettings serializable, User user) 
        {

            this._musicPath = serializable.MusicDirectory;

            this.manager = new LibLastRip.LastManager(this._musicPath);

            this.manager.ListeningPortNumber = user.Port;
            this.manager.IpAdress = user.IpAdress;
            Genre = user.RadioGenre;
            SearchText = user.StationText;
            this._userName = user.Username;
            this._password = user.Password;
            this._PortNum = user.Port;
            this._AfterRipCommand = serializable.AfterRipCommand;
            this._Comment = serializable.ID3Comment;
            this._excludeExistingMusic = serializable.SkipExistingMusic1;
            this._excludeFile = serializable.ExcludeFile;
            this._excludeNewMusic = serializable.SkipExistingMusic2;
            this._FileNamePattern = serializable.FilenamePattern;
            this._healthEnabled = serializable.RenamePossibleDamagedFiles;
            this._healthValue = serializable.Health.ToString();
            this._LockerPassword = serializable.Mp3TunesPassword;
            this._LockerUsername = serializable.EMail;
            this._NewSongCommand = serializable.NewSongCommand;
            this._quarantinePath = serializable.QuaratineDirectory;
            this._SaveMode = serializable.SaveMode.Contains("Save directly to disc");
            this._UploadToLocker = serializable.UploadToMP3Tunes;
            this.TopTracks = serializable.TopTracks;
            this.TopTracksMixed = serializable.TopTracksMixed;
            this.m3u = serializable.M3U;
            this.pls = serializable.PLS;
            this.RecentLovedTracks = serializable.RecentlyLovedLeft;

            this.RecentLovedTracks = serializable.RecentlyLovedRight;
           
            this.smil = serializable.SMIL;
            this.WeeklyTrackChart = serializable.WecklyTrackChart;
            this.WeeklyTrackChartMixed = serializable.WecklyTrackChartMixed;

            this._proxyAddress = serializable.ProxyHost;
            this._proxyEnabled = serializable.ProxyEnabled;
            this._proxyUsername = serializable.ProxyUsername;
            this._proxyPassword = serializable.ProxyPassword;


            if (this.ProxyEnabled && !string.IsNullOrEmpty(this.ProxyAddress))
            {
                WebProxy iwp = new WebProxy(this.ProxyAddress);
                if (!string.IsNullOrEmpty(this.ProxyUsername) || !string.IsNullOrEmpty(this.ProxyPassword))
                {
                    iwp.Credentials = new NetworkCredential(this.ProxyUsername, this.ProxyPassword);
                }
                WebRequest.DefaultWebProxy = iwp;//TODO: reconsider this selections since it's obsolete in .Net 2.0
            }
            else
            {
                WebRequest.DefaultWebProxy = GlobalProxySelection.GetEmptyWebProxy();
            }

            if (!String.IsNullOrEmpty(user.Password) && !String.IsNullOrEmpty(user.Username))
            {
                System.String password;

                password = LibLastRip.LastManager.CalculateHash(user.Password);

                //Subscribe to handshake callback event
                this.manager.HandshakeReturn += new EventHandler(this.LoginCallback);
                this.manager.Handshake(user.Username, password);
            }
    
        
        }

        /// <summary>
        /// Handles LoginCallback from LoginButton
        /// </summary>
        public void LoginCallback(System.Object Sender, System.EventArgs Args)
        {

            LibLastRip.HandshakeEventArgs hArgs = (LibLastRip.HandshakeEventArgs)Args;
            if (hArgs.Success)
            {
                //Check if we need login at locker
                if (this._UploadToLocker)
                {		//Our api key for mp3locker
                    if (!this.locker.IsLoggedin)
                    {
                        this.locker.OnLogin += new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCallback);
                        this.locker.Login(this._LockerUsername, this._LockerPassword);
                        return;	//OK button will be enabled when LockerLoginCallback returns
                    }
                }
            }
        }

        /// <summary>
        /// Handle callback from locker login
        /// </summary>
        void LockerLoginCallback(Object Sender, LockerPut.LockerLoginEventArgs args)
        {


            if (args.Success)
            {
            }
            else
            {
           
       
            }
        }
		

		public virtual void LaunchPreferences()
		{
			this.LaunchPreferences(true);
		}
		
		public virtual void LaunchPreferences(System.Boolean saveSettings)
		{
			//TODO: do settings
			Preferences Pref = new Preferences(this.manager, this);
			Pref.ShowDialog();

			if(!System.IO.Directory.Exists(Pref.MusicPathTextBox.Text))
			{
				try
				{
					System.IO.Directory.CreateDirectory(Pref.MusicPathTextBox.Text);
				}
				catch(System.Exception e)
				{
					//TODO: Inform user in a pleasen't manner
					throw new System.Exception("Music directory doesn't exist and could not be created! Please select another directory.", e);
				}
			}
			
			if (String.IsNullOrEmpty(Pref.QuarantinePathTextBox.Text) == false && !System.IO.Directory.Exists(Pref.QuarantinePathTextBox.Text))
			{
				try
				{
					System.IO.Directory.CreateDirectory(Pref.QuarantinePathTextBox.Text);
				}
				catch(System.Exception e)
				{
					//TODO: Inform user in a pleasen't manner
					throw new System.Exception("Quarantine directory doesn't exist and could not be created! Please select another directory.", e);
				}
				
			}

			if(String.IsNullOrEmpty(Pref.ExcludeFileTextBox.Text) == false && !System.IO.File.Exists(Pref.ExcludeFileTextBox.Text))
			{
				try
				{
					System.IO.File.CreateText(Pref.ExcludeFileTextBox.Text);
				}
				catch(System.Exception e)
				{
					//TODO: Inform user in a pleasen't manner
					throw new System.Exception("Exclude file doesn't exist and could not be created! Please select another file.", e);
				}
			}

			this._musicPath = Pref.MusicPathTextBox.Text;
			this._quarantinePath = Pref.QuarantinePathTextBox.Text;
			this._FileNamePattern = Pref.FilenamePattern.Text;
			this._AfterRipCommand = Pref.AfterRipTextBox.Text;
			this._Comment = Pref.CommentTextBox.Text;
			this._NewSongCommand = Pref.NewSongCommandTextBox.Text;
			this._SaveMode = Pref.SaveModeCombo.SelectedIndex == 0;
			try{
				this._PortNum = Int32.Parse(Pref.PortTextBox.Text);
			}catch(Exception){
				this._PortNum = 0;
			}
			this.manager.NewSongCommand = _NewSongCommand;
			this.manager.MusicPath = Pref.MusicPathTextBox.Text;
			this.manager.QuarantinePath = Pref.QuarantinePathTextBox.Text;
			this.manager.filename_pattern = FileNamePattern;
			this.manager.AfterRipCommand = AfterRipCommand;
			this.manager.Comment = Comment;
			this.manager.SaveDirectlyToDisc = this._SaveMode;
			this.manager.ListeningPortNumber = this._PortNum;
			
			this.manager.ExcludeFile = this._excludeFile = Pref.ExcludeFileTextBox.Text;
			this.manager.ExcludeNewMusic = this._excludeNewMusic = Pref.ExcludeNewMusicCheckBox.Checked;
			this.manager.ExcludeExistingMusic = this._excludeExistingMusic = Pref.ExcludeExistingMusicCheckBox.Checked;
			this.manager.HealthEnabled = this._healthEnabled = Pref.HealthCheckBox.Checked;
			this.manager.HealthValue = this._healthValue = Pref.HealthTextBox.Text;
			
			this.TopTracks = Pref.TopTracksCheckBox.Checked;
			this.RecentLovedTracks = Pref.RecentlyLovedCheckBox.Checked;
			this.WeeklyTrackChart = Pref.WeeklyTrackChartCheckBox.Checked;
			this.TopTracksMixed = Pref.TopTracksMixedCheckBox.Checked;
			this.RecentLovedTracksMixed = Pref.RecentlyLovedMixedCheckBox.Checked;
			this.WeeklyTrackChartMixed = Pref.WeeklyTrackChartMixedCheckBox.Checked;
			this.Mixed = Pref.AllMixedCheckBox.Checked;
			this.m3u = Pref.M3UCheckBox.Checked;
			this.pls = Pref.PLSCheckBox.Checked;
			this.smil = Pref.SMILCheckBox.Checked;
			
			this.savePassword = Pref.SavePasswordCheckBox.Checked;
			
			this._userName = Pref.UserNameTextBox.Text;
			if(!Pref.hasPassword)
			{
				this._password = LibLastRip.LastManager.CalculateHash(Pref.PasswordTextBox.Text);
			}
			
			this._proxyAddress = Pref.ProxyAddressTextBox.Text;
			this._proxyPassword = Pref.ProxyPasswordTextBox.Text;
			this._proxyUsername = Pref.ProxyUsernameTextBox.Text;
			this._proxyEnabled = Pref.ProxyEnabledCheckBox.Checked;
			
			//Get locker settings
			this._UploadToLocker = Pref.LockercheckBox.Checked;
			if(this.UploadToLocker){
				this._LockerUsername = Pref.LockerEmailtextBox.Text;
				this._LockerPassword = Pref.LockerPasswordTextBox.Text;
			}
			
			if(Pref.locker.IsLoggedin){
				this.locker = Pref.locker;
			}
			
			if(saveSettings)
			{
				this.SaveSettings();
			}
			
			/*			if(this._SaveMode) //save to disc
				Pref.NewSongCommandTextBox.Enabled = true;
			else
				Pref.NewSongCommandTextBox.Enabled = false;
			 */
		}
		
		public static Settings Restore()
		{
			if(System.IO.File.Exists(TlrDataFile))
			{
				Settings Obj;
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				System.IO.FileStream Stream = System.IO.File.OpenRead(TlrDataFile);
				
				try
				{
					Obj = (Settings)Formatter.Deserialize(Stream);
					Stream.Close();
					Obj.SaveSettings();
				}
				catch
				{
					Stream.Close();
					Obj = new Settings();
				}
				return Obj;
			}else{
				return new Settings();
			}
		}
		public virtual void SaveSettings()
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			
			System.IO.FileStream Stream = System.IO.File.Create(TlrDataFile);
			
			Formatter.Serialize(Stream,this);
			Stream.Flush();
			Stream.Close();
		}
		
		//Don't mess with these variables during runtime
		public System.String UserName
		{
			get
			{
				return this._userName;
			}
		}
		public System.String Password
		{
			get
			{
				return this._password;
			}
		}
		public System.String MusicPath
		{
			get
			{
				return this._musicPath;
			}
		}
		public System.String QuarantinePath
		{
			get
			{
				return this._quarantinePath;
			}
		}
		public System.String ExcludeFile
		{
			get
			{
				return this._excludeFile;
			}
		}
		public System.Boolean ExcludeNewMusic
		{
			get
			{
				return this._excludeNewMusic;
			}
		}
		public System.Boolean ExcludeExistingMusic
		{
			get
			{
				return this._excludeExistingMusic;
			}
		}
		public System.Boolean HealthEnabled
		{
			get
			{
				return this._healthEnabled;
			}
		}
		public System.String HealthValue
		{
			get
			{
				return this._healthValue;
			}
		}
		public System.Boolean ProxyEnabled
		{
			get
			{
				return this._proxyEnabled;
			}
		}
		
		public System.String ProxyAddress
		{
			get
			{
				return this._proxyAddress;
			}
		}
		public System.String ProxyUsername
		{
			get
			{
				return this._proxyUsername;
			}
		}
		public System.String ProxyPassword
		{
			get
			{
				return this._proxyPassword;
			}
		}
		
		public string Comment {
			get {
				if(String.IsNullOrEmpty(_Comment))
					return manager.Comment;
				return _Comment;
			}
		}
		
		public string FileNamePattern {
			get {
				if(String.IsNullOrEmpty(_FileNamePattern))
					return manager.FilenamePattern;
				return _FileNamePattern;
			}
		}
		
		public string AfterRipCommand {
			get {
				if(_AfterRipCommand == null)
					return manager.AfterRipCommand;
				return _AfterRipCommand;
			}
		}

		public string NewSongCommand {
			get {
				if(_NewSongCommand == null)
					return manager.NewSongCommand;
				return _NewSongCommand;
			}
		}
		
		public bool SaveMode {
			get{
				return this._SaveMode;
			}
		}

		public Int32 PortNumber {
			get {
				if(_PortNum == 0)
					return manager.ListeningPortNumber;
				return _PortNum;
			}
		}
		
		private String _LockerUsername = "";
		private String _LockerPassword = "";
		private Boolean _UploadToLocker = false;
		
		public Boolean UploadToLocker {
			get{
				return this._UploadToLocker;
			}
		}
		
		/// <summary>
		/// Gets the mp3locker password, String.Empty if none
		/// </summary>
		public string LockerPassword {
			get{
				if(!this.UploadToLocker)
					return String.Empty;
				return this._LockerPassword;
			}
		}
		
		/// <summary>
		/// Gets the mp3locker username, String.Empty if none
		/// </summary>
		public string LockerUsername {
			get{
				if(!this.UploadToLocker)
					return String.Empty;
				return this._LockerUsername;
			}
		}

		/// <summary>
		/// Deserializes data from serialization object, used when restoring data saved with serialization.
		/// </summary>
		/// <param name="Info">Object that data must be restored from</param>
		/// <remarks>This method calls parent method, so all fields on LibLastRip.PlayListGenerator is restored by the base class</remarks>
		protected Settings(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info, context) //Note this executes base class and restores data saved in it.
		{
			//Note the names used here MUST match the names used in serialization method below
			
			//Get password from serialization object if it was saved
			if(info.GetBoolean("HasPassword"))
			{
				this._password = info.GetString("Password");
				this.savePassword = true;
			}
			
			//Create LastManager from restored data
			this.manager = new LibLastRip.LastManager(this._musicPath);
			
			if(info.GetString("Comment") != null)
				this._Comment = info.GetString("Comment");
			if(!String.IsNullOrEmpty(info.GetString("FileNamePattern")))
				this._FileNamePattern = info.GetString("FileNamePattern");
			if(info.GetString("AfterRipCommand") != null)
				this._AfterRipCommand = info.GetString("AfterRipCommand");
			if(info.GetString("NewSongCommand") != null)
				this._NewSongCommand = info.GetString("NewSongCommand");
			this._SaveMode = info.GetBoolean("SaveMode");
			this._PortNum = info.GetInt32("PortNumber");
			
			//Get locker settings don't complain if they aren't present
			try{
				this._UploadToLocker = info.GetBoolean("UploadToLocker");
			}
			catch(System.Runtime.Serialization.SerializationException){
				this._UploadToLocker = false;
			}
			if(this.UploadToLocker){
				this._LockerUsername = info.GetString("LockerUsername");
				this._LockerPassword = info.GetString("LockerPassword");
			}
			
			//Launch preferences without saving, after close since that would give IO problems, cause this method would have returned and serialization object/stream would still be open.
			this.LaunchPreferences(false);
		}
		
		
		/// <summary>
		/// Serializes the settings object, used when saving settings
		/// </summary>
		/// <param name="Info">Object that data must be saved to</param>
		/// <remarks>This method class parent method, so all fields on LibLastRip.PlayListGenerator is saved by the base class</remarks>
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			//Add values from base class to serialization object
			base.GetObjectData(info, context);
			
			//Add password if password exists
			if(this.manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
			{
				this.savePassword = false;
			}
			info.AddValue("HasPassword",this.savePassword);
			if(this.savePassword)
			{
				info.AddValue("Password",this._password);
			}
			info.AddValue("Comment",this._Comment);
			info.AddValue("FileNamePattern",this._FileNamePattern);
			info.AddValue("AfterRipCommand",this._AfterRipCommand);
			info.AddValue("NewSongCommand",this._NewSongCommand);
			info.AddValue("SaveMode",this._SaveMode);
			info.AddValue("PortNumber",this._PortNum);
			
			//Save locker settings
			info.AddValue("UploadToLocker", this.UploadToLocker);
			if(this.UploadToLocker){
				info.AddValue("LockerUsername", this._LockerUsername);
				info.AddValue("LockerPassword", this._LockerPassword);
			}
		}
	}
}
