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
		public System.Boolean hasPassword = false;
		private LibLastRip.LastManager manager;
		
		public Preferences(LibLastRip.LastManager manager,Settings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			this.manager = manager;
			
			if(this.manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
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
			if(!String.IsNullOrEmpty(settings.Password))
			{
				this.hasPassword = true;
				this.PasswordTextBox.Text = settings.Password;
				this.PasswordTextBox.Enabled = false;
			}
			this.MusicPathTextBox.Text = settings.MusicPath;
/*			this.FilenamePattern.Text = settings.manager.filename_pattern;
			this.AfterRipTextBox.Text = settings.manager.AfterRipCommand;
			this.CommentTextBox.Text = settings.manager.Comment;*/
			this.FilenamePattern.Text = settings.FileNamePattern;
			this.AfterRipTextBox.Text = settings.AfterRipCommand;
			this.CommentTextBox.Text = settings.Comment;
			this.QuarantinePathTextBox.Text = settings.QuarantinePath;
			this.ExcludeFileTextBox.Text = settings.ExcludeFile;
			this.ExcludeNewMusicCheckBox.Checked = settings.ExcludeNewMusic;
			this.ExcludeExistingMusicCheckBox.Checked = settings.ExcludeExistingMusic;
			this.NewSongCommandTextBox.Text = settings.NewSongCommand;
			this.SaveModeCombo.SelectedIndexChanged += new System.EventHandler(ToggleNewSongCommand);
			this.SaveModeCombo.SelectedIndex = settings.SaveMode ? 0 : 1;
			this.PortTextBox.Text = settings.PortNumber.ToString();
/*			if(settings.SaveMode)
				this.NewSongCommandTextBox.Enabled = true;
			else
				this.NewSongCommandTextBox.Enabled = false;
*/			
			this.ProxyAddressTextBox.Text = settings.ProxyAddress;
			this.ProxyUsernameTextBox.Text = settings.ProxyUsername;
			this.ProxyPasswordTextBox.Text = settings.ProxyPassword;
			
			//Subscribe to handshake callback event
			this.manager.HandshakeReturn += new EventHandler(this.LoginCallback);
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
			if (this.ProxyAddressTextBox.Text != null && this.ProxyAddressTextBox.Text.Length > 0) {
				WebProxy iwp = new WebProxy(this.ProxyAddressTextBox.Text);
				if (this.ProxyUsernameTextBox.Text.Length > 0 || this.ProxyPasswordTextBox.Text.Length > 0) {
					iwp.Credentials = new NetworkCredential(this.ProxyUsernameTextBox.Text, this.ProxyPasswordTextBox.Text);
				}
				WebRequest.DefaultWebProxy = iwp;//TODO: reconsider this selections since it's obsolete in .Net 2.0
			} else {
				WebRequest.DefaultWebProxy = GlobalProxySelection.GetEmptyWebProxy ();
			}
			
			if(!String.IsNullOrEmpty(this.PasswordTextBox.Text) && !String.IsNullOrEmpty(this.UserNameTextBox.Text))
			{
				System.String password;
				if(this.hasPassword)
				{
					password = this.PasswordTextBox.Text;
				}else{
					password = LibLastRip.LastManager.CalculateHash(this.PasswordTextBox.Text);
				}
				this.manager.Handshake(this.UserNameTextBox.Text, password);
				//Disable login to prevent multiple logins
				setLoginElements(false);
			}
		}
		
		public void setLoginElements(bool value) {
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
			if(this.hasPassword)
			{
				this.hasPassword = false;
				this.PasswordTextBox.Text = "";
				this.PasswordTextBox.Enabled = true;
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			this.OpenFileDialog.FileName = "";
			if (!String.IsNullOrEmpty(this.ExcludeFileTextBox.Text)) {
				this.OpenFileDialog.FileName = this.ExcludeFileTextBox.Text;
			}
			System.Windows.Forms.DialogResult Res = this.OpenFileDialog.ShowDialog(this);
			if(Res == System.Windows.Forms.DialogResult.OK)
			{
				this.ExcludeFileTextBox.Text = this.OpenFileDialog.FileName;
			}
		}

		
		void BrowseButton2Click(object sender, EventArgs e)
		{
			this.FolderBrowserDialog.SelectedPath = "";
			if (!String.IsNullOrEmpty(this.QuarantinePathTextBox.Text)) {
				this.FolderBrowserDialog.SelectedPath = this.QuarantinePathTextBox.Text;
			}
			System.Windows.Forms.DialogResult Res = this.FolderBrowserDialog.ShowDialog(this);
			if(Res == System.Windows.Forms.DialogResult.OK)
			{
				this.QuarantinePathTextBox.Text = this.FolderBrowserDialog.SelectedPath;
			}
		}
		
		private void ToggleNewSongCommand(object sender,System.EventArgs e){
			if(this.SaveModeCombo.SelectedIndex == 0)
				this.NewSongCommandTextBox.Enabled = true;
			else
				this.NewSongCommandTextBox.Enabled = false;
		}
	}
}
