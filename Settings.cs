/*
 * Created by SharpDevelop.
 * User: Jop
 * Date: 12-08-2007
 * Time: 00:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using LibLastRip;

namespace TheLastRipper
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	[SerializableAttribute]
	public class Settings : PlayListGenerator
	{
		public LastManager Manager;
		public System.String Password = "";
		public System.Boolean SavePassword = false;
		
		public string MusicPath {
			get { return _MusicPath; }
			set { _MusicPath = value; }
		}
		public string UserName {
			get { return _UserName; }
			set { _UserName = value; }
		}
		public string ProxyAdress {
			get { return _ProxyAdress; }
			set { _ProxyAdress = value; }
		}
		public string ProxyUsername {
			get { return _ProxyUsername; }
			set { _ProxyUsername = value; }
		}
		public string ProxyPassword {
			get { return _ProxyPassword; }
			set { _ProxyPassword = value; }
		}
		
		public Settings()
		{
			this._MusicPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
			if(!System.IO.Directory.Exists(this._MusicPath))
			{
				System.IO.Directory.CreateDirectory(this._MusicPath);
			}
			//Avoid a NullReferenceException by making an empty string.
			this._UserName = "";
			this._ProxyAdress = "";
			this._ProxyUsername = "";
			this._ProxyPassword = "";
			this.Manager = new LastManager(this._MusicPath);
		}
		
		public static Settings Restore()
		{
			System.String AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			if(System.IO.File.Exists(AppData + "/TheLastRipper.xml"))
			{
				Settings Obj;
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				System.IO.FileStream Stream = System.IO.File.OpenRead(AppData + "/TheLastRipper.xml");
				
				try
				{
					Obj = (Settings)Formatter.Deserialize(Stream);
					Stream.Close();
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
		
		/// <summary>
		/// Deserializes data from serialization object, used when restoring data saved with serialization.
		/// </summary>
		/// <param name="Info">Object that data must be restored from</param>
		/// <remarks>This method calls parent method, so all fields on LibLastRip.PlayListGenerator is restored by the base class</remarks>
		protected Settings(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context):base(Info, context) //Note this executes base class and restores data saved in it.
		{	
			//Note the names used here MUST match the names used in serialization method below
			
			//Get password from serialization object if it was saved
			if(Info.GetBoolean("HasPassword"))
			{
				this.Password = Info.GetString("Password");
				this.SavePassword = true;
			}
			
			//Create LastManager from restored data
			this.Manager = new LibLastRip.LastManager(this._MusicPath);
		}
		
		
		/// <summary>
		/// Serializes the settings object, used when saving settings
		/// </summary>
		/// <param name="Info">Object that data must be saved to</param>
		/// <remarks>This method class parent method, so all fields on LibLastRip.PlayListGenerator is saved by the base class</remarks>
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context)
		{
			//Add values from base class to serialization object
			base.GetObjectData(Info, context);
			
			//Add password if password exists
			if(this.Manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
			{
				this.SavePassword = false;
			}
			Info.AddValue("HasPassword",this.SavePassword);
			if(this.SavePassword)
			{
				Info.AddValue("Password",this.Password);
			}
		}
		
		public virtual void SaveSettings()
		{
			System.String AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);    
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			
			System.IO.FileStream Stream = System.IO.File.Create(AppData + "/TheLastRipper.xml");
			
			Formatter.Serialize(Stream,this);
			Stream.Flush();
			Stream.Close();
		}
	}
}
