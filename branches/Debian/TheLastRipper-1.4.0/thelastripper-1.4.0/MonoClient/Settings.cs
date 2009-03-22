
using System;

namespace MonoClient
{
	[SerializableAttribute]
	public class Settings : LibLastRip.PlayListGenerator
	{
		protected System.String _Password = "";
		public LibLastRip.LastManager Manager;
		public System.Boolean SavePassword = false;
		public System.Int32 ListeningPort = 8000;
		public string BeforeRipCmd = "";
		public string AfterRipCmd = "";
		public bool ApplyRegain = false;
		private string _FilenamePattern;
		private string _ID3Comment;
		public bool OverwriteExistingMusic = false;
		
		//Locker stuff
		private String _LockerUsername = "";
		private String _LockerPassword = "";
		public Boolean UploadToLocker = false;
		public LockerPut.Locker Locker = new LockerPut.Locker("1554898388");
		
		public Settings()
		{
			this._musicPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "Music";
			if(!System.IO.Directory.Exists(this._musicPath))
			{
				System.IO.Directory.CreateDirectory(this._musicPath);
			}
			this.Manager = new LibLastRip.LastManager(this._musicPath);
			
			this.LaunchPreferences();
		}
		
		public virtual void LaunchPreferences()
		{
			this.LaunchPreferences(true);
		}
		
		public virtual void LaunchPreferences(System.Boolean SaveSettings)
		{
			Preferences Pref = new Preferences(this.Manager,this);
			Pref.Run();
			this._musicPath = Pref.MusicFolder;
			this.Manager.MusicPath = Pref.MusicFolder;
			
			this.TopTracks = Pref.TopTracks;
			this.RecentLovedTracks = Pref.RecentLovedTracks;
			this.WeeklyTrackChart = Pref.WeeklyTrackChart;
			this.TopTracksMixed = Pref.TopTracksMixed;
			this.RecentLovedTracksMixed = Pref.RecentLovedTracksMixed;
			this.WeeklyTrackChartMixed = Pref.WeeklyTrackChartMixed;
			this.Mixed = Pref.Mixed;
			this.m3u = Pref.m3u;
			this.pls = Pref.pls;
			this.smil = Pref.smil;
			this.SavePassword = Pref.SavePassword;
			this.ListeningPort = Pref.ListeningPort;
			this.Manager.ListeningPortNumber = Pref.ListeningPort;
			
			//Commands
			this.BeforeRipCmd = Pref.BeforeRipCmd;
			this.AfterRipCmd = Pref.AfterRipCmd;
			this.ApplyRegain = Pref.ApplyReplayGain;
			this.Manager.NewSongCommand = this.BeforeRipCmd;
			this.Manager.AfterRipCommand = this.AfterRipCmd;
			
			//save proxy settings:
			this._proxyAddress = Pref.ProxyServer;
			this._proxyUsername = Pref.ProxyUsername;
			this._proxyPassword = Pref.ProxyPassword;
			
			//Advanced
			this.OverwriteExistingMusic = Pref.OverwriteExisting;
			this.Manager.OverwriteExistingMusic = this.OverwriteExistingMusic;
			if(!String.IsNullOrEmpty(Pref.FilenamePattern) && Pref.FilenamePattern.Contains("%"))
				this._FilenamePattern = Pref.FilenamePattern;
			else if(Pref.FilenamePattern.Contains("%"))
				Console.WriteLine("Filename pattern must contain parameters such as %t, otherwise it'll only write one file.");
			this.Manager.filename_pattern = this.FilenamePattern;
			this._ID3Comment = Pref.Comment;
			this.Manager.Comment = this.ID3Comment;
			
			//Locker settings
			this.Locker = Pref.locker;
			this.UploadToLocker = Pref.UploadToLocker;
			if(this.UploadToLocker && (this.Locker == null || !this.Locker.IsLoggedin))
				Console.WriteLine("Not logged in at locker...");
			this._LockerUsername = Pref.LockerMail;
			this._LockerPassword = Pref.LockerPwd;
			
			this._userName = Pref.UserName;
			if(!Pref.HasPassword)
			{
				this._Password = LibLastRip.LastManager.CalculateHash(Pref.Password);
			}
			
			Pref.Destroy();
			if(SaveSettings)
			{
				this.SaveSettings();
			}
		}
		
		public static Settings Restore()
		{
			System.String AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			if(System.IO.File.Exists(AppData + "/TheLastRipper.conf"))
			{
					Settings Obj;
					System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					System.IO.FileStream Stream = System.IO.File.OpenRead(AppData + "/TheLastRipper.conf");
				
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
			System.String AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);		
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			
			if(!System.IO.Directory.Exists(AppData))
				System.IO.Directory.CreateDirectory(AppData);
			System.IO.FileStream Stream = System.IO.File.Create(AppData + "/TheLastRipper.conf");
			
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
				return this._Password;
			}
		}
		public System.String MusicPath
		{
			get
			{
				return this._musicPath;
			}
		}
	
		public System.String ProxyAdress
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
		
		public string FilenamePattern{
			get{
				if(String.IsNullOrEmpty(this._FilenamePattern) || !this._FilenamePattern.Contains("%")){
					return this.Manager.filename_pattern;
				}else{
					return this._FilenamePattern;
				}
			}
		}
		
		public string ID3Comment{
			get{
				if(String.IsNullOrEmpty(this._ID3Comment)){
					return this.Manager.Comment;
				}else
					return this._ID3Comment;
			}
		}
		
		public string LockerUsername{
			get{
				if(this.UploadToLocker)
					return this._LockerUsername;
				else
					return "";
			}
		}
		
		public string LockerPassword{
			get{
				if(this.UploadToLocker)
					return this._LockerPassword;
				else
					return "";
			}
		}		
		
		protected Settings(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context):base(Info, context)
		{
			if(Info.GetBoolean("HasPassword"))
			{
				this._Password = Info.GetString("Password");
				this.SavePassword = true;
			}
			this.Manager = new LibLastRip.LastManager(this._musicPath);
			
			//Set playback port
			this.ListeningPort = Info.GetInt32("ListeningPort");
			this.Manager.ListeningPortNumber = this.ListeningPort;
			
			//Commands
			try{
				this.ApplyRegain = Info.GetBoolean("ApplyReplayGain");
				this.BeforeRipCmd = Info.GetString("BeforeRipCmd");
				this.AfterRipCmd = Info.GetString("AfterRipCmd");
			}
			catch(System.Runtime.Serialization.SerializationException exp){
				Console.WriteLine("Couldn't restore command settings: \n" + exp.Message);
			}
			
			//Advanced
			try{
				this._FilenamePattern = Info.GetString("FilenamePattern");
				this._ID3Comment = Info.GetString("ID3Comment");
				this.OverwriteExistingMusic = Info.GetBoolean("OverwriteExistingMusic");
			}
			catch(System.Runtime.Serialization.SerializationException exp){
				Console.WriteLine("Couldn't restore advanced settings: \n" + exp.Message);
			}
			
			//Locker settings
			try{
				this.UploadToLocker = Info.GetBoolean("UploadToLocker");
				this._LockerUsername = Info.GetString("LockerUsername");
				this._LockerPassword = Info.GetString("LockerPassword");
			}
			catch(System.Runtime.Serialization.SerializationException exp){
				Console.WriteLine("Couldn't restore locker settings: \n" + exp.Message);
			}
			
			this.LaunchPreferences(false);
		}
		
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(Info, context);
			if(this.Manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
			{
				this.SavePassword = false;
			}
			Info.AddValue("HasPassword",this.SavePassword);
			if(this.SavePassword)
			{
				Info.AddValue("Password",this._Password);
			}
			Info.AddValue("ListeningPort", this.ListeningPort);
			
			//Commands
			Info.AddValue("ApplyReplayGain", this.ApplyRegain);
			Info.AddValue("BeforeRipCmd", this.BeforeRipCmd);
			Info.AddValue("AfterRipCmd", this.AfterRipCmd);
			
			//Advanced
			Info.AddValue("FilenamePattern", this.FilenamePattern);
			Info.AddValue("ID3Comment", this.ID3Comment);
			Info.AddValue("OverwriteExistingMusic", this.OverwriteExistingMusic);

			//Locker settings
			Info.AddValue("UploadToLocker", this.UploadToLocker);
			Info.AddValue("LockerUsername", this.LockerUsername);
			Info.AddValue("LockerPassword", this.LockerPassword);
		}
	}
}
