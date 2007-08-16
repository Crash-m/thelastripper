
using System;
using System.Net;

namespace MonoClient
{
	
	
	public partial class Preferences : Gtk.Dialog
	{
		public LibLastRip.LastManager Manager;
		protected Settings setting;
		public System.Boolean HasPassword = false;
		
		public Preferences(LibLastRip.LastManager Manager, Settings setting)
		{
			//Let Stetic generate the GUI
			this.Build();
			
			
			if(Manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
			{
				this.LoginFrame.Sensitive = true;
				this.Closebutton.Sensitive = false;
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
			
			this.UserNameEntry.Text = this.setting.UserName;
			if(this.setting.Password != "")
			{
				this.SaveLogincheckbutton.Active = true;
				this.HasPassword = true;
				this.PasswordEntry.Sensitive = false;
			}
			
			this.MusicPathChooser.SetCurrentFolder(this.setting.MusicPath);
			
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
				this.Closebutton.Sensitive = true;
			}else{
				this.LoginFrame.Sensitive = true;
				this.ProxyFrame.Sensitive = true; //reenable proxy frame
			}
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
	}
}
