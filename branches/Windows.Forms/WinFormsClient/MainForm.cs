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
		
		private LibLastRip.LastManager manager;
		private Settings settings;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			//
			this.settings = Settings.Restore();
			this.manager = this.settings.manager;
			
			if(this.manager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
			{
				this.RadioStationCb.Enabled = true;
				this.TuneInButton.Enabled = true;
			}
			this.manager.OnNewSong += new EventHandler(this.OnNewSong);
			this.manager.OnProgress += new EventHandler(this.OnProgress);
			this.manager.OnLog += new EventHandler(this.OnLog);
			
			//Subscribe to stations changed event
			this.manager.StationChanged += new EventHandler(this.TuneInCallback);
			
			//Subscribe to command callback
			this.manager.CommandReturn += new EventHandler(this.CommandCallback);
			
			//Subscribe to OnError
			this.manager.OnError += new EventHandler(this.OnError);
		}
		
		private void OnError(System.Object sender, System.EventArgs args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.OnError), new System.Object[]{sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			LibLastRip.ErrorEventArgs Args = (LibLastRip.ErrorEventArgs)args;
			if(Args.Exception != null)
			{
				System.Windows.Forms.MessageBox.Show(Args.Message + "\nException:\n" + Args.Exception.ToString(),"An expected exception has occurred",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}else{
				System.Windows.Forms.MessageBox.Show(Args.Message, "A problem has occurred", System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Warning);
			}
		}
		
		private void OnProgress(System.Object sender, System.EventArgs args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.OnProgress), new System.Object[]{sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			LibLastRip.ProgressEventArgs progressArgs = (LibLastRip.ProgressEventArgs)args;
			if (progressArgs.Streamprogress > this.StatusBar.Maximum) {
				this.StatusBar.Value = this.StatusBar.Maximum;
			} else {
				this.StatusBar.Value = progressArgs.Streamprogress;
			}
		}
		
		private void OnLog(System.Object sender, System.EventArgs args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.OnLog), new System.Object[]{sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			LibLastRip.LogEventArgs logArgs = (LibLastRip.LogEventArgs)args;
			if (this.LogListBox.Items.Count > 100) {
				this.LogListBox.Items.RemoveAt(0);
			}
			this.LogListBox.Items.Add(logArgs.Log + "\n");
		}

		private void OnNewSong(System.Object sender, System.EventArgs args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.OnNewSong), new System.Object[]{sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			LibLastRip.MetaInfo info = (LibLastRip.MetaInfo)args;
			this.StatusBar.Value = 0;
			
			//Get length of the track
			System.Int32 TrackLength = System.Convert.ToInt32(System.Convert.ToInt32(info.Trackduration));
			if(TrackLength < 1) //If smaller then 1 set to 1, to avoid devision by zero error fixing issue 48, I think :)
				this.StatusBar.Maximum = 1;
			else
				this.StatusBar.Maximum = TrackLength;
			
			if (info.isEmpty()) {
				this.TrackLabel.Text = "nothing to do";
				this.ArtistLabel.Text = "";
				this.AlbumLabel.Text = "";
				this.DurationLabel.Text = "";
				this.StationLabel.Text = "";
				this.StatuspictureBox.Image = null;
			} else {
				this.TrackLabel.Text = info.Track;
				this.ArtistLabel.Text = "Artist: " + info.Artist;
				this.AlbumLabel.Text = "Album: " + info.Album;
				this.DurationLabel.Text = "Duration: " + info.Trackduration;
				this.StationLabel.Text = "Station: " + info.Station;
				
				//TODO: multithread download of album cover
				if(info.Albumcover != null && info.Albumcover.StartsWith("http://"))
				{
					try
					{
						System.Net.HttpWebRequest hReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(info.Albumcover);
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
		
		void SettingsToolStripMenuItemClick(object sender, EventArgs args)
		{
			this.settings.LaunchPreferences();
			if(this.manager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
			{
				this.RadioStationCb.Enabled = true;
				this.TuneInButton.Enabled = true;
			}
		}
		
		void GeneratePlaylistsToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.settings.Generate();
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs args)
		{
			Application.Exit();
		}
		
		void LegalIssuesToolStripMenuItemClick(object sender, EventArgs args)
		{
			try
			{
				System.Diagnostics.Process.Start("http://code.google.com/p/thelastripper/wiki/LegalNotice");
			}catch(System.Exception e){
				System.Windows.Forms.MessageBox.Show("Failed to open website: http://code.google.com/p/thelastripper/wiki/LegalNotice\nException:\n" + e.ToString(),"Failed to open browser",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}
		}
		
		void OnlineHelpToolStripMenuItemClick(object sender, EventArgs args)
		{
			try
			{
				System.Diagnostics.Process.Start("http://code.google.com/p/thelastripper/wiki/HelpWindows");
			}catch(System.Exception e){
				System.Windows.Forms.MessageBox.Show("Failed to open website: http://code.google.com/p/thelastripper/wiki/HelpWindows\nException:\n" + e.ToString(),"Failed to open browser",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs args)
		{
			About ab = new About();
			ab.ShowDialog(this);
		}
		
		void TuneInButtonClick(object sender, EventArgs args)
		{
			this.TuneInButton.Enabled = false;
			this.manager.ChangeStation(this.RadioStationCb.Text);
		}
		
		void TuneInCallback(System.Object sender, System.EventArgs args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.TuneInCallback), new System.Object[]{sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			
			LibLastRip.StationChangedEventArgs stationChangedEventArgs = (LibLastRip.StationChangedEventArgs)args;
			this.TuneInButton.Enabled = true;
			if(stationChangedEventArgs.Success){
				this.EnableCommands();
			}
		}
		
		void SkipButtonClick(object sender, EventArgs args)
		{
			// only skip if there is a song to skip
			if (!LibLastRip.MetaInfo.GetEmptyMetaInfo().Equals(this.manager.CurrentSong)) {
				this.manager.SkipSong();
			}
		}
		
		void LoveButtonClick(object sender, EventArgs args)
		{
			// only love if there is a song to love
			if (!LibLastRip.MetaInfo.GetEmptyMetaInfo().Equals(this.manager.CurrentSong)) {
				this.DisableCommands();
				this.manager.LoveSong();
			}
		}
		
		void HateButtonClick(object sender, EventArgs args)
		{
			// only hate if there is a song to hate
			if (!LibLastRip.MetaInfo.GetEmptyMetaInfo().Equals(this.manager.CurrentSong)) {
				this.DisableCommands();
				this.manager.BanSong();
			}
		}
		
		void CommandCallback(object sender, EventArgs args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.CommandCallback), new System.Object[]{sender, args});
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

		void TuneInTo(String station) {
			this.RadioStationCb.Text = station;
			this.TuneInButtonClick(null, null);
		}
		
		void ArtistButtonClick(object sender, EventArgs e)
		{
			TuneInTo("lastfm://artist/" + RadioElementCb.Text + "/similarartists");
		}
		
		void TagButtonClick(object sender, EventArgs e)
		{
			TuneInTo("lastfm://globaltags/" + RadioElementCb.Text);
		}

		void PlaylistButtonClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(RadioElementCb.Text)) {
				RadioElementCb.Text = manager.UserName;
			}
			TuneInTo("lastfm://user/" + RadioElementCb.Text + "/playlist");
		}
		
		void PersonalButtonClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(RadioElementCb.Text)) {
				RadioElementCb.Text = manager.UserName;
			}
			TuneInTo("lastfm://user/" + RadioElementCb.Text + "/personal");
		}

		void LovedButtonClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(RadioElementCb.Text)) {
				RadioElementCb.Text = manager.UserName;
			}
			TuneInTo("lastfm://user/" + RadioElementCb.Text + "/loved");
		}
		
		void GroupButtonClick(object sender, EventArgs e)
		{
			TuneInTo("lastfm://group/" + RadioElementCb.Text);
		}
		
		void NeighbourhoodButtonClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(RadioElementCb.Text)) {
				RadioElementCb.Text = manager.UserName;
			}
			TuneInTo("lastfm://user/" + RadioElementCb.Text + "/neighbours");
		}
		
		void RecommendationsButtonClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(RadioElementCb.Text)) {
				RadioElementCb.Text = manager.UserName;
			}
			TuneInTo("lastfm://user/" + RadioElementCb.Text + "/recommended/100");
		}
	}
}
