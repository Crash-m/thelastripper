
using System;

namespace WinFormsClient
{
	[SerializableAttribute]
	public class Settings : LibLastRip.PlayListGenerator
	{
		protected System.String _Password = "";
		public LibLastRip.LastManager Manager;
		public System.Boolean SavePassword = false;
		
		public Settings()
		{
			this._MusicPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
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
			//TODO: do settings
			Preferences Pref = new Preferences(this.Manager, this);
			Pref.ShowDialog();
			
			this._MusicPath = Pref.MusicPathTextBox.Text;
			this.Manager.MusicPath = Pref.MusicPathTextBox.Text;
			
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
			
			this.SavePassword = Pref.SavePasswordCheckBox.Checked;
			
			this._UserName = Pref.UserNameTextBox.Text;
			if(!Pref.HasPassword)
			{
				this._Password = LibLastRip.LastManager.CalculateHash(Pref.PasswordTextBox.Text);
			}
			
			this._ProxyAdress = Pref.ProxyAdressTextBox.Text;
			this._ProxyPassword = Pref.ProxyPasswordTextBox.Text;
			this._ProxyUsername = Pref.ProxyUsernameTextBox.Text;
			
			if(SaveSettings)
			{
				this.SaveSettings();
			}
		}
		
		public static Settings Restore()
		{
			System.String AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			if(System.IO.File.Exists(AppData + "\\TheLastRipper.xml"))
			{
				Settings Obj;
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				System.IO.FileStream Stream = System.IO.File.OpenRead(AppData + "\\TheLastRipper.xml");
				
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
			
			System.IO.FileStream Stream = System.IO.File.Create(AppData + "\\TheLastRipper.xml");
			
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
		public System.String ProxyAdress
		{
			get
			{
				return this._ProxyAdress;
			}
		}
		public System.String ProxyUsername
		{
			get
			{
				return this._ProxyUsername;
			}
		}		public System.String ProxyPassword
		{
			get
			{
				return this._ProxyPassword;
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
