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
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace LibLastRip
{
	
	///<summary>
	///
	///</summary>
	public partial class LastManager
	{
		protected System.String UserID;
		protected System.String _Password;
		protected System.String StreamURL;
		protected System.String SessionID;
		protected System.String BaseURL;
		protected System.String BasePath;
		protected System.Boolean Subscripter;
		protected System.String _MusicPath;
		protected const System.String PathSeparator = "/";
		protected const System.Int32 ProtocolBufferSize = 4096;
		protected System.Boolean SkipSave = false;
		protected ConnectionStatus Status = ConnectionStatus.Created;
		
		///<summary>
		///Initializes an instance of LastManager, and initiates handshake
		///</summary>
		public LastManager(System.String UserID, System.String Password, System.String MusicPath)
		{	
			this.MusicPath = MusicPath;
			
			this.Handshake(UserID, Password);
		}
		
		///<summary>
		///Initializes an instance of LastManager
		///</summary>
		public LastManager(System.String MusicPath)
		{	
			this.MusicPath = MusicPath;
		}

		///<summary>
		///Occurs when a handshake request has returned
		///</summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler HandshakeReturn;
		
		public void Handshake()
		{
			if(this.UserID == null || this._Password == null)
			{
				throw new System.Exception("UserName and password needed.");
			}
			
			HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/radio/handshake.php?version=" + "1.1.1" + "&platform=" + "linux" + "&username=" + this.UserID + "&passwordmd5=" + this.Password + "&debug=" + "0" + "&partner=");
			Request.BeginGetResponse(new System.AsyncCallback(this.OnHandshakeReturn), Request);
		}
		
		protected void OnHandshakeReturn(System.IAsyncResult Ar)
		{
			//Get Response
			HttpWebRequest Request = (HttpWebRequest)Ar.AsyncState;
			HttpWebResponse Response = (HttpWebResponse)Request.EndGetResponse(Ar);
			
			//Get stream and create StreamReader
			Stream Stream = Response.GetResponseStream();
			StreamReader StreamReader = new StreamReader(Stream, Encoding.UTF8);
			
			//Read data sync, since stream.beginRead is worth the trouble when the connections have been established
			System.String Data = StreamReader.ReadToEnd();
			
			//Closeing everything related to a connection, and releasing system resources
			StreamReader.Close();
			Stream.Close();
			Response.Close();
			
			System.Boolean Result = this.ParseHandshake(Data);
			if(Result)
			{
				this.Status = ConnectionStatus.Connected;
			}else{
				this.Status = ConnectionStatus.Created;
			}
			
			if(this.HandshakeReturn != null)
				this.HandshakeReturn(this, new HandshakeEventArgs(Result));
		}
		
		///<summary>
		///Gives the Last.FM server a handshake
		///</summary>
		public void Handshake(System.String UserID, System.String Password)
		{
			this.UserID = UserID;
			this.Password = Password;
			this.Handshake();
		}
		
		///<summary>
		///Parses the reponse data from a handshake
		///</summary>
		protected System.Boolean ParseHandshake(System.String Data)
		{
			/* This method is a rewrite from Last-Exit */
			System.String []Lines = Data.Split(new System.Char[] {'\n'});
			System.Boolean Result = false;
			
			foreach(System.String Line in Lines)
			{
				System.String []Opts = Line.Split (new System.Char[] {'='},2);
				
				switch(Opts[0].ToLower())
				{
					case "session":
						if(Opts[1].ToLower() == "failed")
						{
							Result = false;
						}else{
							Result = true;
							this.SessionID = Opts[1];
						}
						break;
					case "stream_url":
						this.StreamURL = Opts[1];
						break;
					case "subscriber":
						if(Opts[1] == "1")
						{
							this.Subscripter = true;
						}else{
							this.Subscripter = false;
						}
						break;
					case "framehack":
						//Don't know what this is for
						break;
					case "base_url":
						this.BaseURL = Opts[1];
						break;
					case "base_path":
						this.BasePath = Opts[1];
						break;
					default:
						Console.WriteLine("LastManager.ParseHandshake(): Unknown key: " + Opts[0]);
						break;
				}
			}
			return Result;
		}
		
		///<summary>
		///Gets an md5 hash of the password, or sets the password from hash.
		///</summary>
		public System.String Password
		{
			set
			{
				this._Password = value;
			}
			get
			{
				return this._Password;
			}
		}
		
		public static System.String CalculateHash(System.String Pass)
		{
				/*
				Inspired by Last-Exit-4 another GNU GPL licensed Last.FM client
				*/
				MD5 Hasher = MD5.Create ();
				byte[] Hash = Hasher.ComputeHash (Encoding.Default.GetBytes (Pass));
				StringBuilder StrHash = new StringBuilder ();
				
				for (int i = 0; i < Hash.Length; ++i) {
					StrHash.Append (Hash[i].ToString ("x2"));
				}
				
				return StrHash.ToString ();
		}
		
		///<summary>
		///Gets or set the UserName
		///</summary>
		public System.String UserName
		{
			get
			{
				return this.UserID;
			}
			set
			{
				this.UserID = value;
			}
		}
		
		///<summary>
		///Gets or set the UserName
		///</summary>
		public System.String MusicPath
		{
			get
			{
				return this._MusicPath;
			}
			set
			{
				this._MusicPath = value;
			}
		}
		
		///<summary>
		///Gets current connection status
		///</summary>
		public ConnectionStatus ConnectionStatus
		{
			get
			{
				return this.Status;
			}
		}
		
		///<summary>
		///Gets the URL for the Last.FM server
		///</summary>
		protected System.String ServiceURL
		{
			get
			{
				return "http://" + this.BaseURL + this.BasePath + "/";
			}
		}

		///<summary>
		///Remove invalid PathChars from a directory name
		///</summary>
		///<param name="PathName">Directory name from which invalid chars should be removed</param>
		internal static System.String RemoveInvalidPathChars(System.String PathName)
		{
			return LastManager.RemoveChars(PathName, System.IO.Path.GetInvalidPathChars());
		}
		
		///<summary>
		///Remove invalid filename chars from a filename.
		///</summary>
		///<param name="FileName">FileName from which invalid chars should be removed</param>
		internal static System.String RemoveInvalidFileNameChars(System.String FileName)
		{
			return LastManager.RemoveChars(FileName, System.IO.Path.GetInvalidFileNameChars());
		}
		
		///<summary>
		///Replaces chars from an array of invalid chars from an input string with an underline character
		///</summary>
		///<param name="Input">String from which InvalidChars must be removed.</param>
		///<param name="InvalidChars">InvalidChars to be removed from Input string.</param>
		protected internal static System.String RemoveChars(System.String Input, System.Char[] InvalidChars)
		{
			System.String Output = "";
			if (Input == null) {
				return "_ERROR";
			}
			foreach(System.Char TestChar in Input.ToCharArray())
			{	
				System.Boolean IsGood = true;
				foreach(System.Char iChar in InvalidChars)
				{
					if(iChar == TestChar)
						IsGood = false;
				}
				// filter characters for directory separation
				if (TestChar == '/' || TestChar == '\\') {
				    IsGood = false;
				}
				if(IsGood) {
					Output += TestChar.ToString();
				} else {
					Output += '_';
				}
			}
			return Output;
		}
	}

	public enum ConnectionStatus
	{
		Created,	//LastManager is created
		Connected,	//LastManager has connection and owns a SessionID
		Recording	//LastManager is connected to stream and is recording
	}
	
	///<summary>
	///EventArgs for a HandshakeReturn event
	///</summary>
	public class HandshakeEventArgs : System.EventArgs
	{
		protected System.Boolean _Success;
		
		internal HandshakeEventArgs(System.Boolean Success)
		{
			this._Success = Success;
		}
		
		///<summary>Boolean indicating if handshake was successfull</summary>
		public System.Boolean Success
		{
			get
			{
				return this._Success;
			}
		}
	}
}
