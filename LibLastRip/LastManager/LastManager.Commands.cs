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
	This part of the LastManager class handles and exposes Last.FM commands.
	 */
	public partial class LastManager
	{
		///<summary>
		///Constants for commands
		///</summary>
		public static String commandBan = "ban";
		public static String commandSkip = "skip";
		public static String commandLove = "love";
		
		///<summary>
		///Occurs when an command execution is return
		///</summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler CommandReturn;
		
		///<summary>
		///Sends a command to the Last.FM server
		///</summary>
		protected void SendCommand(System.String Command)
		{
			if(this.Status == ConnectionStatus.Recording)
			{
				HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "control.php?session=" + this.SessionID + "&command=" + Command + "&debug=0");
				Request.BeginGetResponse(new System.AsyncCallback(this.OnCommandReturn), new System.Object[]{Request, Command});
			}
		}
		
		protected void OnCommandReturn(System.IAsyncResult Ar)
		{
			try
			{
				System.Object[] Args = (System.Object[])Ar.AsyncState;
				HttpWebRequest Request = (HttpWebRequest)Args[0];
				HttpWebResponse Response = (HttpWebResponse)Request.EndGetResponse(Ar);
				
				Stream Stream = Response.GetResponseStream();
				StreamReader sr = new StreamReader(Stream, Encoding.UTF8);
				
				System.String []Data = sr.ReadToEnd().Split(new System.Char[] {'\n'});
				
				System.Boolean Result = false;
				foreach(System.String Line in Data)
				{
					System.String []Opts = Line.Split(new System.Char[] {'='});
					
					if("response".Equals(Opts[0].ToLower()) && Opts[1].ToLower().StartsWith("ok"))
					{
						Result = true;
					}
				}
				
				//Ensure that song is skipped from save if hate or skip was used
				if((System.String)Args[1] == commandBan || (System.String)Args[1] == commandSkip)
				{
					this.SkipSave = Result;
				}
				
				if(this.CommandReturn != null)
					this.CommandReturn(this, new CommandEventArgs(Result));
			}
			catch(System.Exception e)
			{
				throw new System.Exception("Error while performing command", e);
			}
		}
		
		///<summary>
		///Skips the current song an moves on
		///</summary>
		public void SkipSong()
		{
			if(this.Status == ConnectionStatus.Recording)
			{
				this.SkipSave = true;
			}
		}
		
		///<summary>
		///Loves the current song
		///</summary>
		public void LoveSong()
		{
			if(this.Status == ConnectionStatus.Recording)
			{
				this.SendCommand(commandLove);
			}
		}
		
		///<summary>
		///Bans the current song
		///</summary>
		public void BanSong()
		{
			if(this.Status == ConnectionStatus.Recording)
			{
				this.SendCommand(commandBan);
			}
		}
		
		///<summary>
		///Stops all actions
		///</summary>
		public void Stop()
		{
			this.stopRecording = true;
		}

		///<summary>Occurs when a ChangeStation is returned</summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler StationChanged;
		
		///<summary>Connects to a radio station and starts ripping</summary>
		///<remarks>This method may be used to change station during recording, and to initiate recording.</remarks>
		public void ChangeStation(System.String LastFMStation)
		{
			// Reset stop command
			this.stopRecording = false;

			//Can't do anything if not a least connected
			if(this.Status == ConnectionStatus.Created)
			{
				if(this.StationChanged != null) {
					this.StationChanged(this, new StationChangedEventArgs(false));
				}
			}
			else{
				HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "adjust.php?session="+this.SessionID+"&url="+LastFMStation+"&debug=0");
				Request.BeginGetResponse(new System.AsyncCallback(this.OnStationChanged), Request);
			}
		}
		
		protected void OnStationChanged(System.IAsyncResult Ar)
		{
			HttpWebRequest Request = (HttpWebRequest)Ar.AsyncState;
			HttpWebResponse Response = (HttpWebResponse)Request.EndGetResponse(Ar);
			Stream Stream = Response.GetResponseStream();
			StreamReader StreamReader = new StreamReader(Stream, Encoding.UTF8);
			
			//Read and split data
			System.String[] Data = StreamReader.ReadToEnd().Split(new System.Char[] {'\n'});;
			
			//Close connections
			StreamReader.Close();
			Stream.Close();
			Response.Close();
			
			//Create a result value
			System.Boolean Result = false;
			
			//Look at the response to check for success
			foreach(System.String Line in Data)
			{
				System.String []Opts = Line.Split(new System.Char[] {'='});
				
				if (Opts.Length > 1) {
					if(Opts[0].ToLower() == "response") {
						if (Opts[1].ToLower().StartsWith("ok"))
						{
							Result = true;

							// Don't continue with old playlist
							this.xspf = XSPF.GetEmptyXSPF();

							//If we're already recording then don't save current song
							if(this.Status == ConnectionStatus.Recording)
							{
								// Don't save the current song
								this.SkipSave = true;
							}else{
								//If not already recording, then start it and change status
								this.Status = ConnectionStatus.Recording;
								this.StartRecording();
							}
						} else {
							if (this.OnError != null) {
								this.OnError(this, new ErrorEventArgs("No station found. Please change and restart ripping.", null));
							}
						}
					}}
			}
			
			//Fire an event
			if(this.StationChanged != null) {
				this.StationChanged(this, new StationChangedEventArgs(Result));
			}
		}
	}
	
	///<summary>
	///EventArgs for CommandReturn event
	///</summary>
	public class CommandEventArgs : System.EventArgs
	{
		protected System.Boolean _Success;
		internal CommandEventArgs(System.Boolean Success)
		{
			this._Success = Success;
		}
		
		///<summary>
		///Boolean indicating whether or not the command execution was successfull
		///</summary>
		public virtual System.Boolean Success
		{
			get
			{
				return this._Success;
			}
		}
	}

	///<summary>
	///EventArgs for StationChanged event
	///</summary>
	public class StationChangedEventArgs : System.EventArgs
	{
		protected System.Boolean _Success;
		
		internal StationChangedEventArgs(System.Boolean Success)
		{
			this._Success = Success;
		}
		
		///<summary>
		///Boolean indicating whether or not the station was changed successfully
		///</summary>
		public virtual System.Boolean Success
		{
			get
			{
				return this._Success;
			}
		}
	}
	
	///<summary>
	///EventArgs for Log event
	///</summary>
	public class LogEventArgs : System.EventArgs
	{
		protected String _log;
		
		internal LogEventArgs(String log)
		{
			this._log = log;
		}
		
		///<summary>
		///String containing log message
		///</summary>
		public virtual String Log
		{
			get
			{
				return this._log;
			}
		}
	}

}
