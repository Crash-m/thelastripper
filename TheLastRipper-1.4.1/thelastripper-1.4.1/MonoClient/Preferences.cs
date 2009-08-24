
using System;
using System.Net;

namespace MonoClient
{
	
	
	public partial class Preferences : Gtk.Dialog
	{
		public LibLastRip.LastManager Manager;
		protected Settings setting;
		public System.Boolean HasPassword = false;
		public LockerPut.Locker locker;
		
		public Preferences(LibLastRip.LastManager Manager, Settings setting)
		{
			//Let Stetic generate the GUI
			this.Build();
			
			
			if(Manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
			{
				this.LoginFrame.Sensitive = true;
				this.Closebutton.Sensitive = false;
				this.ProxyFrame.Sensitive = true;
				this.PlayPortNumberSpinButton.Sensitive = true;
			}
			this.setting = setting;
			this.Manager = Manager;
			
			//Load settings
			this.TopTrackscheckbutton.Active = this.setting.TopTracks;
			this.Lovedcheckbutton.Active = this.setting.RecentLovedTracks;
			this.Weekcheckbutton.Active = this.setting.WeeklyTrackChart;
			this.TopTracksMixcheckbutton.Active = this.setting.TopTracksMixed;
			this.LovedMixcheckbutton.Active = this.setting.RecentLovedTracksMixed;
			this.WeekMixcheckbutton.Active = this.setting.WeeklyTrackChartMixed;
			this.AllMixcheckbutton.Active = this.setting.Mixed;
			this.M3Ucheckbutton.Active = this.setting.m3u;
			this.PLScheckbutton.Active = this.setting.pls;
			this.SMILcheckbutton.Active = this.setting.smil;
			this.ProxyServerEntry.Text = this.setting.ProxyAdress;
			this.ProxyUserEntry.Text = this.setting.ProxyUsername;
			this.ProxyPassEntry.Text = this.setting.ProxyPassword;
			this.PlayPortNumberSpinButton.Value = this.setting.ListeningPort;
			
			//Commands
			this.BeforeRipCmdEntry.Text = this.setting.BeforeRipCmd;
			this.AfterRipCmdEntry.Text = this.setting.AfterRipCmd;
			this.ReplayGainCheckButton.Active = this.setting.ApplyRegain;
			try{//Enable replaygain checkbutton if mp3gain is available on the system
				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo.FileName = "which";
				proc.StartInfo.Arguments = "mp3gain";
				proc.Start();
				proc.WaitForExit();
				this.ReplayGainCheckButton.Sensitive = (proc.ExitCode == 0);
			}
			catch{
				this.ReplayGainCheckButton.Sensitive = false;
			}
			
			//Locker settings:
			this.locker = this.setting.Locker;
			this.LockerUploadCheckbutton.Active = this.setting.UploadToLocker;
			this.LockerFrame.Sensitive = this.setting.UploadToLocker;
			this.LockerMailEntry.Text = this.setting.LockerUsername;
			this.LockerPwdEntry.Text = this.setting.LockerPassword;
			
			this.UserNameEntry.Text = this.setting.UserName;
			if(this.setting.Password != "")
			{
				this.SaveLogincheckbutton.Active = true;
				this.HasPassword = true;
				this.PasswordEntry.Sensitive = false;
			}
			
			this.MusicPathChooser.SetCurrentFolder(this.setting.MusicPath);
			
			//Advanced
			this.OverwriteCheckbutton.Active = this.setting.OverwriteExistingMusic;
			this.CommentEntry.Text = this.setting.ID3Comment;
			this.FilePatternEntry.Text = this.setting.FilenamePattern;
						
			//Register for events
			this.Manager.HandshakeReturn += new System.EventHandler(this.OnHandshakeReturn);
		}
		
		
		protected virtual void OnLoginButtonClicked(object sender, System.EventArgs e)
		{
			//Use proxy if needed
			if (this.ProxyServer != null && this.ProxyServer.Length > 0) {
				WebProxy iwp = new WebProxy(this.ProxyServer);
				if (this.ProxyUsername.Length > 0 || this.ProxyPassword.Length > 0) {
					iwp.Credentials = new NetworkCredential(this.ProxyUsername, this.ProxyPassword);
				}
				WebRequest.DefaultWebProxy = iwp;//TODO: reconsider this selections since it's obsolete in .Net 2.0
			} else {
				WebRequest.DefaultWebProxy = GlobalProxySelection.GetEmptyWebProxy ();
			}
			//Disable proxy frame
			this.ProxyFrame.Sensitive = false;
			
			//Disable login frame to insure no multiple request
			this.LoginFrame.Sensitive = false;
			
			//Get the correct password
			System.String Password;
			if(this.HasPassword)
				Password = this.setting.Password;
			else
				Password = LibLastRip.LastManager.CalculateHash(this.PasswordEntry.Text);
			
			//Perform a handskake
			this.Manager.Handshake(this.UserNameEntry.Text, Password);
		}
		
		///<summary>
		///Handles result of handshake
		///</summary>
		protected virtual void OnHandshakeReturn(System.Object Sender, System.EventArgs Args)
		{
			Gtk.Application.Invoke( delegate {
				LibLastRip.HandshakeEventArgs eArgs = (LibLastRip.HandshakeEventArgs)Args;
				//Handle the response
				if(eArgs.Success)
				{
					//Locker login if needed
					if(this.LockerUploadCheckbutton.Active && (!this.locker.IsLoggedin || this.locker.Username != this.LockerMail)){
						this.locker.OnLogin += new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCompleted);
						this.locker.Login(this.LockerMail, this.LockerPwd);
					}else
						this.Closebutton.Sensitive = true;
				}else{
					this.LoginFrame.Sensitive = true;
					this.ProxyFrame.Sensitive = true; //reenable proxy frame
				}
			});
		}
		
		/// <summary>
		/// Handle Locker login
		/// </summary>
		private void LockerLoginCompleted(Object Sender, LockerPut.LockerLoginEventArgs args){
			Gtk.Application.Invoke( delegate {			
				this.locker.OnLogin -= new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCompleted);
				if(!args.Success){
					Console.WriteLine("Failed mp3tunes login: \n" + args.Message);
					Gtk.MessageDialog dl = new Gtk.MessageDialog(this, Gtk.DialogFlags.Modal, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, false, "Failed to login at MP3Tunes\nRestart the application to try again, consider using the 'Test login credentials' button\nError Message:\n" + args.Message);
					dl.Run();
					dl.Destroy();
				}
				this.Closebutton.Sensitive = true;
			});
		}

		protected virtual void OnUserNameEntryChanged(object sender, System.EventArgs e)
		{
			if(this.HasPassword)
			{
				this.HasPassword = false;
				this.PasswordEntry.Sensitive = true;
			}
		}

		/// <summary>
		/// Updates LockerFrame sensitivity 
		/// </summary>
		protected virtual void LockerCheckUpdate (object sender, System.EventArgs e)
		{
			this.LockerFrame.Sensitive = this.LockerUploadCheckbutton.Active;
		}

		/// <summary>
		/// Test locker credentials
		/// </summary>
		protected virtual void TestLockerCredentials (object sender, System.EventArgs e)
		{
			this.TestLockerButton.Sensitive = false;
			this.locker.OnLogin += new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerTestLoginCompleted);
			this.locker.Login(this.LockerMail, this.LockerPwd);			
		}
		
		/// <summary>
		/// Login test return
		/// </summary>
		private void LockerTestLoginCompleted(Object Sender, LockerPut.LockerLoginEventArgs args){
			Gtk.Application.Invoke( delegate {			
				this.locker.OnLogin -= new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerTestLoginCompleted);
				if(!args.Success){
					Console.WriteLine("Failed mp3tunes test login: \n" + args.Message);
					Gtk.MessageDialog dl = new Gtk.MessageDialog(this, Gtk.DialogFlags.Modal, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, false, "Failed to login at MP3Tunes, verify your credentials.\nError Message:\n" + args.Message);
					dl.Run();
					dl.Destroy();
				}else{
					Gtk.MessageDialog dl = new Gtk.MessageDialog(this, Gtk.DialogFlags.Modal, Gtk.MessageType.Info, Gtk.ButtonsType.Ok, false, "Login was successful.");
					dl.Run();
					dl.Destroy();
				}
				this.TestLockerButton.Sensitive = true;
			});
		}

		/// <summary>
		/// Show Create new Locker account dialog
		/// </summary>
		protected virtual void CreateNewLockerAccount (object sender, System.EventArgs e)
		{
			CreateLockerAccount dl = new CreateLockerAccount(this.locker);
			Gtk.ResponseType res = (Gtk.ResponseType)dl.Run();
			
			//If an account was created
			if(res == Gtk.ResponseType.Ok){
				this.LockerUploadCheckbutton.Active = true;
				this.LockerMailEntry.Text = dl.LockerMail;
				this.LockerPwdEntry.Text = dl.LockerPwd;
			}
			
			dl.Destroy();
		}
		

		///<summary>
		///Gets proxy server
		///</summary>
		public virtual System.String ProxyServer
		{
			get{
				return this.ProxyServerEntry.Text;
			}
		}
		
		///<summary>
		///Gets proxy username
		///</summary>
		public virtual System.String ProxyUsername
		{
			get{
				return this.ProxyUserEntry.Text;
			}
		}
		
		///<summary>
		///Gets proxy password
		///</summary>
		public virtual System.String ProxyPassword
		{
			get{
				return this.ProxyPassEntry.Text;
			}
		}
		
		///<summary>
		///Gets current music folder from text entry
		///</summary>
		public virtual System.String MusicFolder
		{
			get
			{
				return this.MusicPathChooser.CurrentFolder;
			}
		}
		
		///<summary>
		///Gets TopTracks settings from checkbox
		///</summary>
		public virtual System.Boolean TopTracks
		{
			get
			{
				return this.TopTrackscheckbutton.Active;
			}
		}

		///<summary>
		///Gets RecentLovedTracks settings from checkbox
		///</summary>
		public virtual System.Boolean RecentLovedTracks
		{
			get
			{
				return this.Lovedcheckbutton.Active;
			}
		}

		///<summary>
		///Gets WeeklyTrackChart settings from checkbox
		///</summary>
		public virtual System.Boolean WeeklyTrackChart
		{
			get
			{
				return this.Weekcheckbutton.Active;
			}
		}

		///<summary>
		///Gets TopTracksMixed settings from checkbox
		///</summary>
		public virtual System.Boolean TopTracksMixed
		{
			get
			{
				return this.TopTracksMixcheckbutton.Active;
			}
		}

		///<summary>
		///Gets RecentLovedTracksMixed settings from checkbox
		///</summary>
		public virtual System.Boolean RecentLovedTracksMixed
		{
			get
			{
				return this.LovedMixcheckbutton.Active;
			}
		}

		///<summary>
		///Gets WeeklyTrackChartMixed settings from checkbox
		///</summary>
		public virtual System.Boolean WeeklyTrackChartMixed
		{
			get
			{
				return this.WeekMixcheckbutton.Active;
			}
		}

		///<summary>
		///Gets Mixed settings from checkbox
		///</summary>
		public virtual System.Boolean Mixed
		{
			get
			{
				return this.AllMixcheckbutton.Active;
			}
		}

		///<summary>
		///Gets m3u settings from checkbox
		///</summary>
		public virtual System.Boolean m3u
		{
			get
			{
				return this.M3Ucheckbutton.Active;
			}
		}

		///<summary>
		///Gets pls settings from checkbox
		///</summary>
		public virtual System.Boolean pls
		{
			get
			{
				return this.PLScheckbutton.Active;
			}
		}

		///<summary>
		///Gets smil settings from checkbox
		///</summary>
		public virtual System.Boolean smil
		{
			get
			{
				return this.SMILcheckbutton.Active;
			}
		}

		///<summary>
		///Get whether or not to save password
		///</summary>
		public virtual System.Boolean SavePassword
		{
			get
			{
				return this.SaveLogincheckbutton.Active;
			}
		}
		
		///<summary>
		///Gets username from text entry
		///</summary>
		public virtual System.String UserName
		{
			get
			{
				return this.UserNameEntry.Text;
			}
		}
		
		///<summary>
		///Gets password from text entry
		///</summary>
		public virtual System.String Password
		{
			get
			{
				return this.PasswordEntry.Text;
			}
		}
		
		/// <value>
		/// Get listening port number for streaming to loopback device
		/// </value>
		public int ListeningPort{
			get{
				return this.PlayPortNumberSpinButton.ValueAsInt;
			}
		}
		
		/// <value>
		/// Gets whether or not to apply replay gain.
		/// </value>
		public bool ApplyReplayGain{
			get{
				return this.ReplayGainCheckButton.Sensitive && this.ReplayGainCheckButton.Active;
			}
		}
		
		/// <value>
		/// Cmd to execute before recording a song
		/// </value>
		public string BeforeRipCmd{
			get{
				return this.BeforeRipCmdEntry.Text;
			}
		}

		/// <value>
		/// Command to execute after recording a track
		/// </value>
		public string AfterRipCmd{
			get{
				return this.AfterRipCmdEntry.Text;
			}
		}
		
		/// <value>
		/// Gets id3 tag comment
		/// </value>
		public string Comment{
			get{
				return this.CommentEntry.Text;
			}
		}
		
		/// <value>
		/// Get file pattern
		/// </value>
		public string FilenamePattern{
			get{
				return this.FilePatternEntry.Text;
			}
		}
		
		/// <value>
		/// Gets overwrite existing checkbox
		/// </value>
		public bool OverwriteExisting{
			get{
				return this.OverwriteCheckbutton.Active;
			}
		}
		
		/// <value>
		/// Gets if to upload new tracks
		/// </value>
		public bool UploadToLocker{
			get{
				return this.LockerUploadCheckbutton.Active;
			}
		}
		
		public String LockerMail{
			get{
				return this.LockerMailEntry.Text;
			}
		}
		public String LockerPwd{
			get{
				return this.LockerPwdEntry.Text;
			}
		}
	}
}
