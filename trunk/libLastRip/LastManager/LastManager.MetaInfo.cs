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
		protected MetaInfo _CurrentSong;
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
		public event System.EventHandler OnNewSong;
		
		//TODO: remove this method since it cause bad multithreading!
		public void UpdateMetaInfo(System.Object Sender, System.EventArgs Args)
		{
			this.UpdateMetaInfo();
		}
		
		///<summary>
		///Updates the metainfo about the current song
		///</summary>
		public void UpdateMetaInfo()
		{
			if(this.Status == ConnectionStatus.Recording)
			{
				HttpWebRequest hReq = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "np.php?session=" + this.SessionID + "&debug=0");
				
				HttpWebResponse hRes = (HttpWebResponse)hReq.GetResponse();
				Stream ResponseStream = hRes.GetResponseStream();
				
				System.Byte []Buffer = new System.Byte[LastManager.ProtocolBufferSize];
				
				System.Int32 Count = ResponseStream.Read(Buffer,0,Buffer.Length);
				
				MetaInfo ConcurrentSong = new MetaInfo(Encoding.UTF8.GetString(Buffer, 0, Count));
				
				//Is this a new song??
				if(this._CurrentSong == null || !MetaInfo.Equals(ConcurrentSong,this._CurrentSong))
				{
					this._CurrentSong = ConcurrentSong;
					if(this.OnNewSong != null)
						this.OnNewSong(this, this._CurrentSong);
				}
				
				/*
				//If this is the first song
				if(this._CurrentSong == null || !this._CurrentSong.Streaming)
				{
					this._CurrentSong = ConcurrentSong;
					if(this._CurrentSong.Streaming)
					{
						this.SetTimer();
						
						//Make the event happen on the UI thread
						this.NewSongEvent.Set();
						if(this.OnNewSong != null)
						{
							this.OnNewSong(this,ConcurrentSong);
						}
					}else{
						this.SetTimer(5000);
					}
				}else{
					if(!MetaInfo.Equals(ConcurrentSong,this._CurrentSong))
					{
						this.SaveSong(this._CurrentSong);
						this._CurrentSong = ConcurrentSong;
						
						//Make the event happen on the UI thread
						if(this.OnNewSong != null)
						{
							this.OnNewSong(this,ConcurrentSong);
						}
						this.SetTimer();
					}
				}*/
			}
		}
		
		
	}
}
