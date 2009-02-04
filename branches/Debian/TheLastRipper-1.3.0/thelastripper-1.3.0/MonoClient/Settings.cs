
using System;

namespace MonoClient
{
	[SerializableAttribute]
	public class Settings : LibLastRip.PlayListGenerator
	{
		protected System.String _Password = "";
		public LibLastRip.LastManager Manager;
		public System.Boolean SavePassword = false;
		
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
			
			//save proxy settings:
			this._proxyAddress = Pref.ProxyServer;
			this._proxyUsername = Pref.ProxyUsername;
			this._proxyPassword = Pref.ProxyPassword;
			
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
		
		protected Settings(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context):base(Info, context)
		{
			if(Info.GetBoolean("HasPassword"))
			{
				this._Password = Info.GetString("Password");
				this.SavePassword = true;
			}
			this.Manager = new LibLastRip.LastManager(this._musicPath);
			
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
		}
	}
}
