
using System;

namespace WinFormsClient
{
	[SerializableAttribute]
	public class Settings : LibLastRip.PlayListGenerator
	{
		private System.String _password = "";
		public LibLastRip.LastManager manager;
		private System.Boolean savePassword;
		
		public Settings()
		{
			this._musicPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
			this.manager = new LibLastRip.LastManager(this._musicPath);
			
			this.LaunchPreferences();
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
			this.manager.MusicPath = Pref.MusicPathTextBox.Text;
			this.manager.QuarantinePath = Pref.QuarantinePathTextBox.Text;
			
			this.manager.ExcludeFile = this._excludeFile = Pref.ExcludeFileTextBox.Text;
			this.manager.ExcludeNewMusic = this._excludeNewMusic = Pref.ExcludeNewMusicCheckBox.Checked;
			
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
			
			if(saveSettings)
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
		}
	}
}
