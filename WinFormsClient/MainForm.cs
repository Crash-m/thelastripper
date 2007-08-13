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
			//Handle all unhandled exceptions as they must be fatal :)
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			catch(System.Exception e)
			{
				FatalExceptionHandler F = new FatalExceptionHandler(e);
				F.ShowDialog();
			}
		}
		
		protected LibLastRip.LastManager Manager;
		protected Settings settings;
		protected System.Int32 TrackDuration = 0;
		protected const System.Int32 UpdateInterval = 10000;
		protected const System.Int32 UIUpdateInterval = 6000;
		protected System.Windows.Forms.Timer Timer;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
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
			
			//Subscribe to stations changed event
			this.Manager.StationChanged += new EventHandler(this.TuneInCallback);
			
			//Subscribe to command callback
			this.Manager.CommandReturn += new EventHandler(this.CommandCallback);	
		}
		
		protected virtual void OnNewSong(System.Object Sender, System.EventArgs Args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.OnNewSong), new System.Object[]{Sender, Args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			
			LibLastRip.MetaInfo Info = (LibLastRip.MetaInfo)Args;
			this.TrackDuration = 0;	
			this.StatusBar.Value = 0;
			
			//Get length of the track
			System.Int32 TrackLength = System.Convert.ToInt32(System.Convert.ToInt32(Info.Trackduration));
			if(TrackLength < 1) //If smaller then 1 set to 1, to avoid devision by zero error fixing issue 48, I think :)
				this.StatusBar.Maximum = 1;
			else
				this.StatusBar.Maximum = TrackLength;
			
			if(Info.Streaming)
			{
				System.String StrT = "";
				StrT += "Track: " + Info.Track;
				StrT += "\nArtist: " + Info.Artist;
				StrT += "\nAlbum: " + Info.Album;
				StrT += "\nDuration: " + Info.Trackduration + " sec.";
				this.StatusLabel.Text = StrT;
				
				//TODO: multithread download of album cover
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
					{
						//Exceptions may accour if URL is bad, bad connection, etc... We'll just ignore that since it's not critical
					}
				}
			}
		}
		
		protected virtual void UpdateProgress(System.Object Sender, System.EventArgs Args)
		{
			System.Int32 Next = this.StatusBar.Value + MainForm.UIUpdateInterval/1000;
			if(Next >= this.StatusBar.Maximum)
			{	//If bigger than Max set it to max to avoid problems when running over
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
			//TODO: Fix to run on mono for OS X using OPEN
			System.Diagnostics.Process.Start("http://code.google.com/p/thelastripper/wiki/LegalNotice");
		}
		
		void OnlineHelpToolStripMenuItemClick(object sender, EventArgs e)
		{
			//TODO: Fix to run on mono for OS X using OPEN
			System.Diagnostics.Process.Start("http://code.google.com/p/thelastripper/wiki/HelpWindows");
			//TODO: Have a look at issue 45, apply patch to both "online help" and "legal issue"
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			About ab = new About();
			ab.ShowDialog(this);
		}
		
		void TuneInButtonClick(object sender, EventArgs e)
		{
			this.TuneInButton.Enabled = false;
			this.Manager.ChangeStation(this.RadioStation.Text);
		}
		
		void TuneInCallback(System.Object Sender, System.EventArgs e)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.TuneInCallback), new System.Object[]{Sender, e});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			
			LibLastRip.StationChangedEventArgs Args = (LibLastRip.StationChangedEventArgs)e;
			this.TuneInButton.Enabled = true;
			if(Args.Success){
				this.EnableCommands();
				this.Timer.Enabled = true;
			}
		}
		
		void SkipButtonClick(object sender, EventArgs e)
		{
			this.DisableCommands();
			this.Manager.SkipSong();
		}
		
		void LoveButtonClick(object sender, EventArgs e)
		{
			this.DisableCommands();
			this.Manager.LoveSong();
		}
		
		void HateButtonClick(object sender, EventArgs e)
		{
			this.DisableCommands();
			this.Manager.BanSong();
		}
		
		void CommandCallback(object Sender, EventArgs e)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.CommandCallback), new System.Object[]{Sender, e});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			
			//LibLastRip.CommandEventArgs Args = (LibLastRip.CommandEventArgs)e;
			this.EnableCommands();
		}
		
		/// <summary>
		/// Disables all commandbuttons
		/// </summary>
		void DisableCommands()
		{
			this.LoveButton.Enabled = false;
			this.HateButton.Enabled = false;
			this.SkipButton.Enabled = false;
		}	
		
		/// <summary>
		/// Enables all command buttons
		/// </summary>
		void EnableCommands()
		{
			this.LoveButton.Enabled = true;
			this.HateButton.Enabled = true;
			this.SkipButton.Enabled = true;
		}
	}
}