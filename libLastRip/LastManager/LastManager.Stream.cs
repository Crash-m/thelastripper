// LibLastRip - A Last.FM ripping library for TheLastRipper
// Copyright (C) 2007  Jop... (Jonas F. Jensen).
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace LibLastRip
{
	/*
	This part of the class handles all stream related matters.
	 */
	public partial class LastManager
	{
		protected static System.Int32 BufferSize = 8192; //8 KiB
		protected XSPF xspf = XSPF.GetEmptyXSPF();
		protected XSPFTrack currentXspfTrack = XSPFTrack.GetEmptyXSPFTrack();
		protected Stream Song;
		protected System.String _Filename;
		protected System.Boolean SkipSave = false;
		public System.Boolean stopRecording = false;
		protected System.Int32 counter = 0;
		protected System.Byte []Buffer = new System.Byte[LastManager.BufferSize];
		protected Hashtable excludeFile = null;
		public System.String AfterRipCommand = "\""+System.Environment.GetEnvironmentVariable("PROGRAMFILES")+"\\Winamp\\Winamp.exe\" /add \"%F\"";
		public System.Boolean SaveDirectlyToDisc = false;
		public System.String NewSongCommand = "\""+System.Environment.GetEnvironmentVariable("PROGRAMFILES")+"\\Winamp\\Winamp.exe\" \"%F\"";
		private bool _executed;
		private ManualResetEvent allDone = new ManualResetEvent(false);
		private Socket server;
		private ArrayList connections;
		public Int32 ListeningPortNumber = 8000;
		public bool OverwriteExistingMusic = false;
		
		/// <summary>
		/// Bytes of the stream that have been announced to OnProgress Event
		/// </summary>
		protected System.Int64 LastPosition = 1;
		
		/// <summary>
		/// Occurs when an handled error happens
		/// </summary>
		/// <remarks>The arguments can be casted to LibLastRip.ErrorEventArgs</remarks>
		public event System.EventHandler OnError;
		
		protected void writeLogLine(String logText) {
			Console.WriteLine(logText);
			if (this.OnLog != null) {
				this.OnLog(this, new LogEventArgs(logText));
			}
		}

		private void AddChildren(XSPFTrack xspfTrack, XmlNode xnod, int level) {
			String pad = new String(' ', level * 2);

			// if this is an element, extract any attributes
			if (xnod.NodeType == XmlNodeType.Element)
			{
				XmlNamedNodeMap mapAttributes = xnod.Attributes;
				if ("location".Equals(xnod.Name)) {
					// got song url
					xspfTrack.Location = xnod.InnerText;
				}
				if ("title".Equals(xnod.Name)) {
					xspfTrack.Title = xnod.InnerText;
				}
				if ("id".Equals(xnod.Name)) {
					xspfTrack.Id = xnod.InnerText;
				}
				if ("album".Equals(xnod.Name)) {
					xspfTrack.Album = xnod.InnerText;
				}
				if ("creator".Equals(xnod.Name)) {
					xspfTrack.Creator = xnod.InnerText;
				}
				if ("duration".Equals(xnod.Name)) {
					xspfTrack.Duration = xnod.InnerText;
				}
				if ("image".Equals(xnod.Name)) {
					xspfTrack.Image = xnod.InnerText;
				}
				if ("lastfm:trackauth".Equals(xnod.Name)) {
					xspfTrack.LastFm.Trackauth = xnod.InnerText;
				}
				if ("lastfm:albumId".Equals(xnod.Name)) {
					xspfTrack.LastFm.AlbumId = xnod.InnerText;
				}
				if ("lastfm:artistId".Equals(xnod.Name)) {
					xspfTrack.LastFm.ArtistId = xnod.InnerText;
				}
				if ("link".Equals(xnod.Name)) {
					// TODO: this is a list xspf.link = xnod.Value;
				}
			}
		}
		
		// Display a node and its children
		private void AddChildren(XmlNode xnod, int level)
		{
			XmlNode xnodWorking;
			String pad = new String(' ', level * 2);
			
			// call recursively on all children of the current node
			if (xnod.HasChildNodes)
			{
				// collect some informations which arent used ;-)
				if ("link".Equals(xnod.Name)) {
					// link is "9999" - skips left as attribute; not needed
					// xspf.Link = xnod.InnerText;
				} else
					if ("playlist".Equals(xnod.Name)) {
					// empty element, contains tracks as children
				} else
					if ("creator".Equals(xnod.Name)) {
					// creator is "Last.fm"; not needed
					xspf.Creator = xnod.InnerText;
				} else
					if ("title".Equals(xnod.Name)) {
					// title is an empty text since some time, so do not use it later; not needed
					xspf.Title = xnod.InnerText;
				}
				
				if ("track".Equals(xnod.Name)) {
					// track handling
					XSPFTrack xspfTrack = XSPFTrack.GetEmptyXSPFTrack();

					xnodWorking = xnod.FirstChild;
					while (xnodWorking != null)
					{
						AddChildren(xspfTrack, xnodWorking, level+1);
						xnodWorking = xnodWorking.NextSibling;
					}
					
					if (xspfTrack.Location != null) {
						xspf.AddTrack(xspfTrack);
					}
				} else {
					// go through everything whats not a track while searching for tracks
					xnodWorking = xnod.FirstChild;
					while (xnodWorking != null)
					{
						AddChildren(xnodWorking, level+1);
						xnodWorking = xnodWorking.NextSibling;
					}
				}
			}
		}
		
		protected bool CheckFileInDirectory(String FilePath, String title, String creator, String album) {
			DirectoryInfo dir = new DirectoryInfo(FilePath);
			FileInfo[] files = getFiles(dir,title);
			// if exist a file with the song title
			foreach(FileInfo file_1 in files){
				if(IsCompatibleSong(file_1,creator,album,title))
					return true;
			}
			DirectoryInfo[] dirs = getDirectories(dir,creator);
			//if exist a folder with artist name
			foreach(DirectoryInfo folder in dirs){
				files = getFiles(folder,title);
				if(IsSongInFiles(files,creator,album,title))
					return true;
				DirectoryInfo[] folders = getDirectories(folder,album);
				//if exists a folder with album name
				foreach(DirectoryInfo fold in folders){
					files = getFiles(fold,title);
					if(IsSongInFiles(files,creator,album,title))
						return true;
				}
			}
			dirs = getDirectories(dir,album);
			//if exist a folder with album name
			foreach(DirectoryInfo folder in dirs){
				files = getFiles(folder,title);
				if(IsSongInFiles(files,creator,album,title))
					return true;
			}
			return false;
		}
		
		protected bool processExcludeExisting() {
			if (_ExcludeExistingMusic) {
				// we check with original data (title, creator, album) from last.fm against files in music collection
				// the converted (RemoveInvalidPathChars) variables should only be used for file access
				if (CheckFileInDirectory(this._MusicPath, this.currentXspfTrack.Title, this.currentXspfTrack.Creator, this.currentXspfTrack.Album)) {
					// File exists - dont process
					counter++;
					writeLogLine("skipFEc(" + counter.ToString() + ") " + "'" + this.currentXspfTrack.Title + "' (" + this.currentXspfTrack.Album + ") " + " from '" + this.currentXspfTrack.Creator + "'");
					return false;
				}
				if (String.IsNullOrEmpty(this._QuarantinePath) == false) {
					if ((CheckFileInDirectory(this._QuarantinePath, this.currentXspfTrack.Title, this.currentXspfTrack.Creator, this.currentXspfTrack.Album))) {
						// File exists - dont process
						counter++;
						writeLogLine("skipFEq(" + counter.ToString() + ") " + "'" + this.currentXspfTrack.Title + "' (" + this.currentXspfTrack.Album + ") " + " from '" + this.currentXspfTrack.Creator + "'");
						return false;
					}
				}
			}
			return true;
		}
		
		protected bool processExcludeFile() {
			if (String.IsNullOrEmpty(ExcludeFile) == false) {
				// if excludeFile contains artist then skip
				
				if (excludeFile == null) {
					excludeFile = new Hashtable();
					
					string line = null;
					using (StreamReader reader = File.OpenText(ExcludeFile)) {
						line = reader.ReadLine();
						while (line != null) {
							excludeFile.Add(line, line);
							line = reader.ReadLine();
						}
					}
				}
				
				if (excludeFile.Contains(this.currentXspfTrack.Creator)) {
					writeLogLine("skipExF(" + counter.ToString() + ") " + this.currentXspfTrack.Creator);
					return false;
				}
			}

			return true;
		}
		
		protected bool processExcludeNew() {
			if (ExcludeNewMusic) {
				// TODO: Works only if filename pattern starts with "%a\..." - this should be included in documentation
				System.String creator = LastManager.RemoveInvalidPathChars(this.currentXspfTrack.Creator);

				// Directory exists not - dont process
				System.String CreatorPath = this.MusicPath + Path.DirectorySeparatorChar + creator;
				System.String QuarantineCreatorPath = this.QuarantinePath + Path.DirectorySeparatorChar + creator;
				if (!Directory.Exists(CreatorPath) && !Directory.Exists(QuarantineCreatorPath)) {
					counter++;
					writeLogLine("skipDnE(" + counter.ToString() + ") " + CreatorPath);
					return false;
				}
			}
			return true;
		}
		
		protected bool processFile() {
			// ProcessModes (multiple choices allowed)
			// a) reload existing files DEFAULT=false
			// b) only load existing artists DEFAULT=false
			
			if (!processExcludeNew()) {
				return false;
			}

			if (!processExcludeFile()) {
				return false;
			}

			if (!processExcludeExisting()) {
				return false;
			}

			writeLogLine("get '" + this.currentXspfTrack.Title + "' (" + this.currentXspfTrack.Album + ") " + " from '" + this.currentXspfTrack.Creator + "'");

			// Default: Process file
			return true;

		}
		
		protected void StopRecording() {
			this.Status = ConnectionStatus.Connected;
			this.stopRecording = false;
			this.currentSong = MetaInfo.GetEmptyMetaInfo();
			if(this.OnNewSong != null)
				this.OnNewSong(this, this.currentSong);
		}
		
		private void RequestPlaylist() {
			// Getting Playlist
			String url = "http://" + this.BaseURL + this.BasePath + "/xspf.php?sk=" + this.SessionID + "&discovery=0&desktop=1.5.1";
			WebRequest wReq = WebRequest.Create(url);
			// setting timeout to 15s
			wReq.Timeout = 1000 * 15;
			HttpWebResponse hRes = (HttpWebResponse)wReq.GetResponse();
			
			Stream Stream = hRes.GetResponseStream();
			
			XmlTextReader xmlTextReader = new XmlTextReader(Stream);
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
			
			// load the file into an XmlDocuent
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlTextReader);
			
			// get the document root node
			XmlNode xmlNode = xmlDocument.DocumentElement;
			
			// recursively walk the node tree
			AddChildren(xmlNode, 0);
			
			// close the reader
			xmlTextReader.Close();
		}
		
		protected void StartRecording(bool newStation) {
			if (this.OnScanning != null) {
				// Update Scanning bar
				this.OnScanning(this, new ScanningEventArgs(ScanningEventArgs.SCANNING_STARTED));
			}

			if (newStation) {
				// new station could be already stored in xspf, so use this
				String station = xspf.Station;
				xspf = XSPF.GetEmptyXSPF();
				xspf.Station = station;
				currentXspfTrack = XSPFTrack.GetEmptyXSPFTrack();
				RequestPlaylist(); // to avoid delaying the first request
			}
			
			bool started = false;
			
			// number of times to try access to playlist - the playlist request can fail or contain an empty list.
			int tryCounter = 8;
			
			while (started == false && this.Status == ConnectionStatus.Recording) {
				if (xspf.CountTracks() == 0) {
					try {
						if (this.OnScanning != null) {
							// Update Scanning bar
							this.OnScanning(this, new ScanningEventArgs(ScanningEventArgs.SCANNING_PROGRESS));
						}
						Thread.Sleep(250);
						RequestPlaylist();
					} catch (WebException) {
						writeLogLine("Server request failed.");
					} catch (Exception) {
						// Web request timeout or other unexpected responses - just continue
						writeLogLine("Timeout/Server failure occured.");
					}
				}
				if (xspf.CountTracks() > 0) {
					this.currentXspfTrack = (XSPFTrack)xspf.getTrack();
					this.StreamURL = this.currentXspfTrack.Location;

					//Check if file exists
					if(processFile())
					{
						try {
							started = true;
							//Getting stream
							WebRequest wReq = WebRequest.Create(this.StreamURL);
							HttpWebResponse hRes = (HttpWebResponse)wReq.GetResponse();
							//this._SongUrl = hRes.ResponseUri.AbsoluteUri;
							//writeLogLine("Response uri: " + hRes.ResponseUri.AbsoluteUri);
							System.IO.Stream RadioStream = hRes.GetResponseStream();

							//Start reading process
							// TODO: Lock could be active if response is fast - re-think about stream locking!
							RadioStream.BeginRead(this.Buffer, 0, LastManager.BufferSize,new System.AsyncCallback(this.Save), RadioStream);
						} catch (Exception e) {
							handleError(true, new ErrorEventArgs(e.ToString()));
							
							// no way to continue...
							StopRecording();
						}
					}
				} else {
					// This code will only be executed if RequestPlaylist fails with an exception or if it gets no tracks
					tryCounter = tryCounter - 1;
					writeLogLine("Retries left: " + tryCounter);

					if (tryCounter <= 0) {
						handleError(true, new ErrorEventArgs("No playlist found or no more songs available. Please restart ripping."));
						
						// no way to continue...
						StopRecording();
					} else {
						// give last.fm time to send more songs
						Thread.Sleep(500);
					}
				}
				
				if (this.stopRecording == true) {
					StopRecording();
				}
			}
			if (started == false && this.OnScanning != null) {
				// Update Scanning bar
				this.OnScanning(this, new ScanningEventArgs(ScanningEventArgs.SCANNING_STOPPED));
			}
		}
		
		protected void StartRecording()
		{
			//Aquire lock for the stream
			//if(System.Threading.Monitor.TryEnter(LastManager.ReadStreamLock))
			{
				StartRecording(true);

				//Release lock when starting asynchronious call, since it will be used there
				//System.Threading.Monitor.Exit(LastManager.ReadStreamLock);
			}
		}
		
		protected double CalculateHealth(Object length, Object duration) {
			double len = double.Parse(length.ToString())*8;
			double dur = double.Parse(duration.ToString()) * 128;
			
			//writeLogLine("len: " + len.ToString());
			//writeLogLine("dur: " + dur.ToString());
			//writeLogLine("DELTA-" + (len-dur).ToString());
			
			double factor = Double.Parse(_HealthValue);
			//writeLogLine("DELTAFACTOR-" + (len+factor-dur).ToString());

			double songHealth = (len+factor)/dur*100;
			writeLogLine("song health " + (songHealth).ToString());

			return songHealth;
		}
		
		protected void SaveToFile() {
			//writeLogLine("Song length in bytes: " + this.Song.Length);
			//writeLogLine("Song length announced: " + this.currentXspfTrack.Duration.ToString());

			//Should we save this song?
			if(this.SkipSave || this.CurrentSong == MetaInfo.GetEmptyMetaInfo())
			{
				writeLogLine("Song skipped (incomplete or user request)");

				//If not, then don't save it
				this.SkipSave = false;
				this.Song.Close();
				if(this.Song is FileStream){
					FileInfo file = new FileInfo(_Filename);
					file.Delete();
				}
			}else{
				bool complete = true;
				if (_HealthEnabled) {
					double songHealth = CalculateHealth(this.Song.Length, this.currentXspfTrack.Duration);
					complete = songHealth >= 100;
				}

				//If so, then save it but do it on another thread
				SaveSongCall SSC = new SaveSongCall(this.SaveSong);
				//Minus one since we don't want the song to end with char 83 = 'S' from SYNC
				SSC.BeginInvoke(this.Song, (int)this.Song.Length, this.CurrentSong, complete, new System.AsyncCallback(this.SaveSongCallback), this.Song);
			}
			
			//Replace this.Song with NewSong, and hope that the asynchronious request keeps the old object.
			if(this.Song is MemoryStream)
				this.Song = new MemoryStream();
		}
		
		/// <summary>
		/// This Method saves data from buffer to song
		/// </summary>
		protected void Save(System.Int32 read) {
			bool firstRead = this.Song.Length == 0;
			//Write data from buffer to memory
			this.Song.Write(this.Buffer,0,read);
			if(this.server == null)
				intializeServer();
			listen();
			sendToClients(read);
			
			if(this.Song is FileStream)
				if(this.Song.Length > 200 * 1024 && ! this._executed){
				ExecuteCommand(this.NewSongCommand,this.CurrentSong);
				this._executed = true;
			}
			//System.Byte []Buf = ((FileStream)this.Song).GetBuffer();
			
			if (this.OnProgress != null) {
				// Update Progress bar every 2 seconds.
				if (this.Song.Length < LastPosition || LastPosition + 16384*2 < this.Song.Length)
				{
					//Note: 16383 [Byte/sec]
					LastPosition = this.Song.Length;
					this.OnProgress(this, new ProgressEventArgs((int)this.Song.Length / 16384));
				}
			}
		}
		
		/// <summary>
		/// This Method saves a stream until it ends
		/// </summary>
		protected void Save(System.IAsyncResult Res)
		{
			//Aquire a lock for the stream, to ensure that it's not already in use
			//if(!System.Threading.Monitor.TryEnter(LastManager.ReadStreamLock)) {
			//If the stream is locked, throw an exception.
			//	throw new UnauthorizedAccessException("Illegal call to method Save - process is already active");
			//}
			try {
				//Parse the xspf data into MetaInfo structure
				MetaInfo nSong = new MetaInfo(xspf, currentXspfTrack);
				
				//Is this a new song?
				if(!MetaInfo.Equals(nSong,this.currentSong))
				{
					this._Filename = GetFilename(this.filename_pattern,nSong);
					Song = NewStream();
					//Save metadata and raise OnNewSong event
					this.currentSong = nSong;
					if(this.OnNewSong != null)
						this.OnNewSong(this, this.currentSong);
				}
				this._executed = false;
				//Save data read from async read.
				Stream RadioStream = (Stream)Res.AsyncState;
				System.Int32 read = RadioStream.EndRead(Res);
				Save(read);
				
				// we just read syncron from here - no nead for another AsyncCallback!
				while (read > 0 && SkipSave == false && this.Status == ConnectionStatus.Recording) {
					read = RadioStream.Read(this.Buffer, 0, LastManager.BufferSize);
					Save(read);
				}
				if (SkipSave == false) {
					// If this line is reached we have no more data in stream
					SaveToFile();
				} else {
					RadioStream.Close();
					SaveToFile();
				}
				
				if (this.stopRecording == false) {
					// Continue recording with next stream
					StartRecording(false);
				} else {
					StopRecording();
				}
				
			} catch (Exception e) {
				// Catch all exceptions to prevent application from falling into a illegal state
				// Raise event so client can display a message
				handleError(true, new ErrorEventArgs("Exception occurred. Please restart ripping.", e));

				this.RestoreState();
			} finally {
				//System.Threading.Monitor.Exit(LastManager.ReadStreamLock);
			}
		}
		
		protected void handleError(bool resetSong, ErrorEventArgs args)
		{
			//Parse the xspf data into MetaInfo structure
			MetaInfo nSong = MetaInfo.GetEmptyMetaInfo();
			
			//Is this a new song?
			if(!MetaInfo.Equals(nSong,this.currentSong))
			{
				//delete error file
				if(this.Song is FileStream){
					FileInfo file = new FileInfo(this._Filename);
					if(file.Exists)
						file.Delete();
				}
				//start next song
				this._Filename = GetFilename(this.filename_pattern,nSong);
				Song = NewStream();
				//Save metadata and raise OnNewSong event
				this.currentSong = nSong;
				if(this.OnNewSong != null) {
					this.OnNewSong(this, this.currentSong);
				}
				this._executed = false;
			}
			
			if (this.OnError != null) {
				this.OnError(this, args);
			}
			
			if (this.Status == ConnectionStatus.Recording) {
				SkipSave = true;
				this.Status = ConnectionStatus.Connected;
			}
		}
		
		/// <summary>
		/// Restore the default variables at the state ConnectionStatus.Connected, when a stream has ended.
		/// </summary>
		protected void RestoreState()
		{
			//Aquire lock to insure Stream isn't in use
			//if(System.Threading.Monitor.TryEnter(LastManager.ReadStreamLock))
			{
				//Song = new System.IO.MemoryStream();
				Song = NewStream();
				Buffer = new System.Byte[LastManager.BufferSize];
				
				LastPosition = 1;
				
				// Metainfo
				currentSong = MetaInfo.GetEmptyMetaInfo();
				
				// LastManager
				Status = ConnectionStatus.Connected;
				
				//Release lock again
				//System.Threading.Monitor.Exit(LastManager.ReadStreamLock);
				
				if(this.OnNewSong != null) {
					this.OnNewSong(this, this.currentSong);
				}
			}
		}
		
		protected void SaveSongCallback(System.IAsyncResult Ar)
		{
			SaveSongCall SSC = (SaveSongCall)(((System.Runtime.Remoting.Messaging.AsyncResult)Ar).AsyncDelegate);

			try{
				SSC.EndInvoke(Ar);
			}catch(Exception){}
			
			if(this.Song is MemoryStream)
				((MemoryStream)Ar.AsyncState).Close();
			//Close the old song
		}
		
		protected delegate void SaveSongCall(Stream Song, System.Int32 Count, MetaInfo SongInfo, bool complete);
		
		public event EventHandler<SongCompletedEventArgs> SongCompleted;
		
		///<summary>Save a song to disk</summary>
		///<param name="Song">A MemoryStream containing the song.</param>
		///<param name="Count">Number of bytes from MemoryStream to save.</param>
		///<param name="SongInfo">MetaInfo about the song to be saved.</param>
		protected void SaveSong(Stream Song, System.Int32 Count, MetaInfo SongInfo, bool complete)
		{
			SongInfo.Comment = this.Comment;
			try {
				String Filename = GetFilename(this.filename_pattern, SongInfo);
				if (!complete) {
					Filename += ".defect.mp3";
					writeLogLine("File may be corrupt, stored to " + Filename);
				}
				String AlbumPath = GetAlbumPath(this.filename_pattern, SongInfo);
				
				Boolean ignoredSinceWeDontOverwrite = false;
				if(this.Song is MemoryStream){
					if(!File.Exists(Filename) || OverwriteExistingMusic){
						checkDirectoriesInPath(Filename);
						FileStream FS = File.Create(Filename);
						FS.Write(((MemoryStream)Song).GetBuffer(), 0, Count);
						FS.Flush();
						FS.Close();
					}else
						ignoredSinceWeDontOverwrite = true;
				}else{
					this.Song.Close();
				}
				
				//Write metadata to stream as ID3v1
				if(!ignoredSinceWeDontOverwrite)
					SongInfo.AppendID3(Filename);
				
				//Run the SongCompleted event if nee
				if(this.SongCompleted != null && !ignoredSinceWeDontOverwrite)		//I think this is the right place for this event to be executed...
					this.SongCompleted(this, new SongCompletedEventArgs(SongInfo, Filename));
				
				try {
					//execute after rip command
					if(!ignoredSinceWeDontOverwrite)
						ExecuteCommand(this.AfterRipCommand,SongInfo);
				} catch (Exception) {
					writeLogLine("Exception occured while running after rip command: " + this.AfterRipCommand);
				}
				
				if (!String.IsNullOrEmpty(SongInfo.Albumcover)) {
					//Download covers - don't care for errors because some not exist
					WebClient Client = new WebClient();
					
					// First download larger covers - because small cover fails more often
					// TODO: FIRST call to DownloadFile will time out... why? Sleep helps...
					Thread.Sleep(5000);
					try {
						String cover = AlbumPath + Path.DirectorySeparatorChar + "cover.jpg";
						if((!File.Exists(cover)) && SongInfo.Albumcover != null) {
							writeLogLine("download cover " + cover);
							Client.DownloadFile(SongInfo.Albumcover, cover);
						}
					} catch (Exception e) {
						// no cover, no pain
						writeLogLine("Exception downloading cover: " + e.ToString());
					}
				}
			} catch (Exception e) {
				writeLogLine("Exception occured: " + e.ToString());
				// TODO: Sometimes the album path is wrong - could be null or contains illegal characters - no exception throwing because this stops ripping next songs
				// TODO: Consider launching an OnError event
			}
		}
		
		public String filename_pattern = "%a"+Path.DirectorySeparatorChar+"%r"+Path.DirectorySeparatorChar+"[%N - ]%t";
		
		public String FilenamePattern{
			set{
				value = value.Replace("/",Path.DirectorySeparatorChar.ToString());
				value = value.Replace("\\",Path.DirectorySeparatorChar.ToString());
				this.filename_pattern = value;
			}
			get{
				return this.filename_pattern;
			}
		}
		
		private static string ReplaceToken(string token,string key,string val,bool emptyOnError){
			if(String.IsNullOrEmpty(val)) {
				val = "";
			}
			val = val.Replace("[","_");
			val = val.Replace("]","_");
			key = key.ToLower();
			if(token.ToLower().IndexOf("%"+key) != -1)
				if(System.String.IsNullOrEmpty(val))
				if(emptyOnError)
				return "";
			else{
				token = token.Replace("%"+key,"");
				token = token.Replace("%"+key.ToUpper(),"");
			}
			else{
				val = LastManager.RemoveInvalidPathChars(val);
				token = token.Replace("%"+key,val);
				val = System.Text.RegularExpressions.Regex.Replace(val,"[^a-zA-Z0-9]{1}","_");
				token = token.Replace("%"+key.ToUpper(),val);
			}
			return token;
		}
		
		private static System.String ReplacePatternBool(System.String str,MetaInfo SongInfo,bool emptyOnError){
			while(str.IndexOf("[") != -1 && str.IndexOf("]") != -1){
				System.Int32 len = str.IndexOf("]");
				System.Int32 index = str.Substring(0,len).IndexOf("[") + 1;
				len -= index;
				System.String prefix = str.Substring(0,index-1);
				System.String suffix = str.Substring(index+len+1);
				System.String part = str.Substring(index,len);
				str = prefix + ReplacePatternBool(part,SongInfo,true) + suffix;
			}
			str = ReplaceToken(str,"a",SongInfo.Artist,emptyOnError);
			str = ReplaceToken(str,"r",SongInfo.Album,emptyOnError);
			str = ReplaceToken(str,"t",SongInfo.Track,emptyOnError);
			str = ReplaceToken(str,"s",SongInfo.Station,emptyOnError);
			str = ReplaceToken(str,"g",SongInfo.Genre,emptyOnError);
			if(str.IndexOf("%n") != -1 || str.IndexOf("%N") != -1) {
				if(SongInfo.TrackNum <= 0) {
					if(emptyOnError) {
						return "";
					} else {
						str = str.Replace("%N","");
						str = str.Replace("%n","");
					}
				}
				else
				{
					str = str.Replace("%n",LastManager.RemoveInvalidPathChars(""+SongInfo.TrackNum));
					if(SongInfo.TrackNum > 9)
						str = str.Replace("%N",LastManager.RemoveInvalidPathChars(""+SongInfo.TrackNum));
					else
						str = str.Replace("%N","0"+LastManager.RemoveInvalidPathChars(""+SongInfo.TrackNum));
				}
			}
			//remove null directories
			str = str.Replace(Path.DirectorySeparatorChar+""+Path.DirectorySeparatorChar,""+Path.DirectorySeparatorChar);
			return str;
		}

		public static System.String ReplacePattern(System.String str,MetaInfo SongInfo){
			str = ReplacePatternBool(str,SongInfo,false);
			return str;
		}
		
		private System.String GetAlbumPath(System.String pattern,MetaInfo SongInfo) {
			System.String albumPath = ReplacePattern(pattern,SongInfo) + ".mp3";
			albumPath = Path.DirectorySeparatorChar + albumPath.Substring(0, albumPath.LastIndexOf(Path.DirectorySeparatorChar));
			if (String.IsNullOrEmpty(_QuarantinePath)) {
				// write directly to music directory
				return _MusicPath + albumPath;
			} else {
				// write to quarantine directory
				return _QuarantinePath + albumPath;
			}
		}
		
		private System.String GetFilename(System.String pattern,MetaInfo SongInfo){
			System.String filename = Path.DirectorySeparatorChar + ReplacePattern(pattern,SongInfo) + ".mp3";
			if (String.IsNullOrEmpty(_QuarantinePath)) {
				// write directly to music directory
				return _MusicPath + filename;
			} else {
				// write to quarantine directory
				return _QuarantinePath + filename;
			}
		}
		
		public void ExecuteCommand(System.String comm,MetaInfo SongInfo){
			if(comm != ""){
				comm = ReplacePattern(comm,SongInfo);
				comm = comm.Replace("%F",_Filename);
				comm = comm.Replace("%f",_Filename.Substring(_Filename.LastIndexOf(Path.DirectorySeparatorChar)+1));
				//comm = comm.Replace("%u",_SongUrl);
				System.String cmd;
				if(comm.StartsWith("\""))
					cmd = comm.Substring(0,comm.Substring(1).IndexOf("\"")+2);
				else if(comm.IndexOf(" ") == -1)
					cmd = comm;
				else
					cmd = comm.Substring(0,comm.IndexOf(" "));
				System.String args;
				if(comm.Length > cmd.Length +1)
					args = comm.Substring(cmd.Length+1);
				else
					args = "";
				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo.FileName = cmd;
				proc.StartInfo.Arguments = args;
				//System.Threading.Thread.Sleep(500);
				proc.Start();
			}
		}
		
		public static XmlDocument getXmlDocument(System.String url){
			WebRequest wReq = WebRequest.Create(url);
			Stream stream = ((HttpWebResponse)wReq.GetResponse()).GetResponseStream();
			XmlTextReader xmlTextReader = new XmlTextReader(stream);
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlTextReader);
			return xmlDocument;
		}
		
		private Stream NewStream(){
			if(this.SaveDirectlyToDisc){
				checkDirectoriesInPath(_Filename);
				return new FileStream(_Filename,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
			}
			else
				return new MemoryStream();
		}
		
		private String workingPath() {
			if (String.IsNullOrEmpty(this._QuarantinePath)) {
				return this._MusicPath;
			} else {
				return this._QuarantinePath;
			}
		}
		
		private void checkDirectoriesInPath(String filename){
			System.String workPath = workingPath();
			System.String AlbumPath = filename.Substring(workPath.Length+1, filename.LastIndexOf(Path.DirectorySeparatorChar)-workPath.Length-1);
			String[] folders = AlbumPath.Split(Path.DirectorySeparatorChar);
			
			foreach(System.String folder in folders){
				workPath += Path.DirectorySeparatorChar + folder;
				if(!Directory.Exists(workPath))
				{
					Directory.CreateDirectory(workPath);
				}
			}
		}
		
		public void intializeServer(){
			connections = new ArrayList();
			server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPAddress addr = IPAddress.Parse("127.0.0.1");
			IPEndPoint ep = new IPEndPoint(addr, this.ListeningPortNumber);
			server.Bind(ep);
			server.Listen(10);
		}
		
		public void listen(){
			allDone.Reset();
			server.BeginAccept(new AsyncCallback(this.AcceptCallback),server);
			//allDone.WaitOne();
		}
		
		private void AcceptCallback(IAsyncResult ar){
			allDone.Set();
			Socket listener = (Socket) ar.AsyncState;
			Socket handler = server.EndAccept(ar);
			try{
				if(handler.Receive(new byte[512],512,SocketFlags.None) > 0){
					char[] str = ("HTTP/1.0 200 OK\r\n" +
					              "Connection: close\r\n" +
					              "\r\n").ToCharArray();
					byte[] bytes = new byte[str.Length];
					for(int i = 0;i<str.Length;i++)
						bytes[i] = (byte)(0xff & str[i]);
					handler.Send(bytes,bytes.Length,SocketFlags.None);
				}
				connections.Add(handler);
			}catch(Exception){}
		}
		
		private void sendToClients(int size){
			for(int i = 0; i < connections.Count; i++){
				try{
					Socket sock = (Socket)connections[i];
					if(sock.Connected){
						sock.Send(this.Buffer,size,SocketFlags.None);
					}else
						connections.RemoveAt(i);
				}catch(Exception){
					connections.RemoveAt(i);
				}
			}
		}
		
		///<summary>removes characters from a string to better compare tag values</summary>
		///<param name="val">Tag value to handle.</param>
		private String unifyTagString(String val) {
			if (String.IsNullOrEmpty(val)) {
				// nothing to do - and Replace throws an exception if used with zero length strings
				return "";
			}
			
			return val.ToLower().
				Replace(" ", "").
				Replace("!", "").
				Replace(",", "").
				Replace("_", "").Replace("-", "").
				Replace("'", "").Replace("´", "").
				Replace("/", "").Replace("\\", "");
		}
		
		private bool compareTagStrings(String tagvalue, String match, bool canBeUnknown) {
			if (canBeUnknown) {
				bool nullTagvalue = String.IsNullOrEmpty(tagvalue);
				bool nullMatch = String.IsNullOrEmpty(match);
				if (nullTagvalue == false) {
					if (tagvalue.Equals("unknown")) {
						nullTagvalue = true;
					}
				}
				if (nullMatch == false) {
					if (match.Equals("unknown")) {
						nullMatch = true;
					}
				}
				if (nullTagvalue && nullMatch) {
					// both tags are empty strings, null  or "unknown"
					return true;
				} else if (nullTagvalue == false && nullMatch == true) {
					// a song with "unknown" album will not be ripped if same song with other album name exists
					return true;
					
				} else if (nullTagvalue || nullMatch) {
					// a song with "unknown" album exists - so get thw same song with album information again
					return false;
				}
			}
			if (String.IsNullOrEmpty(tagvalue)) {
				return false;
			}
			if (String.IsNullOrEmpty(match)) {
				return false;
			}
			return unifyTagString(tagvalue).IndexOf(unifyTagString(match)) != -1;
		}
		
		private bool IsCompatibleSong(FileInfo file,string artist,string album,string title){
			try{
				TagLib.File f = TagLib.File.Create(file.FullName);
				if(compareTagStrings(f.Tag.FirstAlbumArtist, artist, false) || compareTagStrings(f.Tag.FirstPerformer, artist, false)) {
					if(compareTagStrings(f.Tag.Album, album, true)) {
						if(compareTagStrings(f.Tag.Title, title, false)) {
							if (_HealthEnabled) {
								// Check if calculated health of file is below 100 - then return false and rip file again
								double fileHealth = CalculateHealth(file.Length, f.Properties.Duration.TotalMilliseconds);
								return fileHealth > 100;
							}
							return true;
						}
					}
				}
			}catch(Exception e){
				writeLogLine("skipFnA: song not accessible: " + e.ToString());
				return true;
			}
			return false;
		}
		
		private bool IsSongInFiles(FileInfo[] files,string artist,string album,string title){
			foreach(FileInfo file in files)
				if(IsCompatibleSong(file,artist,album,title))
				return true;
			return false;
		}
		
		private static FileInfo[] getFiles(DirectoryInfo dir,string pattern){
			pattern = System.Text.RegularExpressions.Regex.Replace(pattern,"[^a-zA-Z0-9]{1}","*");
			pattern = "*" + pattern + "*.mp3";
			while(pattern.Contains("**"))
				pattern = pattern.Replace("**","*");
			return dir.GetFiles(pattern);
		}
		
		private static DirectoryInfo[] getDirectories(DirectoryInfo dir,string pattern){
			pattern = System.Text.RegularExpressions.Regex.Replace(pattern,"[^a-zA-Z0-9]{1}","*");
			pattern = "*" + pattern + "*";
			while(pattern.Contains("**"))
				pattern = pattern.Replace("**","*");
			return dir.GetDirectories(pattern);
		}
	}
}
