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
using System.Threading;
using System.Xml;
using System.Collections;

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
		protected MemoryStream Song = new System.IO.MemoryStream();
		protected System.Boolean SkipSave = false;
		protected System.Boolean stopRecording = false;
		protected System.Int32 counter = 0;
		protected System.Byte []Buffer = new System.Byte[LastManager.BufferSize];
		protected Hashtable excludeFile = null;
		
		/// <summary>
		/// System.Object used to lock the stream while reading.
		/// </summary>
		// protected static System.Object ReadStreamLock = new System.Object();
		
		/// <summary>
		/// Bytes of the stream that have been announced to OnProgress Event
		/// </summary>
		protected System.Int64 LastPosition = 1;
		
		/// <summary>
		/// Occurs when an handled error happens
		/// </summary>
		/// <remarks>The arguments can be casted to LibLastRip.ErrorEventArgs</remarks>
		public event System.EventHandler OnError;
		
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
				if ("playlist".Equals(xnod.Name)) {
				}
				if ("creator".Equals(xnod.Name)) {
					xspf.Creator = xnod.InnerText;
				}
				if ("title".Equals(xnod.Name)) {
					xspf.Title = xnod.InnerText;
				}
				if ("track".Equals(xnod.Name)) {
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
					xnodWorking = xnod.FirstChild;
					while (xnodWorking != null)
					{
						AddChildren(xnodWorking, level+1);
						xnodWorking = xnodWorking.NextSibling;
					}
				}
			}
		}
		
		protected void writeLogLine(String logText) {
			Console.WriteLine(logText);
			if (this.OnLog != null) {
				this.OnLog(this, new LogEventArgs(logText));
			}
		}
		
		protected bool processFile() {
			System.String creator = LastManager.RemoveInvalidPathChars(this.currentXspfTrack.Creator);
			System.String album = LastManager.RemoveInvalidPathChars(this.currentXspfTrack.Album);
			System.String title = LastManager.RemoveInvalidFileNameChars(this.currentXspfTrack.Title);
			
			System.String CreatorPath = this.MusicPath + Path.DirectorySeparatorChar + creator;
			System.String AlbumPath = CreatorPath + Path.DirectorySeparatorChar + album + Path.DirectorySeparatorChar;
			System.String FilePath = AlbumPath + title + ".mp3";
			
			System.String QuarantineCreatorPath = this.QuarantinePath + Path.DirectorySeparatorChar + creator;
			System.String QuarantineAlbumPath = QuarantineCreatorPath + Path.DirectorySeparatorChar + album + Path.DirectorySeparatorChar;
			System.String QuarantineFilePath = QuarantineAlbumPath + title + ".mp3";
			
			// ProcessModes (multiple choices allowed)
			// a) reload existing files DEFAULT=false
			// b) only load existing artists DEFAULT=false
			
			if (ExcludeExistingMusic) {
				// File exists - dont process
				if (File.Exists(FilePath)) {
					writeLogLine("skipFE(" + counter.ToString() + ") " + FilePath);
					counter++;
					return false;
				}
				
				// File exists - dont process
				if (!String.IsNullOrEmpty(QuarantineFilePath) && File.Exists(QuarantineFilePath)) {
					writeLogLine("skipFE(" + counter.ToString() + ") " + QuarantineFilePath);
					counter++;
					return false;
				}
			}

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
					writeLogLine("skipEF(" + counter.ToString() + ") " + CreatorPath);
					return false;
				}
			}

			if (ExcludeNewMusic) {
				// Directory exists not - dont process
				if (!Directory.Exists(CreatorPath) && !Directory.Exists(QuarantineCreatorPath)) {
					counter++;
					writeLogLine("skipDnE(" + counter.ToString() + ") " + CreatorPath);
					return false;
				}
			}
			
			writeLogLine("get '" + title + "' (" + album + ") " + " from '" + creator + "'");

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
		
		protected void StartRecording(bool newStation) {
			if (newStation) {
				xspf = XSPF.GetEmptyXSPF();
				currentXspfTrack = XSPFTrack.GetEmptyXSPFTrack();
			}
			
			bool started = false;
			
			// number of times to try access to playlist - the playlist request can fail or contain an empty list.
			int tryCounter = 5;
			
			while (started == false && this.Status == ConnectionStatus.Recording) {
				
				if (xspf.CountTracks() == 0) {
					// Getting Playlist
					String url = "http://" + this.BaseURL + this.BasePath + "/xspf.php?sk=" + this.SessionID + "&discovery=0&desktop=1.3.1.1";
					WebRequest wReq = WebRequest.Create(url);
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
				if (xspf.CountTracks() > 0) {
					this.currentXspfTrack = (XSPFTrack)xspf.getTrack();
					this.StreamURL = this.currentXspfTrack.Location;

					//Check if file exists
					if(processFile())
					{
						started = true;
						//Getting stream
						WebRequest wReq = WebRequest.Create(this.StreamURL);
						HttpWebResponse hRes = (HttpWebResponse)wReq.GetResponse();
						System.IO.Stream RadioStream = hRes.GetResponseStream();

						//Start reading process
						// TODO: Lock could be active if response is fast - re-think about stream locking!
						RadioStream.BeginRead(this.Buffer, 0, LastManager.BufferSize,new System.AsyncCallback(this.Save), RadioStream);
					}
				} else {
					tryCounter = tryCounter - 1;

					if (tryCounter <= 0) {
						handleError(true, new ErrorEventArgs("No playlist found. Please restart ripping."));
						
						// no way to continue...
						StopRecording();
					}
				}
				
				if (this.stopRecording == true) {
					StopRecording();
				}
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
		
		protected void SaveToFile() {
			//Create a new MemoryStream
			MemoryStream NewSong = new MemoryStream();
			
			writeLogLine("Song length in bytes: " + this.Song.Length);
			writeLogLine("Song length announced: " + this.currentXspfTrack.Duration.ToString());
			
			//Should we save this song?
			if(this.SkipSave || this.CurrentSong == MetaInfo.GetEmptyMetaInfo())
			{
				//If not, then don't save it
				this.SkipSave = false;
				this.Song.Close();
			}else{
				//If so, then save it but do it on another thread
				SaveSongCall SSC = new SaveSongCall(this.SaveSong);
				//Minus one since we don't want the song to end with char 83 = 'S' from SYNC
				SSC.BeginInvoke(this.Song, (int)this.Song.Length, this.CurrentSong, new System.AsyncCallback(this.SaveSongCallback), this.Song);
			}
			
			//Replace this.Song with NewSong, and hope that the asynchronious request keeps the old object.
			this.Song = NewSong;
		}
		
		/// <summary>
		/// This Method saves data from buffer to song
		/// </summary>
		protected void Save(System.Int32 read) {
			bool firstRead = this.Song.Length == 0;
			
			//Write data from buffer to memory
			this.Song.Write(this.Buffer,0,read);
			
			System.Byte []Buf = this.Song.GetBuffer();
			
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
					//Save metadata and raise OnNewSong event
					this.currentSong = nSong;
					if(this.OnNewSong != null)
						this.OnNewSong(this, this.currentSong);
				}
				
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
				//Save metadata and raise OnNewSong event
				this.currentSong = nSong;
				if(this.OnNewSong != null) {
					this.OnNewSong(this, this.currentSong);
				}
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
				Song = new System.IO.MemoryStream();
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
			SSC.EndInvoke(Ar);
			
			//Close the old song
			((MemoryStream)Ar.AsyncState).Close();
		}
		
		protected delegate void SaveSongCall(MemoryStream Song, System.Int32 Count, MetaInfo SongInfo);
		
		///<summary>Save a song to disk</summary>
		///<param name="Song">A MemoryStream containing the song.</param>
		///<param name="Count">Number of bytes from MemoryStream to save.</param>
		///<param name="SongInfo">MetaInfo about the song to be saved.</param>
		protected void SaveSong(MemoryStream Song, System.Int32 Count, MetaInfo SongInfo)
		{
			System.Byte[] Buffer = Song.GetBuffer();
			
			//Filesystem paths
			System.String AlbumPath;
			if (String.IsNullOrEmpty(QuarantinePath) == false && Directory.Exists(QuarantinePath)) {
				AlbumPath = this.QuarantinePath + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Artist) + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Album) + Path.DirectorySeparatorChar;
			} else {
				AlbumPath = this.MusicPath + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Artist) + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Album) + Path.DirectorySeparatorChar;
			}
			System.String NewFilePath = AlbumPath + LastManager.RemoveInvalidFileNameChars(SongInfo.Track) + ".mp3";
			
			//Check if file exists
			if(File.Exists(NewFilePath))
			{
				//TODO: decide whether or not to delete the file!
				File.Delete(NewFilePath);
			}
			
			try {
				//Check if Album directory exists, if not create it
				if(!Directory.Exists(AlbumPath))
				{
					Directory.CreateDirectory(AlbumPath);
				}
				
				//Save the MemoryStream to file
				FileStream FS = File.Create(NewFilePath);
				FS.Write(Song.GetBuffer(), 0, Count);
				
				//Write metadata to stream as ID3v1
				SongInfo.AppendID3(FS);

				//Close the file
				FS.Flush();
				FS.Close();
				
				//Download covers - don't care for errors because some not exist
				WebClient Client = new WebClient();
				
				// First download larger covers - because small cover fails more often
				// TODO: FIRST call to DownloadFile will time out... why? Sleep helps...
				Thread.Sleep(5000);
				try {
					if((!File.Exists(AlbumPath + "cover.jpg")) && SongInfo.Albumcover != null) {
						writeLogLine("download cover " + AlbumPath + "cover.jpg");
						Client.DownloadFile(SongInfo.Albumcover, AlbumPath + "cover.jpg");
					}
				} catch (System.Net.WebException) {
					// no cover
				}
			} catch (Exception e) {
				writeLogLine("Exception occured: " + e.ToString());
				// TODO: Sometimes the album path is wrong - could be null or contains illegal characters - no exception throwing because this stops ripping next songs
				// TODO: Consider launching an OnError event
			}
		}
	}
}
