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
using System.IO;

namespace WinFormsMultipleStreamsClient
{
	/// <summary>
	/// Description of Preferences.
	/// </summary>
	public partial class Preferences : Form
	{
		public System.Boolean hasPassword = false;
		private LibLastRip.LastManager manager;
		public LockerPut.Locker locker = new LockerPut.Locker("1554898388");
		
		public Preferences(LibLastRip.LastManager manager, Settings settings)
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
			
			this.ExcludeFileTextBox.Text = settings.ExcludeFile;
			this.QuarantinePathTextBox.Text = settings.QuarantinePath;
			this.ExcludeExistingMusicCheckBox.Checked = settings.ExcludeExistingMusic;
			this.ExcludeNewMusicCheckBox.Checked = settings.ExcludeNewMusic;
			this.HealthCheckBox.Checked = settings.HealthEnabled;
			this.HealthTextBox.Text = settings.HealthValue;
			
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
			this.ProxyEnabledCheckBox.Checked = settings.ProxyEnabled;
			
			//Subscribe to handshake callback event
			this.manager.HandshakeReturn += new EventHandler(this.LoginCallback);
			
			//Setup locker settings
			this.LockercheckBox.Checked = settings.UploadToLocker;
			this.LockerEmailtextBox.Text = settings.LockerUsername;
			this.LockerPasswordTextBox.Text = settings.LockerPassword;
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
			if (this.ProxyEnabledCheckBox.Checked && this.ProxyAddressTextBox.Text != null && this.ProxyAddressTextBox.Text.Length > 0) {
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
			this.groupBoxLocker.Enabled = value;
			this.LockercheckBox.Enabled = value;
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
			if(hArgs.Success)  {
				//Check if we need login at locker
				if(this.LockercheckBox.Checked){		//Our api key for mp3locker
					if(!this.locker.IsLoggedin){
						this.locker.OnLogin += new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCallback);
						this.locker.Login(this.LockerEmailtextBox.Text, this.LockerPasswordTextBox.Text);
						return;	//OK button will be enabled when LockerLoginCallback returns
					}
				}
				
				//Enable ok button
				this.OKbutton.Enabled = true;
				this.OKbutton.Focus();
			} else {
				setLoginElements(true);
			}
		}
		
		/// <summary>
		/// Handle callback from locker login
		/// </summary>
		void LockerLoginCallback(Object Sender, LockerPut.LockerLoginEventArgs args){
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCallback), new System.Object[]{Sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			if(args.Success){
				//Enable ok button
				this.OKbutton.Enabled = true;
				this.OKbutton.Focus();
			}else{
				MessageBox.Show(args.Message, "MP3Locker login failed");
				this.setLoginElements(true);
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
		
		private void checkValidValues() {
			if (ExcludeNewMusicCheckBox.Checked) {
				String test = "%a" + Path.DirectorySeparatorChar;
				if (FilenamePattern.Text == null || !FilenamePattern.Text.StartsWith(test)) {
					// Error
					System.Windows.Forms.MessageBox.Show("Skip new music option can only be checked if filename pattern starts with '" + test + "', checked state removed!", "Invalid options selected");
					ExcludeNewMusicCheckBox.Checked = false;
				}
			}
			
			if (HealthCheckBox.Checked) {
				try {
					double health = Int64.Parse(HealthTextBox.Text);
				} catch (Exception) {
					// Error
					System.Windows.Forms.MessageBox.Show("Song health value must be a number, please use the recommended value of 46718 (or higher to get less songs detected as defect)!", "Invalid options selected");
					ExcludeNewMusicCheckBox.Checked = false;
				}
			}

		}
		
		void ExcludeNewMusicCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			checkValidValues();
		}
		
		void FilenamePatternTextChanged(object sender, EventArgs e)
		{
			checkValidValues();
		}
		
		
		void HealthTextBoxTextChanged(object sender, EventArgs e)
		{
			checkValidValues();
		}
		
		void LockercheckBoxCheckedChanged(object sender, EventArgs e)
		{
			//Enable/disable mp3 locker stuff
			this.groupBoxLocker.Enabled = this.LockercheckBox.Checked;
		}
		
		/// <summary>
		/// Create a new locker
		/// </summary>
		void CreateLockerbuttonClick(object sender, EventArgs e)
		{
			CreateLockerDialog dlg = new CreateLockerDialog(this.locker);
			DialogResult res = dlg.ShowDialog();
			if(res == DialogResult.OK){
				this.LockerEmailtextBox.Text = dlg.Username;
				this.LockerPasswordTextBox.Text = dlg.Password;
			}
		}

        private void OKbutton_Click(object sender, EventArgs e)
        {

        }
	}
}
