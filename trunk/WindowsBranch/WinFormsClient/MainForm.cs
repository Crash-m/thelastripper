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
		protected const System.Int32 UIUpdateInterval = 6000;
		public System.Timers.Timer SysTimer = new System.Timers.Timer(MainForm.UpdateInterval);
		protected System.Windows.Forms.Timer Timer;
		
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
			
			this.Timer = new System.Windows.Forms.Timer();
			this.Timer.Interval = MainForm.UIUpdateInterval;
			this.Timer.Tick += new EventHandler(this.UpdateProgress);
			
			this.SysTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.Manager.UpdateMetaInfo);
		}
		
		protected virtual void OnNewSong(System.Object Sender, System.EventArgs Args)
		{
			LibLastRip.MetaInfo Info = (LibLastRip.MetaInfo)Args;
			this.TrackDuration = 0;	
			this.StatusBar.Value = 0;
			this.StatusBar.Maximum = System.Convert.ToInt32(this.UpdateTO.Trackduration);
			this.SetStatus(Info);
		}
		
		protected virtual void SetStatus(LibLastRip.MetaInfo Info)
		{
			if(Info.Streaming)
			{
				System.String StrT = "";
				StrT += "Track: " + Info.Track;
				StrT += "\nArtist: " + Info.Artist;
				StrT += "\nAlbum: " + Info.Album;
				StrT += "\nDuration: " + Info.Trackduration + " sec.";
				this.StatusLabel.Text = StrT;
				if(Info.AlbumcoverSmall != null && Info.AlbumcoverSmall.StartsWith("http://"))
				{
					try
					{
						System.Net.HttpWebRequest hReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Info.AlbumcoverSmall);
						System.Net.HttpWebResponse hRes = (System.Net.HttpWebResponse)hReq.GetResponse();
						System.IO.Stream ResponseStream = hRes.GetResponseStream();
						this.StatuspictureBox.Image = System.Drawing.Image.FromStream(ResponseStream);
					}
					catch
					{}
				}
			}
		}
		
		protected System.Boolean DoUpdate = false;
		protected LibLastRip.MetaInfo UpdateTO;
		static System.Object locker = new System.Object();
		
		protected virtual void UpdateProgress(System.Object Sender, System.EventArgs Args)
		{
			System.Int32 Next = this.StatusBar.Value + MainForm.UIUpdateInterval/1000;
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
			//TODO: Fix to run on mono
			System.Diagnostics.Process.Start("http://code.google.com/p/thelastripper/wiki/LegalNotice");
		}
		
		void OnlineHelpToolStripMenuItemClick(object sender, EventArgs e)
		{
			//TODO: Fix to run on mono
			System.Diagnostics.Process.Start("http://code.google.com/p/thelastripper/wiki/HelpWindows");
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			About ab = new About();
			ab.ShowDialog(this);
		}
		
		void TuneInButtonClick(object sender, EventArgs e)
		{
			this.TuneInButton.Enabled = false;
			if(this.Manager.ChangeStation(this.RadioStation.Text))
			{
				this.TuneInButton.Enabled = true;
				this.LoveButton.Enabled = true;
				this.HateButton.Enabled = true;
				this.SkipButton.Enabled = true;
				this.Timer.Enabled = true;
				this.SysTimer.Start();
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
