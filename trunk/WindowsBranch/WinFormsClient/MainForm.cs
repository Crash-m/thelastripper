/*
 * Created by SharpDevelop.
 * User: jopsen
 * Date: 11-02-2007
 * Time: 14:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsClient
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		protected LibLastRip.LastManager Manager;
		protected Settings settings;
		protected System.Int32 TrackDuration = 0;
		protected const System.Int32 UpdateInterval = 6000;
		public System.Timers.Timer Timer = new System.Timers.Timer(MainForm.UpdateInterval);
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.settings = Settings.Restore();
			this.Manager = this.settings.Manager;
			
			if(this.Manager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
			{
				this.RadioStation.Enabled = true;
				this.TuneInButton.Enabled = true;
			}
			this.Manager.OnNewSong += new EventHandler(this.OnNewSong);
			this.Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Manager.UpdateMetaInfo);
			this.Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateProgress);
		}
		
		protected virtual void OnNewSong(System.Object Sender, System.EventArgs Args)
		{
			LibLastRip.MetaInfo Info = (LibLastRip.MetaInfo)Args;
			this.SetStatus(Info);
			this.TrackDuration = 0;
			this.StatusBar.Value = 0;
			this.StatusBar.Maximum = System.Convert.ToInt32(Info.Trackduration);
		}
		
		protected virtual void SetStatus(LibLastRip.MetaInfo Info)
		{
			if(Info.Streaming)
			{
				this.StatusLabel.Text = Info.ToString(); //TODO: better formatting
				if(Info.AlbumcoverSmall != null && Info.AlbumcoverSmall.StartsWith("http://"))
				{
					//TODO: Port to mono
					try
					{
						//this.StatuspictureBox.Load(Info.AlbumcoverSmall);
					}
					catch
					{}
				}
			}
		}
		
		protected virtual void UpdateProgress(System.Object Sender, System.EventArgs Args)
		{
			System.Int32 Next = this.StatusBar.Value + MainForm.UpdateInterval/1000;
			if(Next >= this.StatusBar.Maximum)
			{
				this.StatusBar.Value = this.StatusBar.Maximum;
			}else{
				this.StatusBar.Value = Next;
			}
		}
		
		void SettingsToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.settings.LaunchPreferences();
			if(this.Manager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
			{
				this.RadioStation.Enabled = true;
				this.TuneInButton.Enabled = true;
			}
		}
		
		void GeneratePlaylistsToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.settings.Generate();
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void LegalIssuesToolStripMenuItemClick(object sender, EventArgs e)
		{
			//TODO: Open website
		}
		
		void OnlineHelpToolStripMenuItemClick(object sender, EventArgs e)
		{
			//TODO: open website
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			About ab = new About();
			ab.ShowDialog(this);
		}
		
		void TuneInButtonClick(object sender, EventArgs e)
		{
			if(this.Manager.ChangeStation(this.RadioStation.Text))
			{
				this.LoveButton.Enabled = true;
				this.HateButton.Enabled = true;
				this.SkipButton.Enabled = true;
				this.Timer.Start();
			}
		}
		
		void SkipButtonClick(object sender, EventArgs e)
		{
			this.Manager.SkipSong();
		}
		
		void LoveButtonClick(object sender, EventArgs e)
		{
			this.Manager.LoveSong();
		}
		
		void HateButtonClick(object sender, EventArgs e)
		{
			this.Manager.BanSong();
		}
	}
}
