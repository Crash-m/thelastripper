
using System;

namespace MonoClient
{
	
	
	public class Preferences : Gtk.Dialog
	{
		protected Gtk.Frame LoginFrame;
		protected Gtk.Button Closebutton;
		public Gtk.Entry UserNameEntry;
		public Gtk.Entry PasswordEntry;
		public Gtk.CheckButton SaveLogincheckbutton;
		public LibLastRip.LastManager Manager;
		public Gtk.FileChooserWidget MusicPathChooser;
		public Gtk.CheckButton TopTrackscheckbutton;
		public Gtk.CheckButton Lovedcheckbutton;
		public Gtk.CheckButton Weekcheckbutton;
		public Gtk.CheckButton TopTracksMixcheckbutton;
		public Gtk.CheckButton LovedMixcheckbutton;
		public Gtk.CheckButton WeekMixcheckbutton;
		public Gtk.CheckButton AllMixcheckbutton;
		public Gtk.CheckButton M3Ucheckbutton;
		public Gtk.CheckButton PLScheckbutton;
		public Gtk.CheckButton SMILcheckbutton;
		protected Settings setting;
		public System.Boolean HasPassword = false;
		
		public Preferences(LibLastRip.LastManager Manager, Settings setting)
		{
			Stetic.Gui.Build(this, typeof(MonoClient.Preferences));
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
	}
}
