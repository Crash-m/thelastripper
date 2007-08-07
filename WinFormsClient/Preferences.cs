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
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.Manager = Manager;
			
			if(this.Manager.ConnectionStatus == LibLastRip.ConnectionStatus.Created)
			{
				this.LoginGroupBox.Enabled = true;
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
			
			if(settings.Password != "")
			{
				this.HasPassword = true;
				this.PasswordTextBox.Text = settings.Password;
				this.PasswordTextBox.Enabled = false;
			}
			this.UserNameTextBox.Text = settings.UserName;
			this.MusicPathTextBox.Text = settings.MusicPath;
			
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
				this.LoginGroupBox.Enabled = false;
			}
		}
		
		/// <summary>
		/// Handles LoginCallback from LoginButton
		/// </summary>
		void LoginCallback(System.Object Sender, System.EventArgs Args)
		{
			LibLastRip.HandshakeEventArgs hArgs = (LibLastRip.HandshakeEventArgs)Args;
			if(hArgs.Success)
				this.OKbutton.Enabled = true;
			else
				this.LoginGroupBox.Enabled = true;
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
