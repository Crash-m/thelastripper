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

namespace LibLastRip
{
	/*
	This part of the class handles all stream related matters.
	 */
	public partial class LastManager
	{
		protected static System.Int32 BufferSize = 8192; //8 KiB
		protected MemoryStream Song = new System.IO.MemoryStream();
		protected System.Byte []Buffer = new System.Byte[LastManager.BufferSize];
		
		/// <summary>
		/// System.Object used to lock the stream while reading.
		/// </summary>
		protected static System.Object ReadStreamLock = new System.Object();
		
		/// <summary>
		/// Bytes of the stream that have been searched for SYNC-strings
		/// </summary>
		protected System.Int32 Position = 1;
		protected System.Int32 LastPosition = 1;
		
		/// <summary>
		/// Occurs when an handled error happens
		/// </summary>
		/// <remarks>The arguments can be casted to LibLastRip.ErrorEventArgs</remarks>
		public event System.EventHandler OnError;
		
		protected void StartRecording()
		{
			//Aquire lock for the stream
			if(System.Threading.Monitor.TryEnter(LastManager.ReadStreamLock))
			{
				//Getting stream
				WebRequest wReq = WebRequest.Create(this.StreamURL);
				HttpWebResponse hRes = (HttpWebResponse)wReq.GetResponse();
				System.IO.Stream RadioStream = hRes.GetResponseStream();
				
				//Release lock when starting asynchronious call, since it will be used there
				System.Threading.Monitor.Exit(LastManager.ReadStreamLock);
				
				//Start reading process
				RadioStream.BeginRead(this.Buffer, 0, LastManager.BufferSize,new System.AsyncCallback(this.Save), RadioStream);
			}
		}
		
		/// <summary>
		/// This Method saves data from buffer to song 
		/// </summary>
		protected void Save(System.Int32 read) {
			//Write data from buffer to memory
			this.Song.Write(this.Buffer,0,read);
			
			System.Byte []Buf = this.Song.GetBuffer();
			
			System.Int32 End = System.Convert.ToInt32(this.Song.Length)-4;

			//Request metadata if needed
			if (this.Position == 1) {
				this.UpdateMetaInfo();
			} else {
				if (this.OnProgress != null) {
					// Update Progress bar every 8 seconds.
					if (Position < LastPosition || LastPosition + 16384*8 < Position)
					{		//Note: 16383 [Byte/sec]
						LastPosition = Position;
						this.OnProgress(this, new ProgressEventArgs(Position / 16384));
					}
				}
			}

			if(End > 0)
			{
				for(;this.Position < End; this.Position++)
				{
					if(Buf[this.Position] == 83 &&		//Hex values: 53
					   Buf[this.Position+1] == 89 &&	//59
					   Buf[this.Position+2] == 78 &&   // 4e
					   Buf[this.Position+3] == 67      // 43
					  )
					{
						//Create a new MemoryStream
						MemoryStream NewSong = new MemoryStream();
						
						//Write the latest data to it
						NewSong.Write(Buf, this.Position, System.Convert.ToInt32(this.Song.Length) - this.Position);
						
						//Should we save this song?
						if(this.SkipSave || this.CurrentSong == MetaInfo.GetEmptyMetaInfo() || !this.CurrentSong.Streaming)
						{
							//If not, then don't save it
							this.SkipSave = false;
							this.Song.Close();
						}else{
							//If so, then save it but do it on another thread
							SaveSongCall SSC = new SaveSongCall(this.SaveSong);
							//Minus one since we don't want the song to end with char 83 = 'S' from SYNC
							SSC.BeginInvoke(this.Song, this.Position - 1, this.CurrentSong, new System.AsyncCallback(this.SaveSongCallback), this.Song);
						}
						
						//Replace this.Song with NewSong, and hope that the asynchronious request keeps the old object.
						this.Song = NewSong;
						
						//Set position to 1, see documentation of position
						this.Position = 1;
						
						//Break, cause finding more songs in the current data would create serious filesystem errors due to threading and lack of metadata update!
						break;
					}
				}
			}
		}
		
        /// <summary>
		/// This Method saves a stream until it ends
		/// </summary>
		protected void Save(System.IAsyncResult Res)
		{
			//Aquire a lock for the stream, to ensure that it's not already in use
			if(!System.Threading.Monitor.TryEnter(LastManager.ReadStreamLock)) {
				//If the stream is locked, throw an exception.
				throw new UnauthorizedAccessException("Illegal call to method Save - process is already active");
			}
			try {
				//Save data read from async read.
				Stream RadioStream = (Stream)Res.AsyncState;
				System.Int32 Count = RadioStream.EndRead(Res);
				Save(Count);
				
				System.Int32 read = 1;
				// we just read syncron from here - no nead for another AsyncCallback!
				while (read > 0) {
					read = RadioStream.Read(this.Buffer, 0, LastManager.BufferSize);
					Save(read);
				}
				// If this line is reached we have no more data in stream - this happens regulary if you listening "special tag-stations"
	
				// Raise event so client can display a message
				if (this.OnError != null)
					this.OnError(this, new ErrorEventArgs("Strem just finished, discarding last song. Please restart ripping.", null));
				
				
			} catch (Exception e) {
				// Catch all exceptions to prevent application from falling into a illegal state
				// Raise event so client can display a message
				if (this.OnError != null) 
					this.OnError(this, new ErrorEventArgs("Exception occured. Please restart ripping.", e));
				
			} finally {
				System.Threading.Monitor.Exit(LastManager.ReadStreamLock);
				this.RestoreState();
			}
		}
		
		/// <summary>
		/// Restore the default variables at the state ConnectionStatus.Connected, when a stream has ended.
		/// </summary>
		protected void RestoreState()
		{
			//Aquire lock to insure Stream isn't in use
			if(System.Threading.Monitor.TryEnter(LastManager.ReadStreamLock))
			{
				Song = new System.IO.MemoryStream();
			    Buffer = new System.Byte[LastManager.BufferSize];
			
				Position = 1;
				LastPosition = 1;		
				
				// Metainfo
				_CurrentSong = MetaInfo.GetEmptyMetaInfo();
				
				// LastManager
				Status = ConnectionStatus.Connected;
				
				//Release lock again
				System.Threading.Monitor.Exit(LastManager.ReadStreamLock);
				
				if(this.OnNewSong != null) {
					this.OnNewSong(this, this._CurrentSong);
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
			//Remove null-bytes at the end of each song.
			//Ensure complete last frame with the magic number of 0-bytes to remove really!
			System.Byte[] Buffer = Song.GetBuffer();
			System.Int32 removed = 0;
			while(Count > 0 && Buffer[Count] == 0 && removed < 26*16) {
				Count--;
				removed++;
			}
			
			//Filesystem paths
			System.String AlbumPath = this.MusicPath + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Artist) + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Album) + Path.DirectorySeparatorChar;
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
					if((!File.Exists(AlbumPath + "LargeCover.jpg")) && SongInfo.AlbumcoverLarge != null)
						Client.DownloadFile(SongInfo.AlbumcoverLarge,AlbumPath + "LargeCover.jpg");
				} catch (System.Net.WebException) {
					// no large cover
				}

				try {
					if((!File.Exists(AlbumPath + "MediumCover.jpg")) && SongInfo.AlbumcoverMedium != null)
						Client.DownloadFile(SongInfo.AlbumcoverMedium,AlbumPath + "MediumCover.jpg");
				} catch (System.Net.WebException) {
					// no medium cover
				}

				try {
					if((!File.Exists(AlbumPath + "SmallCover.jpg")) && SongInfo.AlbumcoverSmall != null)
						Client.DownloadFile(SongInfo.AlbumcoverSmall,AlbumPath + "SmallCover.jpg");
				} catch (System.Net.WebException) {
					// no small cover
				}
				
			} catch (Exception) {
				// TODO: Sometimes the album path is wrong - could be null or contains illegal characters - no exception throwing because this stops ripping next songs
				// TODO: Consider launching an OnError event
			}
		}
	}
}
