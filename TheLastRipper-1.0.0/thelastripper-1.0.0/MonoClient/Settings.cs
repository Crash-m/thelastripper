
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
			this._MusicPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + LibLastRip.PlatformSettings.PathSeparator + "Music";
			if(!System.IO.Directory.Exists(this._MusicPath))
			{
				System.IO.Directory.CreateDirectory(this._MusicPath);
			}
			this.Manager = new LibLastRip.LastManager(this._MusicPath);
			
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
			this._MusicPath = Pref.MusicPathChooser.CurrentFolder;
			this.Manager.MusicPath = Pref.MusicPathChooser.CurrentFolder;
			
			this.TopTracks = Pref.TopTrackscheckbutton.Active;
			this.RecentLovedTracks = Pref.Lovedcheckbutton.Active;
			this.WeeklyTrackChart = Pref.Weekcheckbutton.Active;
			this.TopTracksMixed = Pref.TopTracksMixcheckbutton.Active;
			this.RecentLovedTracksMixed = Pref.LovedMixcheckbutton.Active;
			this.WeeklyTrackChartMixed = Pref.WeekMixcheckbutton.Active;
			this.Mixed = Pref.AllMixcheckbutton.Active;
			this.m3u = Pref.M3Ucheckbutton.Active;
			this.pls = Pref.PLScheckbutton.Active;
			this.smil = Pref.SMILcheckbutton.Active;
			this.SavePassword = Pref.SaveLogincheckbutton.Active;
			
			this._UserName = Pref.UserNameEntry.Text;
			if(!Pref.HasPassword)
			{
				this._Password = LibLastRip.LastManager.CalculateHash(Pref.PasswordEntry.Text);
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
				return this._UserName;
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
				return this._MusicPath;
			}
		}
		
		protected Settings(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context):base(Info, context)
		{
			if(Info.GetBoolean("HasPassword"))
			{
				this._Password = Info.GetString("Password");
				this.SavePassword = true;
			}
			this.Manager = new LibLastRip.LastManager(this._MusicPath);
			
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
