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
		protected int Position = 1;
		
		protected void StartRecording()
		{
			//Getting stream
			WebRequest wReq = WebRequest.Create(this.StreamURL);
			HttpWebResponse hRes = (HttpWebResponse)wReq.GetResponse();
			System.IO.Stream RadioStream = hRes.GetResponseStream();
			
			//Start reading process
			RadioStream.BeginRead(this.Buffer, 0, LastManager.BufferSize,new System.AsyncCallback(this.Save), RadioStream);
		}
		
		protected void Save(System.IAsyncResult Res)
		{
			Stream RadioStream = (Stream)Res.AsyncState;
			System.Int32 Count = RadioStream.EndRead(Res);
			
			//Write data from buffer to memory
			this.Song.Write(this.Buffer,0,Count);
			
			System.Byte []Buf = this.Song.GetBuffer();
			
			System.Int32 End = System.Convert.ToInt32(this.Song.Length)-4;

			Console.Write("GOT " + this.Song.Length);
			Console.Write(" SEARCH " + Position);
			Console.WriteLine(":" + End);
			
			if (this._CurrentSong == null || Position == 1) {
				this.UpdateMetaInfo();
			}
			
			if(End > 0)
			{
				for(;Position < End; Position++)
				{
					if(Buf[Position] == 83 &&		//Hex values: 53
					   Buf[Position+1] == 89 &&	//59
					   Buf[Position+2] == 78 &&   // 4e
					   Buf[Position+3] == 67      // 43
					  )
					{
						//Create a new MemoryStream
						MemoryStream NewSong = new MemoryStream();
						
						//Write the latest data to it
						NewSong.Write(Buf, Position, System.Convert.ToInt32(this.Song.Length) - Position);
						
						//Should we save this song?
						if(this.SkipSave || this.CurrentSong == null || !this.CurrentSong.Streaming)
						{
							//If not, then don't save it
							this.SkipSave = false;
							this.Song.Close();
						}else{
							//If so, then save it but do it on another thread
							SaveSongCall SSC = new SaveSongCall(this.SaveSong);
							SSC.BeginInvoke(this.Song, Position, this.CurrentSong, new System.AsyncCallback(this.SaveSongCallback), this.Song);
						}
						
						//Replace this.Song with NewSong, and hope that the asynchronious request keeps the old object.
						this.Song = NewSong;
						this.Position = 1;
						this._CurrentSong = null;
						
						//Break, cause finding more songs in the current data would create serious filesystem errors due to threading and lack of metadata update!
						break;
					}
				}
			}
			
			//Read from the radio stream again
			RadioStream.BeginRead(this.Buffer , 0, LastManager.BufferSize, new System.AsyncCallback(this.Save),RadioStream);
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
			//Filesystem paths
			System.String AlbumPath = this.MusicPath + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Artist) + Path.DirectorySeparatorChar + LastManager.RemoveInvalidPathChars(SongInfo.Album) + Path.DirectorySeparatorChar;
			System.String NewFilePath = AlbumPath + LastManager.RemoveInvalidFileNameChars(SongInfo.Track) + ".mp3";
			
			//Check if file exists
			if(File.Exists(NewFilePath))
			{
				//TODO: decide whether or not to delete the file!
				File.Delete(NewFilePath);
			}
			
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
			
			try {
				if((!File.Exists(AlbumPath + "SmallCover.jpg")) && SongInfo.AlbumcoverSmall != null)
					Client.DownloadFile(SongInfo.AlbumcoverSmall,AlbumPath + "SmallCover.jpg");
			} catch (System.Net.WebException) {
				// no small cover
			}
			
			try {
				if((!File.Exists(AlbumPath + "MediumCover.jpg")) && SongInfo.AlbumcoverMedium != null)
					Client.DownloadFile(SongInfo.AlbumcoverMedium,AlbumPath + "MediumCover.jpg");
			} catch (System.Net.WebException) {
				// no medium cover
			}
			
			try {
				if((!File.Exists(AlbumPath + "LargeCover.jpg")) && SongInfo.AlbumcoverLarge != null)
					Client.DownloadFile(SongInfo.AlbumcoverLarge,AlbumPath + "LargeCover.jpg");
			} catch (System.Net.WebException) {
				// no large cover
			}
		}
	}
}
