/*
 * Created by SharpDevelop.
 * User: jopsen
 * Date: 11-02-2007
 * Time: 14:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

namespace WinFormsClient
{
	/// <summary>
	/// Description of Preferences.
	/// </summary>
	public partial class Preferences : Form
	{
		public System.Boolean HasPassword = false;
		public LibLastRip.LastManager Manager;
		public Preferences(LibLastRip.LastManager Manager,Settings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.Manager = Manager;
			
			if(this.Manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
			{
				this.LoginGroupBox.Enabled = true;
				this.NetworkGroupBox.Enabled = true;
				this.OKbutton.Enabled = false;
			}
			
			//Load playlist settings
			this.TopTracksCheckBox.Checked = settings.TopTracks;
			this.RecentlyLovedCheckBox.Checked = settings.RecentLovedTracks;
			this.WeeklyTrackChartCheckBox.Checked = settings.WeeklyTrackChart;
			this.TopTracksMixedCheckBox.Checked = settings.TopTracksMixed;
			this.RecentlyLovedMixedCheckBox.Checked = settings.RecentLovedTracksMixed;
			this.WeeklyTrackChartMixedCheckBox.Checked = settings.WeeklyTrackChartMixed;
			this.AllMixedCheckBox.Checked = settings.Mixed;
			this.M3UCheckBox.Checked = settings.m3u;
			this.PLSCheckBox.Checked = settings.pls;
			this.SMILCheckBox.Checked = settings.smil;
			
			// to avoid password reset on change of PasswordTextBox set first the userName...
			this.UserNameTextBox.Text = settings.UserName;

			// ...and then the password
			if(settings.Password != "")
			{
				this.HasPassword = true;
				this.PasswordTextBox.Text = settings.Password;
				this.PasswordTextBox.Enabled = false;
			}
			this.MusicPathTextBox.Text = settings.MusicPath;
			
			this.ProxyAdressTextBox.Text = settings.ProxyAdress;
			this.ProxyUsernameTextBox.Text = settings.ProxyUsername;
			this.ProxyPasswordTextBox.Text = settings.ProxyPassword;
			
			//Subscribe to handshake callback event
			this.Manager.HandshakeReturn += new EventHandler(this.LoginCallback);
		}
		
		void BrowseButtonClick(object sender, EventArgs e)
		{
			this.FolderBrowserDialog.SelectedPath = this.MusicPathTextBox.Text;
			System.Windows.Forms.DialogResult Res = this.FolderBrowserDialog.ShowDialog(this);
			if(Res == System.Windows.Forms.DialogResult.OK)
			{
				this.MusicPathTextBox.Text = this.FolderBrowserDialog.SelectedPath;
			}
		}
		
		void LoginButtonClick(object sender, EventArgs e)
		{
			// Proxy Setting from Settings-Group
			if (this.ProxyAdressTextBox.Text != null && this.ProxyAdressTextBox.Text.Length > 0) {
				WebProxy iwp = new WebProxy(this.ProxyAdressTextBox.Text);
				if (this.ProxyUsernameTextBox.Text.Length > 0 || this.ProxyPasswordTextBox.Text.Length > 0) {
					iwp.Credentials = new NetworkCredential(this.ProxyUsernameTextBox.Text, this.ProxyPasswordTextBox.Text);
				}
				WebRequest.DefaultWebProxy = iwp;//TODO: reconsider this selections since it's obsolete in .Net 2.0
			} else {
				WebRequest.DefaultWebProxy = GlobalProxySelection.GetEmptyWebProxy ();
			}
			
			if(this.PasswordTextBox.Text != "" && this.UserNameTextBox.Text != "")
			{
				System.String Password;
				if(this.HasPassword)
				{
					Password = this.PasswordTextBox.Text;
				}else{
					Password = LibLastRip.LastManager.CalculateHash(this.PasswordTextBox.Text);
				}
				this.Manager.Handshake(this.UserNameTextBox.Text,Password);
				//Disable login to prevent multiple logins
				setLoginElements(false);
			}
		}
		
		void setLoginElements(bool value) {
			this.LoginGroupBox.Enabled = value;
			this.NetworkGroupBox.Enabled = value;
			this.LoginButton.Enabled = value;
		}
		
		/// <summary>
		/// Handles LoginCallback from LoginButton
		/// </summary>
		void LoginCallback(System.Object Sender, System.EventArgs Args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.LoginCallback), new System.Object[]{Sender, Args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			
			LibLastRip.HandshakeEventArgs hArgs = (LibLastRip.HandshakeEventArgs)Args;
			if(hArgs.Success)
				this.OKbutton.Enabled = true;
			else {
				setLoginElements(true);
			}
		}
		
		void UserNameTextBoxTextChanged(object sender, EventArgs e)
		{
			if(this.HasPassword)
			{
				this.HasPassword = false;
				this.PasswordTextBox.Text = "";
				this.PasswordTextBox.Enabled = true;
			}
		}
	}
}
