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
using System.Net;
using System.IO;
using System.Text;

namespace LibLastRip
{
	/*
	This part of the LastManager class contains all MetaInfo related logic.
	*/
	public partial class LastManager
	{
		protected MetaInfo _CurrentSong = MetaInfo.GetEmptyMetaInfo();
	
		///<summary>
		///Gets the meta info about the current song
		///</summary>
		public MetaInfo CurrentSong
		{
			get
			{
				return this._CurrentSong;
			}
		}
		/// <summary>
		/// Occurs when a new song is detected.
		/// </summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler OnNewSong;
		
		/// <summary>
		/// Occurs when ripping make progress.
		/// </summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler OnProgress;

		///<summary>
		///Boolean indicating whether or not we are currently updating metadata, since we don't want to drive the system out of resources with multiple requests
		///</summary>
		protected System.Boolean IsUpdateing = false;
		
		///<summary>
		///Updates metainfo, and pushes an OnNewSong event if new song is detected.
		///</summary>
		public void UpdateMetaInfo()
		{
			//Do not update if we're already doing that, and don't request if we're not recording
			if(!this.IsUpdateing && this.Status == ConnectionStatus.Recording)
			{
				//We've commenced downloading of metadata
				this.IsUpdateing = true;

				//Create a WebRequest
				HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "np.php?session=" + this.SessionID + "&debug=0");
				
				//Begin to optain a response
				Request.BeginGetResponse(new AsyncCallback(this.MetaInfoDownloaded), Request);
			}
		}
		
		///<summary>
		///Handles metadata callback from UpdateMetaInfo
		///</summary>					
		protected void MetaInfoDownloaded(System.IAsyncResult Ar)
		{
			try
			{
				//Get the HttpWebRequest
				HttpWebRequest Request = (HttpWebRequest)Ar.AsyncState;
				
				//Get the response
				HttpWebResponse Response = (HttpWebResponse)Request.EndGetResponse(Ar);
				
				//Get the stream
				Stream Stream = Response.GetResponseStream();
				
				//Create a StreamReader to warp the stream
				StreamReader StreamReader = new StreamReader(Stream, Encoding.UTF8);
				
				//Read all the data from the stream, notice this could be multithreaded with use of Stream.beginRead
				//But sources on the net suggest that it's not relevant
				System.String Data = StreamReader.ReadToEnd();
				
				//Close the StreamReader, Stream and Response to release system resources
				StreamReader.Close();
				Stream.Close();
				Response.Close();
				
				//Parse the newly recived data
				MetaInfo nSong = new MetaInfo(Data, currentXspf);
				
				//Is this a new song?
				if(!MetaInfo.Equals(nSong,this._CurrentSong))
				{
					//Save metadata and raise OnNewSong event
					this._CurrentSong = nSong;
					if(this.OnNewSong != null)
						this.OnNewSong(this, this._CurrentSong);
				}
			}
			catch(System.Exception)
			{
				// Nothing to do - metadata request will just be repeated as long as it's missing and if no metadata is fetched until next SYNC song will be skipped
			}
			finally 
			{
				//We're no longer updating metadata
				this.IsUpdateing = false;			
			}
		}
	}
}
