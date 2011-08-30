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
using System.Threading;

namespace WinFormsMultipleStreamsClient
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class StreamForm : Form
	{
        private User myUser;
        private MultipleStreamSettings multiSettings;

        private LibLastRip.LastManager manager;

        public LibLastRip.LastManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }
        private Settings settings;

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }
		private LockerPut.Locker locker;

        public static void Main(string[] args)
        {
            //Handle all unhandled exceptions as they must be fatal :)
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (System.Exception e)
            {
                FatalExceptionHandler F = new FatalExceptionHandler(e);
                F.ShowDialog();
            }
        }

		public StreamForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			//
			this.settings = Settings.Restore();
			this.manager = this.settings.Manager;
			this.locker = this.settings.locker;
            serverConnecting = true;
			if(this.manager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
			{
				this.RadioStationCb.Enabled = true;
				this.TuneInButton.Enabled = true;
			}
			this.manager.OnNewSong += new EventHandler(this.OnNewSong);
			this.manager.OnProgress += new EventHandler(this.OnProgress);
			this.manager.OnLog += new EventHandler(this.OnLog);
			this.manager.OnScanning += new EventHandler(this.OnScanning);
			
			//Subscribe to stations changed event
			this.manager.StationChanged += new EventHandler(this.TuneInCallback);
			
			//Subscribe to command callback
			this.manager.CommandReturn += new EventHandler(this.CommandCallback);
			
			//Subscribe to OnError
			this.manager.OnError += new EventHandler(this.OnError);
			
			//Subscribe to locker
			if(this.locker != null && this.locker.IsLoggedin){
				this.locker.OnPutTrackComplete += new EventHandler<LockerPut.PutTrackCompleteEventArgs>(this.LockerPutSongCallback);
				this.manager.SongCompleted += new EventHandler<LibLastRip.SongCompletedEventArgs>(this.SongCompleted);
			}
		}


        public StreamForm(MultipleStreamSettings source, User user)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //
            //
            Settings mySettings = new Settings(source,
    user);
            multiSettings = source;

            myUser = user;
            mySettings.Manager.HandshakeReturn += new EventHandler(Manager_HandshakeReturn);
            this.settings = mySettings;
            this.manager = this.settings.Manager;
            this.locker = this.settings.locker;
            this.manager.IpAdress = user.IpAdress;
            this.manager.ListeningPortNumber = user.Port;
 
        

            if (this.manager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
            {
                this.RadioStationCb.Enabled = true;
                this.TuneInButton.Enabled = true;
            }
            this.manager.OnNewSong += new EventHandler(this.OnNewSong);
            this.manager.OnProgress += new EventHandler(this.OnProgress);
            this.manager.OnLog += new EventHandler(this.OnLog);
            this.manager.OnScanning += new EventHandler(this.OnScanning);
            

            //Subscribe to stations changed event
            this.manager.StationChanged += new EventHandler(this.TuneInCallback);

            //Subscribe to command callback
            this.manager.CommandReturn += new EventHandler(this.CommandCallback);

            //Subscribe to OnError
            this.manager.OnError += new EventHandler(this.OnError);
            

            //Subscribe to locker
            if (this.locker != null && this.locker.IsLoggedin)
            {
                this.locker.OnPutTrackComplete += new EventHandler<LockerPut.PutTrackCompleteEventArgs>(this.LockerPutSongCallback);
                this.manager.SongCompleted += new EventHandler<LibLastRip.SongCompletedEventArgs>(this.SongCompleted);
            }
            RadioElementCb.Text = mySettings.SearchText;  
        }

        void Manager_HandshakeReturn(object sender, EventArgs e)
        {
            //HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
            //Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
            //This is done since the event launching the method may occur on a different thread.
            if (this.InvokeRequired)
            {
                //Invoke this method and it's arguments to the correct thread.
                this.Invoke(new System.EventHandler(this.Manager_HandshakeReturn), new System.Object[] { sender, e });
                //Return this method to avoid executing the logic on the wrong thread.
                return;
            }
            //Check if we're on the right thread now, we should be!
            System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
            serverConnecting = false;
            TuneInNow();
        }

        bool serverConnecting = false;

        public void RefreshManagerAndTuneIn()
        {
            if (!serverConnecting && this.Settings.Manager.Server != null)
            {
          
                serverConnecting = true;
   
                this.Settings.Manager.Server.Close();
         


                this.Settings = new Settings(multiSettings, myUser);
                this.manager = this.settings.Manager;

                Settings.Manager.HandshakeReturn += new EventHandler(Manager_HandshakeReturn);
                this.settings = Settings;
                this.manager = this.settings.Manager;
                this.locker = this.settings.locker;
                this.manager.IpAdress = myUser.IpAdress;
                this.manager.ListeningPortNumber = myUser.Port;



                if (this.manager.ConnectionStatus != LibLastRip.ConnectionStatus.Created)
                {
                    this.RadioStationCb.Enabled = true;
                    this.TuneInButton.Enabled = true;
                }
                this.manager.OnNewSong += new EventHandler(this.OnNewSong);
                this.manager.OnProgress += new EventHandler(this.OnProgress);
                this.manager.OnLog += new EventHandler(this.OnLog);
                this.manager.OnScanning += new EventHandler(this.OnScanning);


                //Subscribe to stations changed event
                this.manager.StationChanged += new EventHandler(this.TuneInCallback);

                //Subscribe to command callback
                this.manager.CommandReturn += new EventHandler(this.CommandCallback);

                //Subscribe to OnError
                this.manager.OnError += new EventHandler(this.OnError);


                //Subscribe to locker
                if (this.locker != null && this.locker.IsLoggedin)
                {
                    this.locker.OnPutTrackComplete += new EventHandler<LockerPut.PutTrackCompleteEventArgs>(this.LockerPutSongCallback);
                    this.manager.SongCompleted += new EventHandler<LibLastRip.SongCompletedEventArgs>(this.SongCompleted);
                }
                RadioElementCb.Text = this.Settings.SearchText;
            }
            

        }
        
        public void TuneInNow()
        {
            RadioElementCb.Text = myUser.StationText;

           
            switch (settings.Genre)
            {
                case "Artist":
                    TuneInTo("lastfm://artist/" + RadioElementCb.Text + "/similarartists");
                    break;
                case "Tag":
                    TuneInTo("lastfm://globaltags/" + RadioElementCb.Text);
                    break;
                case "Personal":
                    if (String.IsNullOrEmpty(RadioElementCb.Text))
                    {
                        RadioElementCb.Text = manager.UserName;
                    }
                    TuneInTo("lastfm://user/" + RadioElementCb.Text + "/personal");
                    break;
                case "Group":
                    TuneInTo("lastfm://group/" + RadioElementCb.Text);
                    break;
                case "Playlist":
                    if (String.IsNullOrEmpty(RadioElementCb.Text))
                    {
                        RadioElementCb.Text = manager.UserName;
                    }
                    TuneInTo("lastfm://user/" + RadioElementCb.Text + "/playlist");
                    break;
                case "Loved":
                    if (String.IsNullOrEmpty(RadioElementCb.Text))
                    {
                        RadioElementCb.Text = manager.UserName;
                    }
                    TuneInTo("lastfm://user/" + RadioElementCb.Text + "/loved");
                    break;
                case "Neighbourhood":
                    if (String.IsNullOrEmpty(RadioElementCb.Text))
                    {
                        RadioElementCb.Text = manager.UserName;
                    }
                    TuneInTo("lastfm://user/" + RadioElementCb.Text + "/neighbours");
                    break;
                case "Recommendations":
                    if (String.IsNullOrEmpty(RadioElementCb.Text))
                    {
                        RadioElementCb.Text = manager.UserName;
                    }
                    TuneInTo("lastfm://user/" + RadioElementCb.Text + "/recommended/100");
                    break;

            }
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
            serverConnecting = false;

            
		}
		
		private void setStopButton(bool value) {
			if (this.manager.stopRecording == true) {
				  this.StopButton.Enabled = false;
			} else {
				this.StopButton.Enabled = value;
			}
		}
		
		private void OnScanning(System.Object sender, System.EventArgs args)
		{
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler(this.OnScanning), new System.Object[]{sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			LibLastRip.ScanningEventArgs scanningArgs = (LibLastRip.ScanningEventArgs)args;
			if (LibLastRip.ScanningEventArgs.SCANNING_STARTED.Equals(scanningArgs.Streamprogress)) {
				this.TrackLabel.Text = "Scanning started -";
				this.ArtistLabel.Text = "Artist: ";
				this.AlbumLabel.Text = "Album: ";
				this.RemainingTimeLabel.Text = "Remaining Time: ";
				this.DurationLabel.Text = "Duration: ";
				this.TracknrLabel.Text = "Track Nr.: ";
				this.GenreLabel.Text = "Genre: ";
				this.StationLabel.Text = "Station: ";
				setStopButton(true);
			} else if (LibLastRip.ScanningEventArgs.SCANNING_STOPPED.Equals(scanningArgs.Streamprogress)) {
				this.TrackLabel.Text = "Nothing to do...";
				setStopButton(false);				
			} else if (LibLastRip.ScanningEventArgs.SCANNING_PROGRESS.Equals(scanningArgs.Streamprogress)) {
				if ("Scanning started -".Equals(this.TrackLabel.Text)) {
					this.TrackLabel.Text = "Scanning started \\";
				} else if ("Scanning started \\".Equals(this.TrackLabel.Text)) {
					this.TrackLabel.Text = "Scanning started |";
				} else if ("Scanning started |".Equals(this.TrackLabel.Text)) {
					this.TrackLabel.Text = "Scanning started /";
				} else {
					this.TrackLabel.Text = "Scanning started -";
				}
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
				this.RemainingTimeLabel.Text = "";
			} else {
				this.StatusBar.Value = progressArgs.Streamprogress;
				System.Int32 time = (this.StatusBar.Maximum - progressArgs.Streamprogress);
				System.String timerem = (time / 60) + ":";
				time = time % 60;
				if(time < 10)
					timerem += "0";
				timerem += time;
				this.RemainingTimeLabel.Text = "Remaining Time: " + timerem;
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

			this.LogListBox.BeginUpdate();

			if (this.LogListBox.Items.Count > 250) {
				int _topIndex = this.LogListBox.TopIndex;
				this.LogListBox.Items.RemoveAt(0);
				if (_topIndex > 0) {
					_topIndex--;
				}
				this.LogListBox.TopIndex = _topIndex;
			}
			this.LogListBox.Items.Add(logArgs.Log);
			
			this.LogListBox.EndUpdate();
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
		
			// enable all command buttons
			EnableCommands(true);
			
			LibLastRip.MetaInfo info = (LibLastRip.MetaInfo)args;
			this.StatusBar.Value = 0;
			
			//Get length of the track
			System.Int32 TrackLength = System.Convert.ToInt32(System.Convert.ToInt32(info.Trackduration));
			if(TrackLength < 1) //If smaller then 1 set to 1, to avoid devision by zero error fixing issue 48, I think :)
				this.StatusBar.Maximum = 1;
			else
				this.StatusBar.Maximum = TrackLength;
			
			if (info.isEmpty()) {
				this.TrackLabel.Text = "Not recording...";
				setStopButton(false);
				this.ArtistLabel.Text = "Artist: ";
				this.TracknrLabel.Text = "Track Nr.: ";
				this.AlbumLabel.Text = "Album: ";
				this.DurationLabel.Text = "Duration: ";
				this.StationLabel.Text = "Station: ";
				this.GenreLabel.Text = "Genre: ";
				this.RemainingTimeLabel.Text = "Remaining Time: ";
				this.StatuspictureBox.Image = null;
			} else {
				this.TrackLabel.Text = info.Track;
				setStopButton(true);
				this.ArtistLabel.Text = "Artist: " + info.Artist;
				this.TracknrLabel.Text = "Track Nr.: " + info.TrackNum;
				this.AlbumLabel.Text = "Album: " + info.Album;
				System.Int32 dur = System.Int32.Parse(info.Trackduration);
				System.String durat = (dur / 60) + ":";
				dur = dur % 60;
				if(dur < 10)
					durat += "0";
				durat += dur;
				this.DurationLabel.Text = "Duration: " + durat;
				this.StationLabel.Text = "Station: " + info.Station;
				if(!String.IsNullOrEmpty(info.Genre))
					this.GenreLabel.Text = "Genre: " + info.Genre;
				
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
		
		/// <summary>
		/// Handle song completed event
		/// </summary>
		void SongCompleted(Object Sender, LibLastRip.SongCompletedEventArgs args){
			//Note we're not on the UI thread
			
			//Try to upload to locker if this should be done
			if(this.locker != null && this.locker.IsLoggedin){
				this.locker.PutTrack(args.Filename);
			}
		}
		
		/// <summary>
		/// Handle locker put song callback
		/// </summary>
		void LockerPutSongCallback(Object Sender, LockerPut.PutTrackCompleteEventArgs args){
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new EventHandler<LockerPut.PutTrackCompleteEventArgs>(this.LockerPutSongCallback), new System.Object[]{Sender, args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			//We should probably log this somewhere else... but terminal is probably good enought for most purposes
			if(!args.Success){
				Console.WriteLine("Couldn't upload song to mp3tunes locker: " + args.Message);
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
				this.EnableCommands(true);
			}
		}
		
		void SkipButtonClick(object sender, EventArgs args)
		{
            SkipSong();
		}

        public void SkipSong()
        {
            // only skip if there is a song to skip
            if (!LibLastRip.MetaInfo.GetEmptyMetaInfo().Equals(this.manager.CurrentSong))
            {
                this.manager.SkipSong();
                this.EnableCommands(true);
            }
        }
		
		void LoveButtonClick(object sender, EventArgs args)
		{
			// only love if there is a song to love
			if (!LibLastRip.MetaInfo.GetEmptyMetaInfo().Equals(this.manager.CurrentSong)) {
				this.DisableCommands(false);
				this.manager.LoveSong();
			}
		}
		
		void HateButtonClick(object sender, EventArgs args)
		{
			// only hate if there is a song to hate
			if (!LibLastRip.MetaInfo.GetEmptyMetaInfo().Equals(this.manager.CurrentSong)) {
				this.DisableCommands(false);
				this.manager.BanSong();
			}
		}
		
		void StopButtonClick(object sender, System.EventArgs e)
		{
			// Empty next songs list
			this.manager.Stop();
			this.setStopButton(false);
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
			this.EnableCommands(true);
		}
		
		/// <summary>
		/// Disables all commandbuttons
		/// </summary>
		void DisableCommands(Boolean includeSkip)
		{
			this.LoveButton.Enabled = false;
			this.HateButton.Enabled = false;
			if (includeSkip) {
				this.SkipButton.Enabled = false;
			}
		}
		
		/// <summary>
		/// Enables all command buttons
		/// </summary>
		void EnableCommands(Boolean includeSkip)
		{
			this.LoveButton.Enabled = true;
			this.HateButton.Enabled = true;
			if (includeSkip) {
				this.SkipButton.Enabled = true;
			}
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