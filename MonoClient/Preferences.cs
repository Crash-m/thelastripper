
using System;

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
			
			this.UserNameEntry.Text = this.setting.UserName;
			if(this.setting.Password != "")
			{
				this.SaveLogincheckbutton.Active = true;
				this.HasPassword = true;
				this.PasswordEntry.Sensitive = false;
			}
			
			this.MusicPathChooser.SetCurrentFolder(this.setting.MusicPath);
		}
		
		
		protected virtual void OnLoginButtonClicked(object sender, System.EventArgs e)
		{
			if(!this.HasPassword )
			{
				if(this.Manager.Handshake(this.UserNameEntry.Text, LibLastRip.LastManager.CalculateHash(this.PasswordEntry.Text)))
				{
					this.Closebutton.Sensitive = true;
					this.LoginFrame.Sensitive = false;
				}
			}else{
				if(this.Manager.Handshake(this.UserNameEntry.Text, this.setting.Password))
				{
					this.Closebutton.Sensitive = true;
					this.LoginFrame.Sensitive = false;
				}
			}
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
