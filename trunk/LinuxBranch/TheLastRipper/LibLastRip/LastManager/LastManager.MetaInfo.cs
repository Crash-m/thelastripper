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
		static System.Object UpdateLocker = new System.Object();
		public event System.EventHandler OnNewSong;
		
		///<summary>
		///Updates the metainfo about the current song
		///</summary>
		public void UpdateMetaInfo()
		{
			if(this.Status == ConnectionStatus.Recording && System.Threading.Monitor.TryEnter(LastManager.UpdateLocker))
			{
				try
				{
					HttpWebRequest hReq = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "np.php?session=" + this.SessionID + "&debug=0");
					
					HttpWebResponse hRes = (HttpWebResponse)hReq.GetResponse();
					Stream ResponseStream = hRes.GetResponseStream();
					
					System.Byte []Buffer = new System.Byte[LastManager.ProtocolBufferSize];
					
					System.Int32 Count = ResponseStream.Read(Buffer,0,Buffer.Length);
					
					MetaInfo ConcurrentSong = new MetaInfo(Encoding.UTF8.GetString(Buffer, 0, Count));
					
					if(this._CurrentSong == null || !this._CurrentSong.Streaming)
					{
						this._CurrentSong = ConcurrentSong;
						if(this._CurrentSong.Streaming)
						{
							this.SetTimer();
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
							if(this.OnNewSong != null)
							{
								this.OnNewSong(this,ConcurrentSong);
							}
							this.SetTimer();
						}
					}
				}
				finally
				{
					System.Threading.Monitor.Exit(LastManager.UpdateLocker);
				}
			}
		}
		public void UpdateMetaInfo(System.Object Sender, System.EventArgs Args)
		{
			this.UpdateMetaInfo();
		}
		
		protected void SetTimer()
		{
			this.SetTimer(this.CurrentSong);
		}
		
		protected void SetTimer(MetaInfo SongInfo)
		{
			this.SetTimer(System.Convert.ToDouble(SongInfo.Trackduration) * 1000 );
		}
		
		protected void SetTimer(System.Double Interval)
		{
			if((Interval - 500) > 0)
			{
				//Set timer 1
				this.Timer1.Stop();
				this.Timer1 = new System.Timers.Timer(Interval - 500);
				this.Timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
				this.Timer1.Start();
				
				//Set timer 2
				this.Timer2.Stop();
				this.Timer2 = new System.Timers.Timer(Interval + 100);
				this.Timer2.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
				this.Timer2.Start();
				
				//Set timer 3
				this.Timer3.Stop();
				this.Timer3 = new System.Timers.Timer(Interval + 700);
				this.Timer3.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
				this.Timer3.Start();
			}
			/*
			We're using 3 timers to make it easier to hit the right moment.
			*/
		}
	}
}
